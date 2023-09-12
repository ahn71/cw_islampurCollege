<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualStudentReport.aspx.cs" Inherits="DS.Report.IndivisualStudentReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Student Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptmanager"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="reportHeader">
                    <h3>HARIHAR PARA HIGH SCHOOL</h3>
                    STUDENT INFORMATION-<asp:Label runat="server" ID="lblYear"></asp:Label>
                </div>
                <div id="IndividualStudentReport" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/back.png" onclick="goBack()" />
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
    </form>
    <script type="text/javascript">
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            window.close();
            document.getElementById('divButton').style.display = '';
        }
        function goBack() {
            window.location = '/Forms/AdmissionDetails.aspx';
        }
    </script>
</body>
</html>
