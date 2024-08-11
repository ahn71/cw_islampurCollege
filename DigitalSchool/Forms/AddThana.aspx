<%@ Page Title="Thana/Upazala Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddThana.aspx.cs" Inherits="DS.Admin.AddThana" %>
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
    <asp:HiddenField ID="lblThanaId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Thana/Upazila List</p>
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
                        <div id="divThanaList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel runat="server" ID="upThana" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Thana/Upazila</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Select District
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="dlDistrict" ClientIDMode="Static" CssClass="ddlBox"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Thana/Upazila
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtThana" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
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
            if (validateText('txtThana', 1, 30, 'Enter a Thana/Upazila Name') == false) return false;
            return true;
        }
        function editThana(thanaId) {
            $('#lblThanaId').val(thanaId);
            var strThana = $('#r_' + thanaId + ' td:first-child').html();
            var strDistrict = $('#r_' + thanaId + ' td:nth-child(2)').html();
            $('#txtThana').val(strThana);
            $('#dlDistrict').val(strDistrict);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblThanaId').val('');
            $('#txtThana').val('');
            setFocus('txtThana');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>

