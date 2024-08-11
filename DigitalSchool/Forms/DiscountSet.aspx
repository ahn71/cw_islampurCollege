<%@ Page Title="Discount Set" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiscountSet.aspx.cs" Inherits="DS.Forms.DiscountSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel{
            width:570px;
        }
        .controlLength{
            width:150px;
        }
        hr{
            margin:0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblid" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Discount Set</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlBatchName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                            <asp:AsyncPostBackTrigger ControlID="ddlRoll" />
                            <asp:AsyncPostBackTrigger ControlID="ddlParticulars" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel-Discount">
                                <tr>
                                    <td>
                                        Batch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatchName" ClientIDMode="Static" CssClass="input controlLength" runat="server"
                                            AutoPostBack="True" OnSelectedIndexChanged="ddlBatchName_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        Section
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlSection" ClientIDMode="Static" runat="server" CssClass="input controlLength" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        Shift
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" AutoPostBack="True" CssClass="input controlLength" ClientIDMode="Static"
                                             OnSelectedIndexChanged="dlShift_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">Morning</asp:ListItem>
                                            <asp:ListItem>Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Roll
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRoll" runat="server" AutoPostBack="True" CssClass="input controlLength" ClientIDMode="Static" 
                                             OnSelectedIndexChanged="ddlRoll_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        Particulars
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlParticulars" ClientIDMode="Static" runat="server" CssClass="input controlLength" AutoPostBack="True" 
                                            OnSelectedIndexChanged="ddlParticulars_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Discount (%)
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDiscountAmount" ClientIDMode="Static" CssClass="input controlLength" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" 
                                            OnClientClick="return validateInputs();" OnClick="btnAdd_Click" Text="Add"/>
                                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" 
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" Text="Save" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="btn btn-default" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divscroll" style="height: 200px; max-height: 200px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvDiscount" runat="server" AutoGenerateColumns="false"                                    
                                    ClientIDMode="Static" CssClass="table table-striped table-bordered tbl-controlPanel"
                                    OnRowCommand="gvDiscount_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="Remove" Text="Remove" ButtonType="Button" />
                                        <asp:BoundField DataField="PId" HeaderText="PId" Visible="false" />
                                        <asp:BoundField DataField="PName" HeaderText="Particulars Name" />
                                        <asp:BoundField DataField="Discount" HeaderText="Discount (%)" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("PId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>                                    
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Discount List</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                            <asp:AsyncPostBackTrigger ControlID="ddlBatchlist" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSectionlist" />
                            <asp:AsyncPostBackTrigger ControlID="ddlRolllist" />
                            <asp:AsyncPostBackTrigger ControlID="ddlBatchName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <div>
                                <table class="tbl-controlPanel">
                                    <tr>
                                        <td>Batch
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBatchlist" ClientIDMode="Static" runat="server" CssClass="input controlLength"
                                                OnSelectedIndexChanged="ddlBatchlist_SelectedIndexChanged" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                        <td>Section
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSectionlist" ClientIDMode="Static" runat="server" CssClass="input controlLength"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSectionlist_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>Roll
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRolllist" ClientIDMode="Static" runat="server" CssClass="input controlLength">
                                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td colspan="3">
                                            <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success"
                                                runat="server" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnEdit" Text="Edit" ClientIDMode="Static" CssClass="btn btn-primary"
                                                runat="server" OnClick="btnEdit_Click" />
                                            <asp:Button ID="btnRefresh" Text="Refresh" ClientIDMode="Static" CssClass="btn btn-default"
                                                runat="server" OnClick="btnRefresh_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="tgPanelHead">Searching Result</div>
                            <hr />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlBatchName" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                                    <asp:AsyncPostBackTrigger ControlID="btnRefresh" />
                                </Triggers>
                                <ContentTemplate>
                                    <div id="divDiscountList" class="datatables_wrapper" runat="server" 
                                        style="width: 100%; min-height: 100px; overflow: auto; overflow-x: hidden; height:150px;"></div>
                                    <div id="divDiscount" class="datatables_wrapper" runat="server" 
                                        style="width: 100%; min-height: 100px; overflow: auto; overflow-x: hidden; height:120px;"></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>          
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="../Scripts/jx.js"></script>
    <script type="text/javascript">
        document.getElementById('#divscroll').scrollTop = 0;
        function validateInputs() {
            if (validateText('ddlBatchName', 1, 20, 'Select Batch') == false) return false;
            else if (validateText('ddlSection', 1, 20, 'Select Section') == false) return false;
            else if (validateText('ddlRoll', 1, 30, 'Select Roll') == false) return false;
            else if (validateText('ddlParticulars', 1, 80, 'Select Particulars') == false) return false;
            return true;
        }
        function editDiscount(id) {
            $('#lblid').val(id);
            var strParticularsName = $('#r_' + id + ' td:first-child').html();
            $('#ddlParticulars').val(strParticularsName);
            var strD = $('#r_' + id + ' td:nth-child(2)').html();
            $('#txtDiscountAmount').val(strD);
            var strBatch = $('#ddlBatchlist').val();
            $('#ddlBatchName').val(strBatch);
            var strSection = $('#ddlSectionlist').val();
            $('#ddlSection').val(strSection);
            var strRoll = $('#ddlRolllist').val();
            $('#ddlRoll').val(strRoll);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#txtDiscountAmount').val('');
            $("#btnSave").val('Save');
        }
        function loadDiscount(id) {
            var strRoll = $('#roll' + id).html();
            var strSection = $('#section' + id).html();
            var strBatch = $('#ddlBatchName').val();
            jx.load('/ajax.aspx?id=' + id + '&todo=Discount' + '&Roll=' + strRoll + '&SectionName=' + strSection + '&BatchName=' + strBatch + '', function (data) {
                //var divd = $('#MainContent_divDiscount').html();
                alert(divd);
                $('#MainContent_divDiscount').html(data);
            });
        }
    </script>
</asp:Content>

