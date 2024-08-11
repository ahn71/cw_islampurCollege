<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherWeeklyLoad.aspx.cs" Inherits="DS.Report.TeacherWeeklyLoad" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Teacher Weekly Load</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
    <link href="/Styles/gridview.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="reportHeader">
            <h3>HARIHAR PARA HIGH SCHOOL
                <br />
                Teacher Weekly Load -
                <asp:Label ID="lblYear" runat="server"></asp:Label>
            </h3>
        </div>
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div id="divTeacherWeeklyLoad" runat="server"></div>
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
