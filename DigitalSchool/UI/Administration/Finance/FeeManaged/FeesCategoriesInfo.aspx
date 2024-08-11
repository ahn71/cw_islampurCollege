<%@ Page Title="Fees Category" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FeesCategoriesInfo.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.FeesCategoriesInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            /*width: 250px;*/
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         /*.dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
         #tblParticularCategory_length {
             display: none;
            padding: 15px;
        }
          #tblParticularCategory_filter {
            display: none;
            padding: 15px;
        }
        #tblParticularCategory_info {
             display: none;
            padding: 15px;
        }
        #tblParticularCategory_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
        #tblParticularCategory {
        
        margin-top:0px!important;
        margin-bottom:0px!important;
        }*/
         @media (min-width: 320px) and (max-width: 480px) {
             .input{
            color:#000;
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
                <li class="active">Fees Category</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
  
            <asp:HiddenField ID="lblFeesCateId" ClientIDMode="Static" runat="server"/>
            <div class="">
                <div class="row">
                    <div class="col-md-7">
                        <h4 class="text-right" style="float:left">Fees Category Information</h4>
                         <div class="dataTables_filter_New" style="float: right;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                     </label>
                 </div>                        
                    </div>
                    <div class="col-md-5"></div>
                </div>
                <div class="row">
                    <div class="col-md-7">
                        <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dlBatchName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlPaymentFor" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="tgPanel">
                                    <div id="divFeesCategoryList" class="datatables_wrapper" runat="server"
                                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-5">
                        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                            <Triggers>    
                                <asp:AsyncPostBackTrigger ControlID="dlBatchName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlPaymentFor" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="tgPanel">
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfAcademicInfo" Value="0" />
                                    <div class="tgPanelHead">Fees Category</div>
                                    <div class="row tbl-controlPanel">
	                                    <div class="col-sm-10 col-sm-offset-1">   
                                             <div class="form-group row">
			                                    <label class="col-sm-4">Payment For</label>
			                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlPaymentFor" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentFor_SelectedIndexChanged">
                                                        <%--<asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        <asp:ListItem Value="admission">Admission</asp:ListItem>
                                                        <asp:ListItem Value="regular">Regular Fee</asp:ListItem>
                                                        <asp:ListItem Value="openPayment">Open Payment</asp:ListItem>--%>
                                                </asp:DropDownList>
			                                    </div>
		                                    </div>

                                             

                                            <asp:Panel runat="server" ClientIDMode="Static" ID="pnlAcademicInfo">
		                                    <div class="form-group row">
			                                    <label class="col-sm-4">Batch Name</label>
			                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="dlBatchName" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                    OnSelectedIndexChanged="dlBatchName_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
			                                    </div>
		                                    </div>
                                             <div class="form-group row">
			                                    <label class="col-sm-4">Group</label>
			                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                    >
                                                        
                                                </asp:DropDownList>
			                                    </div>
		                                    </div>                                           
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Exam</label>
			                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlExam" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">
                                                </asp:DropDownList>
			                                    </div>
		                                    </div>
                                                </asp:Panel>
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Fees Category</label>
			                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="txtFeesCatName" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
			                                    </div>
		                                    </div>
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Payment Store</label>
			                                    <div class="col-sm-8">
                                                    <asp:DropDownList ID="ddlPaymentStore" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">                                                       
                                                </asp:DropDownList>
			                                    </div>
		                                    </div>
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Date of Start</label>
			                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateStart" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtDateStart"></asp:CalendarExtender>
			                                    </div>
		                                    </div>
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Date of End</label>
			                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="txtDateEnd" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtDateEnd"></asp:CalendarExtender>
			                                    </div>
		                                    </div>
                                            <div class="form-group row">
			                                    <label class="col-sm-4">Fine Amount</label>
			                                    <div class="col-sm-8">
                                                    <asp:TextBox ID="txtFeesFine" runat="server" ClientIDMode="Static" Text="0" CssClass="input controlLength form-control"></asp:TextBox>
			                                    </div>
		                                    </div>
                                            <div class="form-group row">
			                                    <label class="col-sm-4"></label>
			                                    <div class="col-sm-8">
                                                    <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                                <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
			                                    </div>
		                                    </div>
	                                    </div>
                                    </div>
                                    
                                </div>
                            </ContentTemplate>
    </asp:UpdatePanel>
                    </div>
                </div>
            </div>
       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">     
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblParticularCategory', '');
            });
           // $("#dlBatchName").select2();
            $('#tblParticularCategory').dataTable({
                //"iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblParticularCategory').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            
            if (validateCombo('ddlPaymentFor', '0', "Select Payment For") == false) return false;
            if ($('#hfAcademicInfo').val() == "1") {
                if (validateCombo('dlBatchName','0',"Select Batch") == false) return false;
            }             
            if (validateText('txtFeesCatName', 1, 200, "Enter Fees Category") == false) return false;  
            if (validateCombo('ddlPaymentStore', '0', "Select Payment Store") == false) return false;
            if (validateText('txtDateStart', 10, 10, "Select Start Date") == false) return false;
            if (validateText('txtDateEnd', 10, 10, "Select End Date") == false) return false;
            return true;
        }
        function editFeesCategory(feesCatId, BatchId, ClassId,ExInSl,PaymentFor,ClsGrpId,StoreNameKey) {            
            $('#ddlPaymentFor').val(PaymentFor);
            $('#lblFeesCateId').val(feesCatId);
            var strBatch = $('#r_' + feesCatId + ' td:nth-child(1)').html();
            var strFeesCatName = $('#r_' + feesCatId + ' td:nth-child(2)').html();
            var strStartDate = $('#r_' + feesCatId + ' td:nth-child(3)').html();
            var strEndDate = $('#r_' + feesCatId + ' td:nth-child(4)').html();
            var strFine = $('#r_' + feesCatId + ' td:nth-child(5)').html();
            var BatchID = BatchId + '_' + ClassId;
            $("#dlBatchName").prop("disabled", true);
            $('#hfAcademicInfo').val('0');
           //  $('#dlBatchName option[value='+BatchID+']').attr('selected','selected');        
             $('#ddlGroup option[value='+ClsGrpId+']').attr('selected','selected');        
             $('#ddlExam option[value='+ExInSl+']').attr('selected','selected');        
             $('#ddlPaymentStore option[value='+StoreNameKey+']').attr('selected','selected');        
            
            $('#txtFeesCatName').val(strFeesCatName);
            $('#txtDateStart').val(strStartDate);
            $('#txtDateEnd').val(strEndDate);
            $('#txtFeesFine').val(strFine);
            $("#btnSave").val('Update');
            //load();
        }
        function clearIt() {
            $('#lblFeesCateId').val('');
            $('#txtFeesCatName').val('');
            $('#txtFeesFine').val('');
            $('#txtDateStart').val('');
            $('#txtDateEnd').val('');
            setFocus('#txtFeesCatName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            load();
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function load() {
            loaddatatable();
           // $("#dlBatchName").select2();
        }
        var ddlText, ddlValue, ddl, lblMesg;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=dlBatchName.ClientID %>");
            lblMesg = document.getElementById("<%=lblMessage.ClientID%>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }
        window.onload = CacheItems;

        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            lblMesg.innerHTML = ddl.options.length + " items found.";
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }

        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }
    </script>
</asp:Content>
