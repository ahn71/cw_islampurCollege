<%@ Page Title="Class Routine Print" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ClassRoutine.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ClassRoutine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;             
        }
        .controlLength{
            width: 200px;
        }
        .tbl-controlPanel{
            width:100%;
        }
        .tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(3){
            text-align:center;
            padding-right: 5px;
        }
        .DivStyle{
            padding: 10px;
        }
        .drg-event-title{
            border-bottom: none;
            font-weight: normal;
            padding-bottom: 0;
            margin-top: 5px;
            margin-bottom: 5px;
        }
        #buildings{
            margin: 5px auto;
        }
        .table-defination {
            width: 100%
        }
        .dropped {
            width: 150px;
            text-align: center;
        }
        ul, ol {
            margin-bottom:0px;
        }
        .table-defination th,
        .table-defination td:first-child {
            background-color: #f6eded;
        }
        .table-defination td div.dropped{
            background-color: #e2dddd;
            padding:10px;
        }
        .table-defination td div.dropped ul{
            list-style: none;
        }
        .table-defination td div.dropped ul li{           
            margin-bottom: 8px;
        }
        .table-defination td div.dropped ul li:last-child{           
            margin-bottom: 0px;
        }
        .table-defination td div.dropped ul li span a {
            margin-left: 5px;
            color: white;
        }
        ul, .list-unstyled {
            padding-left: 0px;
        }
        .table-defination th.rotate {
          /* Something you can count on */
          height: 100%;
          white-space: nowrap;          
        }
        .table-defination th.rotate > div {
          transform: 
            /* Magic Numbers */
            translate(2px, 0px)
            /* 45 is really 360 - 45 */
            rotate(-90deg);
          width: 65px;        
        } 
        .table > thead > tr > th, 
        .table > tbody > tr > th, 
        .table > tfoot > tr > th{
            padding : 0px;
            vertical-align : middle;
        }  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tbl-controlPanel">
        <tr>
            <asp:Button ID="btnPrint" runat="server" Text="Print" ClientIDMode="Static" CssClass="btn btn-warning"
                OnClientClick="printCall();" />
        </tr>
    </table>
    <div>
        <asp:Panel ID="DayTimePanel" runat="server" CssClass="" 
            BorderStyle="Double" BorderColor="LightGray" BorderWidth="2px" ClientIDMode="Static" ScrollBars="Auto">
            <table class="tbl-controlPanel">
                <tr>
                    <td>
                        <asp:Label ID="lblSchoolName" Style="text-align: center" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Digital High School"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBatch" Font-Size="Large" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblShift" Font-Size="Large" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <div class="clearfix"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        function printCall() {
            var restorepage = document.body.innerHTML;
            var printcontent = document.getElementById('DayTimePanel').innerHTML;
            document.body.innerHTML = printcontent;
            window.print();
            document.body.innerHTML = restorepage;                
        }
    </script>
</asp:Content>
