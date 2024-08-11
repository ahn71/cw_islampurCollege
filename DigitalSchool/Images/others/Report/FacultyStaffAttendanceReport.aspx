<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacultyStaffAttendanceReport.aspx.cs" Inherits="DS.Report.FacultyStaffAttendanceReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Faculty Staff Attendance Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4>HARIHAR PARA HIGH SCHOOL</h4>
            <span style="font-size: 16px">
                <asp:Label runat="server" ID="lblReportName" CssClass="lblFontStyle"></asp:Label></span><br />
            <span style="font-size: 16px">
                <asp:Label runat="server" ID="lblDepartment" CssClass="lblFontStyle"></asp:Label></span><br />
            <span style="font-size: 16px">
                <asp:Label runat="server" ID="lblDesignation" CssClass="lblFontStyle"></asp:Label>
            </span>
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <br />
        <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
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
