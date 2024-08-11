<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademicTranscriptPrint.aspx.cs" Inherits="DS.Report.AcademicTranscriptPrint" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <link href="/Styles/reports/CommonBorder.css" rel="stylesheet" />
    <link href="/Styles/dataTables.css" rel="stylesheet" />
    <link href="/Styles/feeCollection.css" rel="stylesheet" />
    <link href="/Styles/gridviewat.css" rel="stylesheet" />
    <style type="text/css" media="print">
        /*@page{
            size: auto A4 landscape;
            margin: 4mm;
        }*/

        #divButton{
            display:none;
        }

        body 
        {
            background-color:#FFFFFF; 
            border: solid 1px black ;
            margin: 0px;  /* the margin on the content before printing */
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divButton" style="position: fixed; padding: 5px; top: 0; right: 0; margin-top: 5px;">
            <img alt="" src="/images/action/print.png" onclick="printCall()" />
        </div>
        <div runat="server" id="divAcademicTranscript" class="datatables_wrappe" style="width: 595px; margin: 100px  auto; border: 1px solid gray"></div>
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
       <div id="div_print">
      <div id="header" style="background-color:White;"></div>
   <div id="footer" style="background-color:White;"></div>
      </div>
</body>
</html>
