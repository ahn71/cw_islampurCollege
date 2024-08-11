<%@ Page Title="Template" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="DS.Forms.sms.Template" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
        .controlLength{
            width: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblTId" ClientIDMode="Static" Value="0" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Add Template</p>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <div class="row">
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divTemplate" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Template</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Title
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTitel" runat="server" ClientIDMode="Static"
                                            CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Body
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBody" runat="server" TextMode="MultiLine" Rows="10"
                                            Height="200px" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server"
                                    Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <asp:Button CssClass="btn btn-default" ID="btnReset" ClientIDMode="Static" runat="server"
                                    Text="Reset" OnClick="btnReset_Click" />
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
            if (validateText('txtTitel', 1, 50, 'Enter a Titel') == false) return false;
            return true;
        }
        function editTemplate(TId) {
            $('#lblTId').val(TId);
            var strTemplate = $('#r_' + TId + ' td:first-child').html();
            $('#txtTitel').val(strTemplate);
            var strBody = $('#r_' + TId + ' td:nth-child(2)').html();
            $('#txtBody').val(strBody);
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>

