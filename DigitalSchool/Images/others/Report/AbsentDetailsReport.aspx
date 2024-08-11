<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AbsentDetailsReport.aspx.cs" Inherits="DS.Report.AbsentDetailsReport" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>Month Wise Absent Details</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>DIGITAL SCHOOL</h3>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label></h3>
            <p style="margin: 0px; font-size: 14px;">Absent Details Report </p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>
        <div id="divAbsentDetails" class="datatables_wrapper" runat="server" 
            style="margin: 0 auto; height: auto; width: 595px; font-size: 11px; border: 1px solid black"></div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/back.png" onclick="goBack()" />
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
    </form>
    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            window.location = '/Forms/AbsentDetails.aspx';
        }
    </script>
</body>
</html>
