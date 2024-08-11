<%@ Page Title="Student Fine Setting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentFineSetting.aspx.cs" Inherits="DS.StudentFineSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSFSId" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="main-div">
                <div class="leftSide-div">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Student Fine Settings</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Fine Purpose</td>
                                <td>
                                    <asp:DropDownList ID="ddlFinePurpose" ClientIDMode="Static" runat="server" Height="26px" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Amount</td>
                                <td>
                                    <asp:TextBox ID="txtAmount" runat="server" Width="250px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div class="buttonBox">
                            <asp:Button CssClass="greenBtn" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save" 
                                OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                            <input type="button" value="Reset" class="blackBtn" onclick="clearIt();" />
                        </div>
                    </div>
                </div>
                <div class="rightSide-div">
                    <div style="text-align: center; font-weight: 700; font-size: 16px">Student Fine Settings</div>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divStudentFineSettings" class="datatables_wrapper" runat="server" 
                                style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtAmount', 1, 20, 'Type Amount') == false) return false;
            else if (validateText('ddlFinePurpose.Text', 1, 20, 'Select Fine Purpose') == false) return false;
            return true;
        }
        function editFineAmount(id) {
            $('#lblSFSId').val(id);
            var strFinePurpose = $('#r_' + id + ' td:nth-child(1)').html();
            $('#ddlFinePurpose').val(strFinePurpose);
            var strAmount = $('#r_' + id + ' td:nth-child(2)').html();
            $('#txtAmount').val(strAmount);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblSFSId').val('');
            $('#txtAmount').val('');
            setFocus('txtAmount');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
