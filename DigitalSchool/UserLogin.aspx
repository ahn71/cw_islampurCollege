<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="DS.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <link rel="shortcut icon" href="digital_school_icon.ico"/>
    <title>ODEMS LOGIN</title>
    <!--Core CSS -->
    <link href="AssetsNew/bs3/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="AssetsNew/css/bootstrap-reset.css" rel="stylesheet"/>
    <link href="AssetsNew/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="AssetsNew/css/entypo.css" rel="stylesheet" />
    <!-- Custom styles for this template -->
    <link href="AssetsNew/css/style.css" rel="stylesheet"/>
    <link href="AssetsNew/css/style-responsive.css" rel="stylesheet" />    

    <style type="text/css">
        .form-control{
            font-size: 1.2em;
            font-weight: bold;
            color: black;
        }
        .form-signin h2.form-signin-heading {
            font-size: 14px!important;
            padding: 2px!important;
            font-weight: 900!important;
        }
        .form-signin p {
            color: #3c763d;
            font-size: inherit;
            font-weight: normal;
            text-align: inherit;
        }
        p{
            margin: 15px 0;
        }
        .radioButtonList label
        {
            padding-right:5px;
            margin-right:3px;
            margin-left:2px;
        }
        .icon-hover {
            cursor: pointer;
        }
    </style>

    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]>
        <script src="AssetsNew/js/ie8-responsive-file-warning.js"></script>
    <![endif]-->
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
</head>
<body class="login-body">
    <div class="container">
        <form id="form1" class="form-signin" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="uplMessage" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblexpiredmessage" runat="server" CssClass="label-danger" Visible="false"></asp:Label>
                    <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class='box_container2 ulogin-container'>
             <div class='image_div2 left2'><img src="Images/Osis_left.jpg" />
			</div>
			<div class='image_div2 right2'><img src="Images/Osis_right.jpg" />
			</div>
                <div class='middleImg'>
                    
            <h2 class="form-signin-heading">
                
                <p><img src="AssetsNew/images/logo.png" style="width:85px"></p>
             
                <div style="margin-top:25px; display:none">
                    <asp:RadioButtonList   ID="rblUserType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radioButtonList" >
                    <asp:ListItem Text="Admin" Value="Faculty" Selected="True" ></asp:ListItem>
                    <asp:ListItem Text="Teacher" Value="Adviser" ></asp:ListItem>
                    <asp:ListItem Text="Student" Value="Student"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </h2>
                
            <%--<h4 class="form-signin-heading"></h4>--%>               
            <div  class="login-wrap">
                <div class="user-login-info">                    
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="User ID" 
                        ClientIDMode="Static" autofocus></asp:TextBox>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" 
                        placeholder="Password" ClientIDMode="Static"></asp:TextBox>                    
                </div>       
                
                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-lg btn-login btn-block" style="margin-top:42px" Text="Sign in" OnClientClick="return validateLogIn();"
                                    OnClick="btnLogin_Click" />
                <div runat="server" visible="false">
                <center> <a style="color:#000" href="Index.aspx">Back To Website</a></center>
                <div class="row">
                   <table class="table table-bordered table-responsive">
            <thead>
                <tr>
                    <th>Account</th>
                    <th>User</th>
                    <th>Password</th>
                    <th>Copy</th>
                </tr>
            </thead>
        
            <tbody>
                <tr>
                    <td>ADMIN</td>
                    <td>oitl</td>
                    <td>oitl</td>
                    <td>
                        <i data-original-title="copy" title="" data-placement="top" data-toggle="tooltip" onclick="copy('oitl' , 'oitl','Faculty')" class="fa fa-copy icon-hover tooltip-default"></i>
                    </td>
                </tr>
                <tr>
                    <td>Guide Teacher</td>
                    <td>guide</td>
                    <td>1234</td>
                    <td>
                        <i data-original-title="copy" title="" data-placement="top" data-toggle="tooltip" onclick="copy('guide' , '1234','Adviser')" class="fa fa-copy icon-hover tooltip-default"></i>
                    </td>
                </tr>
                <tr>
                    <td>Student</td>
                    <td>1000</td>
                    <td>1234</td>
                    <td>
                        <i data-original-title="copy" title="" data-placement="top" data-toggle="tooltip" onclick="copy('1000' , '1234','Student')" class="fa fa-copy icon-hover tooltip-default"></i>
                    </td>
                </tr>               
            </tbody>
        </table>
                </div>
              <%--  <div class="registration">
                    Don't have an account yet?           
                    <a class="" href="#"> Request For New Account</a>
                </div>--%>

                <!-- Modal -->
                <div aria-hidden="true" aria-labelledby="myModalLabel" role="dialog" tabindex="-1" id="myModal" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%--<h4 class="modal-title">Forgot Password ?</h4>--%>
                            </div>
                            <div class="modal-body">
                                <p>Enter your e-mail address below to reset your password.</p>
                                <input type="text" name="email" placeholder="Email" autocomplete="off" class="form-control placeholder-no-fix" />
                            </div>
                            <div class="modal-footer">
                                <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                                <button class="" type="button">Submit</button>                                
                            </div>
                        </div>
                    </div>
                </div>
                </div>
                <!-- modal -->
            </div>
                    </div>
                </div>
            <div id="lblErrorMessage" style="display: none; min-width: 100px; position: fixed; top: 45px; z-index: 1; background-color: #dff0d8; color: white; padding: 0px 30px 0px 15px; border-radius: 5px; text-align: center;">
                <p style="float: left; width: auto; padding-right: 30px;">Enter a valid username</p>
                <div style="position: absolute; right: 10px; top: 13px; vertical-align: middle;">
                    <img src="/images/master/close2.png" style="color: black; height: 8px; width: 8px; cursor: pointer;" onclick="$('#lblErrorMessage').fadeOut('slow');" />
                </div>
            </div>
        </form>
    </div>
    <!-- Placed js at the end of the document so the pages load faster -->

    <!--Core js-->
    <script src="AssetsNew/js/jquery.js"></script>
    <script src="AssetsNew/bs3/js/bootstrap.min.js"></script>
    <script src="/Scripts/adviitJS.js" type="text/javascript"></script>
    <script src="/Scripts/custom.js" type="text/javascript"></script>
    <script src="/Scripts/master.js" type="text/javascript"></script>
    <script src="/Scripts/easing.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($('#lblMessage').text().length > 1) {
                showMessage($('#lblMessage').text(), '');
            }
            $('[data-toggle="tooltip"]').tooltip();
            //FOR EASING EFFECT - NOT NEEDED
            $('.box_container2').hover(function () {

                var width = $(this).outerWidth() / 2;

                $(this).find('.left2').animate({
                    right: width
                }, {
                    easing: 'easeOutBounce',
                    queue: false,
                    duration: 1000
                });
                $(this).find('.right2').animate({
                    left: width
                }, {
                    easing: 'easeOutBounce',
                    queue: false,
                    duration: 1000
                });
            }, function () {

                $(this).find('.left2').animate({
                    right: 0
                }, {
                    easing: 'easeOutBounce',
                    queue: false,
                    duration: 1000
                });
                $(this).find('.right2').animate({
                    left: 0
                }, {
                    easing: 'easeOutBounce',
                    queue: false,
                    duration: 1000
                });

            });
        });
        function validateLogIn() {
            if (validateText('txtUsername', 1, 20, 'Enter a valid username') == false) return false;
            if (validateText('txtPassword', 3, 20, 'Password must be 3-20 characters long') == false) return false;
            return true;
        }
        function copy(user, password, accounttype) {
            document.getElementById("txtUsername").value = user;
            document.getElementById("txtPassword").value = password;
            //document.getElementById("rblUserType").SelectedValue = accounttype;

            var arrRadios = document.getElementsByName('<% Response.Write(rblUserType.ClientID); %>');

            for (var i = 0; i < arrRadios.length; i++) {
                if (arrRadios[i].value == accounttype) {
                    arrRadios[i].checked = true;
                }
                else {
                    arrRadios[i].checked = false;
                }
            }
        }
    </script>
</body>
</html>
