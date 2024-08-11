<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentFinalResultReport.aspx.cs" Inherits="DS.Report.StudentFinalResultReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Student Final Result</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div id="divFinalResult" runat="server" class="datatables_wrapper"></div>
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
