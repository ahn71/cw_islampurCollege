<%@ Page Title="Salary Allowance Type" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalaryAllowanceType.aspx.cs" Inherits="DS.Admin.SalaryAllowanceType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblAId" ClientIDMode="Static" Value="" runat="server" />
    <asp:HiddenField ID="lblOldPercentage" ClientIDMode="Static" Value="" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Allowance Type Details</p>
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
                        <div id="divAllowanceType" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Salary Allowance Type</div>
                    <table class="tbl-controlPanel">                        
                        <tr>
                            <td>
                                Allowance Type
                            </td>
                            <td>
                                <asp:TextBox ID="txtAllowanceTyppe" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Percentage
                            </td>
                            <td>
                                <asp:TextBox ID="txtPercentage" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                            </td>
                        </tr>                        
                        <tr>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="chkStatus" runat="server" Text="Is Active" ClientIDMode="Static" Checked="True" />                                
                            </td>
                        </tr>
                    </table>
                    <div class="buttonBox">
                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                    </div>
                </div>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('#txtAllowanceTyppe', 1, 50, 'Enter Allowance Type') == false) return false;
            if (validateText('#txtPercentage', 1, 30, 'Enter Percentage') == false) return false;
            return true;
        }
        function editEmployee(empId) {
            $('#lblAId').val(empId);
            var strAT = $('#r_' + empId + ' td:first-child').html();
            $('#txtAllowanceTyppe').val(strAT);

            var strP = $('#r_' + empId + ' td:nth-child(2)').html();
            $('#txtPercentage').val(strP);
            $('#lblOldPercentage').val(strP);
            $("#btnSave").val('Update');
            var strAS = $('#r_' + empId + ' td:nth-child(3)').html();
            if (strAS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            var n = $("#chkStatus:checked").length;
            if (n == 0) {
                $('#chkStatus').click();
            }
            $('#lblAId').val('');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            showMessage('Saved successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
