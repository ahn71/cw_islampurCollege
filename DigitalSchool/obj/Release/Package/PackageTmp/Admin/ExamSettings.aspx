<%@ Page Title="Exam Settings" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ExamSettings.aspx.cs" Inherits="DigitalSchool.Admin.ExamSettings" %>
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
                <div class="tgPanelHead">Exam Settings</div>

                <table class="tbl-controlPanel">
                    <tr>
                        <td>
                            Select Class</td>
                        <td>
                            <asp:DropDownList ID="dlClasses" runat="server" Height="26px" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Select Exam Type</td>
                        <td>
                            <asp:DropDownList ID="dlExam" runat="server" Height="26px" Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            Exam Fee</td>
                        <td>
                           <asp:TextBox ID="txtAmount" runat="server" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Exam Marks</td>
                        <td>
                           <asp:TextBox ID="txtEx_Marks" runat="server" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            Marks Convert to</td>
                        <td>
                           <asp:TextBox ID="txtEx_Marks_Convert_To" runat="server" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                  <div class="buttonBox">
                    <asp:Button  CssClass="greenBtn" ID="btnSave" style="margin-top:0px;"  runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click"/> 
                      <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="greenBtn" OnClick="btnClear_Click"/>
                    </div>
            </div>
            
        </div>

<div class="rightSide-div">

            <div style="text-align: center; font-weight: 700; font-size:16px">Exam Settings Details</div>
            <br />
          <asp:GridView ID="gvExamSettings" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="gvExamSettings_SelectedIndexChanged">
              <Columns>
                  <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                  <ItemStyle Width="60px" />
                  </asp:CommandField>
                  <asp:BoundField HeaderText="Id" DataField="EDId" ItemStyle-Width="5%" >
                    <ItemStyle Width="60px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Class Name" DataField="ClassName" ItemStyle-Width="20%" >

                    <ItemStyle Width="40px"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Exam Name" DataField="ExName" ItemStyle-Width="20%" >

                    <ItemStyle Width="40px"></ItemStyle>
                  </asp:BoundField>

                  <asp:BoundField DataField="ExFee" HeaderText="Exam Fee">
                  <ItemStyle Width="40px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ExMarks" HeaderText="Exam Marks">
                  <ItemStyle Width="60px" />
                  </asp:BoundField>
                  <asp:BoundField DataField="ExMarksConvertTo" HeaderText="Marks Convert To">
                  <ItemStyle Width="60px" />
                  </asp:BoundField>

              </Columns>

          </asp:GridView>
        </div>


    </div>




<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtAmount', 1, 30, 'Enter Exam Amount Taka') == false) return false;
            else if (validateText('txtEx_Marks', 1, 30, 'Enter Exam Marks') == false) return false;
            else if (validateText('txtEx_Marks_Convert_To', 1, 30, 'Enter Exam Marks Convert To') == false) return false;
            return true;
        }
    </script>
</asp:Content>
