using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.ComponentModel;
using CfsNamespace;

namespace AtomImageEditor
{
    public struct OverlayImage
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public Guid imageID;

        public OverlayImage( int x,
                            int y,
                            int width,
                            int height,
                            Guid imageID ) 
        { 
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.imageID = imageID;        
        }
    }
    
    public class ImageManager
    {
        public static Guid GetImageGuid(int imageIndex, int talentID, string talentType, string imageFileSystemName)
        {
            // NOTE: imageIndex is base 1
            string[] separator = talentType.Split('*');
            talentType = separator[0];
                
            // open a db instance
            using (CfsEntity cfsEntity = new CfsEntity())
            {
                TalentImages[] images = null;
                try
                {
                    // fetch images from DB
                    bool isApplicant = talentType == CfsCommon.TALENT_TYPE_ID_APPLICANT;
                    if (cfsEntity.TalentImages.Count() > 0)
                        images = cfsEntity.TalentImages
                            .Where(item => item.FK_talentID == talentID)
                            .Where(item => item.imageIndex == imageIndex)
                            .Where(item => item.isApplicant == isApplicant)
                            .ToArray() as TalentImages[];

                    if (images != null && images.Length > 0)
                        return images[0].imageID;
                    else
                    {
                        // search for the image on the file system
                        Image fileSystemImage = ImageManager.GetImageFromFileSystem(talentType, imageFileSystemName);
                        if (fileSystemImage != null)
                        {
                            // image was found, add it to the DB and return the Guid
                            return ImageManager.AddImage(ImageManager.ConvertImageToBytes(fileSystemImage), talentID, isApplicant, imageIndex);
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return Guid.Empty;
        }

        private static byte[] ConvertImageToBytes( Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static bool DeleteImage(int imageIndex, int talentID)
        {
            // NOTE: imageIndex is base 1

            // open a db instance
            using (CfsEntity cfsEntity = new CfsEntity())
            {
                TalentImages[] images = null;
                try
                {
                    // fetch images from DB
                    if (cfsEntity.TalentImages.Count() > 0)
                        images = cfsEntity.TalentImages
                            .Where(item => item.FK_talentID == talentID)
                            .Where(item => item.imageIndex == imageIndex)
                            .ToArray() as TalentImages[];

                    if (images != null && images.Length > 0)
                    {
                        for (int i = 0; i < images.Length; i++)
                        {
                            cfsEntity.DeleteObject(images[i]);
                        }
                        cfsEntity.SaveChanges();
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex) {
                    return false;
                }
            }
            return true;
        }
        public static Image GetImageFromFileSystempublic(string talentType, string imageFileSystemName)
        {
            if (string.IsNullOrEmpty(imageFileSystemName))
                return null;

            // just want the image name, so strip off the relative path if it's present
            string[] filenameParts = imageFileSystemName.Split('/');
            imageFileSystemName = filenameParts.Last();

            // get the whole image path
            // string fullImagePath = System.IO.Path.GetFullPath(".") + "/" + CfsCommon.IMAGE_PATH_BASE + talentType + "/" + imageFileSystemName;
            string fullImagePath = HttpContext.Current.Server.MapPath("~/" + CfsCommon.IMAGE_PATH_BASE + talentType + "/" + imageFileSystemName);

            try
            {
                // get the image from the file system
                FileStream imageStream = new FileStream(fullImagePath, FileMode.Open);
                Image image = Image.FromStream(imageStream);
                return image;
            }
            catch (Exception ex) { return null; };
        }
        private static Image GetImageFromFileSystem(string talentType, string imageFileSystemName)
        {
            if (string.IsNullOrEmpty(imageFileSystemName))
                return null;

            // just want the image name, so strip off the relative path if it's present
            string[] filenameParts = imageFileSystemName.Split('/');
            imageFileSystemName = filenameParts.Last();

            // get the whole image path
            // string fullImagePath = System.IO.Path.GetFullPath(".") + "/" + CfsCommon.IMAGE_PATH_BASE + talentType + "/" + imageFileSystemName;
            string fullImagePath = HttpContext.Current.Server.MapPath("~/" + CfsCommon.IMAGE_PATH_BASE + talentType + "/" + imageFileSystemName);

            try
            {
                // get the image from the file system
                FileStream imageStream = new FileStream(fullImagePath, FileMode.Open);
                Image image = Image.FromStream(imageStream);
                return image;
            }
            catch (Exception ex) { return null; };
        }
        public static Image GetImage(Guid imageID)
        {
            return ImageManager.GetImage(imageID, false);
        }
        private static Image GetImage(Guid imageID, bool fetchOriginal)
        {
            byte[] imageData = ImageManager.GetImageBytes(imageID, fetchOriginal);
            MemoryStream imageStream = new MemoryStream(imageData);
            Image image = Image.FromStream(imageStream);
            return image;
        }

        public static byte[] GetImageBytes(Guid imageID, bool fetchOriginal)
        {

            TalentImages dbImage = ImageManager.GetDbImage(imageID, fetchOriginal);
            return dbImage.data;
        }

        private static TalentImages GetDbImage(Guid imageID, bool fetchOriginal)
        {
            return ImageManager.GetDbImage(imageID, fetchOriginal, null);
        }
        private static TalentImages GetDbImage(Guid imageID, bool fetchOriginal, CfsEntity cfsEntity)
        {
            bool disposeEntities = false;
            if( cfsEntity == null ) {
                // open a db instance
                cfsEntity = new CfsEntity();
                disposeEntities = true;
            }

            TalentImages[] images = null;
            try
            {
                // fetch images from DB
                if (cfsEntity.TalentImages.Count() > 0)
                    images = cfsEntity.TalentImages.Where(item => item.imageID == imageID).OrderBy(item => item.isOriginal).ToArray() as TalentImages[];
            }
            catch (Exception ex) { }

            if (disposeEntities)
            {
                // clean-up db instance
                cfsEntity.Dispose();
            }

            // if no image found, then exit
            if (images == null)
                return null; // NOTE: throw exception instead?

            // separate into orig and usage
            TalentImages usageImage = images[0];
            TalentImages originalImage;
            if (images.Length > 1) // original exists
                originalImage = images[1];
            else // original doesn't exist
            {
                // save a copy of "usage" in the DB as "original"
                ImageManager.AddImage(usageImage.data, usageImage.imageID, true, usageImage.FK_talentID, usageImage.isApplicant, usageImage.imageIndex);

                // original is a copy of usage (except for rowID, but that's unused)
                originalImage = usageImage;
            }

            // return the image data
            if (fetchOriginal)
            {
                return originalImage;
            }
            else
            {
                return usageImage;
            }
        }
        public static void EditImage(Guid imageID)
        {
            Bitmap bitmap = (Bitmap)ImageManager.GetImage(imageID);
            ImageManager.SaveImage(imageID, bitmap);
        }
        public static void EditImage(Guid imageID, int resizeWidth, int resizeHeight, int cropX, int cropY, int cropWidth, int cropHeight, OverlayImage[] overlayImages )
        {
            // get image
            Bitmap bitmap = (Bitmap)ImageManager.GetImage(imageID);

            // resize image
            bitmap = ImageManager.ResizeBitmap(bitmap, resizeWidth, resizeHeight);

            // add overlays
            bitmap = ImageManager.AddOverlayImages(bitmap, overlayImages);
            
            // crop image
            bitmap = ImageManager.CropBitmap(bitmap, cropX, cropY, cropWidth, cropHeight);

            // save image
            ImageManager.SaveImage(imageID, bitmap);
        }

        private static Bitmap AddOverlayImages(Bitmap destinationBitmap, OverlayImage[] overlayImages)
        {
            Graphics graphics = Graphics.FromImage( destinationBitmap );

            foreach( OverlayImage overlay in overlayImages ) {

                // get overlay
                Bitmap overlayImage = ImageManager.GetImage( overlay.imageID ) as Bitmap;

                // resize overlay
                overlayImage = ImageManager.ResizeBitmap( overlayImage, overlay.width, overlay.height );

                // draw overlay on the destination bitmay
                graphics.DrawImage(overlayImage, overlay.x, overlay.y);
            }

            return destinationBitmap;
        }
        public static void SaveImagebyte(Guid imageID, byte[] imagedate)
        {
            using (CfsEntity cfsEntity = new CfsEntity())
            {
                TalentImages image = ImageManager.GetDbImage(imageID, false, cfsEntity);

                // if no image found, then exit
                if (image == null)
                    return;

                // get image data
                byte[] newImageData = imagedate;

                // set image data
                image.data = newImageData;

                // save
                cfsEntity.SaveChanges();
            }   
        
        }
        private static void SaveImage(Guid imageID, Bitmap bitmap)
        {
            using (CfsEntity cfsEntity = new CfsEntity())
            {
                TalentImages image = ImageManager.GetDbImage(imageID, false, cfsEntity);
          
                // if no image found, then exit
                if (image == null)
                    return;

                // get image data
                byte[] newImageData = (byte[])TypeDescriptor.GetConverter(bitmap).ConvertTo(bitmap, typeof(byte[]));
                
                // set image data
                image.data = newImageData;

                // save
                cfsEntity.SaveChanges();
            }   
        }

        private static Bitmap CropBitmap(Bitmap sourceBitmap, int cropRegionX, int cropRegionY, int cropRegionWidth, int cropRegionHeight)
        {
            // create a new image
            Bitmap croppedBitmap = new Bitmap(cropRegionWidth, cropRegionHeight);

            // fill the new image with a crop of the source image
            Graphics graphics = Graphics.FromImage(croppedBitmap);
            graphics.DrawImage(sourceBitmap, new Rectangle(0, 0, cropRegionWidth, cropRegionHeight), cropRegionX, cropRegionY, cropRegionWidth, cropRegionHeight, GraphicsUnit.Pixel);

            // return cropped image
            return croppedBitmap;
        }

        private static Bitmap ResizeBitmap(Bitmap original, int newWidth, int newHeight)
        {
            Bitmap result = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(original, 0, 0, newWidth, newHeight);

            return result;
        }

        /// <summary>
        /// Use this to add new images when uploading etc.
        /// </summary>
        public static Guid AddImage(byte[] imageData, int talentID, bool isApplicant, int imageIndex)
        {
            // NOTE: imageIndex is base 1

            return ImageManager.AddImage(imageData, Guid.Empty, false, talentID, isApplicant, imageIndex);
        }
        /// <summary>
        /// This is used privately by the ImageManager, and the extra params are used to create "original" images 
        /// </summary>
        private static Guid AddImage(byte[] imageData, Guid imageID, bool isOriginal, int talentID, bool isApplicant, int imageIndex)
        {
            // NOTE: imageIndex is base 1

            if (imageID == Guid.Empty)
            {
                // get new guid             
                imageID = Guid.NewGuid();
            }

            using (CfsEntity imageDB = new CfsEntity())
            {
                // save image to db
                Guid rowID = Guid.NewGuid();
                imageDB.AddToTalentImages(new TalentImages { 
                    rowID= rowID, 
                    imageID = imageID, 
                    data = imageData, 
                    isOriginal = isOriginal ,
                    isApplicant = isApplicant,
                    imageIndex = imageIndex,
                    FK_talentID = talentID });
                imageDB.SaveChanges();
            }

            // return guid
            return imageID;
        }

        public static Guid GetImageIdFromPath(string imagePath)
        {
            // get the guid
            string guidString = imagePath.Split(new string[] { "ID=" }, StringSplitOptions.RemoveEmptyEntries)[1];

            // parse off any extra characters after the guid
            int properGuidStringLength = Guid.NewGuid().ToString().Length;
            if (guidString.Length > properGuidStringLength)
                guidString = guidString.Remove(properGuidStringLength);

            // convert string guid to an actual Guid
            Guid imageID = Guid.Empty;
            try
            {
                imageID = new Guid(guidString);
            }
            catch (Exception ex) { /* ignore */ }

            // return ID
            return imageID;
        }

        public static void RevertImageToOriginal(Guid imageID)
        {
            Bitmap originalImageData = ImageManager.GetImage( imageID, true ) as Bitmap; // get "original" image
            ImageManager.SaveImage(imageID, originalImageData); // save "original" image to "usage" image
        }

        public static void SetImageIndex(Guid imageID, int index)
        { 
            using (CfsEntity cfsEntity = new CfsEntity())
            {
                TalentImages newImage = ImageManager.GetDbImage(imageID, false, cfsEntity);
                TalentImages newImageOrig = ImageManager.GetDbImage(imageID, true, cfsEntity); 

                // Delete existing images with this index
                TalentImages[] oldImages = cfsEntity.TalentImages
                            .Where(item => item.FK_talentID == newImage.FK_talentID)
                            .Where(item => item.imageIndex == index)
                            .Where(item => item.imageID != newImage.imageID)
                            .ToArray() as TalentImages[];

                if (oldImages.Count() > 0)
                {
                    foreach (TalentImages img in oldImages)
                    {
                        cfsEntity.DeleteObject(img);
                    }
                }

                newImage.imageIndex = index;
                newImageOrig.imageIndex = index;

                cfsEntity.SaveChanges();
            }
        }
    }
}
