<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentResultReport.aspx.cs" Inherits="DS.Report.StudentResultReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Student Result Report</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>DIGITAL SCHOOL</h3>
            STUDENT RESULT INFO-<asp:Label ID="lblYear" runat="server"></asp:Label>
            <br />
            <asp:Label ID="lblClass" runat="server" Text=""></asp:Label>
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div style="width: 595px; margin: 0 auto">
            <div runat="server" id="divLoadFailSubject"></div>
            <asp:GridView ID="gvPassListOfStudent" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
            <br />
            <asp:GridView ID="gvFailListOfStudent" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
        </div>
    </form>
    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            window.location = '/Forms/StudentList.aspx';
        }
    </script>
</body>
</html>
