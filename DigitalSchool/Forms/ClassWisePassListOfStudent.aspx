<%@ Page Title="Class Wise Pass List Of Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClassWisePassListOfStudent.aspx.cs" Inherits="DS.Forms.ClassWisePassListOfStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength {
            width: 120px;
            margin: 5px;
        }
        .tgInput td:first-child, 
        .tgInput td:nth-child(3), 
        .tgInput td:nth-child(5), 
        .tgInput td:nth-child(4) {
            padding: 0px;
            width: 20px;    
        }     
        .tgPanel {
            width: 100%;
        }        
        .littleMargin {
            margin-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="tgPanel">
            <div class="tgPanelHead">Class Wise Student Pass List</div>
            <table class="tbl-controlPanel">
                <tr>
                    <td>Batch</td>
                    <td>
                        <asp:DropDownList ID="ddlBatch" runat="server" CssClass="input controlLength"
                            ClientIDMode="Static" AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td>Section</td>
                    <td>
                        <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength"
                            ClientIDMode="Static" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td>Shift</td>
                    <td>
                        <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength"
                            ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>Morning</asp:ListItem>
                            <asp:ListItem>Day</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Status</td>
                    <td>
                        <asp:DropDownList ID="dlResultStatus" runat="server" CssClass="input controlLength"
                            ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Fail</asp:ListItem>
                            <asp:ListItem>Pass</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success littleMargin"
                            runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <table class="tbl-controlPanel">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnFailSubjectWise" CssClass="btn btn-primary littleMargin" Text="Fail Subject"
                            ClientIDMode="Static" runat="server" OnClick="btnFailSubjectWise_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnNumberOfFailStudent" CssClass="btn btn-primary littleMargin" Text="Number of Fail Student"
                            ClientIDMode="Static" runat="server" OnClick="btnNumberOfFailStudent_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnGrateInfo" CssClass="btn btn-primary littleMargin" Text="Result Overview"
                            ClientIDMode="Static" runat="server" OnClick="btnGrateInfo_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnResultGenderWise" CssClass="btn btn-primary littleMargin" Text="Gender Wise Result"
                            ClientIDMode="Static" runat="server" OnClick="btnResultGenderWise_Click" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="tgPanel">
            <div class="tgPanelHead">Searching Result</div>
            <div class="widget">
                <div class="head">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview"
                                CssClass="btn btn-success pull-right" Width="120px" OnClick="btnPrintPreview_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnFailSubjectWise" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnNumberOfFailStudent" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnGrateInfo" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnResultGenderWise" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div runat="server" id="divLoadFailSubject"></div>
                        <asp:GridView ID="gvPassListOfStudent" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
                        <br />
                        <asp:GridView ID="gvFailListOfStudent" ClientIDMode="Static" CssClass="display" runat="server" Width="100%"></asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

