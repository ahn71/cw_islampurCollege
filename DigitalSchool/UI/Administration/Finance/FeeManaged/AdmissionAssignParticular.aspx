<%@ Page Title="Admission Fees Particular Category" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmissionAssignParticular.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.AdmissionAssignParticular" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }  
        .input{
            /*width: 300px;*/
        }   
        .tbl-controlPanel{
           /*width:760px;*/          
        }
        /*.tbl-controlPanel td{
           width:66%;          
        }*/
        .tbl-controlPanel td:first-child{
            width:25%;       
            text-align: right;  
            padding-right: 5px;      
        } 
        .table tr th {
            background-color: #23282C;
            color: white;
        }

            .table tr th:nth-child(3) {
                text-align: center;
            }

        .table tr td:nth-child(2) {
            text-align: left;
        }   
           @media only screen and (min-width: 320px) and (max-width: 479px) {

            .btn-danger {
               margin-top:10px;
                }
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
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Admission Fees Particular Category</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                    <asp:AsyncPostBackTrigger ControlID="dlCategory" />
                    <asp:AsyncPostBackTrigger ControlID="dlParticular" />
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    <asp:AsyncPostBackTrigger ControlID="btnClear" />             
                    <asp:AsyncPostBackTrigger ControlID="btnAddParticular" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddFeesCat" />
                    <asp:AsyncPostBackTrigger ControlID="btnFeeCatSave" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddParticularSave" />
                </Triggers>
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Assign Admission Particulars </div>
                            <div class="row tbl-controlPanel">
	                            <div class="col-sm-6 col-sm-offset-3">
		                            <div class="form-group row">
			                            <label class="col-sm-3">Class Name</label>
			                            <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlClass" runat="server" CssClass="input form-control" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                            </asp:DropDownList>
			                            </div>
                                        <div class="col-sm-3"></div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-3">Admission Fees Category</label>
			                            <div class="col-sm-6">
                                            <asp:DropDownList ID="dlCategory" ClientIDMode="Static" CssClass="input form-control" runat="server">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                        
			                            </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnAddFeesCat" runat="server" Text="Add Category" CssClass="btn btn-danger"
                                            OnClientClick="return validationInput1();"
                                            OnClick="btnAddFeesCat_Click" />
                                        </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-3">Particular</label>
			                            <div class="col-sm-6">
                                            <asp:DropDownList ID="dlParticular" ClientIDMode="Static" CssClass="input form-control" runat="server">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                        
			                            </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnAddParticular" runat="server" Text="Add Particular" CssClass="btn btn-danger"
                                            OnClick="btnAddParticular_Click" />
                                        </div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-3">Amount</label>
			                            <div class="col-sm-6">
                                            <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" CssClass="input form-control" Text="0"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" ValidChars="."
                                            FilterType="Numbers,Custom">
                                        </asp:FilteredTextBoxExtender>
			                            </div>
                                        <div class="col-sm-3"></div>
		                            </div>
                                    <div class="form-group row">
			                            <label class="col-sm-3"></label>
			                            <div class="col-sm-9">
                                            <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="btn btn-success" OnClientClick="return validateInputs1();"
                                            OnClick="btnAdd_Click" ClientIDMode="Static" />
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="" OnClick="btnSave_Click" />
                                        <asp:Button CssClass="btn btn-default" Text="Clear" runat="server" ID="btnClear" OnClick="btnClear_Click" />
			                            </div>
		                            </div>
                                    
	                            </div>
                            </div>
                            <%--<table class="tbl-controlPanel">
                                <tr>
                                    <td>Class Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlClass" runat="server" CssClass="input" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Admission Fees Category
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlCategory" ClientIDMode="Static" CssClass="input" runat="server">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnAddFeesCat" runat="server" Text="Add Category" CssClass="btn btn-danger"
                                            OnClientClick="return validationInput1();"
                                            OnClick="btnAddFeesCat_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Particular
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlParticular" ClientIDMode="Static" CssClass="input" runat="server">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Button ID="btnAddParticular" runat="server" Text="Add Particular" CssClass="btn btn-danger"
                                            OnClick="btnAddParticular_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount" runat="server" ClientIDMode="Static" CssClass="input" Text="0"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" ValidChars="."
                                            FilterType="Numbers,Custom">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Button runat="server" ID="btnAdd" Text="Add" CssClass="btn btn-success" OnClientClick="return validateInputs1();"
                                            OnClick="btnAdd_Click" ClientIDMode="Static" />
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="" OnClick="btnSave_Click" />
                                        <asp:Button CssClass="btn btn-default" Text="Clear" runat="server" ID="btnClear" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                <Triggers> 
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />                  
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" />
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    <asp:AsyncPostBackTrigger ControlID="btnClear" />             
                </Triggers>
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="tgPanel">
                            <div class="row">
                                <div class="col-lg-3"></div>
                                <div class="col-lg-6">
                                    <div id="divscroll" style="min-height: 80px; overflow: auto; overflow-x: hidden;">
                                        <asp:GridView ID="gvParticulars" runat="server" AutoGenerateColumns="false"
                                            CssClass="table table-bordered"
                                            ClientIDMode="Static"
                                            OnRowCommand="gvParticulars_RowCommand">
                                            <Columns>
                                                <asp:ButtonField CommandName="Remove" Text="Remove" ButtonType="Button" />
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
                                </div>
                                <div class="col-lg-3"></div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="row">             
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Filter By Category</div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">  
                <asp:UpdatePanel runat="server" ID="updatepanel2" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlClassFilter" />                    
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />                   
                    <asp:AsyncPostBackTrigger ControlID="btnClear" />             
                    <asp:AsyncPostBackTrigger ControlID="btnAddParticular" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddFeesCat" />
                    <asp:AsyncPostBackTrigger ControlID="dlFilter" />
                </Triggers>
                <ContentTemplate>                 
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Class
                            </td>
                            <td>
                                <asp:DropDownList ID="dlClassFilter" Width="180px" runat="server" CssClass="input" ClientIDMode="Static"
                                  OnSelectedIndexChanged="dlClassFilter_SelectedIndexChanged"  AutoPostBack="true">
                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                             Fees Category
                            </td>
                            <td>
                                <asp:DropDownList runat="server" ID="dlFilter" Width="180px" CssClass="input" AutoPostBack="True" ClientIDMode="Static"
                                    OnSelectedIndexChanged="dlFilter_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" OnClientClick="return validateInputs3();"
                                    OnClick="btnEdit_Click" />
                            </td>
                        </tr>
                    </table> 
                     </ContentTemplate>
                 </asp:UpdatePanel>
                          <asp:UpdatePanel runat="server" ID="updatepanel3" UpdateMode="Conditional">
                <Triggers>                             
                    <asp:AsyncPostBackTrigger ControlID="btnClear" /> 
                    <asp:AsyncPostBackTrigger ControlID="dlFilter" />
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                      
                </Triggers>
                <ContentTemplate>                  
                    <div id="divParticularCategoryList" class="datatables_wrapper" runat="server"
                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                    </div>
                    </ContentTemplate>
                              </asp:UpdatePanel>

                    <!-- fee category modal -->
                    <asp:UpdatePanel runat="server" ID="updatepanel4" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dlClassFilter" />                    
                    <asp:AsyncPostBackTrigger ControlID="btnEdit" />                   
                    <asp:AsyncPostBackTrigger ControlID="btnClear" />             
                    <asp:AsyncPostBackTrigger ControlID="btnAddParticular" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddFeesCat" />
                </Triggers>
                <ContentTemplate>  
                    <asp:ModalPopupExtender ID="ShowFeeCatModal" runat="server" BehaviorID="modalpopup1" CancelControlID="Button2"
                        OkControlID="LinkButton1"
                        TargetControlID="button3" PopupControlID="showFeeCat" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showFeeCat" runat="server" style="display: none;" class="confirmationModal500">
                        <div class="modal-header">
                            <button id="Button2" type="button" class="close white"></button>
                            <div class="tgPanelHead">Fees Category</div>
                        </div>
                        <div class="modal-body">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Class Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlClassName" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                            Enabled="false">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fees Category
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesCatName" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of Start
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateStart" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" runat="server" CssClass="" TargetControlID="txtDateStart"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of End
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" runat="server" CssClass="" TargetControlID="txtDateEnd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr style="visibility:hidden">
                                    <td>Fine Amount
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesFine" runat="server" ClientIDMode="Static" Text="0" CssClass="input controlLength"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="F1" runat="server" TargetControlID="txtFeesFine" ValidChars="."
                                            FilterType="Numbers,Custom">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="button3" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                        <i class="icon-remove"></i>                                    
                                        Close
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnFeeCatSave" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                OnClientClick="return validateFeeCat();" OnClick="btnFeeCatSave_Click">
                                        <i class="icon-ok"></i>
                                        Save
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- END fee category modal -->

                    <!-- particular modal -->
                    <asp:ModalPopupExtender ID="showAddParticularModal" runat="server" BehaviorID="modalpopup2" CancelControlID="Button4"
                        OkControlID="LinkButton2"
                        TargetControlID="button5" PopupControlID="showParticular" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showParticular" runat="server" style="display: none;" class="confirmationModal500">
                        <div class="modal-header">
                            <button id="Button4" type="button" class="close white"></button>
                            <div class="tgPanelHead">Add Particular</div>
                        </div>
                        <div class="modal-body">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesType" runat="server" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="button5" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                        <i class="icon-remove"></i>                                    
                                        Close
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnAddParticularSave" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                OnClientClick="return validateParticular();" OnClick="btnAddParticularSave_Click">
                                        <i class="icon-ok"></i>
                                        Save
                            </asp:LinkButton>
                        </div>
                    </div>
                    </ContentTemplate>
                        </asp:UpdatePanel>
                    <!-- END particular modal -->
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
        function validateFeeCat() {
            if (validateText('txtFeesCatName', 1, 100, 'Enter a Fee Category Name') == false) return false;
            if (validateText('txtDateStart', 1, 100, 'Enter a Start Date') == false) return false;
            if (validateText('txtDateEnd', 1, 100, 'Enter a End Date') == false) return false;
            if (validateText('txtFeesFine', 1, 100, 'Enter a Fine Amount') == false) return false;
        }
        function validateParticular() {
            if (validateText('txtFeesType', 1, 100, 'Enter a Fee Particular Name') == false) return false;
        }
        function validationInput1() {
            if (validateCombo('ddlClass', "0", 'Select a Class Name') == false) return false;
        }
        function validateInputs1() {
            if ($("#btnSave").val() == 'Update') {
                if (validateCombo('ddlClass', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('dlCategory', '0', 'First click Edit then Update') == false) return false;
            }
            if (validateCombo('ddlClass', "0", 'Select a Class Name') == false) return false;
            if (validateCombo('dlCategory', "0", 'Select a Fees Category') == false) return false;
            if (validateCombo('dlParticular', "0", 'Select a Particulars') == false) return false;
            if (validateText('txtAmount', 1, 100, 'Enter an Amount') == false) return false;
            return true;
        }
        function validateInputs3() {
            if (validateCombo('dlFilter', "0", 'Select a Fees Category For Edit') == false) return false;
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
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
