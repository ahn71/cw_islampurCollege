<%@ Page Title="Fee Collection" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeesCollection.aspx.cs" Inherits="DS.Forms.FeesCollection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }

        .tgPanel {
        }

        .controlLength {
            width: 150px;
        }

        .tbl-controlPanel {
            width: 700px;
        }

        .grandTotal {
            float: right;
            width: 100%;
            height: 28px;
            padding-top: 4px;
            padding-right: 40px;
            font-size: 18px;
            text-align: right;
            margin-top: 5px;
            color: #ff0000;
        }

        .head {
            border: none;
        }

        #tbl tr td:nth-child(2) {
            width: 114px;
        }

        #tbl tr td:nth-child(1) {
            width: 114px;
        }

        #tbl tr td:nth-child(3) {
            padding: 5px;
        }

        .auto-style1 {
            width: 965px;
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
                <div class="tgPanelHead">Fee Collection Panel</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Batch</td>
                        <td>
                            <asp:DropDownList ID="dlBatch" AutoPostBack="true" CssClass="input controlLength"
                                OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" runat="server">
                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>Section</td>
                        <td>
                            <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength">
                            </asp:DropDownList>
                        </td>
                        <td>Shift</td>
                        <td>
                            <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                AutoPostBack="false">
                                <asp:ListItem>Morning</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Fee Category </td>
                        <td>
                            <asp:DropDownList ID="dlFeesCategory" runat="server"
                                CssClass="input controlLength">
                            </asp:DropDownList>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary"
                                ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="upPanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <table class="tbl-controlPanel" style="width: 350px;">
                            <tr>
                                <td>Roll</td>
                                <td>
                                    <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static" AutoPostBack="false" CssClass="input controlLength"
                                        OnTextChanged="txtRoll_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSearchByRoll" Text="Search" ClientIDMode="Static"
                                        OnClick="btnSearchByRoll_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success"
                                        OnClientClick="makePayment()" ClientIDMode="Static" Text="Pay Now" OnClick="btnSave_Click" />
                                </td>

                            </tr>
                        </table>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" ClientIDMode="Static" ID="lblFineNote" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div id="divPrint" style="margin-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearchByRoll" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divParticularCategoryList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearchByRoll" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divFineInfo" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                        </div>
                        <div id="divGrandTotal" class="head grandTotal">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function CallPrint() {
            var prtContent = document.getElementById('divPrint');
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1024,height=680,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function validateInputs() {
            try {
                var rowCount = $('#tblParticularCategory tr').length;

                console.log('validating inputs');
                if (validateText('txtRoll', 1, 20, 'Enter a roll number') == false) return false;

                if (rowCount < 1) {
                    return false;
                }
                else {
                    goURL('/Report/FeeCollectionReport.aspx');
                }
            }
            catch (e) { };
        }
        function makePayment() {
            $('#tblFine tbody tr td:nth-child(3) input[type=checkbox]').each(function () {
                if (this.checked) {
                    var id = this.id;
                    var famount = this.value;
                    jx.load('/ajax.aspx?id=' + id + '&todo=putDate' + '&amount= ' + famount + '', function (data) {
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
            jx.load('/ajax.aspx?id=' + id + '&todo=storeFineInfo' + '&amount= ' + famount + '&status=' + status + ' ', function (data) {
                $('#divGrandTotal').html('Grand Total : ' + data);
            });
        }
    </script>
</asp:Content>
