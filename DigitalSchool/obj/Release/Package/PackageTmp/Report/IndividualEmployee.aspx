<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualEmployee.aspx.cs" Inherits="DigitalSchool.Report.IndividualEmployee" %>

<!DOCTYPE html>


<html >
<head id="Head1" runat="server">
    <title>Employee Report</title>
     <link href="/Styles/dataTables.css" rel="stylesheet" />
</head>
<body>

<form id="form1" runat="server">
    
<asp:ScriptManager runat="server" ID="scriptmanager"></asp:ScriptManager>



<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="reportHeader"><h3>HARIHAR PARA HIGH SCHOOL</h3>EMPLOYEE INFORMATION-<asp:Label runat="server" ID="lblYear"></asp:Label></div>
        <div id="IndividualEmployeeReport" class="datatables_wrapper"  runat="server" style=" width:100%; height:auto"></div>
    </ContentTemplate>
</asp:UpdatePanel>



<div id="divButton" style="bottom: 0px; height: auto; width: 230px; position:fixed; padding: 5px; text-align: center; background-color: whitesmoke; border: 1px solid green; margin-left: 50px; float: left; left: 0px; margin-right: 40px; margin-top: 5px;">

    <img alt="" src="/images/action/close.png" onclick="window.parent.close();" />
    <img alt="" src="/images/action/print.png"  onclick="printCall()"  />

</div>

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