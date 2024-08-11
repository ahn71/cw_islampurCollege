<%@ Page Title="ফটো অ্যালবাম" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="PhotoAlbum.aspx.cs" Inherits="DS.UI.DSWS.PhotoAlbum" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">     
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        $(document).ready(function () {
            $(".fancybox").fancybox({
                openEffect: 'none',
                closeEffect: 'none'
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
        <div class="row">
             <div runat="server" id="divBoardDirContacts" class="main-content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <Triggers>
              
          </Triggers>
            <ContentTemplate>
                 <div class="col-lg-12">                                              
                 <h2 style="margin:0px 0 10px;">ফটো অ্যালবাম</h2> 
            </div>
        <div runat="server" id="divPhotoAlbum">             			          		  
            </div>              
         </ContentTemplate>
        </asp:UpdatePanel>
	</div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
