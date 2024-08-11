<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarksSheet.aspx.cs" Inherits="DS.Report.MarksSheet" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>Marks Sheet</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <link href="/Styles/dataTables.css" rel="stylesheet" />
    <link href="../AssetsNew/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../AssetsNew/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; left: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div class="reportHeader" style="width: 960px; margin:10px auto;">
            <h3><asp:Label ID="lblSchoolName" runat="server" Text="Label"></asp:Label></h3>
            <div style="width: 100%; margin: 2px auto">
                <asp:Label ID="lblExamName" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <asp:Label ID="lblShift" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                <asp:Label ID="lblBatch" runat="server" Text="Label" Font-Bold="True" Font-Size="15px"></asp:Label><br />               
                <div style="border: 1px solid black;"  class='datatables_wrapper' runat="server" id="divProgressReport"></div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#divProgressReport').children().attr('disabled', true)
        //});
        function printCall() {
            document.getElementById('divButton').style.display = 'none';
            window.print();
            document.getElementById('divButton').style.display = '';
        }       
    </script>
</body>
</html>
