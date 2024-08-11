<%@ Page Title="Off Day Settings" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="OffDaysSet.aspx.cs" Inherits="DS.UI.Academics.Attendance.Student.Manually.OffDaysSet" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfOffDateId" runat="server" ClientIDMode="Static"/>
    <asp:HiddenField ID="hfStatus" ClientIDMode="Static" runat="server" Value="Save"/>
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
                <li class="active">Off Day Settings</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnWeekendDaysGenerator" />
            <asp:AsyncPostBackTrigger ControlID="Sat"/>
            <asp:AsyncPostBackTrigger ControlID="Sun"/>
            <asp:AsyncPostBackTrigger ControlID="Mon"/>
            <asp:AsyncPostBackTrigger ControlID="Tue"/>
            <asp:AsyncPostBackTrigger ControlID="Wed"/>
            <asp:AsyncPostBackTrigger ControlID="Thu"/>
            <asp:AsyncPostBackTrigger ControlID="Fri"/>            
        </Triggers>
        <ContentTemplate>
            <div class="">
                <div class="row">
                    <div class="col-md-6">
                        <h4 class="text-right">Off Day List</h4>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div id="divOffDayList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 450px; overflow: auto; overflow-x: hidden;">
                        </div>                        
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Off Day Settings</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" ClientIDMode="Static" CssClass="input" Width="250px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" Format="dd-MM-yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Purpose</td>
                                    <td>
                                        <asp:TextBox ID="txtPurpose" runat="server" Width="250px" ClientIDMode="Static" CssClass="input"
                                             Height="67px" TextMode="MultiLine"></asp:TextBox>
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
                            <div class="tgPanelHead">Weekend Generator</div>                            
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnWeekendDaysGenerator" ClientIDMode="Static" runat="server" Text="Generator"
                                    OnClick="btnWeekendDaysGenerator_Click" UseSubmitBehavior="false" />                                
                                <span style="position: absolute">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="updatepanelFeesSett">
                                        <ProgressTemplate>                                         
                                            <img class="LoadingImg" src="../../../../AssetsNew/images/input-spinner.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- weekly days Modal -->
            <asp:ModalPopupExtender ID="showWeeklyDaysModal" runat="server" BehaviorID="modalpopup1" CancelControlID="Button4"
                OkControlID="LinkButton2"
                TargetControlID="button5" PopupControlID="showWeeklyDay" BackgroundCssClass="ModalPopupBG">
            </asp:ModalPopupExtender>
            <div id="showWeeklyDay" runat="server" style="display: none;" class="confirmationModal500">
                <div class="modal-header">
                    <button id="Button4" type="button" class="close white"></button>
                    <div class="tgPanelHead">Create Weekly Days</div>
                </div>
                <div class="modal-body">
                    <p class="text-danger text-center">Please make sure that those uncheck days are weekend</p>
                    <table class="tbl-controlPanel table table-bordered">
                        <tr>
                            <td style="text-align:center">Saturday</td>
                            <td>
                                <asp:CheckBox ID="Sat" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Sunday</td>
                            <td>
                                <asp:CheckBox ID="Sun" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Monday</td>
                            <td>
                                <asp:CheckBox ID="Mon" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static"
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Tuesday</td>
                            <td>
                                <asp:CheckBox ID="Tue" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Wednesday</td>
                            <td>
                                <asp:CheckBox ID="Wed" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Thursday</td>
                            <td>
                                <asp:CheckBox ID="Thu" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td style="text-align:center">Friday</td>
                            <td>
                                <asp:CheckBox ID="Fri" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static"
                                    AutoPostBack="true" /></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button id="button5" type="button" runat="server" style="display: none;" />
                    <asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                        <i class="icon-remove"></i>                                    
                        Close
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnConfirmGenerator" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false"
                        OnClick="btnConfirmGenerator_Click">
                        <i class="icon-ok"></i>
                        Please Confirm
                    </asp:LinkButton>
                </div>
            </div>
            <!-- END weekly days Modal -->
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtDate', 1, 20, 'Select off date') == false) return false;
            else if (validateText('txtPurpose', 1, 90, 'Type purpose') == false) return false;
            return true;
        }
        function editOffDate(offDateId) {
            $('#hfOffDateId').val(offDateId);
            var strOffDate = $('#r_' + offDateId + ' td:nth-child(1)').html();
            var strPurpos = $('#r_' + offDateId + ' td:nth-child(4)').html();
            $('#txtDate').val(strOffDate);
            $('#txtPurpose').val(strPurpos);
            $("#btnSave").val('Update');
            $("#hfStatus").val('Update');
        }
        function clearIt() {
            $('#hfOffDateId').val('');
            $('#txtDate').val('');
            $('#txtPurpose').val('');
            $("#btnSave").val('Save');
            $("#hfStatus").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SaveSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>