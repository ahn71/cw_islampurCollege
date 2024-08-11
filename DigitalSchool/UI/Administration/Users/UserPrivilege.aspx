<%@ Page Title="User Privilege" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="UserPrivilege.aspx.cs" Inherits="DS.UI.Administration.Users.UserPrivilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style> 
        .tgPanel{
            width: 100%;
            margin-bottom: 10px;
            padding: 10px;
        }       
        .fieldset1{             
             float: left; 
             width: 35%;             
             height: 86%;             
        }
        .fieldset2{
            float: right;
            height: 100%;           
            width: 57%;            
        }
        input[type="checkbox"]{
            margin-right: 5px;
        }
        legend{
            border: none;
        }
        .CheckList{
            border: 1px solid #ddd9d9;
            height: 350px;
        }
        .privilegeControl input{
            margin-right: 10px;
        }
        .tgInput select{
            height: 320px;
        }
        .tgInput select option{
            margin: 5px;
            padding: 5px 10px;
            font-size: 1em;
            border: 1px solid #ddd9d9;
        }
        .tgInput td:first-child, .tgInput td:nth-child(3) {
            font-size: 1.2em;
            width: auto;
        }
        .controlLength{
            width: 200px;
        }
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">User Managed Module</a></li>
                <li class="active">User Privilege</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="tgPanel">
                <div class="tgPanelHead">Privilege</div>
                <div class="tgInput">
                    <asp:Panel ID="Panel1" runat="server" Height="60%" Width="100%" BorderStyle="Solid " Style="border: 0px solid">
                        <fieldset class="fieldset1">
                            <legend>All User</legend>
                            <asp:TextBox ID="txtUserName" CssClass="input controlLength" runat="server"></asp:TextBox>
                            <asp:ListBox ID="lbUserList" runat="server" 
                                OnSelectedIndexChanged="lbUserList_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                        </fieldset>
                        <fieldset class="fieldset2">
                            <legend>Privilege For User</legend>
                            <div class="CheckList">
                                <asp:CheckBoxList ID="chkUserPrivilegeList" runat="server"></asp:CheckBoxList>
                            </div>
                        </fieldset>
                    </asp:Panel>
                    <div class="clearfix"></div>
                </div>
                <div style="float: left; width: 200px; margin-left: 60px">
                    <asp:Button Text="Set" ID="btnSet" runat="server" CssClass="btn btn-inverse" ClientIDMode="Static" OnClick="btnSet_Click" />
                </div>
                <div class="privilegeControl">
                    <asp:Button ID="btnAllSelect" runat="server" ClientIDMode="Static" CssClass="btn btn-inverse" Text="Select All" OnClick="btnAllSelect_Click" />
                    <asp:CheckBox ID="chkRead" runat="server" ClientIDMode="Static" /><label for="chkRead" class="chkControl">Read</label>
                    <asp:CheckBox ID="chkWrite" runat="server" ClientIDMode="Static" /><label for="chkWrite" class="chkControl">Write</label>
                    <asp:CheckBox ID="chkUpdate" runat="server" ClientIDMode="Static" /><label for="chkUpdate" class="chkControl">Update</label>
                    <asp:CheckBox ID="chkAll" runat="server" ClientIDMode="Static" /><label for="chkAll" class="chkControl">All</label>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAllSelect" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#chkAll").click(function () {
                $("#chkRead").prop('checked', $(this).prop('checked'));
            });
            $("#chkAll").click(function () {
                $("#chkWrite").prop('checked', $(this).prop('checked'));
            });
            $("#chkAll").click(function () {
                $("#chkUpdate").prop('checked', $(this).prop('checked'));
            });
            $("#chkAll").click(function () {
                $("#chkRead").prop('checked', $(this).prop('checked'));
            });
        });
    </script>
</asp:Content>
