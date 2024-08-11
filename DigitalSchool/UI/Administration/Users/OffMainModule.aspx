<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="OffMainModule.aspx.cs" Inherits="DS.UI.Administration.Users.OffMainModule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table tr th:first-child,tr th:nth-child(3),tr th:nth-child(4),tr th:nth-child(5),tr th:nth-child(6)
        {
            text-align:center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">Control Panel</a></li>             
                <li class="active">Main Module Privilege</li>
            </ul>
            <!--breadcrumbs end -->
        </div>

    <div class="col-lg-4"></div>
    <div class="col-lg-4">
        
    </div>
    <div class="col-lg-4"></div>
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <Triggers>
               
                <%--      <asp:AsyncPostBackTrigger ControlID="btnSetPage" />  --%>
            </Triggers>
            <ContentTemplate>

                <div id="showParticular" runat="server" style="display: block; width: 100%; height: 100% auto; background-color: white; top: 60px">
                    <div style="background-color: #23282C; color: white">
                        <h3>Chosen user type for setup main module privilege</h3>
                    </div>

                    <asp:Panel runat="server" ScrollBars="Vertical" Width="100%" Height="100%">
                        <asp:GridView ID="gvUserTypeList" runat="server" Width="100%" DataKeyNames="UserTypeDId" CssClass="table table-bordered" AutoGenerateColumns="False">

                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hideSubId" runat="server"
                                            Value='<%# DataBinder.Eval(Container.DataItem, "UserTypeDId")%>' />
                                        <%# Container.DataItemIndex+1%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Page Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubName" Style="float: left" runat="server"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "UserType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Academic">
                                    <HeaderTemplate>
                                       Academic
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAcademicModule" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("AcademicModule").ToString())%>' OnCheckedChanged="chkAcademicModule_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Administration">
                                    <HeaderTemplate>
                                       Administration
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdministrationModule" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("AdministrationModule").ToString())%>' OnCheckedChanged="chkAdministrationModule_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Notification">
                                    <HeaderTemplate>    
                                        Notification                                
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkNotificationModule" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("NotificationModule").ToString())%>' OnCheckedChanged="chkNotificationModule_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reports">
                                    <HeaderTemplate>
                                       Reports
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkReportsModule" runat="server" AutoPostBack="true" Checked='<%#bool.Parse(Eval("ReportsModule").ToString())%>' OnCheckedChanged="chkReportsModule_CheckedChanged" />
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                            </Columns>
                        </asp:GridView>
                        <%--  </div>--%>
                    </asp:Panel>

                </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
