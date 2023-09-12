<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CollectionDetailsReport.aspx.cs" Inherits="DS.Report.CollectionDetailsReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Collection Details Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 595.28px; height: 841.89px; margin: 0 auto">
            <div style="height: 32px">
                <%--<div style="width: 80px; float: left">
                    <asp:Image ID="Image1" ImageUrl="/Images/home/horihopara-logo-color-80.png" runat="server" /></div>--%>
                <div style="text-align: center">
                    <h3 style="margin-bottom: -14px;">DIGITAL SCHOOL</h3>
                    <%--<p>ENAYETNAGAR, NARAYANGANJ.</p>--%>
                </div>
            </div>
            <hr />
            <p style="text-align: center; margin: 0px;">
                <asp:Label runat="server" ID="lblYear"></asp:Label></p>
            <p style="text-align: center; margin: 0px">
                <asp:Label runat="server" ID="lblClass" ClientIDMode="Static"></asp:Label></p>
            <p style="text-align: center; margin: 0px">
                <asp:Label runat="server" ID="lblFromDate" ClientIDMode="Static"></asp:Label>
                <asp:Label runat="server" ID="lblToDate" ClientIDMode="Static"></asp:Label>
            </p>
            <hr />
            <div id="divCollectionDetails" runat="server" class="datatables_wrapper" 
                style="border: 1px solid gray; width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
            </div>
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <%--<img alt="" src="/images/action/back.png"  onclick="goBack()" />--%>
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <script type="text/javascript">
            function printCall() {
                document.getElementById('divButton').style.display = 'none';
                window.print();
                document.getElementById('divButton').style.display = '';

            }
            function goBack() {
                window.location = '/Forms/CollectionDetails.aspx?back=' + "ftd";
            }
        </script>
    </form>
</body>
</html>
