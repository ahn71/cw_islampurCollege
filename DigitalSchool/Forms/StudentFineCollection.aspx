<%@ Page Title="Student Fine Collection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentFineCollection.aspx.cs" Inherits="DS.Forms.StudentFineCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength{
            width:150px;
            margin: 5px;
        }
        .tbl-controlPanel{
            width: 600px;            
        }
        .grandTotal{
            float:right; 
            width: 100%; 
            height: 28px; 
            padding-top: 4px; 
            padding-right: 40px; 
            font-size: 18px; 
            text-align: right;
            margin-top: 5px;
            color: #ff0000;
        }
        .head{
            border:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Fine Collection Panel</div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>
                                Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="dlBatch" AutoPostBack="true" runat="server" class="input controlLength">
                                    <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                Section
                            </td>
                            <td>
                                <asp:DropDownList ID="dlSection" runat="server" class="input controlLength"></asp:DropDownList></td>
                            <td>
                                Shift
                            </td>
                            <td>
                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" class="input controlLength"
                                     AutoPostBack="false">
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                Roll
                            </td>
                            <td>
                                <asp:UpdatePanel runat="server" ID="upRoll" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnPayNow" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtRoll" AutoComplete="off" runat="server" class="input controlLength"
                                            ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success" 
                                    OnClick="btnSearch_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>                            
                        </tr>
                        <tr>                            
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="upPanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>
                                <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentName" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Button runat="server" OnClick="btnPayNow_Click" ID="btnPayNow" 
                                    OnClientClick="makePayment()" Visible="false" ClientIDMode="Static" 
                                    Text="Pay Now" CssClass="btn btn-primary" />
                            </td>
                        </tr>                        
                    </table>                    
                </ContentTemplate>
            </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div id="divFineInfo" class="datatables_wrapper" runat="server" 
                        style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;"></div>
                    <div id="divGrandTotal" class="head grandTotal" 
                        style=" "></div>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function makePayment() {
            $('#tblFine tbody tr td:nth-child(3) input[type=checkbox]').each(function () {
                if (this.checked) {
                    var id = this.id;
                    var famount = this.value;
                    jx.load('/ajax.aspx?id=' + id + '&todo=putFineDate' + '&amount= ' + famount + '', function (data) {
                    });
                }
                else {
                    // alert('no selected');
                }
            });
        }
        function checkFine(checkbox) {
            var id = checkbox.id;
            var famount = checkbox.value;
            var status = checkbox.checked;
            jx.load('/ajax.aspx?id=' + id + '&todo=fineCollection' + '&amount= ' + famount + '&status=' + status + ' ', function (data) {
                $('#divGrandTotal').html('Grand Total : ' + data);
            });
        }
    </script>
</asp:Content>
