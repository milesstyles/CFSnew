using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;

public partial class AtomImageEditor_ImageSelector : System.Web.UI.UserControl
{
    protected int talentId = 0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BindImageList();
    }

    private void BindImageList()
    {
        using (CfsEntity cfsEntity = new CfsEntity())
        {
            // get list of images
            List<Images> images = new List<Images>(cfsEntity.Images.AsEnumerable());

            // bind listview
            this.ImagesList.DataSource = images;
            this.ImagesList.DataTextField = "imageID";
            this.ImagesList.DataValueField = "imageID";
            this.ImagesList.DataBind();
        }
    }

    public int TalentID
    {
        get { return this.talentId; }
        set { this.talentId = value; }
    }
}
