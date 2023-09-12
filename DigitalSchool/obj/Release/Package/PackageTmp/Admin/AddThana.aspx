<%@ Page Title="Thana/Upazala Info" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="AddThana.aspx.cs" Inherits="DigitalSchool.Admin.AddThana" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script> 
        <link href="/Styles/gridview.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

<div class="main-div">

        <div class="leftSide-div">

              <div class="tgPanel">
                <div class="tgPanelHead">Add Thana/Upazila</div>

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
                          Select District
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="dlDistrict" CssClass="ddlBox"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="height:10px"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                           Thana/Upazila
                        </td>
                        <td>
                           <asp:TextBox ID="txtThana" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                        </td>
                    </tr>
                    
                </table>
                  <div class="buttonBox">
                    <asp:Button ID="btnSave" style="margin-top:30px;"  CssClass="greenBtn" runat="server" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSave_Click"/> 
                    <input id="tnReset" type="reset" value="Reset" class="blueBtn" />
                    </div>
            </div>
            
        </div>

 <div class="rightSide-div">

          <asp:GridView ID="gvThana" runat="server" Width="100%" AllowSorting="True" AutoGenerateColumns="False" PageSize="50" OnSelectedIndexChanged="gvThana_SelectedIndexChanged">
              <Columns>
                  <asp:CommandField HeaderText="Edit" SelectText="Edit" ShowSelectButton="True">
                  <ItemStyle Width="60px" />
                  </asp:CommandField>
                  <asp:BoundField HeaderText="Id" DataField="ThanaId" ItemStyle-Width="5%" >
                    <ItemStyle Width="5%"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="Thana" DataField="ThanaName" ItemStyle-Width="20%" >
                    <ItemStyle Width="40%"></ItemStyle>
                  </asp:BoundField>
                  <asp:BoundField HeaderText="District" DataField="DistrictName" ItemStyle-Width="16%" >
                    <ItemStyle Width="40%"></ItemStyle>
                  </asp:BoundField>

              </Columns>

          </asp:GridView>
        </div>


    </div>


<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtThana', 1, 30, 'Enter a Thana/Upazila Name') == false) return false;
            return true;
        }
    </script>


</asp:Content>
