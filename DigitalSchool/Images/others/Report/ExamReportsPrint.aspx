<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamReportsPrint.aspx.cs" Inherits="DS.Report.ExamReportsPrint" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Exam Report</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; left: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div class="reportHeader" style="width: 960px; margin:10px auto;">
            <h3>DIGITAL SCHOOL</h3>
            <div style="width: 100%; margin: 2px auto">
                <asp:Label ID="lblProgressReport" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <asp:Label ID="lblName" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <asp:Label ID="lblClass" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <asp:Label ID="lblShift" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <div style="border: 1px solid black;" class='datatables_wrapper' runat="server" id="divProgressReport"></div>
            </div>
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
