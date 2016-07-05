<%@ Page Language="C#" AutoEventWireup="true" CodeFile="videopage.aspx.cs" Inherits="videopage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
</head>
<body>
<div id="player">Loading videos.</div>
<script type="text/javascript" src="media/swfobject.js"></script>


<script type="text/javascript">

    var so = new SWFObject('media/player.swf', 'mpl', '400', '188', '9');
    so.addParam('allowscriptaccess', 'always');
    so.addParam('allowfullscreen', 'true');
    so.addParam('flashvars', '&file=media/playlist.xml&playlistsize=200&playlist=right');
    so.write('player');
</script>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
