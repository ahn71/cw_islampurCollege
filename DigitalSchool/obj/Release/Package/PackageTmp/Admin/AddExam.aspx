<%@ Page Title="Add Exam" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddExam.aspx.cs" Inherits="DigitalSchool.Admin.AddExam" %>
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
                <div class="tgPanelHead">ADD Exam</div>

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
                           Exam Name
                        </td>
                        <td>
                           <asp:TextBox ID="txtEx_Name" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                  <div class="buttonBox">
                    <asp:Button  CssClass="greenBtn" ID="btnSave" style="margin-top:30px;"  runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click"/> 
                      <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="greenBtn"/>
                    </div>
            </div>
            
        </div>

        <div class="rightSide-div">
           <div style="text-align: center; font-weight: 700; font-size:16px">Exam Details Information</div>
            <br />
          <asp:GridView ID="gvExam" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="gvExam_SelectedIndexChanged">
              <Columns>
                  <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                  <ItemStyle Width="60px" />
                  </asp:CommandField>
                  <asp:BoundField HeaderText="Id" DataField="ExId" ItemStyle-Width="5%" >
                    <ItemStyle Width="70px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Exam Name" DataField="ExName" ItemStyle-Width="20%" >

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
            if (validateText('txtEx_Name', 1, 50, 'Enter a Exam Name') == false) return false;
            return true;
        }
    </script>


</asp:Content>
