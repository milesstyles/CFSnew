using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AtomImageEditor;

public partial class AtomImageEditor_ImageHandler : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "image/jpeg"; // set response as image type

        if (this.Request.Params["ID"] != null)
        {
            // convert ID param to GUID
            Guid id;
            try
            {
                id = new Guid(this.Request.Params["ID"]);
            }
            catch (Exception ex) { return; }

            // get image
            byte[] imageData = ImageManager.GetImageBytes(id, false);

            if (imageData.Length == 0)
                return;

            // return image via response
            Response.Buffer = true;
            Response.BinaryWrite(imageData);
        }
    }
}
