<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualAttendanceReport.aspx.cs" Inherits="DS.Report.IndividualAttendanceReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Individual Attendance Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="margin: 0 auto; width: 595px">
            <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
                <img alt="" src="/images/action/print.png" onclick="printCall()" />
            </div>
            <div id="lblSectionDiv" runat="server" style="height: auto; margin: 0px">
                <span style="font-size: 16px">
                    <asp:Label ID="lblSName" runat="server" Text="HARIHAR PARA HIGH SCHOOL" CssClass="lblFontStyle"></asp:Label></span><br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblMonthName" runat="server" CssClass="lblFontStyle"></asp:Label></span><br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblRollNo" runat="server" CssClass="lblFontStyle"></asp:Label>
                </span>
                <br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblName" runat="server" CssClass="lblFontStyle"></asp:Label>
                </span>
                <br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblBatchName" runat="server" CssClass="lblFontStyle"></asp:Label></span>
                <br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblShiftName" runat="server" CssClass="lblFontStyle"></asp:Label>
                </span>
                <br />
                <span style="font-size: 16px">
                    <asp:Label ID="lblSectionName" runat="server" CssClass="lblFontStyle"></asp:Label>
                </span>
            </div>
            <br />
            <div id="divIndividualAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 598px; height: auto"></div>
        </div>
    </form>
    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            //  window.location = '/Forms/AbsentDetails.aspx';
        }
    </script>
</body>
</html>
