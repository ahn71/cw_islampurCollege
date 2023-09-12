<%@ Page Title="Add Fees" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddFeesType.aspx.cs" Inherits="DigitalSchool.Admin.AddFeesType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

<div class="main-div">

        <div class="leftSide-div">

              <div class="tgPanel">
                <div class="tgPanelHead">Add Fees</div>

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
                             Type
                        </td>
                        <td>
                            <asp:TextBox ID="txtFeesType" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
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
                    <asp:Button ID="btnSave" style="margin-top:30px;"  CssClass="greenBtn" runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" /> 
                        <input id="tnReset" type="button" value="Clear" class="blueBtn" onclick="clearAll();" />
                    </div>
            </div>
            
        </div>

<div class="rightSide-div">

             <div style="text-align: center; font-weight: 700; font-size:16px">Fees Type Details</div>
            <br />
          <asp:GridView ID="gvFeesType" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanged="gvFeesType_SelectedIndexChanged" > 
              <AlternatingRowStyle BackColor="#CCCCCC" />
           <Columns>
               <asp:CommandField  ItemStyle-Width="20px" ItemStyle-Height="25px" HeaderText="Edit" NewImageUrl="~/Images/datatables/edit.png" SelectText="Edit" ShowSelectButton="True">
               </asp:CommandField>
               <asp:BoundField HeaderText="Id" DataField="FId" ItemStyle-Width="60px" >
                <ItemStyle Width="60px"></ItemStyle>
               </asp:BoundField>
               <asp:BoundField HeaderText="Fees Type" DataField="FType" ItemStyle-Width="200px" >
                <ItemStyle Width="200px"></ItemStyle>
               </asp:BoundField>
           </Columns>

          </asp:GridView>
        </div>


    </div>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtFeesType', 1, 60, 'Enter a Fees Type') == false) return false;
            return true;
        }

        function clearAll() {
            $('#txtFeesType').val('');
            $("#btnSave").val('Save');
        }

    </script>

</asp:Content>
