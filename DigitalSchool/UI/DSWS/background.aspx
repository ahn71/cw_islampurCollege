<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="background.aspx.cs" Inherits="DS.UI.DSWS.background" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-section">
        <div class="container">
            <div class="page-inner">
                <div class="row">
                    <div class="col-md-12">
                        <div class="page-title">
                            <h3><label runat="server" id="lblBackgroundTitle"></label></h3>
                        </div>
                        <div class="page-fimg" style="width:263px;float:left;margin-right: 20px;">
                            <asp:Image runat="server" id="imgforBackground" ClientIDMode="Static" width="100%" />                            
                        </div>
                        <div class="page-prgp msg-pdiv">
                            <p runat="server" id="pSpeech"></p>
                        </div>                       
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
