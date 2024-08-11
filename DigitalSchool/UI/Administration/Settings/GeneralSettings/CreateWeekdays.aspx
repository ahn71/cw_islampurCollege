<%@ Page Title="Create Weekly Days" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="CreateWeekdays.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetTimings.CreateWeekdays" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
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
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A1" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A2" runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">Create Weekly Days</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Creat Weekly Days</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Sat"/>
                    <asp:AsyncPostBackTrigger ControlID="Sun"/>
                    <asp:AsyncPostBackTrigger ControlID="Mon"/>
                    <asp:AsyncPostBackTrigger ControlID="Tue"/>
                    <asp:AsyncPostBackTrigger ControlID="Wed"/>
                    <asp:AsyncPostBackTrigger ControlID="Thu"/>
                    <asp:AsyncPostBackTrigger ControlID="Fri"/>
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel table table-bordered" style="text-align:center">
                        <tr>
                            <td>Saturday</td>
                            <td>
                                <asp:CheckBox ID="Sat" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Sunday</td>
                            <td>
                                <asp:CheckBox ID="Sun" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Monday</td>
                            <td>
                                <asp:CheckBox ID="Mon" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static"
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Tuesday</td>
                            <td>
                                <asp:CheckBox ID="Tue" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Wednesday</td>
                            <td>
                                <asp:CheckBox ID="Wed" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Thursday</td>
                            <td>
                                <asp:CheckBox ID="Thu" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static" 
                                    AutoPostBack="true" /></td>
                        </tr>
                        <tr>
                            <td>Friday</td>
                            <td>
                                <asp:CheckBox ID="Fri" runat="server" OnCheckedChanged="chk_CheckedChanged" ClientIDMode="Static"
                                    AutoPostBack="true" /></td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
