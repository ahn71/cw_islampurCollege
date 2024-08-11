﻿<%@ Page Title="Set Payment Date" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DateOfPayment.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.DateOfPayment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .tgPanel{
             width:500px;
         }
         hr{
             margin:0;
         }
         .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .controlLength{
            width:200px;
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
                <li class="active">Set Payment Date</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="dlFeeCatName" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="lblDateOfPayment" ClientIDMode="Static" runat="server" />
            <div class="">
                <div class="row">
                    <div class="col-md-6">
                        <h4 class="text-right">Set Payment Date</h4>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div style="min-height: 305px; overflow: auto;">
                            <div id="divDateOfPaymentList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Date Of Payment</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Fee Category
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlFeeCatName" ClientIDMode="Static" CssClass="input controlLength" AutoPostBack="True"
                                            OnSelectedIndexChanged="dlFeeCatName_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of Start
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateStart" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateStart"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Date of End
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDateEnd" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateEnd"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="upProgress" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" ClientIDMode="Static"></asp:UpdateProgress>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                    </td>
                                </tr>
                            </table>                            
                        </div>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Particulars Details</div>
                            <hr />
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div id="divFeesCategoryList" class="datatables_wrapper" runat="server"
                                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            //if (validateText('dlFeeCatName', 1, 20, '---Select---') == false) return false;
            if (validateCombo('dlFeeCatName',0, 'Select Fee Category') == false) return false;
            if (validateText('txtDateStart', 1, 20, 'Select Start Date') == false) return false;
            if (validateText('txtDateEnd', 1, 20, 'Select End Date') == false) return false;
            return true;
        }
        function editDateOfPayment(feesSetId, FeeCatId) {
            $('#lblDateOfPayment').val(feesSetId);
            var strFees = $('#r_' + feesSetId + ' td:nth-child(1)').html();
            var strStartDate = $('#r_' + feesSetId + ' td:nth-child(2)').html();
            var strEndDate = $('#r_' + feesSetId + ' td:nth-child(3)').html();
            $('#dlFeeCatName').val(FeeCatId);
           // $('#dlFeeCatName option:selected').text(strFees); //for dropdown list select data
            $('#txtDateStart').val(strStartDate);
            $('#txtDateEnd').val(strEndDate);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#txtDateStart').val('');
            $('#txtDateEnd').val('');
            setFocus('txtAmount');
            $("#btnSave").val('Save');
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
            clearIt();
        }
        function updateSuccess() {
            showMessage('Update successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
