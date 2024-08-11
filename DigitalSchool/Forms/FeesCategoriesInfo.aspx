<%@ Page Title="Fee Category" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeesCategoriesInfo.aspx.cs" Inherits="DS.Forms.FeesCategoriesInfo" %>
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
    <asp:HiddenField ID="lblFeesCateId" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <p class="text-right">Fee Category Information</p>
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
                                <div id="divFeesCategoryList" class="datatables_wrapper" runat="server"
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Category</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Batch Name
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlBatchName" ClientIDMode="Static" runat="server" Height="26px" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fee Category
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesCatName" runat="server" Width="250px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Fee Fine
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFeesFine" runat="server" Width="250px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
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
            if (validateText('txtAmount', 1, 20, 'Type Amount') == false) return false;
            else if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            return true;
        }
        function editFeesCategory(feesCatId) {
            $('#lblFeesCateId').val(feesCatId);
            var strBatch = $('#r_' + feesCatId + ' td:nth-child(1)').html();
            var strFeesCatName = $('#r_' + feesCatId + ' td:nth-child(4)').html();
            var strFine = $('#r_' + feesCatId + ' td:nth-child(3)').html();
            $('#dlBatchName').val(strBatch);
            $('#txtFeesCatName').val(strFeesCatName);
            $('#txtFeesFine').val(strFine);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblFeesCateId').val('');
            $('#txtFeesCatName').val('');
            $('#txtFeesFine').val('');
            setFocus('txtFeesCatName');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
