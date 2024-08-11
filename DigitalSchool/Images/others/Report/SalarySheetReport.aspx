<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalarySheetReport.aspx.cs" Inherits="DS.Report.SelarySheetReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Selary Sheet Report </title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3><asp:Label runat="server" ID="lblSchoolName"></asp:Label></h3>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div id="divSalaryDetailsInfo" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
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
