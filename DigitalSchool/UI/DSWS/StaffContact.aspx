<%@ Page Title="কর্মচারী পরিচিতি" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="StaffContact.aspx.cs" Inherits="DS.UI.DSWS.StaffContact" %>
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
  
    
    <div runat="server" id="divTeacherContacts" class="main-content">
        <section class="page-section">
        <div class="container">
            <div class="page-inner">
                <div class="row">
                    <div class="col-md-12">
                        <div class="page-title">
                            <h3>Staff Information</h3>
                        </div>
                        <div class="page-fimg" style="width:263px;float:left;margin-right: 20px;">
                            
                        </div>
                        <div class="page-prgp">
                            <h1  class="text-center upcomming">Content will be Published soon</h1>
                            <p></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    </div>
      

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>

