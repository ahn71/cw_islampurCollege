<%@ Page Title="Batch Assign" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateBatch.aspx.cs" Inherits="DS.Forms.CreateBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 450px;
        }        
        table.display td:first-child {
            border-left:1px solid #e7e7e7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblFeesSettId" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <table class="col-md-12">
                            <tr>
                                <td></td>
                                <td>
                                    <p class="text-right">Batch List</p>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-4">
                                <div id="divBatchList" class="datatables_wrapper" runat="server"
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="tgPanel">
                                    <div class="tgPanelHead">Create Batch</div>
                                    <table class="tbl-controlPanel">
                                        <tr>
                                            <td>Class
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlClassName" ClientIDMode="Static" runat="server" Height="26px" Width="293px">
                                                    <asp:ListItem Selected="True">---Select Class---</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Session
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSession" ClientIDMode="Static" runat="server" Height="26px" Width="293px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>New Batch
                                            </td>
                                            <td>
                                                <asp:Label ID="lblbatch" ClientIDMode="Static" runat="server" Style="color: green" Font-Size="18px"
                                                    Font-Bold="true" Width="293px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="buttonBox">
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlClassName").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlSession').val();
                var val3 = val1 + "" + val2;
                $('#lblbatch').html(val3);
            });
            $("#ddlSession").change(function () {
                var val1 = this.value;
                var val2 = $('#ddlClassName').val();
                var val3 = val2 + "" + val1;
                $('#lblbatch').html(val3);
            });
        });
        function validateInputs() {
            if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            else if (validateText('ddlSession.Text', 1, 20, 'Select Session') == false) return false;
            return true;
        }
        function clearIt() {
            $('#ddlClassName').val('');
            $('#ddlSession').val('');
            $('#lblbatch').val('');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>

