<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamRoutinePrint.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ExamRoutinePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
         /**/
        .gvExamSchedule td:nth-child(2), td:nth-child(3),td:nth-child(4)
        , td:nth-child(5), td:nth-child(6), td:nth-child(7){
            width:250px;
            color:black;
        }
        .gvExamSchedule td:first-child {
            width:100px;
        }
         /**/
        .controlLength{
            width: 200px;
        }
       .tbl-controlPanel1{
            width: 670px;            
            font-family: Calibri;
        font-size: 15px;
       margin: 10px auto;
       padding: 5px;
        }
         .table tr th:first-child, tr td:first-child, tr th:nth-child(1), tr td:nth-child(1),tr th:nth-child(2), tr td:nth-child(2), tr th:nth-child(3),tr td:nth-child(3), tr th:nth-child(4),tr td:nth-child(4),tr th:nth-child(5),tr td:nth-child(5), tr th:nth-child(6),tr td:nth-child(6), tr th:nth-child(7),tr td:nth-child(7),tr th:nth-child(8),tr td:nth-child(8) {
            text-align: center;           
        }
    </style>
    <title>Class Routine</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
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
            <p runat="server" id="pExamTitle" style="margin: 0px; font-size: 16px; font-weight: bold;"></p>
            <p runat="server" id="pBatch" style="margin: 0px; font-size: 14px; font-weight: bold;"></p>
             <p runat="server" id="pTitle" style="margin: 0px; font-size: 16px; font-weight: bold;">Exam Schedule</p><br />
            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>         <div id="divClassRoutine" style="width:862px;margin:0px auto" runat="server"></div> 
            
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
