<%@ Page Title="Leave Application" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="application.aspx.cs" Inherits="DS.UI.Academics.Attendance.Leave.application" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        #gvLeaveApplicationList th, #gvLeaveApplicationList td {
            text-align: center;
        }
        .controlLength {
            width: 200px;
        }
        input[type="checkbox"] {
            margin: 7px;
        }
        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }
        .ajax__tab_tab
        {
            -webkit-box-sizing: content-box!important;
            -moz-box-sizing: content-box!important;
            box-sizing: content-box!important;
        }  
        .ajax__tab_default .ajax__tab_tab
        {
            display: inline!important;            
        } 
        .ajax__tab_xp .ajax__tab_header .ajax__tab_outer{
            height: 20px!important;
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .tbl-controlPanel1{
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 500px;
        }
        .tbl-controlPanel1 td>span{
            margin: 10px;
            padding: 5px;
        }
        .tbl-controlPanel1 td:first-child{
            width: 40%;
            text-align: right;         
        }
        .controlLength1 {
            width: 200px;
            font-size: 1em;              
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <%--<asp:AsyncPostBackTrigger ControlID="gvLeaveApplicationList" />--%>
            <asp:AsyncPostBackTrigger ControlID="btnCalculation" />
            <asp:AsyncPostBackTrigger ControlID="ddlLeaveName" />
            <asp:AsyncPostBackTrigger ControlID="TabContainer1" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="hfLeaveNameId" ClientIDMode="Static" Value=" " runat="server" />
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
                        <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                        <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                        <li><a runat="server" href="~/UI/Academic/Attendance/Leave/LeaveHome.aspx">Leave Management</a></li>
                        <li class="active">Leave Application</li>
                    </ul>
                    <!--breadcrumbs end -->
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Leave Application</div>
                        <table class="tbl-controlPanel">
                            <asp:HiddenField ID="hdLeaveId" ClientIDMode="Static" runat="server" Value="" />
                            <tbody>
                                <tr>
                                    <td>Shift
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList>
                                    </td>
                                </tr>
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
                                        <asp:DropDownList ID="ddlLeaveName" AutoPostBack="true" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlLeaveName_SelectedIndexChanged" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Notes
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNotes" runat="server" Enabled="true" ClientIDMode="Static"
                                            CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trPregnantDate" runat="server" visible="false">
                                    <td>Pregnant Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPregnantDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                            Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtPregnantDate" ID="CalendarExtender2">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr id="trPrasaberaDate" runat="server" visible="false">
                                    <td>Prasabera Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrasaberaDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                            Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                            TargetControlID="txtPrasaberaDate" ID="CalendarExtender3">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr id="trStatus" runat="server" visible="false">
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
                        <br />
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Leave Application Status</div>
                        <asp:TabContainer ID="TabContainer1" runat="server" Font-Bold="true" Font-Size="20px" AutoPostBack="true" OnActiveTabChanged="TabContainer1_ActiveTabChanged">
                            <asp:TabPanel ID="tab1" runat="server" TabIndex="0" HeaderText="Approved List">
                                <HeaderTemplate>Approved List</HeaderTemplate>
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
                                            <asp:BoundField DataField="ShiftName" HeaderText="Shift" />
                                            <asp:ButtonField  CommandName="Status" Text=" Status " ControlStyle-CssClass="btn btn-primary" 
                                                ButtonType="Button" HeaderText="Status"/>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                   Edit
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Button CommandName="Alter" Text=" Edit " runat="server" ID="btnEdit" CommandArgument='<%#((GridViewRow)Container).RowIndex %>'  CssClass="btn btn-success" />
                                                </ItemTemplate>
                                            </asp:TemplateField>                                           

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Delete
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    
                                                    <asp:Button  ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"
                                                        CommandArgument='<%# Eval("LACode") %>' OnClientClick="return confirm('Are you sure, you want to delete the record?')">                                                
                                                    </asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>  
                                              <asp:TemplateField >
                                        <HeaderTemplate >
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="View" CssClass="btn btn-primary" ClientIDMode="Static" ID="btnView" Text="View"  CommandArgument='<%#Eval("LACode")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                        </Columns>
                                    </asp:GridView>
                                    <!--Bello code for show  Modele popup box and with data -->                                    
                                    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="Button4"
                                        PopupControlID="PopupWindow" TargetControlID="button5" OkControlID="btnCancel" BackgroundCssClass="ModalPopupBG">
                                    </asp:ModalPopupExtender>
                                    <div id="PopupWindow" runat="server" style="display: none;" class="confirmationModal400">
                                        <div class="modal-header">
                                            <button id="Button4" type="button" class="close white"></button>
                                            <div class="tgPanelHead">Leave Status</div>
                                        </div>
                                        <div class="modal-body">
                                            <table class="tbl-controlPanel1">
                                                <tr>
                                                    <td>Name 
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblName" CssClass="label text-success controlLength1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Card No
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCardNo" CssClass="label text-success controlLength1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Leave
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblTotalLeave" CssClass="label text-success controlLength1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div style="border: 1px solid #ddd"></div>
                                            <table style="margin-top: 3px" class="tbl-controlPanel1">
                                                <tr>
                                                    <td>Used
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblUsed" CssClass="label text-success controlLength1"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Unused
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblUnUsed" Text="Unused No"
                                                            ClientIDMode="Static" ForeColor="Red" CssClass="label text-success controlLength1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="modal-footer">
                                            <button id="button5" type="button" runat="server" style="display: none;" />
                                            <asp:LinkButton ID="btnCancel" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                                <i class="icon-remove"></i>                                    
                                                Close
                                            </asp:LinkButton>                                            
                                        </div>
                                    </div>
                                    <!-- End Model Popup Part-->
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="tab2" runat="server" HeaderText="Rejected List" TabIndex="1">
                                <HeaderTemplate>Rejected List</HeaderTemplate>
                                <ContentTemplate>
                                    <asp:GridView runat="server" ID="gvRejectedList" Width="100%" DataKeyNames="LACode" CssClass="table table-bordered" 
                                        AutoGenerateColumns="false" AllowPaging="true" PageSize="20"  OnRowCommand="gvRejectedList_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="LACode" HeaderText="LACode" Visible="false"/>
                                            <asp:BoundField DataField="ECardNo" HeaderText="Card No" Visible="true"/>
                                            <asp:BoundField DataField="EName" HeaderText="Name" Visible="true"/>
                                            <asp:BoundField DataField="FromDate" HeaderText="From" Visible="true"/>
                                            <asp:BoundField DataField="ToDate" HeaderText="To" Visible="true"/>
                                            <asp:BoundField DataField="WeekHolydayNo" HeaderText="W.H." Visible="true"/>
                                            <asp:BoundField DataField="TotalDays" HeaderText="T.Days" Visible="true"/>
                                            <asp:BoundField DataField="ShortName" HeaderText="Leave " Visible="true"/>                                                                                    
                                      <asp:TemplateField >
                                        <HeaderTemplate >
                                            View
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" ClientIDMode="Static" CommandName="View" CssClass="btn btn-primary" CommandArgument='<%#Eval("LACode")%>' ID="btnView" Text="View" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        </Columns>                                       
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </div>
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
