using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace AtomImageEditorServerControls
{
    public class WatermarkDefinition : Image
    {
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.CssClass += " watermarkDefinition";
        }

        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);
        }
    }
}
