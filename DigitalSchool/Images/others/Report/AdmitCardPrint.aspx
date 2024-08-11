<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmitCardPrint.aspx.cs" Inherits="DS.Report.AdmitCardPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Admit Card</title>
    <link href="/Styles/reports/admitstyle.css" rel="stylesheet" />
    <link href="/Styles/main.css" rel="stylesheet" />
    <style>
        .contentAdmit {
    /*padding-bottom: 20px; overflow: hidden;*/
    border: 0 solid;
    margin:10px auto;
    padding: 1%;
    /*width: 930px;*/
}

        
         @media print {
             .admin-card-box {
                    margin: 30px auto;
                }
            
          }
    </style>
</head>
<body>
    
<form id="form1" runat="server">
    
<div class="admitCardBox">
    
    <div id="divAdmitPrint" runat="server"  >  </div>

</div>

<div id="divButton" style="position:fixed; padding: 5px;  top:0; right:0; margin-top: 5px;">
    <%--<img alt="" src="/images/action/back.png"  onclick="goBack()" />--%>
    <img alt="" src="/images/action/print.png"  onclick="printCall()"  />
</div>

</form>

    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            window.location = '/Report/AdmitCardGenerator.aspx';
        }
    </script>

</body>
</html>
