/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("AtomImageEditor");

AtomImageEditor.SelectImageButton = function(element) {
    AtomImageEditor.SelectImageButton.initializeBase(this, [element]);

    this.targetEditableImage = null;
}

AtomImageEditor.SelectImageButton.prototype = {
    initialize: function() {
        AtomImageEditor.SelectImageButton.callBaseMethod(this, 'initialize');
        // attach click handler
        $addHandler(this.get_element(), "click", Function.createDelegate(this, this.onClick));
    },
    onClick: function(e) {
        e.preventDefault();
        ImageSelector.getImageSelection(this);
    },
    onImageSelected: function(imageUrl) {
        this.targetEditableImage.setImageURL(imageUrl);
    },
    set_targetEditableImage: function(editableImageComponentInstanceName) {
        this.targetEditableImage = eval(editableImageComponentInstanceName);
    },
    dispose: function() {
        //Add custom dispose actions here
        AtomImageEditor.SelectImageButton.callBaseMethod(this, 'dispose');
    }
}
AtomImageEditor.SelectImageButton.registerClass('AtomImageEditor.SelectImageButton', Sys.UI.Behavior);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
