<%@ Page Title="Leave Application" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="application.aspx.cs" Inherits="DS.Forms.leave.application" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
        }
        #gvLeaveApplicationList th, #gvLeaveApplicationList td{
            text-align: center;
        }
        .controlLength{
            width:150px;           
        }        
        input[type="checkbox"]
        {
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfLeaveNameId" ClientIDMode="Static" Value=" " runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Leave Application</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="gvLeaveApplicationList" />
                            <asp:AsyncPostBackTrigger ControlID="btnCalculation" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <asp:HiddenField ID="hdLeaveId" ClientIDMode="Static" runat="server" Value="" />
                                <tbody>
                                    <tr>
                                        <td>Card No
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtECardNo" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                Enabled="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>From Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFromDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                Enabled="true"></asp:TextBox>
                                            <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtFromDate" ID="CExtApplicationDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>To Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtToDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                            <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                                TargetControlID="txtToDate" ID="CalendarExtender1">
                                            </asp:CalendarExtender>

                                        </td>
                                        <td>
                                            <asp:Button ID="btnCalculation" runat="server" Text="Calculation" CssClass="btn btn-primary btn-sm"
                                                OnClick="btnCalculation_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No of Days
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoOfDays" runat="server" Enabled="false" ClientIDMode="Static"
                                                CssClass="input controlLength"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No Of Weekend
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTotalHolydays" runat="server" Enabled="false" ClientIDMode="Static"
                                                CssClass="input controlLength"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Leave Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLeaveName" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Status
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkProcessed" CssClass="" runat="server"
                                                Enabled="false" ClientIDMode="Static" Text=" Processed" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                                OnClientClick="return InputValidationBasket();" OnClick="btnSave_Click" />
                                            <asp:Button ID="tnReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClientClick="clearIt()"
                                                OnClick="tnReset_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                </div>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Leave Application List</div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gvLeaveApplicationList" runat="server" ClientIDMode="Static"
                                CssClass="table table-bordered tbl-controlPanel"
                                DataKeyNames="LACode" AutoGenerateColumns="False" AllowPaging="True"
                                OnPageIndexChanging="gvLeaveApplicationList_PageIndexChanging"
                                OnRowCommand="gvLeaveApplicationList_RowCommand"
                                OnRowDeleting="gvLeaveApplicationList_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="LACode" Visible="false" />
                                    <asp:BoundField DataField="ECardNo" ItemStyle-HorizontalAlign="Center" HeaderText="Card No" />
                                    <asp:BoundField DataField="FromDate" HeaderText="From" />
                                    <asp:BoundField DataField="ToDate" ItemStyle-HorizontalAlign="Center" HeaderText="To" />
                                    <asp:BoundField DataField="WeekHolydayNo" HeaderText="H.days" />

                                    <asp:BoundField DataField="TotalDays" HeaderText="T. Days" />
                                    <asp:BoundField DataField="ShortName" HeaderText="Type" />
                                    <asp:ButtonField CommandName="Status" Text=" Status " ControlStyle-CssClass="btn btn-primary" ButtonType="Button" HeaderText="Status" />
                                    <asp:ButtonField CommandName="Alter" Text=" Edit " ControlStyle-CssClass="btn btn-success" HeaderText="Edit" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Delete
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"
                                                CommandArgument='<%# Eval("LACode") %>' OnClientClick="return confirm('Are you sure, you want to delete the record?')">                                                
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <!--Bello code for show  Modele popup box and with data -->
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" Drag="True"
                                DropShadow="True" PopupControlID="PopupWindow" TargetControlID="btnClick" CancelControlID="btnCancel"
                                PopupDragHandleControlID="drag" CacheDynamicResults="False" Enabled="True">
                            </asp:ModalPopupExtender>
                            <asp:Button ID="btnClick" runat="server" Text="Popup click" Width="75px" Style="display: none" />

                            <div id="PopupWindow" runat="server" class="vb_area" style="display: none">
                                <h3 id="drag" runat="server">
                                    <div runat="server" id="msg"></div>
                                    <span id="btnCancel" style="float: right; margin-right: -10px; margin-top: -30px;">
                                        <img src="../../Images/action/Error.png" alt="" /></span></h3>
                                <table class="fontStyle" style="margin-top: 3px">
                                    <tr style="font-family: Arial; font-weight: bold;">
                                        <td>Name 
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblName"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="font-family: Arial; font-weight: bold;">
                                        <td>Card No
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblCardNo"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="font-family: Arial; font-weight: bold;">
                                        <td>Total Leave
                                        </td>
                                        <td>:</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalLeave"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="border: 1px solid #3b5998"></div>
                                <table style="margin-top: 3px">
                                    <tr style="font-family: Arial; font-weight: bold;">
                                        <td>Used
                                        </td>
                                        <td style="padding-left: 28px;">:</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblUsed" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="font-family: Arial; font-weight: bold;">
                                        <td>Unused
                                        </td>
                                        <td style="padding-left: 28px;">:</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblUnUsed" Text="Unused No" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <!-- End Model Popup Part-->
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtBoardName', 1, 50, 'Enter a Board Name') == false) return false;
            return true;
        }
        function editLeaveConfig(leaveNameId) {
            $('#hfLeaveNameId').val(leaveNameId);
            var strLeaveName = $('#r_' + leaveNameId + ' td:first-child').html();
            if (strLeaveName == "Casula Leave") $('#ddlLeaveTypes').val('c/l');
            else if (strLeaveName == "Sick Leave") $('#ddlLeaveTypes').val('s/l');
            else if (strLeaveName == "Maternity Leave") $('#ddlLeaveTypes').val('m/l');
            else if (strLeaveName == "Others Leave") $('#ddlLeaveTypes').val('o/l');
            else $('#ddlLeaveTypes').val('s');
            $('#txtLeaveDays').val($('#r_' + leaveNameId + ' td:nth-child(2)').html());
            $('#txtLeaveNature').val($('#r_' + leaveNameId + ' td:nth-child(3)').html());
            var status = $('#r_' + leaveNameId + ' td:nth-child(4)').html();
            if (status == "True") {
                $('#chkDeductionAllowed').prop("checked", true);
            }
            else $('#chkDeductionAllowed').prop("checked", false);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#hfLeaveNameId').val(' ');
            $('#txtLeaveDays').val('');
            $('#txtLeaveNature').val('');
            $("#btnSave").val('Save');
            $('#chkDeductionAllowed').prop("checked", false);
            $('#ddlLeaveTypes').val('s');
        }
        function InputValidationBasket() {
            try {
                if ($('#txtECardNo').val().trim().length == 0) {
                    showMessage('Please type employee card no.', 'error');
                    $('#txtECardNo').focus(); return false;
                }
                if ($('#txtFromDate').val().trim().length == 0) {
                    showMessage('Please select from date', 'error');
                    $('#txtFromDate').focus(); return false;
                }
                if ($('#txtToDate').val().trim().length == 0) {
                    showMessage('Please select to date', 'error');
                    $('#txtToDate').focus(); return false;
                }
                if ($('#txtNoOfDays').val().trim().length == 0) {
                    showMessage('Please press calculation button', 'error');
                    $('#txtNoOfDays').focus(); return false;
                }
                if ($('#txtTotalHolydays').val().trim().length == 0) {
                    showMessage('Please press calculation button', 'error');
                    $('#txtTotalHolydays').focus(); return false;
                }
                if ($('#ddlLeaveName').val().trim() == 's') {
                    showMessage('Please type leave name  ', 'error');
                    $('#ddlLeaveName').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
    </script>
</asp:Content>
