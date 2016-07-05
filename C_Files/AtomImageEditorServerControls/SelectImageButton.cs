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
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ServerControl1 runat=server></{0}:ServerControl1>")]
    public class SelectImageButton : HtmlAnchor
    {
        public string CssClass { get; set; }
        public string TargetEditableImageClientID { get; set; }

        public SelectImageButton() 
            : base()
        {
            
        }

        protected override void OnPreRender(EventArgs e)
        {
            // set style properties
            this.CssClass += " selectImageButton";

            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
            writer.RenderBeginTag(HtmlTextWriterTag.A);

            this.RenderChildren(writer);

            // init JavaScript adapter
            string initFunctionName = this.ClientID+"_init";
            writer.Write(
            "<script>" +    
            "function "+initFunctionName+"() {" +
            "var imageSelectButton = $create(AtomImageEditor.SelectImageButton, {}, {}, {}, $get('" + this.ClientID + "'));" +
            "imageSelectButton.set_targetEditableImage('" + this.TargetEditableImageClientID + "');" +
            "}" +
            "Sys.Application.add_init(" + initFunctionName + ");" +
            "</script>");

            writer.RenderEndTag();
        }
    }
}
