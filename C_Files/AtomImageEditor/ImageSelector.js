/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("AtomImageEditor");

AtomImageEditor.ImageSelector = function(element) {
    AtomImageEditor.ImageSelector.initializeBase(this, [element]);

    this.imageUrlFormat = "../AtomImageEditor/ImageHandler.aspx?ID={0}";
    this.requestingSelectImageButton = null;
    this.imagesList = null;
    this.cancelButton = null;
}

AtomImageEditor.ImageSelector.prototype = {
    initialize: function() {
        AtomImageEditor.ImageSelector.callBaseMethod(this, 'initialize');

        // wire up cancel button
        this.cancelButton = $(".cancelSelect", this.get_element())[0];
        $addHandler(this.cancelButton, "click", Function.createDelegate(this, this.onCancelClick));
    },
    set_imagesList: function(imagesList) {
        this.imagesList = imagesList;

        // attach onchange to images list
        $addHandler(this.imagesList, "change", Function.createDelegate(this, this.onImageChange));
    },
    onCancelClick: function(e) {
        e.preventDefault();
        this.hide();
    },
    onImageChange: function() {
        if (this.requestingSelectImageButton == null)
            return; // abort if not targetting an image

        // get the newly selected image ID
        var newImageID = this.imagesList.options[this.imagesList.selectedIndex].value;

        // construct new image URL
        var newURL = String.format(this.imageUrlFormat, newImageID);

        // update image
        this.requestingSelectImageButton.onImageSelected(newURL);

        this.hide();
    },
    onImageUploaded: function(newImageID) {
        // add to images list
        var newImageEntry = new Option();
        newImageEntry.text = newImageID;
        newImageEntry.value = newImageID;
        this.imagesList.options.add(newImageEntry);

        // select it
        this.imagesList.selectedIndex = this.imagesList.options.length - 1;
        this.onImageChange();
    },
    getImageSelection: function(requestingSelectImageButton) {
        this.requestingSelectImageButton = requestingSelectImageButton;
        this.show();
    },
    show: function() {
        this.get_element().style.display = "";
    },
    hide: function() {
        this.get_element().style.display = "none";
    },
    set_targetImageElement: function(targetImageElement) {
        this.targetImageElement = targetImageElement;
    },

    dispose: function() {
        //Add custom dispose actions here
        AtomImageEditor.ImageSelector.callBaseMethod(this, 'dispose');
    }
}
AtomImageEditor.ImageSelector.registerClass('AtomImageEditor.ImageSelector', Sys.UI.Behavior);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
