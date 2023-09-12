<%@ Page Title="Add Designation" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddDesignation.aspx.cs" Inherits="DigitalSchool.Admin.AddDsignation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="/Styles/gridview.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

    <asp:HiddenField ID="lblDesignationId" ClientIDMode="Static" runat="server"/>

<div class="main-div">

        <div class="leftSide-div">

              <div class="tgPanel">
                <div class="tgPanelHead">Add Designation</div>

                <table class="tbl-controlPanel">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                             Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtDes_Name" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
                  <div class="buttonBox">
                    <asp:Button ID="btnSave" style="margin-top:30px;"  ClientIDMode="Static" CssClass="greenBtn" runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" /> 
                        &nbsp;<input type="button" class="blackBtn" value="Clear" onclick="clearIt();" />
                    </div>
            </div>
            
        </div>



<div class="rightSide-div" style="width:600px;">

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSave" />
</Triggers>
<ContentTemplate>
    <div id="divDesignationList" class="datatables_wrapper" runat="server" style="width:100%; height:auto; max-height:500px;overflow:auto;overflow-x:hidden;"></div>
</ContentTemplate>
 </asp:UpdatePanel>

 </div>

</div>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtDes_Name', 1, 30, 'Enter Designation Name') == false) return false;
            return true;
        }

        function editEmployee(empId) {

            $('#lblDesignationId').val(empId);

            var strDesName = $('#r_' + empId + ' td:first-child').html();
            $('#txtDes_Name').val(strDesName);
            $("#btnSave").val('Update');
        }

        function clearIt()
        {
            $('#lblDesignationId').val('');
            $('#txtDes_Name').val('');
            setFocus('txtDes_Name');
            $("#btnSave").val('Save');
        }
        function updateSuccess()
        {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>


</asp:Content>
