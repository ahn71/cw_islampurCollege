<%@ Page Title="District Info" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddDistrict.aspx.cs" Inherits="DigitalSchool.Admin.AddDistrict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/gridview.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

  <asp:HiddenField ID="lblDistrictId" ClientIDMode="Static" runat="server"/>

<div class="main-div">

        <div class="leftSide-div">

              <div class="tgPanel">
                <div class="tgPanelHead">Add District</div>

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
                            District Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtDistrict" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
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
                    <asp:Button ID="btnSaveDistrict" style="margin-top:30px;"  CssClass="greenBtn" runat="server" ClientIDMode="Static" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSaveDistrict_Click"/> 
                        <input id="tnReset" type="reset" value="Reset" class="blueBtn" />
                    </div>
            </div>
            
        </div>

<div class="rightSide-div">



<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

<Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSaveDistrict" />
</Triggers>
<ContentTemplate>
    <div id="divDistrictList" class="datatables_wrapper" runat="server" style="width:100%; height:auto; max-height:500px;overflow:auto;overflow-x:hidden;"></div>
</ContentTemplate>

</asp:UpdatePanel>

        
</div>

</div>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


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

    </script>

</asp:Content>
