<%@ Page Title="Add Board" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBoard.aspx.cs" Inherits="DS.Admin.AddBoard" %>
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
    <asp:HiddenField ID="lblBoardId" ClientIDMode="Static" Value="" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Board List</p>
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
                        <div id="divBoard" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Board</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Board Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBoardName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
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
            if (validateText('txtBoardName', 1, 50, 'Enter a Board Name') == false) return false;
            return true;
        }
        function editBoards(BoardId) {
            $('#lblBoardId').val(BoardId);
            var strBoardName = $('#r_' + BoardId + ' td:first-child').html();
            $('#txtBoardName').val(strBoardName);
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
