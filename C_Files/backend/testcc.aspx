<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testcc.aspx.cs" Inherits="backend_testcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <style>
        .demo-container {
            width: 100%;
            max-width: 350px;
            margin: 50px auto;
        }

        form {
            margin: 30px;
        }
        input {
            width: 200px;
            margin: 10px auto;
            display: block;
        }

    </style>
    <div class="demo-container">
        <div class="card-wrapper"></div>

        <div class="form-container active">
            <form action="">
                <input placeholder="Card number" type="text" name="number">
                <input placeholder="Full name" type="text" name="name">
                <input placeholder="MM/YY" type="text" name="expiry">
                <input placeholder="CVC" type="text" name="cvc">
            </form>
        </div>
    </div>

    <script src="lib/js/card.js"></script>
    <script>
        new Card({
            form: document.querySelector('form'),
            container: '.card-wrapper'
        });
    </script>
    </form>
</body>
</html>
