<%@ Page Title="Off Days Setting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OffDaysSet.aspx.cs" Inherits="DS.Forms.OffDaysSet" %>
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
    <asp:HiddenField ID="hfOffDateId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hfStatus" ClientIDMode="Static" runat="server" Value="Save" />
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <p class="text-right">Off Day List</p>
                    </div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="divOffDayList" class="datatables_wrapper" runat="server"
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Off Day Setting</div>
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
                                        <asp:TextBox ID="txtPurpose" runat="server" Width="250px" ClientIDMode="Static" CssClass="input" Height="67px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                            </div>
                        </div>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Weekend Generator</div>
                            <div style="float: left">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" style="margin-top: -23px;">
                                    <ProgressTemplate>
                                        <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                            <p style="margin-top: 39px">Wait&nbsp; processing</p>
                                        </span>
                                        <img style="width: 26px; height: 26px; cursor: pointer; float: left; margin-top: 39px; margin-left: 7px;" src="/images/wait.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <br>
                            </div>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnFridayGenerate" ClientIDMode="Static" runat="server" Text="Generate"
                                    OnClick="btnFridayGenerate_Click" />
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
            if (validateText('txtDate', 1, 20, 'Select off date') == false) return false;
            else if (validateText('txtPurpose', 1, 90, 'Type purpose') == false) return false;
            return true;
        }
        function editOffDate(offDateId) {
            $('#hfOffDateId').val(offDateId);
            var strOffDate = $('#r_' + offDateId + ' td:nth-child(1)').html();
            var strPurpos = $('#r_' + offDateId + ' td:nth-child(2)').html();
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
