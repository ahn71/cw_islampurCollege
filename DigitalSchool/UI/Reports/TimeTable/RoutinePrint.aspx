<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutinePrint.aspx.cs" Inherits="DS.Report.RoutinePrint" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Class Routine</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <style type="text/css">
.tg {
  border-collapse: collapse;
  border-spacing: 0;
  margin: 0 auto;
  width: 100%;
}
.tg td {
  border-style: solid;
  border-width: 1px;
  font-family: Arial,sans-serif;
  font-size:8px;
  font-weight: bold;
  line-height: 12px;
  overflow: hidden;
  padding: 2px 3px;
  word-break: normal;
}
.tg th {
  border-style: solid;
  border-width: 1px;
  font-family: Arial,sans-serif;
  font-size: 13px;
  font-weight: bold;
  overflow: hidden;
  padding: 2px 5px;
  word-break: normal;
}
.time > td {
  font-size: 8px;
  padding: 2px 23px;
}
.tg .tg-baqh{text-align:center;vertical-align:top}
.tg .tg-yw4l{vertical-align:top}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div class="reportHeader" style="height: 72px!important">
            <h3 runat="server" id="hSchoolName"></h3>
            <a style="font-size:14px" runat="server" id="aAddress"></a>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <asp:Label runat="server" ID="lblShit"></asp:Label>
            <p runat="server" id="pTitle" style="margin: 0px; font-size: 16px; font-weight: bold;">Teacher's Class Routine</p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>         <div id="divClassRoutine" style="width:100%;margin:0px auto" runat="server"></div> 
            
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
