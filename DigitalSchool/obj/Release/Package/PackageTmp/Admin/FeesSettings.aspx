<%@ Page Title="Fees Settings" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="FeesSettings.aspx.cs" Inherits="DigitalSchool.Admin.FeesSettings" %>
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
                <div class="tgPanelHead">Fees Settings</div>

                <table class="tbl-controlPanel">
                    <tr>
                        <td>
                            Select Class</td>
                        <td>
                            <asp:DropDownList ID="ddlClassName" runat="server" Height="26px" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Select Fees Type</td>
                        <td>
                            <asp:DropDownList ID="ddlFessType" runat="server" Height="26px" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Amount</td>
                        <td>
                           <asp:TextBox ID="txtAmount" runat="server" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                  <div class="buttonBox">
                    <asp:Button  CssClass="greenBtn" ID="btnSave" style="margin-top:30px;"  runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" /> 
                      <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="greenBtn" OnClick="btnClear_Click"/>
                    </div>
            </div>
            
        </div>

<div class="rightSide-div">

             <div style="text-align: center; font-weight: 700; font-size:16px">Fees Settings Details</div>
            <br />
          <asp:GridView ID="gvFeesSettings" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="gvFeesSettings_SelectedIndexChanged">
              <Columns>
                  <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                  <ItemStyle Width="60px" />
                  </asp:CommandField>
                  <asp:BoundField HeaderText="ID" DataField="FSId" ItemStyle-Width="5%" >
                    <ItemStyle Width="70px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Class Name" DataField="ClassName" ItemStyle-Width="20%" >

                    <ItemStyle Width="400px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Fees Type" DataField="FType" ItemStyle-Width="20%" >

                    <ItemStyle Width="400px"></ItemStyle>
                  </asp:BoundField>
                   <asp:BoundField HeaderText="Amount" DataField="FAmount" ItemStyle-Width="20%" >

                    <ItemStyle Width="400px"></ItemStyle>
                  </asp:BoundField>

              </Columns>

          </asp:GridView>
        </div>


    </div>




<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtAmount', 1, 20, 'Type Amount') == false) return false;
            else if (validateText('ddlClassName.Text', 1, 20, 'Select Class') == false) return false;
            return true;
        }
    </script>

</asp:Content>
