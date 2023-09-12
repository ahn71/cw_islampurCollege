<%@ Page Title="User Info" Language="C#" MasterPageFile="~/Admin/AdminPanel.Master" AutoEventWireup="true" CodeBehind="ShowUserInfo.aspx.cs" Inherits="DigitalSchool.Admin.ShowUserInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="show-user">
        <asp:GridView ID="gvUserInfo" runat="server" Width="100%">
        </asp:GridView>
    </div>
</asp:Content>
