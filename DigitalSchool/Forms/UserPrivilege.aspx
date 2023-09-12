<%@ Page Title="User Privilege" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPrivilege.aspx.cs" Inherits="DS.Admin.UserPrivilege" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .fieldset1{             
             float: left; 
             width: 35%;             
             height: 86%;             
        }
        .fieldset {
            float: right;
            height: 100%;           
            width: 57%;
        }
        input[type="checkbox"]{
            margin-right: 5px;
        }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="tgPanel" style="margin-top: 20px;">
                <div class="tgPanelHead">Privilege</div>
                <div class="tgInput">
                    <asp:Panel ID="Panel1" runat="server" Height="60%" Width="100%" BorderStyle="Solid " Style="border: 0px solid">
                        <fieldset class="fieldset1">
                            <legend>All User</legend>
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:ListBox ID="lbUserList" runat="server" Height="85%" Style="padding: 5px;" 
                                OnSelectedIndexChanged="lbUserList_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                        </fieldset>
                        <fieldset class="fieldset2">
                            <legend>Privilege For User</legend>
                            <div class="CheckList">
                                <asp:CheckBoxList ID="chkUserPrivilegeList" runat="server" Height="80%" Width="80%"></asp:CheckBoxList>
                            </div>
                        </fieldset>
                    </asp:Panel>
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