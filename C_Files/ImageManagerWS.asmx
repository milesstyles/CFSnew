<%@ WebService Language="C#" Class="AtomImageEditor.ImageManagerWS" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using AtomImageEditor;

namespace AtomImageEditor
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.Web.Script.Services.ScriptService]
    public class ImageManagerWS : System.Web.Services.WebService {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool RevertImageToOriginal(string imagePath)
        {
            // get image ID
            Guid imageID = ImageManager.GetImageIdFromPath(imagePath);

            // call a method in imagemanager to revert
            ImageManager.RevertImageToOriginal(imageID);

            // return a value just so client knows this is done ( prob can just return void )
            return true;
        }
    }   
}

