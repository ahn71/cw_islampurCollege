<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdmCollectionReport.aspx.cs" Inherits="DS.Report.AdmCollectionReport" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>Fee Collection Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="width: 595.28px; height: 841.89px; margin: 0 auto">
            <div style="height: 32px">
                <div style="width: 80px; float: left">
                    <%--<asp:Image ID="Image1" ImageUrl="/Images/home/horihopara-logo-color-80.png" runat="server" />--%>
                </div>
                <div style="text-align: center">
                    <h3><asp:Label ID="lblSchoolName" runat="server"></asp:Label></h3>
                    <%--<p>ENAYETNAGAR, NARAYANGANJ.</p>--%>
                </div>
            </div>
            <hr />
            <p style="text-align: center">
                Money Receipt -<asp:Label runat="server" ID="lblYear"></asp:Label>
            </p>
            <hr />
            <asp:Label runat="server" ID="lblStName" ClientIDMode="Static"></asp:Label> <p style="float:right;margin-top:0px;border:1px solid">
                <asp:Label runat="server" ID="lblTransactionNo" ClientIDMode="Static"></asp:Label>
            </p><br />
            <asp:Label runat="server" ID="lblStRoll" ClientIDMode="Static"></asp:Label><br />
            <asp:Label runat="server" ID="lblClass" ClientIDMode="Static"></asp:Label>
            <p style="height: 23px">
                <asp:Label runat="server" ID="lblCategory" ClientIDMode="Static"></asp:Label>
            </p>
            <div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="divFeesCollection" class="datatables_wrapper" runat="server"
                            style="border: 1px solid gray; width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>           
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <script type="text/javascript">
            function printCall() {
                document.getElementById('divButton').style.display = 'none';
                window.print();
                document.getElementById('divButton').style.display = '';

            }
            function goBack() {
                window.location = '/Forms/FeesCollection.aspx';
            }
        </script>
    </form>
</body>
</html>
