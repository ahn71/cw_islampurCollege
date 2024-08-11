<%@ Page Title="Set Discount Amount" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" 
    CodeBehind="DiscountSet.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.DiscountSet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
        }
        .controlLength{
            /*width:150px;*/
        }
        hr{
            margin:0;
        }
        .tbl-controlPanel-Discount td:first-child,
        .tbl-controlPanel-Discount td:nth-child(2n+1){
            text-align:right;
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

        .bot{
            padding: 6px 10px!important;
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
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Set Discount Amount</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Discount Set</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlBatchName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                            <asp:AsyncPostBackTrigger ControlID="ddlRoll" />
                            <asp:AsyncPostBackTrigger ControlID="ddlParticulars" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-10 col-sm-offset-1">
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Shift</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="dlShift" runat="server" AutoPostBack="false"
                                                    CssClass="input controlLength form-control" ClientIDMode="Static">
                                                    <asp:ListItem Selected="True">Morning</asp:ListItem>
                                                    <asp:ListItem>Day</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Batch</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlBatchName" ClientIDMode="Static"
                                                    CssClass="input controlLength form-control" runat="server"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlBatchName_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Group</label>
                                            <div class="col-sm-8">
                                                 <asp:DropDownList ID="ddlGroup" ClientIDMode="Static" runat="server"
                                                    CssClass="input controlLength form-control" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Section</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlSection" ClientIDMode="Static" runat="server"
                                                    CssClass="input controlLength form-control" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Roll</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlRoll" runat="server" AutoPostBack="False"
                                                    CssClass="input controlLength form-control" ClientIDMode="Static">                                           
                                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Particulars</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlParticulars" ClientIDMode="Static" runat="server"
                                                    CssClass="input controlLength form-control" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlParticulars_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-6">
                                            <label class="col-sm-4">Discount (%)</label>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtDiscountAmount" ClientIDMode="Static"
                                            CssClass="input controlLength form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel">
                                        <div class="col-sm-8">
                                            <label class="col-sm-3"></label>
                                            <div class="col-sm-9">
                                                <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success"
                                            OnClientClick="return validateInputs();" OnClick="btnAdd_Click" Text="Add" />
                                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" CssClass="btn btn-primary"
                                            OnClientClick="return validateInputForSave();" OnClick="btnSave_Click" Text="Save" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server"
                                            CssClass="btn btn-default" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                           
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
                            <div id="divscroll" style="height: 275px; max-height: 275x; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvDiscount" runat="server" AutoGenerateColumns="false"
                                    ClientIDMode="Static" CssClass="table table-bordered"
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
                                <div class="row tbl-controlPanel">
	                                <div class="col-sm-10 col-sm-offset-1">
		                                <div class="row tbl-controlPanel">
			                                <div class="col-sm-6">
				                                <label class="col-sm-3">Shift</label>
				                                <div class="col-sm-9">
                                                     <asp:DropDownList ID="ddlShiftList" runat="server" AutoPostBack="false"
                                                        CssClass="input controlLength form-control" ClientIDMode="Static">
                                                        <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
				                                </div>
			                                </div>
			                                <div class="col-sm-6">
				                                <label class="col-sm-3">Batch</label>
				                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlBatchlist" ClientIDMode="Static" runat="server"
                                                        CssClass="input controlLength form-control"
                                                        OnSelectedIndexChanged="ddlBatchlist_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
				                                </div>
			                                </div>
		                                </div>
		                                <div class="row tbl-controlPanel">
			                                <div class="col-sm-6">
				                                <label class="col-sm-3">Group</label>
				                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlGroupList" ClientIDMode="Static" runat="server"
                                                       OnSelectedIndexChanged="ddlGroupList_SelectedIndexChanged"  CssClass="input controlLength form-control"
                                                        AutoPostBack="True">                                               
                                                    </asp:DropDownList>
				                                </div>
			                                </div>
			                                <div class="col-sm-6">
				                                <label class="col-sm-3">Section</label>
				                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlSectionlist" ClientIDMode="Static" runat="server"
                                                        CssClass="input controlLength form-control"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSectionlist_SelectedIndexChanged">                                               
                                                    </asp:DropDownList>
				                                </div>
			                                </div>
		                                </div>
		                                <div class="row tbl-controlPanel">
			                                <div class="col-sm-6">
				                                <label class="col-sm-3">Roll</label>
				                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlRolllist" ClientIDMode="Static" runat="server"
                                                CssClass="input controlLength form-control">                                                
                                            </asp:DropDownList>
				                                </div>
			                                </div>
			                                <div class="col-sm-6">
				                                <label class="col-sm-3"></label>
				                                <div class="col-sm-9"></div>
			                                </div>
		                                </div>
		                                <div class="row tbl-controlPanel">
			                                <div class="col-sm-12">
				                                <label class="col-sm-3"></label>
				                                <div class="col-sm-9">
                                                    <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static"
                                                        CssClass="btn btn-success bot"
                                                        runat="server" OnClientClick="return validateInputsForSearch();"  OnClick="btnSearch_Click" />
                                                    <asp:Button ID="btnEdit" Text="Edit" ClientIDMode="Static" CssClass="btn btn-primary bot"
                                                        runat="server" OnClientClick="return validateInputsForSearch();" OnClick="btnEdit_Click" />
                                                    <asp:Button ID="btnRefresh" Text="Refresh" ClientIDMode="Static"
                                                        CssClass="btn btn-default bot"
                                                        runat="server" OnClick="btnRefresh_Click" />
				                                </div>
			                                </div>
			                                
		                                </div>
	                                </div>
                                </div>
                                
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
                                        style="width: 100%; min-height: 100px; overflow: auto; overflow-x: hidden; height: 150px;">
                                    </div>
                                    <div id="divDiscount" class="datatables_wrapper" runat="server"
                                        style="width: 100%; min-height: 140px; overflow: auto; overflow-x: hidden; height: 120px;">
                                    </div>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlRoll").select2();
            $("#ddlRolllist").select2();
        });
        //document.getElementById('#divscroll').scrollTop = 0;
        function validateInputsForSearch() {
            if (validateCombo('ddlShiftList', '0', 'Select a Shift') == false) return false;
            if (validateCombo('ddlBatchlist', '0', 'Select a Batch') == false) return false;
            if ($('#ddlGroupList').prop("disabled") == false) {
                if (validateCombo('ddlGroupList', '0', 'Select a Group') == false) return false;               
            }            
            if (validateCombo('ddlSectionlist', '0', 'Select a Section') == false) return false;
            if (validateCombo('ddlRolllist', '0', 'Select a Roll No.') == false) return false;
            return true;
        }
        function validateInputs() {
            if ($("#btnSave").val() == 'Update') {
                if (validateCombo('dlShift', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('ddlBatchName', '0', 'First click Edit then Update') == false) return false;
                if ($('#ddlGroup').prop("disabled") == false) {
                    if (validateCombo('ddlGroup', '0', 'First click Edit then Update') == false) return false;
                }
                if (validateCombo('ddlSection', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('ddlRoll', '0', 'First click Edit then Update') == false) return false;                
            }
            if (validateCombo('dlShift', '0', 'Select a Shift') == false) return false;
            if (validateCombo('ddlBatchName', '0', 'Select a Batch') == false) return false;
            if ($('#ddlGroup').prop("disabled") == false) {
                if (validateCombo('ddlGroup', '0', 'Select a Group') == false) return false;
            }
            if (validateCombo('ddlSection', '0', 'Select a Section') == false) return false;
            if (validateCombo('ddlRoll', '0', 'Select a Roll No') == false) return false;
            if (validateCombo('ddlParticulars', '0', 'Select Particulars') == false) return false;
            if (validateText('txtDiscountAmount',1,20, 'Type Discount(%)') == false) return false;
            return true;
        }
        function validateInputForSave() {
            if ($("#btnSave").val() == 'Update') {
                if (validateCombo('dlShift', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('ddlBatchName', '0', 'First click Edit then Update') == false) return false;
                if ($('#ddlGroup').prop("disabled") == false) {
                    if (validateCombo('ddlGroup', '0', 'First click Edit then Update') == false) return false;
                }
                if (validateCombo('ddlSection', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('ddlRoll', '0', 'First click Edit then Update') == false) return false;
            }
            if (validateCombo('dlShift', '0', 'Select a Shift') == false) return false;
            if (validateCombo('ddlBatchName', '0', 'Select a Batch') == false) return false;
            if ($('#ddlGroup').prop("disabled") == false) {
                if (validateCombo('ddlGroup', '0', 'Select a Group') == false) return false;
            }
            if (validateCombo('ddlSection', '0', 'Select a Section') == false) return false;
            if (validateCombo('ddlRoll', '0', 'Select a Roll No') == false) return false;          
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
        function load() {
            $("#ddlRoll").select2();
            $("#ddlRolllist").select2();
        }
    </script>
</asp:Content>
