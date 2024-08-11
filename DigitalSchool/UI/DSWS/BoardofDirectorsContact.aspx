<%@ Page Title="পরিচালনা পর্ষদ" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="BoardofDirectorsContact.aspx.cs" Inherits="DS.UI.DSWS.BoardofDirectorsContact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main-content-inner {
    float: left;
    min-height: 468px;
    padding: 0 15px;
    width: 900px;
    
}
          .TContact {              
              width:100%;
              margin:5px auto;             
          }
          .tech-box {
              border:1px solid red;
          }
          .col-md-5 {
              border:1px solid silver;
              margin-left:10px;   
              height:100px;          
          }
          .img-responsive {              
              height:88px;
          }
          .CotactsBody {
              float:right;
              width:60%;
              border:1px solid red;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
     <div class="container">
     <div class="row">
    
    <div runat="server" id="divBoardDirContacts" class="main-content">
    </div>
    </div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
