<%@ Page Title="Register User" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DigitalSchool.Admin.CreateUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/oitlStyle.css" rel="stylesheet" />
    <link href="../Styles/reg_style.css" rel="stylesheet" />
     <style type="text/css">
        .auto-style2 {
            padding: 7px;
            width: 117px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

    <div class="box">
        	<h1>Salary Set<span class="edit_button"><a href="#"><img src="images/file-edit.png" alt="" /></a></span> </h1>
            <div class="main_box">
            	<table width="978" border="0">
                   
                    <tr>
                    <td class="auto-style2">Title</td>
                    <td class="level_col_2">:</td>
                    <td class="level_col_3">                     
                        <asp:TextBox ID="txtTitle" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td class="auto-style2"> First Name</td>
                    <td>:</td>
                    <td class="level_col_3">
                        <asp:TextBox ID="txtFirstName" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>

                    <tr>
                    <td class="auto-style2">Middle Name</td>
                    <td >:</td>
                    <td class="level_col_3">
                       <asp:TextBox ID="txtMiddleName" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>

                    <tr>
                    <td class="auto-style2">Last Name</td>
                    <td >:</td>
                    <td class="level_col_3">
                       <asp:TextBox ID="txtLastName" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>
                           <tr>
                    <td class="auto-style2">Initial</td>
                    <td >:</td>
                    <td class="level_col_3">
                      <asp:TextBox ID="txtInitial" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>
                     <tr>
                    <td class="auto-style2">User Name</td>
                    <td >:</td>
                    <td class="level_col_3">
                       <asp:TextBox ID="txtUserName" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>
                     <tr>
                    <td class="auto-style2">User Password</td>
                    <td >:</td>
                    <td class="level_col_3">
                       <asp:TextBox ID="txtUserPassword" ClientIDMode="Static" runat="server" TextMode="Password" class="input"></asp:TextBox>
                    </td>
                  </tr>
                     <tr>
                    <td class="auto-style2">Email</td>
                    <td >:</td>
                    <td class="level_col_3">
                      <asp:TextBox ID="txtEmail" ClientIDMode="Static" runat="server" class="input"></asp:TextBox>
                    </td>
                  </tr>   
                    
                       <tr>
                    <td class="auto-style2">Email</td>
                    <td >:</td>
                    <td class="level_col_3">
                        <asp:DropDownList ID="ddlUserType" runat="server">
                            <asp:ListItem>Admin</asp:ListItem>
                            <asp:ListItem>Super Admin</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                  </tr> 
		 <td ></td>
		 <td>
			<asp:Button ID="btnSaveUserAccount" style="margin-top:30px;"  CssClass="greenBtn" runat="server" Text="Save" 
             OnClientClick="return validateInputs();" onclick="btnSaveUserAccount_Click" /> 
            <input type="reset" value="Reset" class="blueBtn" />
		</td>
	 </tr>       
                </table>
                 </div>
        </div>

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

        return true;
    }
</script>
</asp:Content>
