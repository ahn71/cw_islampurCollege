<%@ Page Title="Add Fees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddParticular.aspx.cs" Inherits="DS.Admin.AddFeesType" %>
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
    <asp:HiddenField ID="lblFId" ClientIDMode="Static" Value="" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Particulars Information</p>
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
                        <div id="divFeesType" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Particulars</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesType" runat="server" Width="318px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" value="Clear" class="btn btn-default" onclick="clearAll();" />
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
            if (validateText('txtFeesType', 1, 100, 'Enter a Fee Type') == false) return false;
            return true;
        }
        function editFeesType(Fid) {
            $('#lblFId').val(Fid);
            var strFT = $('#r_' + Fid + ' td:first-child').html();
            $('#txtFeesType').val(strFT);
            $("#btnSave").val('Update');
        }
        function clearAll() {
            $('#lblFId').val('');
            $('#txtFeesType').val('');
            setFocus('txtFeesType');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>

