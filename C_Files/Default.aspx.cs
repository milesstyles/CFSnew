using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CfsNamespace;
using AtomImageEditor;

public partial class _Default : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //mymobile f = new mymobile();
      
        // Hide bookmark/share button in left column control
        Panel pnlShare = (Panel)leftColumn1.FindControl("pnlShare");
        if (pnlShare != null)
        {
            pnlShare.Visible = false;
        }

        dlNewTalent.ItemDataBound += new DataListItemEventHandler(dlNewTalent_ItemDataBound);

        BindNewTalent();
    }

    #region Data Binding
    private void BindNewTalent()
    {
        try
        {
            CfsEntity cfse = new CfsEntity();
            List<Talent> talList = ((ObjectQuery<Talent>)cfse.Talent.Where("it.NewTalentImg <> ''").Where("it.IsActive == true").Where("it.TalentType== 'female'").OrderBy("it.DateCreated desc").Top("5")).ToList();
            dlNewTalent.DataSource = talList;
            dlNewTalent.DataBind();
        }
        catch(Exception ex) {
        }
    }

    protected void dlNewTalent_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        Talent talRec = (Talent)e.Item.DataItem;
        
        Image imgNewTalent = (Image)e.Item.FindControl("imgNewTalent");
        if (imgNewTalent != null)
        {
            if (!string.IsNullOrEmpty(talRec.NewTalentImg))
            {
                Guid thumbGuid = ImageManager.GetImageGuid(7, talRec.TalentId, talRec.TalentType, talRec.NewTalentImg);
                
                if (thumbGuid != Guid.Empty)
                {                    
                    imgNewTalent.ImageUrl = CfsCommon.GetTalentImagePath("", thumbGuid);
                    imgNewTalent.Visible = true;
                }
            }
        }
    }
    #endregion
}
