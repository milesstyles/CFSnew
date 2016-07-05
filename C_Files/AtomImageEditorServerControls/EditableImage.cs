using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace AtomImageEditorServerControls
{
    public struct WatermarkInfo
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public string imagePath;
    }

    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class EditableImage : HiddenField
    {
        public string DefaultImageURL { get; set; }
        public string CssClass { get; set; }
        public string ClearButtonText { get; set; }
        public string RevertButtonText { get; set; }
        
        public int Width { get; set; }
        public int Height { get; set; }

        public bool HasEditedImage { private set; get; }

        public string ImagePath { private set; get; }
        public int ResizeWidth { private set; get; }
        public int ResizeHeight { private set; get; }
        public int CropX { private set; get; }
        public int CropY { private set; get; }
        public int CropWidth { private set; get; }
        public int CropHeight { private set; get; }
        public WatermarkInfo[] Watermarks { private set; get; }
        
        private string style = string.Empty;

        public EditableImage() 
            : base()
        {
            this.Watermarks = new WatermarkInfo[] { };
        }

        protected override bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            bool loadSuccessful = base.LoadPostData(postDataKey, postCollection);

            this.OnLoadPostDataComplete();

            return loadSuccessful;
        }

        private void OnLoadPostDataComplete()
        {
            this.HasEditedImage = !string.IsNullOrEmpty(this.Value);
            if (this.HasEditedImage)
            {
                string unparsedSaveData = this.Value; // format: "path={0}width={1},height={2},cropX={3},cropY={4},cropWidth={5},cropHeight={6},watermarks=x:aoeu|y:aoeu;x:aoeu|y:aoeu";

                // split data into separate properties
                string[] saveProperties = unparsedSaveData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string saveProperty in saveProperties)
                {
                    // get property name and value
                    string[] propertyParts = saveProperty.Split(new string[] { "=" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string propertyName = propertyParts[0];
                    string propertyValue = propertyParts[1];

                    // save property value
                    switch (propertyName)
                    {
                        case "path":
                            string imagePathWithoutTimeStamp = propertyValue.Split( new string[] { "TimeStamp=" }, StringSplitOptions.RemoveEmptyEntries)[0]; // remove old timestamp
                            this.ImagePath = imagePathWithoutTimeStamp;
                            break;

                        case "width":
                            this.ResizeWidth = int.Parse(propertyValue);
                            break;

                        case "height":
                            this.ResizeHeight = int.Parse(propertyValue);
                            break;

                        case "cropX":
                            this.CropX = int.Parse(propertyValue);
                            break;

                        case "cropY":
                            this.CropY = int.Parse(propertyValue);
                            break;

                        case "cropWidth":
                            this.CropWidth = int.Parse(propertyValue);
                            break;

                        case "cropHeight":
                            this.CropHeight = int.Parse(propertyValue);
                            break;

                        case "watermarks":
                            this.extractWatermarks(propertyValue);
                            break;
                    }
                }
            }        
        }

        private void extractWatermarks(string joinedWatermarks)
        {
            string[] encodedWatermarks = joinedWatermarks.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries); // separate into individual watermarks

            List<WatermarkInfo> watermarkList = new List<WatermarkInfo>();
            foreach (string encodedWatermark in encodedWatermarks)
            {
                WatermarkInfo newWatermark = new WatermarkInfo();

                // separate into properties
                string[] encodedWatermarkProperties = encodedWatermark.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string encodedWatermarkProperty in encodedWatermarkProperties)
                {

                    // get property name and value
                    string[] watermarkPropertyParts = encodedWatermarkProperty.Split(new string[] { "=" }, 2, StringSplitOptions.RemoveEmptyEntries);
                    string propertyName = watermarkPropertyParts[0];
                    string propertyValue = watermarkPropertyParts[1];

                    // save property value
                    switch (propertyName)
                    {
                        case "x":
                            newWatermark.x = int.Parse(propertyValue);
                            break;

                        case "y":
                            newWatermark.y = int.Parse(propertyValue);
                            break;

                        case "w":
                            newWatermark.width = int.Parse(propertyValue);
                            break;

                        case "h":
                            newWatermark.height = int.Parse(propertyValue);
                            break;

                        case "imgPath":
                            newWatermark.imagePath = propertyValue;
                            break;
                    }

                }
                
                // add watermark
                watermarkList.Add(newWatermark);
            }

            // save watermarks
            this.Watermarks = watermarkList.ToArray();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            // set default width
            if (this.Width == 0)
                this.Width = 200;

            // set default height
            if (this.Height == 0)
                this.Height = 200;

            // set default ResetButtonText
            if( string.IsNullOrEmpty( this.ClearButtonText ))
                this.ClearButtonText = "Clear edits";

            // set default ResetButtonText
            if (string.IsNullOrEmpty(this.RevertButtonText))
                this.RevertButtonText = "Revert to original";

            // set style properties
            this.CssClass += " editableImageControl";
            this.style += "width:" + this.Width + "px;";
            this.style += "height:" + this.Height + "px;";

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            string editableImageControlID = this.ClientID + "_editableImageControl";

            // start wrapper
            writer.AddAttribute(HtmlTextWriterAttribute.Id, editableImageControlID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // render the hidden field
            base.Render(writer);

            // start image container
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "editImageContainer");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, this.style);
            writer.RenderBeginTag( HtmlTextWriterTag.Div);

            // image
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "editImage");
            //writer.AddAttribute(HtmlTextWriterAttribute.Height, "150");
            //writer.AddAttribute(HtmlTextWriterAttribute.Width, "150");
            //writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
            //writer.AddAttribute(HtmlTextWriterAttribute.Style, "position:relative");
            writer.RenderBeginTag(HtmlTextWriterTag.Img);
            writer.RenderEndTag();

            // end image container
            writer.RenderEndTag();

            // render slider
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "scaleSlider");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();

            // render "clear" link
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "clearButton");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write( this.ClearButtonText );
            writer.RenderEndTag();

            // render "revert to original" link
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "revertButton");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            writer.Write(this.RevertButtonText);
            writer.RenderEndTag();

            // init JavaScript adapter
            string initFunctionName = this.ClientID+"_init";

            writer.Write(
            "<script>" +    
            "var "+this.ClientID+";" +
            "function "+initFunctionName+"() {" +
            this.ClientID + " = $create(AtomImageEditor.EditableImage, {}, {}, {}, $get('" + editableImageControlID + "'));" +
            this.ClientID+".defaultImageURL = '"+this.DefaultImageURL+"';");

            if( this.HasEditedImage ) {
                writer.Write( this.ClientID + ".imagePath = '" + this.ImagePath + "';" );
                //this.ClientID+".imagePath = '"+this.ImagePath+"';" +
                //this.ClientID+".resizeWidth = "+this.ResizeWidth+";" +
                //this.ClientID+".resizeHeight = "+this.ResizeHeight+";" +
                //this.ClientID+".cropX = "+this.CropX+";" +
                //this.ClientID+".cropY = "+this.CropY+";" +
                //this.ClientID+".cropWidth = "+this.CropWidth+";" +
                //this.ClientID+".cropHeight = "+this.CropHeight+";");
            }

            writer.Write(
            "}" +
            "Sys.Application.add_init(" + initFunctionName + ");" +
            //"SET_DHTML('"+this.ClientID+"');" +
            "</script>");

            // end wrapper
            writer.RenderEndTag();
        }
    }
}
