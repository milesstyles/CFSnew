using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace AtomImageEditorServerControls
{
    public class WatermarkDefinitionLibrary : Panel
    {
        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            base.Render(writer);

            // init JavaScript adapter
            string initFunctionName = this.ClientID + "_init";
            writer.Write(
            "<script>" +
            "function " + initFunctionName + "() {" +
            "var watermarkDefinitionLibrary = $create(AtomImageEditor.WatermarkDefinitionLibrary, {}, {}, {}, $get('" + this.ClientID + "'));" +
            //"imageSelectButton.set_targetEditableImage('" + this.TargetEditableImageClientID + "');" +
            "}" +
            "Sys.Application.add_init(" + initFunctionName + ");" +
            "</script>");
        }
    }
}
