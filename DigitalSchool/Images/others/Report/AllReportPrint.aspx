<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllReportPrint.aspx.cs" Inherits="DS.Report.AllReportPrint" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <link href="/Styles/dataTables.css" rel="stylesheet" />
    <title>Reports</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scriptmanager"></asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="reportHeader">
                        <h3>HARIHAR PARA HIGH SCHOOL</h3>
                        <asp:Label runat="server" ID="lblYear"></asp:Label></div>
                    <div id="employeeList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div runat="server" id="divAllReport" style="width: 77%; text-align: center; margin: 0 auto">
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/back.png" onclick="goBack();" />
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
            window.location = '/Default.aspx';
        }
    </script>
</body>
</html>
