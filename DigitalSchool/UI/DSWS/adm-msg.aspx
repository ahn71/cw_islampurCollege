<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="adm-msg.aspx.cs" Inherits="DS.UI.DSWS.adm_msg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="page-section">
        <div class="container">
            <div class="page-inner">
                <div class="row">
                    <div runat="server" id="divContent" class="col-md-12">
                        <h1 runat="server" id="hMsg" style="color:#ff0000;"></h1>
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
