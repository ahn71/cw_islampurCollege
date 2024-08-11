<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DueListReport.aspx.cs" Inherits="DS.Report.DueListReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Due List Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>DIGITAL SCHOOL</h3>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <p style="margin: 0px; font-size: 14px;"></p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
            (Due List)
        </div>
        <div id="divDueList" class="datatables_wrapper" runat="server"
            style="width: 595px; margin: 0 auto; border: 1px solid black; overflow: auto; overflow-x: hidden;">
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
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
            window.location = '/Forms/GuardianContactList.aspx';
        }
    </script>
</body>
</html>
