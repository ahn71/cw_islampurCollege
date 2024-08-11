<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DueList.aspx.cs" Inherits="DS.UI.Reports.Students.DueList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            width: 150px;
        }
        .tbl-controlPanel{
            width: 700px;
        }
        .ExtraPadding{
            padding-left: 5px;
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
                <li><a runat="server" href="~/UI/Reports/Students/StudentInfoHome.aspx">Student Information</a></li>
                <li class="active">Due List</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>  
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Due List</div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatch" AutoPostBack="true" runat="server" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength"
                                    class="TabSelect">
                                </asp:DropDownList>
                            </td>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="false">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Day</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Fee Category </td>
                            <td>
                                <asp:DropDownList ID="dlFeesCategory" runat="server" class="input controlLength">
                                </asp:DropDownList>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                                    runat="server" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPreview" OnClick="btnPreview_Click" Text="Print Preview"
                                    CssClass="btn btn-success litleMargin" ClientIDMode="Static" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Searching Result</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div class="ExtraPadding">
                        <h5>
                            <asp:Label runat="server" Text="" ID="lblBatch" class=""></asp:Label>
                        </h5>
                        <h5>
                            <asp:Label runat="server" Text="" ID="lblShift" class=""></asp:Label>
                        </h5>
                        <h5>
                            <asp:Label runat="server" Text="" ID="lblSection" class=""></asp:Label>
                        </h5>
                    </div>                    
                    <div id="divDueList" class="datatables_wrapper" runat="server"
                        style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
