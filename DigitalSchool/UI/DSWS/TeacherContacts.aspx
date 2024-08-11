<%@ Page Title="শিক্ষক পরিচিতি" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="TeacherContacts.aspx.cs" Inherits="DS.UI.DSWS.TeacherContacts" %>
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
          .info-box{
            border: 5px solid #ededed;
            height: 100px;
            margin-left: 10px;
            min-height: 150px;
            padding: 5px;      
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
    
    <div runat="server" id="divTeacherContacts" class="main-content">
    </div>
        </div>
        </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
