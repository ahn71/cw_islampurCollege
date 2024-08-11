<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="BloodGroup.aspx.cs" Inherits="DS.UI.Reports.StafforFaculty.BloodGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        input[type="radio"] {
            margin: 10px;
        }
        .btn {
            margin: 3px;
        }
        .controlLength{
            width: 250px;
        }
        .tbl-controlPanel td:first-child{
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">  
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
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                <li><a runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">Staff or Faculty </a></li>
                <li class="active">Blood Group Wise List</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoWithImage" />
                <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            </Triggers>
            <ContentTemplate>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Blood Group</td>
                        <td>
                            <asp:DropDownList ID="ddlBloodGroup" Width="150px" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>Unknown</asp:ListItem>
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>B-</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>AB-</asp:ListItem>
                                <asp:ListItem>O+</asp:ListItem>
                                <asp:ListItem>O-</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="Preview & Print" ClientIDMode="Static" runat="server" CssClass="btn btn-primary litleMargin"
                               OnClick="btnSearch_Click"   />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:RadioButton runat="server" ID="rdoWithImage" Checked="true" ClientIDMode="Static" Text="With Image"
                                AutoPostBack="true" OnCheckedChanged="rdoWithImage_CheckedChanged" />
                            <asp:RadioButton runat="server" ID="rdoNoImage" ClientIDMode="Static" Text="Without Image"
                                AutoPostBack="true" OnCheckedChanged="rdoNoImage_CheckedChanged" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    
</asp:Content>
