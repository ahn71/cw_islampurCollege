<%@ Page Title="Add Class" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="DS.Admin.AddClass" %>
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
    <asp:HiddenField ID="lblClassId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfClassName" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Class List</p>
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
                        <div id="divClassList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add CLASS</div>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Class Name
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Order
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrder" runat="server" Width="135px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <div class="buttonBox">
                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                            runat="server" Text="Save" OnClick="btnSave_Click" />
                        <input type="button" value="Clear" class="btn btn-default" onclick="clearIt();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtClassName', 1, 50, 'Enter a Class Name') == false) return false;
            if (validateText('txtOrder', 1, 15, 'Type Order') == false) return false;
            return true;
        }
        function editClass(clsId) {
            $('#lblClassId').val(clsId);

            var strClass = $('#r_' + clsId + ' td:first-child').html();
            var strOrder = $('#r_' + clsId + ' td:nth-child(2)').html();
            $('#hfClassName').val(strClass);
            $('#txtClassName').val(strClass);
            $('#txtOrder').val(strOrder);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#hfClassName').val('');
            $('#lblClassId').val('');
            $('#txtClassName').val('');
            $('#txtOrder').val('');
            setFocus('txtClassName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>

