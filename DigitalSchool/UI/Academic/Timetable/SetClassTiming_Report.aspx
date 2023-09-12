<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="SetClassTiming_Report.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetClassTiming_Report" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Class Routine</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <style>
      
        .table-bordered {
            border:1px solid gray;
            margin:0px auto;
        }
        .droppable{
            border:1px solid red;
        }
           .table-defination th,
        .table-defination td:first-child {
           /*background-color: #f6eded;*/
            border:1px solid gray;
        }
           .table thead > tr > th, .table tbody > tr > th, .table tfoot > tr > th, .table thead > tr > td, .table tbody > tr > td, .table tfoot > tr > td {
     border:1px solid gray;
    
}
        /*.dropped {
            border:1px solid Gray;
        }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div class="reportHeader" style="height: 102px!important">
            <h3 runat="server" id="hSchoolName"></h3>
            <a style="font-size:14px" runat="server" id="aAddress"></a>
            <h3 style="margin: 0px">
                <asp:Label runat="server" ID="lblBatch"></asp:Label>
            </h3>
            <asp:Label runat="server" ID="lblShit"></asp:Label>
            <p runat="server" id="pTitle" style="margin: 0px; font-size: 16px; font-weight: bold;">Class Routine</p>
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>         <div id="divClassRoutine" style="width:1200px;margin:0px auto; font-family:'Times New Roman';font-size:small" runat="server"></div> 
            
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