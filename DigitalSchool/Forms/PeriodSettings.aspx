<%@ Page Title="Period Settings" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PeriodSettings.aspx.cs" Inherits="DS.Forms.PeriodSettings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblPeriod" ClientIDMode="Static" Value="" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Period List</p>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divPeriod" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Period</div>
                            <table class="tbl-controlPanel">                                
                                <tr>
                                    <td>
                                        Period Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Start Time
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlHours" ClientIDMode="Static" Width="130px" CssClass="dropDownListRoutine"></asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="dlMinute" ClientIDMode="Static" Width="128px" CssClass="dropDownListRoutine"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        End Time
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlHoursEndTime" ClientIDMode="Static" Width="130px" CssClass="dropDownListRoutine"></asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="dlMinuteEndTime" ClientIDMode="Static" Width="128px" CssClass="dropDownListRoutine"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input id="tnReset" type="reset" value="Reset" class="btn btn-default" />
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
        function validateInputs() {
            if (validateText('txtPeriodName', 1, 50, 'Enter a Period Name') == false) return false;
            if (validateText('txtStartTime', 1, 50, 'Enter a Start Time') == false) return false;
            if (validateText('txtEndTime', 1, 50, 'Enter a End Time') == false) return false;
            return true;
        }
        function editPeriod(PeriodId) {
            $('#lblPeriod').val(PeriodId);
            var strPeriodName = $('#r_' + PeriodId + ' td:first-child').html();
            $('#txtPeriodName').val(strPeriodName);
            var strStartTime = $('#r_' + PeriodId + ' td:nth(1)').html();
            $('#dlHours').val(strStartTime.substr(0, 2));
            $('#dlMinute').val(strStartTime.substr(3, 2));
            var strEndTime = $('#r_' + PeriodId + ' td:nth(2)').html();
            $('#dlHoursEndTime').val(strEndTime.substr(0, 2));
            $('#dlMinuteEndTime').val(strEndTime.substr(3, 2));
            $("#btnSave").val('Update');
        }
        function clearInputBox() {
            $('#txtPeriodName').val('');
        }
    </script>
</asp:Content>
