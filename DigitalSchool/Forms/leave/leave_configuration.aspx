<%@ Page Title="Leave Configuration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="leave_configuration.aspx.cs" Inherits="DS.Forms.leave.leave_configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
        .controlLength {
            width: 200px;
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
            <div class="col-md-6">
                <p class="text-right">Leave Configuration List</p>
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
                        <div id="divLeaveConfigList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Leave Configuration</div>
                            <asp:HiddenField ID="hdLeaveId" ClientIDMode="Static" runat="server" Value="" />
                            <table class="tbl-controlPanel">
                                <tbody>
                                    <tr>
                                        <td>Leave Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlLeaveTypes" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                                <asp:ListItem Value="s">Select</asp:ListItem>
                                                <asp:ListItem Value="c/l">Casula Leave</asp:ListItem>
                                                <asp:ListItem Value="s/l">Sick Leave</asp:ListItem>
                                                <asp:ListItem Value="m/l">Maternity Leave</asp:ListItem>
                                                <asp:ListItem Value="o/l">Others Leave</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Leave Days
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtLeaveDays" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Leave Nature
                                        </td>
                                        <td>

                                            <asp:TextBox ID="txtLeaveNature" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                            <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="save"
                                                ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtLeaveNature" ErrorMessage="*">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chkDeductionAllowed" CssClass="" Text="Is Deduction Allowed" runat="server" ClientIDMode="Static" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                                OnClientClick="return validateInputs();" Style="margin-left: 80px;" OnClick="btnSave_Click" />
                                            <input id="tnReset" type="reset" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br />
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
    </script>
</asp:Content>
