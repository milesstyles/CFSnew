function isIEtwo() {
    var myNav = navigator.userAgent.toLowerCase();
    return (myNav.indexOf('msie') != -1) ? parseInt(myNav.split('msie')[1]) : false;
}
function bodyloadTwo() {
   
   // alert("hi");
    //var myelement = document.getElementById("webbringheader");

    try {
        var fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("type", "text/css")
        var filename = "";
        if (isIEtwo() == 7) {
           filename = "css/subStyleie7.css";
           //document.getElementById("iconrow").style.cssText = "background-color:Black; height=20px; position:absolute; margin-left:0px;"
          // document.getElementById("fixgoogle").style.cssText = "position:absolute; margin-left:250px;"
        }
        else {
        //    filename = "css/subStyle.css";
           // document.getElementById("fixmygoogle").style.cssText = "position:absolute;margin-right:800px;"
        }

        fileref.setAttribute("href", filename)
        if (typeof fileref != "undefined")
            document.getElementsByTagName("head")[0].appendChild(fileref);
    }
    catch (err) {
        alert(err);
    }
}
function bodyloadOne() {
    
    // alert("hi");
    //var myelement = document.getElementById("webbringheader");

    try {
       
      
        if (isIEtwo() == 7) {
         
            document.getElementById("iconrow").style.cssText = "background-color:Black; height=20px; position:absolute; margin-left:0px;"
            document.getElementById("fixgoogle").style.cssText = "position:absolute; margin-left:250px;"
        }
        else {
         //   filename = "css/subStyle.css";
            // document.getElementById("fixmygoogle").style.cssText = "position:absolute;margin-right:800px;"
        }

    
    }
    catch (err) {
        alert(err);
    }
}