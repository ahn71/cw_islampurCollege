<%@ Page Title="Account Setting" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AccountSetting.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.AccountSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tgPanel {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>   
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/Accounts/AccountsHome.aspx">Accounts Management</a></li>                          
                <li class="active">Account Setting</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
     <div class="">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="rblcount" />
              </Triggers>
        <ContentTemplate>
        <div class="row">            
            <div class="col-sm-6 col-sm-offset-3">
                <div class="tgPanel">                   
                    <div class="tgPanelHead">Account Setting</div>
                <div class="group">
                <label class="col-sm-4">Student Fees Count</label>
                    <div class="col-md-8">
                        <asp:RadioButtonList ID="rblcount" OnSelectedIndexChanged="rblcount_SelectedIndexChanged" AutoPostBack="true" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server">
                        <asp:ListItem class="radio-inline" Value="1" Text="Yes" Selected="True"></asp:ListItem>
                        <asp:ListItem class="radio-inline" Value="0" Text="No"></asp:ListItem>
                    </asp:RadioButtonList>
                        </div>
                    </div>
            </div>
                        </div>
        </div>
            </ContentTemplate>
              </asp:UpdatePanel>
         </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
