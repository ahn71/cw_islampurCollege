<%@ Page Title="Add Board" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddBoard.aspx.cs" Inherits="DigitalSchool.Admin.AddBoard" %>
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
                <div class="tgPanelHead">ADD BOARD</div>

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
                           Board Name
                        </td>
                        <td>
                           <asp:TextBox ID="txtBoardName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                  <div class="buttonBox">
                    <asp:Button  CssClass="greenBtn" ID="btnSave" style="margin-top:30px;"  runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click" /> 
                    <input id="tnReset" type="reset" value="Reset" class="blueBtn" />
                    </div>
            </div>
            
        </div>

        <div class="rightSide-div">
          <asp:GridView ID="gvBoard" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="gvBoard_SelectedIndexChanged">
              <Columns>
                  <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                  <ItemStyle Width="60px" />
                  </asp:CommandField>
                  <asp:BoundField HeaderText="Id" DataField="BoardId" ItemStyle-Width="5%" >
                    <ItemStyle Width="70px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Board" DataField="BoardName" ItemStyle-Width="20%" >

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
            if (validateText('txtBoardName', 1, 20, 'Enter a Board Name') == false) return false;
            return true;
        }
    </script>


</asp:Content>
