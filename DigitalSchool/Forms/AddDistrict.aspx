<%@ Page Title="District Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDistrict.aspx.cs" Inherits="DS.Admin.AddDistrict" %>
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
    <asp:HiddenField ID="lblDistrictId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">District List</p>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveDistrict" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divDistrictList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 300px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add District</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>District Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDistrict" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSaveDistrict" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                    OnClientClick="return validateInputs();" OnClick="btnSaveDistrict_Click" />
                                <input id="tnReset" type="reset" value="Reset" class="btn btn-default" />
                            </div>
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
            if (validateText('txtDistrict', 1, 30, 'Enter a District Name') == false) return false;
            return true;
        }
        function editDistrict(districtId) {

            $('#lblDistrictId').val(districtId);

            var strDistrict = $('#r_' + districtId + ' td:first-child').html();
            $('#txtDistrict').val(strDistrict);
            $("#btnSaveDistrict").val('Update');
        }
        function clearIt() {
            $('#lblDistrictId').val('');
            $('#txtDistrict').val('');
            setFocus('txtDistrict');
            $("#btnSaveDistrict").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function acceptValidCharacter(e, targetInput) {
            try {
                if (e.keyCode != 65) {
                    if (e.keyCode != 80) {
                        if (e.keyCode != 8) {
                            alert(e.keyCode);
                            $('#' + targetInput.id).val('');
                        }
                    }
                }
            }
            catch (e) {
            }
        }
    </script>
</asp:Content>

