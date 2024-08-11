<%@ Page Title="Student Fine Collection List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentFineList.aspx.cs" Inherits="DS.Report.StudentFineList" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength{
            width:150px;
            margin: 5px;
        }
        .tgPanel
        {
            width: 100%;
        }        
        .litleMargin{
            margin-left: 5px;
        } 
        input[type="checkbox"]
        {
            margin: 5px;
        }  
        .tbl-controlPanel{
            width:700px;
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
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Fine Collection Report</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Batch</td>
                        <td>
                            <asp:DropDownList ID="dlBatch" AutoPostBack="true" runat="server" CssClass="input controlLength">
                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Section</td>
                        <td>
                            <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength">
                            </asp:DropDownList>
                        </td>
                        <td>Fine Purpose </td>
                        <td>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:DropDownList ID="dlFinePurpose" ToolTip="Select Fine Purpose" 
                                        runat="server" CssClass="input controlLength">
                                        <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td>From</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="input controlLength">
                            </asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" 
                                TargetControlID="txtFromDate"></ajax:CalendarExtender>
                        </td>
                        <td>To</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="input controlLength">
                            </asp:TextBox>
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" 
                                TargetControlID="txtToDate"></ajax:CalendarExtender>
                        </td>
                        <td>Shift</td>
                        <td>
                            <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength"
                                ClientIDMode="Static" AutoPostBack="false">
                                <asp:ListItem>Morning</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="4">
                            <asp:CheckBox ID="chkToday" ClientIDMode="Static" runat="server" Text="Today Collection"/>
                            <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server"
                                OnClick="btnSearch_Click" CssClass="btn btn-primary" /> 
                        </td>                  
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                    <ContentTemplate>
                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview" CssClass="btn btn-success pull-right" 
                            OnClick="btnPrintPreview_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="up1">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <div runat="server" id="divStudentFine"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
