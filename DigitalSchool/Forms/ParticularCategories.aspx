<%@ Page Title=" Particular Categorie" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ParticularCategories.aspx.cs" Inherits="DS.Forms.ParticularCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 900px;
        }  
        .input{
            width: 200px;
        }   
        .tbl-controlPanel{
           width:500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblParticularCatId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlCategory" />
                        <asp:AsyncPostBackTrigger ControlID="dlParticular" />
                        <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Assign Particulars </div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>
                                        Fee Category
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlCategory" ClientIDMode="Static" CssClass="input" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Particular
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlParticular" ClientIDMode="Static" CssClass="input" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="btn btn-success" OnClick="btnAdd_Click" ClientIDMode="Static" />
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" 
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <asp:Button CssClass="btn btn-default" Text="Clear" runat="server" ID="btnClear" OnClick="btnClear_Click" />
                                    </td>                                    
                                </tr>
                            </table>                            
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">
                    <asp:UpdatePanel runat="server" ID="updatepanel3" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divscroll" style="min-height: 100px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvParticulars" runat="server" AutoGenerateColumns="false" 
                                    CssClass="table table-striped table-bordered tbl-controlPanel"
                                    ClientIDMode="Static" 
                                    OnRowCommand="gvParticulars_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="Remove" Text="Remove" ButtonType="Button"/>                               
                                        <asp:BoundField DataField="PId" HeaderText="PId" Visible="false" />
                                        <asp:BoundField DataField="PName" HeaderText="Particulars" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" />
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
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Filter By Category</div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlFilter" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlFilter" CssClass="input" AutoPostBack="True" 
                                            OnSelectedIndexChanged="dlFilter_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" 
                                            OnClick="btnEdit_Click" />
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="dlFilter" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divParticularCategoryList" class="datatables_wrapper" runat="server" 
                                style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("change", '#dlFilter', function () {
                searchTable($(this).val(), 'tblParticularCategory', '');
            });
        });
        function validateInputs() {
            // if (validateText('txtAmount', 1, 20, 'Type Amount') == false) return false;
            //  else if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            return true;
        }
        function editParticularCat(id) {

            var splitedData = "ParticularsLoad";
            jx.load('/ajax.aspx?id=' + id + '&todo=plist', function (data) {
                $('#tblParticularData').html(data);
            });
        }
        function delParticular(id, val) {
            jx.load('/ajax.aspx?id=' + id + '&val=' + val + '&todo=delp', function (data) {
                if (data == "ok") {
                    $('#r_' + id).remove();
                }
                else {
                    alert('Process Failed. Please Try Again.');
                }
            });
        }
        function addParticular() {
            var feeCategory = $('#dlCategory').val();
            var particular = $('#dlParticular').val();
            var amount = $('#txtAmount').val();
            jx.load('/ajax.aspx?feeCategory=' + feeCategory + '&particular=' + particular + '&amount=' + amount + '&todo=addp', function (data) {
                if (data != "" && data != "ExitsData") {
                    var tbl = '<tr id="r_' + data + '">';
                    tbl += '<td>' + particular + '</td>';
                    tbl += '<td class=numeric>' + amount + '</td>';
                    tbl += '<td style="max-width: 20px;" class="numeric control"><img src="/Images/action/delete.png" onclick="delParticular(' + data + ',' + amount + ');"></td>';
                    tbl += '</tr>';
                    $('#tblParticularData tbody').prepend(tbl);
                }
                else if (data == "ExitsData") {
                    alert('Alrady exist');
                }
                else {
                    alert('Process Failed. Please Try Again.');
                }
            });
            jx.load('/ajax.aspx?feeCategory=' + feeCategory + '&todo=getAmountTotal', function (data) {
                $('#fc' + feeCategory).html(data);
            });
        }
        function clearIt() {
            $('#lblParticularCatId').val('');
            $('#txtAmount').val('');
            setFocus('txtAmount');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
