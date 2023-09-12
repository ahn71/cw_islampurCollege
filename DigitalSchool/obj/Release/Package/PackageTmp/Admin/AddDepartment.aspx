<%@ Page Title="Department Info" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddDepartment.aspx.cs" Inherits="DigitalSchool.Admin.AddDepartment" %>
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
                <div class="tgPanelHead">Add Department</div>

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
                            <asp:TextBox ID="txtDepartment" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
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
                           <label for="chkStatus"> Status </label> 
                        </td>
                        <td>                           
                            <asp:CheckBox ID="chkStatus" runat="server" Checked="True" />
                        </td>
                    </tr>
                </table>
                  <div class="buttonBox">
                    <asp:Button ID="btnSave" style="margin-top:30px;"  CssClass="greenBtn" runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" /> 
                        <input id="tnReset" type="reset" value="Reset" class="blueBtn" />
                    </div>
            </div>
            
        </div>

        <div class="rightSide-div">
            <div><h1>Department List</h1></div>
          <asp:GridView ID="gvDepartment" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanged="gvDepartment_SelectedIndexChanged" > 
              <AlternatingRowStyle BackColor="#CCCCCC" />
           <Columns>
               <asp:CommandField  ItemStyle-Width="20px" ItemStyle-Height="25px" HeaderText="Edit" NewImageUrl="~/Images/datatables/edit.png" SelectText="Edit" ShowSelectButton="True">
                <ItemStyle Height="25px" Width="20px"></ItemStyle>
               </asp:CommandField>
               <asp:BoundField HeaderText="Id" DataField="DId" ItemStyle-Width="60px" >
                <ItemStyle Width="60px"></ItemStyle>
               </asp:BoundField>
               <asp:BoundField HeaderText="Name" DataField="DName" ItemStyle-Width="200px" >
                <ItemStyle Width="200px"></ItemStyle>
               </asp:BoundField>
               <asp:BoundField DataField="DStatus" HeaderText="Status" />
           </Columns>

          </asp:GridView>
        </div>


    </div>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtDepartment', 1, 30, 'Enter Department Name') == false) return false;
            return true;
        }
    </script>


</asp:Content>
