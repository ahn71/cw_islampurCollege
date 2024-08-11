<%@ Page Title="Register User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DS.Admin.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <link href="../Styles/reg_style.css" rel="stylesheet" />
     <style type="text/css">
        input[type="password"]
        {
            margin-bottom:8px;
        }
        .tgPanel
        {
            width:500px;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgPanelHead">New Use</div>  
        <div class="main_box">
            <table>
                <tr>
                    <td style="width: 115px;">Title</td>
                    <td style="width: 30px;">:</td>
                    <td>
                        <asp:TextBox ID="txtTitle" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>First Name</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>Middle Name</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtMiddleName" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>Last Name</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Initial</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtInitial" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>User Name</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>User Password</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtUserPassword" ClientIDMode="Static" Style="width: 200px;" runat="server" TextMode="Password" class="SRegpass"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" class="RegInput"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>User Type</td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlUserType" runat="server" class="RegInput" ClientIDMode="Static">
                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                            <asp:ListItem>Super_Admin</asp:ListItem>
                            <asp:ListItem>Admin</asp:ListItem>
                            <asp:ListItem>Teacher</asp:ListItem>
                            <asp:ListItem>Accountant</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td colspan="2">
                        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Save" 
                            OnClientClick="return validateInputs();" OnClick="btnSaveUserAccount_Click" />
                        <input type="reset" value="Reset" class="btn btn-default" />
                    </td>
                </tr>
            </table>

        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;
            if (validateText('txtFirstName', 1, 100, 'Enter a First Name') == false) return false;
            if (validateText('txtMiddleName', 1, 100, 'Enter a Middle Name') == false) return false;
            if (validateText('txtLastName', 1, 100, 'Enter a Last Name') == false) return false;
            if (validateText('txtInitial', 1, 100, 'Enter a Initial') == false) return false;
            if (validateText('txtUserName', 1, 100, 'Enter a User Name') == false) return false;
            if (validateText('txtUserPassword', 1, 100, 'Enter a User Password') == false) return false;
            if (validateText('txtEmail', 1, 100, 'Enter a Email') == false) return false;
            if (validateCombo('ddlUserType', "0", 'Select User Type') == false) return false;
            return true;
        }
    </script>
</asp:Content>
