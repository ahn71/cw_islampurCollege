<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="PrincipalMessage.aspx.cs" Inherits="DS.UI.DSWS.PrincipalMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <section class="page-section">
        <div class="container">
            <div class="page-inner">
                <div class="row">
                    <div class="col-md-12">
                        <div class="page-title">
                            <h3>Principal Message</h3>
                        </div>
                        <div class="page-fimg" style="width:263px;float:left;margin-right: 20px;">
                            <asp:Image runat="server" id="imgPrincipal" ClientIDMode="Static" width="100%" />
                            
                        </div>
                        <div class="page-prgp msg-pdiv">
                            <p runat="server" id="pSpeech"></p>
                        </div>
                        <div class="msg-sign pull-right text-center">
                            <h5 runat="server" id="hPrincipalName"></h5>
                            <p><i><label runat="server" id="lblPricipalDsg"></label></i></p>
                            <p><label runat="server" id="lblInstituteTitle"></label></p>
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
