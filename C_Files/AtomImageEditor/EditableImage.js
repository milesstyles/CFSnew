/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("AtomImageEditor");

AtomImageEditor.EditableImage = function(element) {
    AtomImageEditor.EditableImage.initializeBase(this, [element]);

    this.defaultImageURL = "";
    this.imageElement = null;
    this.clearButtonElement = null;
    this.hiddenInputElement = null;
    this.scaleSlider = null;
    this.isImageInitialized = false;
    this.saveFormat = "path={0},width={1},height={2},cropX={3},cropY={4},cropWidth={5},cropHeight={6}";
    this.watermarksParamFormat = ",watermarks={0}";
    this.watermarkSaveFormat = "x={0}|y={1}|w={2}|h={3}|imgPath={4}";
    this.watermarkSaveDelimiter = ";";
    this.minimumImageScale = 25;
    this.maximumImageScale = 400;
    this.nextWatermarkZIndex = 0;
    this.imageUrlFormat = "{0}&TimeStamp={1}"; // 0 = the image URl, 1 = current time

    this.imageLeftPositionBeforeDrag = 0;
    this.imageTopPositionBeforeDrag = 0;

    this.watermarks = [];

    this.imagePath = null;

    //    this.resizeWidth = null;
    //    this.resizeHeight = null;
    //    this.cropX = null;
    //    this.cropY = null;
    //    this.cropWidth = null;
    //    this.cropHeight = null;
}

