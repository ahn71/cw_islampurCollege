<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceDetailsReport.aspx.cs" Inherits="DS.Report.AttendanceDetailsReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Attendance Details</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h4 style="margin: 0px">DIGITAL SCHOOL</h4>
            <h5 style="margin: 0px">
                <asp:Label runat="server" ID="lblReportName"></asp:Label></h5>
            <h5 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label></h5>
            <h5 style="margin: 0px">
                <asp:Label runat="server" ID="lblShift"></asp:Label>
            </h5>
            <h5 style="margin: 0px">
                <asp:Label runat="server" ID="lblSection"></asp:Label>
            </h5>
        </div>
        <br />
        <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>

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
            window.location = '/Forms/AbsentDetails.aspx';
        }
    </script>
</body>
</html>
