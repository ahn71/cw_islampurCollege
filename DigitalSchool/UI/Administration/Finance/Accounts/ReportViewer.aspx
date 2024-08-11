<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.ReportViewer" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Report</title>
    <link href="/Styles/gridview.css" rel="stylesheet" />
    <style>
        .gvpring {
            min-width: 600px;
           
        }

            .gvpring th {
                text-align: center;
            }

            .gvpring td {
                text-align: center;
                height:25px;
            }
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
            <p runat="server" id="pTitle" style="margin: 0px; font-size: 16px; font-weight: bold;"></p>
            <p runat="server" id="pTitle2" style="margin: 0px; font-size: 16px; font-weight: bold;"></p>

            <asp:Label ID="lblYear" runat="server"></asp:Label>
        </div>         <div id="divClassRoutine" style="width:862px;margin:0px auto" runat="server">
           <center>
               <asp:GridView ID="GridView1" CssClass="gvpring" runat="server"></asp:GridView>
           </center> 
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
