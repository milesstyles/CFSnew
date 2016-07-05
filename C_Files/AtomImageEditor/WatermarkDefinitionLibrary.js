/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("AtomImageEditor");

AtomImageEditor.WatermarkDefinitionLibrary = function(element) {
    AtomImageEditor.WatermarkDefinitionLibrary.initializeBase(this, [element]);
    
    
}
AtomImageEditor.WatermarkDefinitionLibrary.prototype = {
    initialize: function() {
        AtomImageEditor.WatermarkDefinitionLibrary.callBaseMethod(this, 'initialize');

        // make image draggable
        var watermarkDefinitions = $(".watermarkDefinition", this.get_element());
        $(watermarkDefinitions).draggable({
            stop: Function.createDelegate(this, this.onWatermarkDefinitionDragged),
            helper: 'clone',
            opacity: 0.6
        });
    },
    onWatermarkDefinitionDragged: function(event, ui) {
        Sys.Debug.trace("dragged WatermarkDefinition");

        // return the watermark definition to the library space
        //ui.helper.position.top = 0;
        //ui.helper.position.left = 0;
        //var watermarkDefinition = ui.helper[0];
        //watermarkDefinition.style.top = "";
        //watermarkDefinition.style.left = "";

    },
    dispose: function() {
        //Add custom dispose actions here
        AtomImageEditor.WatermarkDefinitionLibrary.callBaseMethod(this, 'dispose');
    }
}
AtomImageEditor.WatermarkDefinitionLibrary.registerClass('AtomImageEditor.WatermarkDefinitionLibrary', Sys.UI.Behavior);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
