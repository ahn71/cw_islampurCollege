<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceSheetReport.aspx.cs" Inherits="DS.Report.MonthWiseAttendanceSheeReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Month Wise Attendance Sheet</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>HARIHAR PARA HIGH SCHOOL</h3>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <p style="margin: 0px; font-size: 14px;">Attendance Sheet Report </p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>
        <div id="divAttendanceSheet" class="datatables_wrapper" runat="server" style="margin: 0 auto; height: auto; font-size: 11px; border: 1px solid black"></div>
        <asp:GridView runat="server" ID="gvAttendanceSheet" ClientIDMode="Static" class='display'></asp:GridView>

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
            window.location = '/Forms/MonthWiseAttendanceSheetSummary.aspx';
        }
    </script>
</body>
</html>
