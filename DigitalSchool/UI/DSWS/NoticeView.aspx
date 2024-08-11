<%@ Page Title="পৃষ্ঠা" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="NoticeView.aspx.cs" Inherits="DS.UI.DSWS.NoticeView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-content {
    float: left;
    min-height: 468px;
    padding: 0 15px;
    width: 590px;
    margin-top:10px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    <div class="container">
         <div  class="row">
        <div runat="server" id="divNoticeViewer" class="main-content">
        </div>        
      </div>
    </div>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
