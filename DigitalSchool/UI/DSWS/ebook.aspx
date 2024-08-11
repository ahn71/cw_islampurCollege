<%@ Page Title="ই-বুক" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="ebook.aspx.cs" Inherits="DS.UI.DSWS.ebook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    <div class="row">
         <div runat="server" id="divBoardDirContacts" class="main-content">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <Triggers>
              <asp:AsyncPostBackTrigger ControlID="ddlClass" />
          </Triggers>
            <ContentTemplate>
                 <div class="col-lg-12">
                <div class="col-lg-2">
                    <div class="form-inline" style="padding:7px;border-bottom:1px solid #ccc;margin-bottom:10px">
                      <div class="form-group">                        
                 <h3  style="margin:0px 0 10px;">ই-বুক</h3>
                          </div>
                        </div>
                    </div>
                <div class="col-lg-8">
                    <div class="form-inline" style="padding:10px;border-bottom:1px solid #ccc;margin-bottom:10px">
                      <div class="form-group">
                        <label>ক্লাস</label>
                <asp:DropDownList CssClass="input control" runat="server" ClientIDMode="Static" ID="ddlClass" AutoPostBack="true"  OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                    <asp:ListItem Value="1">Class 1</asp:ListItem>  
                    <asp:ListItem Value="2">Class 2</asp:ListItem>                     
                     <asp:ListItem Value="3">Class 3</asp:ListItem>
                     <asp:ListItem Value="4">Class 4</asp:ListItem>
                     <asp:ListItem Value="5">Class 5</asp:ListItem>
                     <asp:ListItem Value="6">Class 6</asp:ListItem>
                     <asp:ListItem Value="7">Class 7</asp:ListItem>
                     <asp:ListItem Value="8">Class 8</asp:ListItem>
                    <asp:ListItem Value="9">Class 9-10</asp:ListItem>
                 </asp:DropDownList>
                          </div>
                        </div>
                    </div>
            </div>
        <div runat="server" id="divEbooks"> 			  			  
            </div>
         </ContentTemplate>
        </asp:UpdatePanel>
	</div>
        </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
