<%@ Page Title="Send SMS" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="DS.UI.Notification.SMS.SendSMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }      
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
            width:23%;
        }
        .tbl-controlPanel td:nth-child(2){           
            width: 50%;
            padding-right:5px;
        }
        .charCount {
            padding: 0px;
            list-style: outside none none;
            margin: 0px;
        }
        .charCount li{
            display: inline;
        }
        table.tbl-controlPanel1 {
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 500px;
        }
        table.tbl-controlPanel1 tr{
            margin-bottom: 5px;
        }
        table.tbl-controlPanel1 td{
            width: 25%;
           padding-bottom: 5px;
        }
        table.tbl-controlPanel1 td:first-child,
        table.tbl-controlPanel1 td:nth-child(3){
            width: 15%;
            text-align: right;
            padding-right: 5px;
        }
        .litleMarging{
            margin-left: 5px;
        }        
        .tahoma {
            text-align:center;
        }        
        #MainContent_CalendarExtender1_daysTable td,
        #MainContent_CalendarExtender1_daysTable td:first-child,
        #MainContent_CalendarExtender1_daysTable td:nth-child(3){
            width: auto;
            margin: 0;
            padding: 0;
        }
        .ajax__calendar_footer {
            height: auto !important;
        }
        .btnRadio {
            padding: 3px;
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        table.tbl-controlPanel2 {
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 100%;
        }
        table.tbl-controlPanel2 tr{
            margin-bottom: 5px;
        }
        table.tbl-controlPanel2 td{
            width: 25%;
           padding-bottom: 5px;
        }
        table.tbl-controlPanel2 td:first-child,
        table.tbl-controlPanel2 td:nth-child(3){
            width: 5%;
            text-align: right;
            padding-right: 5px;
        }
        legend{
            font-size:15px;
            margin-bottom:2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>    
    <asp:HiddenField ID="lblSmsBodyTitle" ClientIDMode="Static" runat="server"/>
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
                <li class="active">Send SMS</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
            <div class="tgPanel">
                <div class="tgPanelHead">SMS For</div>
                <header class="panel-heading tab-bg-dark-navy-blue ">
                    <ul class="nav nav-tabs">
                        <li class="active">
                            <a data-toggle="tab" href="#tabs-1">Today's Students Absent</a>
                        </li>
                        <li class="">
                            <a data-toggle="tab" href="#tabs-2">Fail Students List</a>
                        </li>
                        <li class="">
                            <a data-toggle="tab" href="#tabs-3">Notice</a>
                        </li>
                        <li class="">
                            <a data-toggle="tab" href="#tabs-4">Greetings</a>
                        </li>
                        <li class="">
                            <a data-toggle="tab" href="#tabs-5">Others</a>
                        </li>
                    </ul>
                </header>
                <div class="panel-body">
                    <div class="tab-content">
                        <div id="tabs-1" class="tab-pane active">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                                </Triggers>
                                <ContentTemplate>
                                    <table class="tbl-controlPanel1">
                                        <tr>
                                            <td>Date</td>
                                            <td>
                                                <asp:TextBox ID="txtDate" runat="server" Width="100%" CssClass="input" ClientIDMode="Static"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                    TargetControlID="txtDate">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td>Batch</td>
                                            <td>
                                                <asp:DropDownList ID="dlBatch" runat="server" Width="100%" CssClass="input" ClientIDMode="Static" AutoPostBack="true"
                                                    OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblGrpSection" runat="server" Text="Section" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlSection" runat="server" Width="100%" CssClass="input" Visible="false"
                                                    ClientIDMode="Static">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>shift</td>
                                            <td>
                                                <asp:DropDownList ID="dlShift" runat="server" Width="100%" CssClass="input" ClientIDMode="Static">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                    <asp:ListItem Value="Day">Day</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                    OnClientClick="return btnSearch_validation();" OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <hr />
                                    <asp:Panel ID="absentgridPanel" runat="server" Height="238px" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                                        <asp:GridView ID="adsentStdView" runat="server" DataKeyNames="AbsentStdID" CssClass="table table-bordered table-strip"
                                            ClientIDMode="Static" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" OnClick="javascript:CheckAll(this,adsentStdView.id);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkIndivisual" runat="server" OnClick="javascript:singleChk(this,adsentStdView.id);" />
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="btnRadio" />
                                                    <ItemStyle CssClass="btnRadio" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                                <asp:BoundField DataField="Roll" HeaderText="Roll" />
                                                <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                                <asp:BoundField DataField="Section" HeaderText="Section" />
                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                <asp:BoundField DataField="GuardiantMobile" HeaderText="Guardiant Mobile" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="tabs-2" class="tab-pane">Fail Student List</div>
                        <div id="tabs-3" class="tab-pane">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatchN" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <fieldset>
                                                <legend>Student And Guardiant</legend>
                                                <table class="tbl-controlPanel2">
                                                    <tr>
                                                        <td>Batch</td>
                                                        <td>
                                                            <asp:DropDownList ID="dlBatchN" runat="server" Width="100%" CssClass="input" ClientIDMode="Static" AutoPostBack="true"
                                                                OnSelectedIndexChanged="dlBatchN_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblGrdSectionN" runat="server" Text="Section" Visible="false"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="dlSectionN" runat="server" Width="100%" CssClass="input" Visible="false"
                                                                ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Shift</td>
                                                        <td>
                                                            <asp:DropDownList ID="dlShiftN" runat="server" Width="100%" CssClass="input" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                                <asp:ListItem Value="Day">Day</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="btnSTDSearchN" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                                OnClientClick="return btnSTDSearchN_validation();" OnClick="btnSTDSearchN_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr />     
                                            </fieldset>
                                        </div>                                        
                                        <div class="col-md-6">
                                            <fieldset>
                                                <legend>Employee/Teacher</legend>
                                                <table class="tbl-controlPanel2">
                                                    <tr>
                                                        <td>Department</td>
                                                        <td>
                                                            <asp:DropDownList ID="dlDeptN" runat="server" Width="100%" CssClass="input" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Designation</td>
                                                        <td>
                                                            <asp:DropDownList ID="dlDesignationN" runat="server" Width="100%" CssClass="input" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Employee Shift</td>
                                                        <td>
                                                            <asp:DropDownList ID="dlEShiftN" runat="server" Width="100%" CssClass="input" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                <asp:ListItem Value="Morning">Morning</asp:ListItem>
                                                                <asp:ListItem Value="Day">Day</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button ID="btnESearchN" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                                OnClientClick="return btnESearchN_validation();" OnClick="btnESearchN_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <hr />     
                                            </fieldset>
                                        </div>                                        
                                    </div> 
                                                                  
                                    <div class="col-md-12">
                                        <asp:Panel ID="noticeSTDGridPanel" runat="server" Height="238px" CssClass="datatables_wrapper" 
                                            Width="100%" ScrollBars="Auto" Visible="false">
                                            <asp:GridView ID="noticeSTDView" runat="server" DataKeyNames="StudentID" CssClass="table table-bordered table-strip"
                                                ClientIDMode="Static" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" OnClick="javascript:CheckAll(this,noticeSTDView.id);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIndivisual" runat="server" OnClick="javascript:singleChk(this,noticeSTDView.id);" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="btnRadio" />
                                                        <ItemStyle CssClass="btnRadio" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                                    <asp:BoundField DataField="Roll" HeaderText="Roll" />
                                                    <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                                    <asp:BoundField DataField="Section" HeaderText="Section" />
                                                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                    <asp:BoundField DataField="GuardiantMobile" HeaderText="Guardiant Mobile" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Panel ID="noticeEmpGridPanel" runat="server" Height="238px" CssClass="datatables_wrapper" 
                                            Width="100%" ScrollBars="Auto" Visible="false">
                                            <asp:GridView ID="noticeEmpView" runat="server" DataKeyNames="EmployeeId" CssClass="table table-bordered table-strip"
                                                ClientIDMode="Static" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" OnClick="javascript:CheckAll(this,noticeEmpView.id);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkIndivisual" runat="server" OnClick="javascript:singleChk(this,noticeEmpView.id);" />
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="btnRadio" />
                                                        <ItemStyle CssClass="btnRadio" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmpName" HeaderText="Teacher/Employee Name" />
                                                    <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                                    <asp:BoundField DataField="IsTeacher" HeaderText="Is Teacher" />
                                                    <asp:BoundField DataField="IsExaminer" HeaderText="Is Examiner" />
                                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>                       
                        </div>
                        <div id="tabs-4" class="tab-pane">Greetings</div>
                        <div id="tabs-5" class="tab-pane">Others</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="tgPanel">
                <div class="tgPanelHead">SMS Template And Send</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Template
                        </td>
                        <td>
                            <asp:DropDownList ID="dlSMSTemplate" runat="server" Width="100%" CssClass="input"
                                OnSelectedIndexChanged="dlSMSTemplate_SelectedIndexChanged" ClientIDMode="Static">
                                <asp:ListItem Value="0">...Selete...</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnAddNewTemplate" runat="server" Text="Add New" OnClick="btnAddNewTemplate_Click"
                                ClientIDMode="Static" CssClass="btn btn-success" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">Body
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtMsgBody" runat="server" TextMode="MultiLine" Width="98%" Rows="15"
                                CssClass="input" ClientIDMode="Static"></asp:TextBox>
                            <ul class="charCount">
                                <li>Characters : </li>
                                <li>
                                    <asp:Label ID="lblCharCount" runat="server" Text="0" Width="30px" ClientIDMode="Static" CssClass="tahoma"></asp:Label></li>
                                <%--<li>SMS Count  : </li>
                            <li>
                                <asp:Label ID="Label2" runat="server" Text="1" Width="30px" CssClass="tahoma"></asp:Label></li>--%>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="2">
                            <asp:Button ID="btnSend" runat="server" Text="Send" ClientIDMode="Static"
                                OnClientClick="return btnSend_validation();" OnClick="btnSend_Click"
                                CssClass="btn btn-primary" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" ClientIDMode="Static"
                                OnClientClick="return btnClear_Clear();"
                                CssClass="btn btn-default" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <!-- add message body modal -->
    <asp:ModalPopupExtender ID="showAddMsgBody" runat="server" BehaviorID="modalpopup1" CancelControlID="Button4"
        OkControlID="LinkButton2"
        TargetControlID="button5" PopupControlID="showAddMsg" BackgroundCssClass="ModalPopupBG">
    </asp:ModalPopupExtender>
    <div id="showAddMsg" runat="server" style="display: none;" class="confirmationModal400">
        <div class="modal-header">
            <button id="Button4" type="button" class="close white"></button>
            <div class="tgPanelHead">Add SMS Template</div>
        </div>
        <div class="modal-body">
            <table class="tbl-controlPanel">
                <tr>
                    <td style="width: 15%">Title
                    </td>
                    <td style="width: 78%;">
                        <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static"
                            Width="100%" CssClass="input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%; vertical-align: top">Message
                    </td>
                    <td style="width: 78%;">
                        <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" Rows="10" ClientIDMode="Static"
                            Width="100%" CssClass="input"></asp:TextBox>
                        <ul class="charCount">
                            <li>Characters : </li>
                            <li>
                                <asp:Label ID="lblBWordCount" runat="server" ClientIDMode="Static"
                                    Text="0" Width="30px" CssClass="tahoma"></asp:Label></li>
                            <%--<li>SMS Count  : </li><li><asp:Label ID="lblBSMSCount" runat="server" Text="1" Width="30px" CssClass="tahoma"></asp:Label></li>--%>
                        </ul>
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
            <asp:LinkButton ID="btnAddMsg" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false"
                OnClientClick="return btnAddMsg_validation();" OnClick="btnAddMsg_Click">
                <i class="icon-ok"></i>
                Save
            </asp:LinkButton>
        </div>
    </div>
    <!-- END add message body modal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        function btnAddMsg_validation() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;
            if (validateText('txtMsg', 1, 100, 'Enter a Message Body') == false) return false;
            return true;
        }
        function btnSend_validation() {
            if (validateCombo('dlSMSTemplate', "0", 'Select a SMS Template') == false) return false;
            if (validateText('txtMsgBody', 1, 1000, 'Enter a Message Body') == false) return false;
            return true;
        }
        function btnClear_Clear() {
            $('#dlSMSTemplate').val('0');
            $('#txtMsg').val('');
        }
        function pageLoad() {
            $('#txtMsgBody').bind("keyup change", function () {
                $('#lblCharCount').text($(this).val().length);
            });
            $('#txtMsg').bind("keyup change", function () {
                $('#lblBWordCount').text($(this).val().length);
            });
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');            
        }
        function SavedSuccess() {
            showMessage('Saved successfully', 'success');           
        }
        function btnSearch_validation() {            ;
            if (validateCombo('dlBatch', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            var dlBatch = $('#dlBatch option:selected').text();
            if(dlBatch != 'All')
            {     
                if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
            }
            return true;
        }
        function btnSTDSearchN_validation() {
            if (validateCombo('dlBatchN', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlShiftN', "0", 'Select a Shift') == false) return false;
            var dlBatch = $('#dlBatchN option:selected').text();
            if (dlBatch != 'All') {
                if (validateCombo('dlSectionN', "0", 'Select a Section') == false) return false;
            }
            return true;
        }
        function btnESearchN_validation(){
            if (validateCombo('dlDeptN', "0", 'Select a Department Name') == false) return false;
            if (validateCombo('dlDesignationN', "0", 'Select a Designation') == false) return false;
            if (validateCombo('dlEshiftN', "0", 'Select a Shift') == false) return false;
            return true;
        }
        function CheckAll(oCheckbox, gridID) {
            var GridView = document.getElementById(gridID);
            $('#contactId').css({ border: '2px solid #1A4C1A' });
            for (i = 1; i < GridView.rows.length; i++) {
                GridView.rows[i].cells[0].getElementsByTagName("input")[0].checked = oCheckbox.checked;
            }
        }
        function singleChk(oCheckbox, gridID) {
            var GridView = document.getElementById(gridID);
            var inputList = GridView.getElementsByTagName("input");
            $('#contactId').css({ border: '2px solid #1A4C1A' });
            if (oCheckbox.checked == false) {
                inputList[0].checked = false;
            }
            else {
                for (var i = 0; i < inputList.length; i++) {
                    var headerCheckBox = inputList[0];
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;
            }
        }        
    </script>
</asp:Content>
