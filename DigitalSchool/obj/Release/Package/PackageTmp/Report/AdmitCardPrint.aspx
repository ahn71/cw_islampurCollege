<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmitCardPrint.aspx.cs" Inherits="DigitalSchool.Report.AdmitCardPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Admit Card</title>
    <link href="/Styles/reports/admitstyle.css" rel="stylesheet" />
    <link href="/Styles/main.css" rel="stylesheet" />
</head>
<body>
    
<form id="form1" runat="server">
    
<div class="admitCardBox">

    <div id="divAdmitPrint" runat="server"  >  </div>

</div>

<div id="divButton" style="bottom: 0px; height: auto; width: 230px; position:fixed; padding: 5px; text-align: center; background-color: whitesmoke; border: 1px solid green; margin-left: 50px; float: left; left: 0px; margin-right: 40px; margin-top: 5px;">

    <a href="AdmitCardGenerator.aspx"> <img alt="" src="/images/action/back.png"  /></a>
    <img alt="" src="/images/action/print.png"  onclick="printCall()"  />

</div>

</form>

    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';

        }    
    </script>

</body>
</html>
