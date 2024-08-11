<%@ Page Title="User Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShowUserInfo.aspx.cs" Inherits="DS.Admin.ShowUserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="show-user">
        <asp:GridView ID="gvUserInfo" runat="server" Width="100%">
        </asp:GridView>
    </div>
</asp:Content>