AtomImageEditor.EditableImage.prototype = {
    initialize: function() {
        setTimeout(Function.createDelegate(this, function() {
            AtomImageEditor.EditableImage.callBaseMethod(this, 'initialize');

            // get ref to hidden element
            this.hiddenInputElement = $("input[type=hidden]", this.get_element())[0];

            // get ref to image element
            this.imageElement = $(".editImage", this.get_element())[0];

            // create default slider
            this.scaleSlider = $(".scaleSlider", this.get_element()).slider();

            // set image
            if (this.imagePath != null && this.imagePath != "") {
                this.setImageURL(this.imagePath);
            } else if (this.defaultImageURL != null && this.defaultImageURL != "") {
                this.setImageURL(this.defaultImageURL);
            }
        }), 2000);
    },
    setImageURL: function(imageUrl) {
        if (!this.isImageInitialized) {
            this.isImageInitialized = true;

            // set z-index
            this.imageElement.style.zIndex = "0";

            // add onload handler
            $addHandler(this.imageElement, "load", Function.createDelegate(this, this.onImageLoad));

            // make image draggable
            $(this.imageElement).draggable({
                stop: Function.createDelegate(this, this.onImageDragged),
                drag: Function.createDelegate(this, this.onImageDragging)
            });

            // make image droppable
            $(this.imageElement).droppable({
                drop: Function.createDelegate(this, this.onWatermarkDefinitionDropped)
            });
        }

        // store current image path
        this.imagePath = imageUrl;

        // get time-stamp
        var now = new Date();
        var nowTimeStamp = now.getDay() + "-" + now.getMonth() + "-" + now.getFullYear() + "-" + now.getTime();

        // add time-stamp
        var timeStampedImageUrl = String.format(this.imageUrlFormat, this.imagePath, now);

        // set new image
        $(this.imageElement).attr("src", timeStampedImageUrl);
    },
    onWatermarkDefinitionDropped: function(event, ui) {

        var draggedElement = ui.draggable[0]; // get dragged element

        if (!Sys.UI.DomElement.containsCssClass(draggedElement, "watermarkDefinition"))
            return; // only allow dropping of watermarkDefitions

        var watermarkDefinition = draggedElement; // get watermark definition

        var containerBounds = this.getBounds(this.imageElement.parentNode);
        var dropX = ui.position.left - containerBounds.x;
        var dropY = ui.position.top - containerBounds.y;

        this.addWatermark(dropX, dropY, watermarkDefinition); // add watermark
    },
    addWatermark: function(dropX, dropY, watermarkDefinition) {
        Sys.Debug.trace("adding watermark");

        this.nextWatermarkZIndex++; // get new zIndex

        // create watermark
        var newWatermark = document.createElement("img");
        newWatermark.src = watermarkDefinition.src;

        // add watermark to DOM
        this.imageElement.parentNode.appendChild(newWatermark);

        // determine destination dimensions
        var watermarkDefinitionBounds = this.getBounds(watermarkDefinition);
        var destWidth = watermarkDefinitionBounds.width;
        var destHeight = watermarkDefinitionBounds.height;

        // make the watermark resizable
        $(newWatermark).resizable({
            stop: Function.createDelegate(this, this.onWatermarkResized),
            containment: 'parent',
            handles: 'n, e, s, w'
        });

        // account for the new resize container
        var watermarkContainer = newWatermark.parentNode;
        watermarkContainer.style.zIndex = this.nextWatermarkZIndex;
        Sys.UI.DomElement.addCssClass(watermarkContainer, "watermark");
        watermarkContainer.style.position = "absolute";
        watermarkContainer.style.width = destWidth + "px";
        watermarkContainer.style.height = destHeight + "px";
        newWatermark.style.width = "100%";
        newWatermark.style.height = "100%";

        // store ref to watermark element
        this.watermarks.push(watermarkContainer);

        // resize watermark
        this.recalculateOriginalWatermarkSize(watermarkContainer, destWidth, destHeight);

        // make watermark draggable
        $(watermarkContainer).draggable({
            stop: Function.createDelegate({ adapter: this, watermark: watermarkContainer }, this.onWatermarkDragged),
            opacity: 0.6,
            containment: 'parent'
        });

        // calculate the watermark's "drop" onto the image
        this.dropWatermark(watermarkContainer, dropX, dropY);
    },
    recalculateOriginalWatermarkSize: function(watermark, newWidth, newHeight) {
        // get current image scale
        var currentScale = this.scaleSlider.slider('option', 'value') / 100;

        // calculate original dimensions
        var origWidth = newWidth / currentScale;
        var origHeight = newHeight / currentScale;

        if (origWidth == 0 || origHeight == 0)
            Sys.Debug.trace("resizeWatermark: origHeight or origWidth is zero"); // log warning

        // store original dimensions
        watermark.originalWidth = origWidth;
        watermark.originalHeight = origHeight;

        // save image
        this.saveImageState();
    },
    dropWatermark: function(watermark, dropX, dropY) {
        // calculate start position
        var imageBounds = this.getBounds(this.imageElement);
        var x = dropX - imageBounds.x;
        var y = dropY - imageBounds.y;

        // get current image scale
        var currentScale = this.scaleSlider.slider('option', 'value') / 100;

        // get normalized pos
        var normalizedPosX = x / currentScale;
        var normalizedPosY = y / currentScale;

        // set custom position attributes, are relative to the image's x,y position
        watermark.normalizedPosX = normalizedPosX;
        watermark.normalizedPosY = normalizedPosY;
        watermark.posX = x;
        watermark.posY = y;

        // refresh watermark position
        this.refreshWatermarkPosition(watermark, imageBounds);

        // save image
        this.saveImageState();
    },
    onWatermarkDragged: function(event, ui) {
        var watermark = this.watermark; // get dragged watermark

        // calculate the drop x, y
        var containerBounds = this.adapter.getBounds(this.adapter.imageElement.parentNode);

        var dropX = ui.position.left; // - containerBounds.x;
        var dropY = ui.position.top; //- containerBounds.y;

        this.adapter.dropWatermark(watermark, dropX, dropY);
    },
    onWatermarkResized: function(event, ui) {
        var watermark = ui.element[0]; // get resized watermark

        // resize
        var destWidth = ui.size.width;
        var destHeight = ui.size.height;
        this.recalculateOriginalWatermarkSize(watermark, destWidth, destHeight);

        // reposition
        var containerBounds = this.getBounds(this.imageElement.parentNode);
        var dropX = ui.position.left; //-containerBounds.x;
        var dropY = ui.position.top; //-containerBounds.y;
        this.dropWatermark(watermark, dropX, dropY);
    },
    refreshWatermarkPositions: function() {
        var imageBounds = this.getBounds(this.imageElement);
        for (var i = 0, len = this.watermarks.length; i < len; i++)
            this.refreshWatermarkPosition(this.watermarks[i], imageBounds);
    },
    refreshWatermarkPosition: function(watermark, /* optional, pass if you already have it */imageBounds) {
        Sys.Debug.trace("refreshing watermark poso");
        if (imageBounds == undefined)
            imageBounds = this.getBounds(this.imageElement);

        watermark.style.top = watermark.posY + imageBounds.y + "px";
        watermark.style.left = watermark.posX + imageBounds.x + "px";
    },
    onImageDragged: function(event, ui) {
        // drag complete - store new position
        this.imageLeftPositionBeforeDrag = ui.position.left;
        this.imageTopPositionBeforeDrag = ui.position.top;

        // constrain image position
        this.constrainImagePosition();

        // update watermarks
        this.refreshWatermarkPositions();

        // save image
        this.saveImageState();
    },
    onImageDragging: function(event, ui) {
        // refresh the watermark positions
        this.refreshWatermarkPositions();
    },
    constrainImagePosition: function() {

        // get image position
        var imageBounds = this.getBounds(this.imageElement);
        var toLeft = imageBounds.x;
        var toRight = imageBounds.x + imageBounds.width;
        var toTop = imageBounds.y;
        var toBottom = imageBounds.y + imageBounds.height;

        // get constrain values
        var containerBounds = this.getBounds(this.imageElement.parentNode, true);

        var maxLeft = 0;
        if (imageBounds.width < containerBounds.width)
            maxLeft = containerBounds.width - imageBounds.width;

        var minRight = containerBounds.width;
        if (imageBounds.width < containerBounds.width)
            minRight = imageBounds.width;

        var maxTop = 0;
        if (imageBounds.height < containerBounds.height)
            maxTop = containerBounds.height - imageBounds.height;

        var minBottom = containerBounds.height;
        if (imageBounds.height < containerBounds.height)
            minBottom = imageBounds.height;

        // constrain moving too far to the right
        if (maxLeft < toLeft)
            this.imageElement.style.left = maxLeft + "px";

        // constrain moving too far to the left
        if (toRight < minRight)
            this.imageElement.style.left = minRight - imageBounds.width + "px";

        // constrain moving too far down
        if (maxTop < toTop)
            this.imageElement.style.top = maxTop + "px";

        // constrain moving too far up
        if (toBottom < minBottom)
            this.imageElement.style.top = minBottom - imageBounds.height + "px";
    },
    onImageLoad: function(e) {

        Sys.Debug.trace("onImageLoad");

        // clear drag+drop positioning
        this.imageElement.style.left = "0px";
        this.imageElement.style.top = "0px";

        // clear scaling
        this.imageElement.style.width = "";
        this.imageElement.style.height = "";

        // measure image
        var imageBounds = this.getBounds(this.imageElement);
        this.imageElement.originalWidth = imageBounds.width;
        this.imageElement.originalHeight = imageBounds.height;

        // clear previous edits
        this.clearAllModifications();


        //      NOTE: This block of code only makes sense if the image is not croppe+resized on post-back:
        //        if (this.imagePath != null && this.imagePath != "") {
        //            // load previous image edit properties

        //            // set drag+drop positioning
        //            this.imageElement.style.left = -this.cropX + "px";
        //            this.imageElement.style.top = -this.cropY + "px";

        //            // set scaling
        //            this.imageElement.style.width = this.resizeWidth + "px";
        //            this.imageElement.style.height = this.resizeHeight + "px";
        //            // TODO - set slider poso            

        //            // constrain image
        //            this.constrainImagePosition();
        //        }

        // save image
        this.saveImageState();

        // fetch clear button
        this.clearButtonElement = $(".clearButton", this.get_element())[0];

        // wire up clear button
        $addHandler(this.clearButtonElement, "click", Function.createDelegate(this, this.onClearClick));

        // show clear button
        this.clearButtonElement.style.display = "";

        // fetch revert button
        this.revertButtonElement = $(".revertButton", this.get_element())[0];

        // wire up revert button
        $addHandler(this.revertButtonElement, "click", Function.createDelegate(this, this.onRevertClick));

        // show revert button
        this.revertButtonElement.style.display = "";
    },
    onClearClick: function(e) {
        e.preventDefault();

        this.clearAllModifications();
    },
    onRevertClick: function(e) {
        e.preventDefault();

        this.revertToOriginal();
    },
    revertToOriginal: function() {
        // call WS to revert
        AtomImageEditor.ImageManagerWS.RevertImageToOriginal(this.imagePath, Function.createDelegate(this, this.onImageManagerWebserviceSuccess), Function.createDelegate(this, this.onImageManagerWebserviceFailure));
    },
    onImageManagerWebserviceSuccess: function() {
        // NOTE: Currently this is the only WS method, so this is fine. If any other WS methods are added then a switch statement will have to be added here.
        this.refreshImageFromServer();
    },
    onImageManagerWebserviceFailure: function() {
        Sys.Debug.trace("warning: ImageManager webservice call failed");
    },
    refreshImageFromServer: function() {
        this.setImageURL(this.imagePath);
    },
    clearAllModifications: function() {

        // remove all watermarks
        for (var i = this.watermarks.length - 1; i > -1; i--)
            $(this.watermarks[i]).empty().remove();
        this.watermarks = [];

        // reset scale to 100%
        this.setScale(1);

        // reset scale slider to 100
        this.resetSlider();

        // clear drag+drop positioning
        this.imageElement.style.left = "0px";
        this.imageElement.style.top = "0px";
    },
    resetSlider: function() {
        this.scaleSlider.slider('option', 'min', this.minimumImageScale);
        this.scaleSlider.slider('option', 'max', this.maximumImageScale);
        this.scaleSlider.slider('option', 'value', 100);
        this.scaleSlider.slider('option', 'slide', Function.createDelegate(this, this.onSliderChange));
    },
    onSliderChange: function(event, ui) {
        // update text
        //ui.handle.innerHTML = ui.value + "%";

        // apply scale         
        var scale = ui.value / 100;
        this.setScale(scale);
    },
    setScale: function(scale) {
        // scale image
        this.setImageScale(scale);

        // reposition image
        this.constrainImagePosition();

        // scale watermarks
        this.setWatermarksScale(scale);

        // compute watermark positions
        this.refreshWatermarkPositions();

        // save image
        this.saveImageState();
    },
    setImageScale: function(scale) {
        // get current dimensions
        var elementBounds = this.getBounds(this.imageElement);
        var currentWidth = elementBounds.width;
        var currentHeight = elementBounds.height;
        var currentTop = elementBounds.y;
        var currentLeft = elementBounds.x;

        // calculate new size
        var newWidth = this.imageElement.originalWidth * scale;
        var newHeight = this.imageElement.originalHeight * scale;

        // set new style size
        this.imageElement.style.width = newWidth + "px";
        this.imageElement.style.height = newHeight + "px";

        // calculate differences
        var leftAdjust = (currentWidth - newWidth) / 2;
        var topAdjust = (currentHeight - newHeight) / 2;

        // calc new position
        var newTop = currentTop + topAdjust;
        var newLeft = currentLeft + leftAdjust;

        // set new style positions
        this.imageElement.style.left = newLeft + "px";
        this.imageElement.style.top = newTop + "px";
    },
    setWatermarksScale: function(scale) {

        for (var i = 0, len = this.watermarks.length; i < len; i++) {
            // get watermark
            var watermark = this.watermarks[i];

            // calculate new size
            var newWidth = watermark.originalWidth * scale;
            var newHeight = watermark.originalHeight * scale;

            Sys.Debug.trace("scale=" + scale);
            Sys.Debug.trace("oldW=" + watermark.style.width + ", newW=" + newWidth + ", oldH=" + watermark.style.height + ", newH=" + newHeight);
            Sys.Debug.trace("oldX=" + watermark.posX + ", newX=" + watermark.normalizedPosX * scale + ", oldY=" + watermark.posY + ", newY=" + watermark.normalizedPosY * scale);

            // set new style size
            watermark.style.width = newWidth + "px";
            watermark.style.height = newHeight + "px";

            // set new position
            watermark.posX = watermark.normalizedPosX * scale;
            watermark.posY = watermark.normalizedPosY * scale;
        }
    },
    getBounds: function(element, getSizeFromStyle) {
        var bounds = Sys.UI.DomElement.getBounds(element);

        if (element.style.left != "")
            bounds.x = element.style.left.replace(/px/, "") - 0; // get from style

        if (element.style.top != "")
            bounds.y = element.style.top.replace(/px/, "") - 0;

        if (getSizeFromStyle) {
            bounds.width = element.style.width.replace(/px/, "") - 0;
            bounds.height = element.style.height.replace(/px/, "") - 0;
        }

        if (bounds.width == 0 || bounds.height == 0)
            Sys.Debug.trace("warning: height or width is zero for element: " + element.tagName + "#" + element.id + "." + element.className);

        return bounds;
    },
    saveImageState: function() {

        // get image info
        var imageBounds = this.getBounds(this.imageElement);
        var containerBounds = this.getBounds(this.imageElement.parentNode, true);

        // format save value
        var saveValue = String.format(this.saveFormat, this.imagePath, Math.round(imageBounds.width), Math.round(imageBounds.height), Math.round(-imageBounds.x), Math.round(-imageBounds.y), containerBounds.width, containerBounds.height, joinedWatermarkSaveInfo);

        // get watermark infos
        var watermarkSaveInfos = [];
        for (var i = 0, len = this.watermarks.length; i < len; i++) {
            var watermark = this.watermarks[i]; // get watermark
            var watermarkBounds = this.getBounds(watermark); // get watermark bounds

            var watermarkImagePath = watermark.getElementsByTagName("img")[0].src; // get picture path

            var watermarkSaveInfo = String.format(this.watermarkSaveFormat, Math.round(watermark.posX), Math.round(watermark.posY), Math.round(watermarkBounds.width), Math.round(watermarkBounds.height), watermarkImagePath);

            watermarkSaveInfos.push(watermarkSaveInfo);
        }
        var joinedWatermarkSaveInfo = watermarkSaveInfos.join(this.watermarkSaveDelimiter); // join the watermark save infos

        if (joinedWatermarkSaveInfo != "")
            saveValue += String.format(this.watermarksParamFormat, joinedWatermarkSaveInfo);

        // store save value
        this.hiddenInputElement.value = saveValue;
    },
    dispose: function() {
        //Add custom dispose actions here
        AtomImageEditor.EditableImage.callBaseMethod(this, 'dispose');
    }
}
AtomImageEditor.EditableImage.registerClass('AtomImageEditor.EditableImage', Sys.UI.Behavior);

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
