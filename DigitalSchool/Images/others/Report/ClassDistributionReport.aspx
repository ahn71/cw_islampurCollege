<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassDistributionReport.aspx.cs" Inherits="DS.Report.ClassDistributionReport" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Class Distribution Report</title>
    <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div id="divButton" style="position:fixed; padding: 5px;  top:0; right:0; margin-top: 5px;">
    <img alt="" src="/images/action/print.png"  onclick="printCall()"  />
</div>
<div class="reportHeader" style="height:98px;">
    <h3>HARIHAR PARA HIGH SCHOOL <br/> 
        Teacher class schedule 
        <asp:Label ID="lblYear" runat="server"></asp:Label>
    </h3> 
    <h3 style="margin:0px">
        <asp:Label runat="server" ID="lblTeacherName"></asp:Label>
        <br /> 
        <asp:Label runat="server" ID="lblShift"></asp:Label>
    </h3>
</div>    
<div class="clasDistribution">
    <div id="divClassInfo" runat="server" class="display" style="border:1px solid gray; font-family:monospace"></div>
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
