<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentContactReport.aspx.cs" Inherits="DS.Report.StudentContactReport" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>Student Contact List Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>HARIHAR PARA HIGH SCHOOL</h3>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <p style="margin: 0px; font-size: 14px;">STUDENT CONTACT LIST</p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>
        <div id="divStudentContactListReport" class="datatables_wrapper" runat="server"
            style="margin: 0 auto; width: 595px; height: auto; border: 1px solid black">
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <%--<img alt="" src="/images/action/back.png"  onclick="goBack()" />--%>
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
            window.location = '/Forms/StudentContactList.aspx';
        }
    </script>
</body>
</html>
