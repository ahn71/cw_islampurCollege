<%@ Page Title="Department Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDepartment.aspx.cs" Inherits="DS.Admin.AddDepartment" %>

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
    <asp:HiddenField ID="lblDepartmentId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Department List</p>
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
                        <div id="divDepartmentList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanelHead">Add Department</div>
                        <table class="tbl-controlPanel">                            
                            <tr>
                                <td>
                                    Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDepartment" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    Status
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkStatus" ClientIDMode="Static" runat="server" CssClass="chkStatusDp" Checked="True" />                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IsTeacher
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkIsTeacher" ClientIDMode="Static" runat="server" Checked="True" />
                                </td>                                
                            </tr>
                        </table>
                        <div class="buttonBox">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static" 
                                OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
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
            if (validateText('txtDepartment', 1, 50, 'Enter Department Name') == false) return false;
            return true;
        }
        function editDepartment(Did) {
            $('#lblDepartmentId').val(Did);
            var strDepartment = $('#r_' + Did + ' td:first-child').html();
            $('#txtDepartment').val(strDepartment);
            var strS = $('#r_' + Did + ' td:nth-child(2)').html();
            var strIsTeacher = $('#r_' + Did + ' td:nth-child(3)').html();
            if (strS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            if (strIsTeacher == 'True') {
                $("#chkIsTeacher").click();
            }
            else {
                $("#chkIsTeacher").removeProp('checked');
            }

            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
