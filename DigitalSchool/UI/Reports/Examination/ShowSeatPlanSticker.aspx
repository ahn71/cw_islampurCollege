<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowSeatPlanSticker.aspx.cs" Inherits="DS.UI.Reports.Examination.ShowSeatPlanSticker" %>

<!DOCTYPE html>

<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Seat Plan Sticker</title>
    
</head>
<body>

    <a href="" onclick="printDiv()">Print</a>
    <main style="width: 795px;" id="printDiv">
    
        <div runat="server" id="divShow">
       
        </div>
    </main>
    <script>
        function printDiv() {
            var divContents = document.getElementById("printDiv").innerHTML;
            var a = window.open('', '', 'height=500, width=500');
            a.document.write('<html>');
            a.document.write('<body >');
            a.document.write(divContents);
            a.document.write('</body></html>');
            a.document.close();
            a.print();
        }
    </script>
</body>
</html>
