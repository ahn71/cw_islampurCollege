<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="TeacherWiseResult.aspx.cs" Inherits="DS.UI.Reports.Examination.TeacherWiseResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
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
        #btnPrintPreview{
            margin: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Teacher Wise Result View</div>
            <table class="tbl-controlPanel">
                <tr>
                    <td>Batch</td>
                    <td>
                        <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" AutoPostBack="False"
                            CssClass="input controlLength">
                        </asp:DropDownList>
                    </td>
                    <td>Section</td>
                    <td>
                        <asp:DropDownList ID="dlSection" runat="server" ClientIDMode="Static" AutoPostBack="false"
                            CssClass="input controlLength">
                        </asp:DropDownList>
                    </td>
                    <td>Shift</td>
                    <td>
                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" AutoPostBack="false"
                            CssClass="input controlLength">
                            <asp:ListItem>Morning</asp:ListItem>
                            <asp:ListItem>Day</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Department</td>
                    <td>
                        <asp:DropDownList runat="server" ID="dlDepartment" ClientIDMode="Static" AutoPostBack="true" CssClass="input controlLength"
                            OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                            <asp:ListItem>--Select--</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>Teacher</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:DropDownList runat="server" ID="dlTeacher"
                                    CssClass="input controlLength" ClientIDMode="Static">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td colspan="2">
                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary littleMargin"
                            ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tgPanel">
            <div class="tgPanelHead">Searching Result</div>
            <div class="widget">
                <div class="head">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview" ClientIDMode="Static"
                                 CssClass="btn btn-success pull-right"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>                    
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div runat="server" id="divResultTeacherWise"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
