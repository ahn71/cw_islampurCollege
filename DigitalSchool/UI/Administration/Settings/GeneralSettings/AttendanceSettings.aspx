<%@ Page Title="Attendance Settings" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AttendanceSettings.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.AttendanceSettings" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
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
                <li><a runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">Absent Fine Settings</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
         <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Absent Fine Details</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
                 </div>                
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row"> 
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div runat="server" id="divAbsentFineList" class="datatables_wrapper"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                        <asp:HiddenField ID="lblAbsentId" ClientIDMode="Static" Value="0" runat="server" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveFineAmount" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>           
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Absent Fine Settings</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                               
                                <tr>
                                    <td>Amount
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox runat="server" ID="txtAbsentFineAmount" ClientIDMode="Static" CssClass="input"
                                                    Width="176px" Visible="true"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="F1" runat="server" FilterType="Numbers" TargetControlID="txtAbsentFineAmount" ValidChars=""></asp:FilteredTextBoxExtender>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="chkAbsentFineCount" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                 <tr>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="chkAbsentFineCount" ClientIDMode="Static" 
                                           Checked="true"  />
                                        <label for="chkAbsentFineCount">Absent Fine Count</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnSaveFineAmount" OnClick="btnSaveFineAmount_Click"
                                          OnClientClick="return validateInputs();"   Text="Save" ClientIDMode="Static" CssClass="btn btn-primary" />
                                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear"
                                            ClientIDMode="Static" CssClass="btn btn-default" />
                                    </td>
                                </tr>
                            </table>
                            <br />
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
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblParticularCategory', '');
            });
            $('#tblParticularCategory').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblParticularCategory').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function editAbsentAmount(absId) {
            $('#lblAbsentId').val(absId);

            var strAbsentAmount = $('#r_' + absId + ' td:first-child').html();

            $('#txtAbsentFineAmount').val(strAbsentAmount);
            var status = $('#r_' + absId + ' td:nth-child(3)').html();
            if (status == "Yes") {
                $('#chkAbsentFineCount').removeProp('checked');
                $('#chkAbsentFineCount').click();

            }
            else $('#chkAbsentFineCount').removeProp('checked');
            $("#btnSaveFineAmount").val('Update');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function saveSuccess() {
            loaddatatable();
            showMessage('Save successfully', 'success');
            clearIt();
        }
        function validateInputs() {
            if (validateText('txtAbsentFineAmount', 1, 50, 'Enter Fine Amount') == false) return false;
            return true;
        }
    </script>
</asp:Content>
