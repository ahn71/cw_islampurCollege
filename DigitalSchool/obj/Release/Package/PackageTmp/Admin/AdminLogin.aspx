<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminLogin.aspx.cs" Inherits="DigitalSchool.Admin.AdminLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login</title>
    <link href="/Styles/admin/adminLogin.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script> 
</head>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>


     <div class="fixed"> 
    
    </div>
       <%-----------------------------------------------------%>
    <div class="page">
        <div class="main">
            <div class="loginBox">
                <div class="hedTitle">
                <div class="logImg"> <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/user.png" /></div>                 
                   <div class="style1"><strong>Admin Login</strong></div>
                </div>

                    <div class="userNameDiv">
                        <div class="textName">Username</div>
                        <div class="textBox">
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="txtBoxStyle" Width="206px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="userIdDiv">
                        <div class="textName">Password</div>
                        <div class="textBox">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtBoxStyle" Width="206px" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>

                    <div class="PasswordDiv">
                        <div class="textBtn">
                            <asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" />
                        </div>
                        <div class="btnBox">
                            <asp:Button ID="btnLogin" runat="server" CssClass="btnLogin" Text="Login"  OnClientClick="return validateLogIn();"
                                Width="68px" PostBackUrl="~/Admin/Default.aspx" OnClick="btnLogin_Click" />
                        </div>
                    </div>
            </div>
        </div>
   </div>
       <%-----------------------------------------------------%>
    <div id="footer">
        <div class="fleft"></div>
        <div class="fright">Powered by Optimal IT Limited</div>
    </div>


      <%--- MESSAGE BOX ---%>
<div id="lblErrorMessage" style="display:none;min-width:100px; position:fixed;top:45px;z-index:1; background-color:#5EA8DE;color:white;padding:0px 30px 0px 15px;border-radius:5px;text-align:center;">
    <p style="float:left;width:auto;padding-right:30px;"></p>
    <div style="position:absolute;right: 10px;top: 13px;vertical-align:middle;"> <img src="/images/master/close2.png" style="color:black;height:8px;width:8px;cursor:pointer;padding-top:5px" onclick="$('#lblErrorMessage').fadeOut('slow');"/></div>
</div>

</form>

<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/custom.js" type="text/javascript"></script>
<script src="/Scripts/master.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {
        if ($('#lblMessage').text().length > 1) {
            showMessage($('#lblMessage').text(), '');
        }
    });


    function validateLogIn() {


        if (validateText('txtUsername', 1, 20, 'Enter a valid username') == false) return false;
        if (validateText('txtPassword', 3, 20, 'Password must be 3-20 characters long') == false) return false;

        return true;
    }

</script>




</body>
</html>
