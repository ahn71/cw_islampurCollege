<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadReportPrint.aspx.cs" Inherits="DS.UI.Reports.TimeTable.LoadReportPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Teacher Load Report</title>
    <link href="../../../AssetsNew/css/bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div class="reportHeader" style="height: 102px!important;text-align: center;">
            <h3 runat="server" id="hSchoolName"></h3>
            <a style="font-size:14px" runat="server" id="aAddress"></a>            
            <p runat="server" id="pTitle" style="margin: 0px; font-size: 16px; font-weight: bold;"></p>
        </div>        
         <div id="divLoadReport" style="width:862px;margin:0px auto" runat="server"></div> 
            
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
