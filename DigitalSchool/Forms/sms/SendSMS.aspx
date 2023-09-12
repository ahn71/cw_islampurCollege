<%@ Page Title="Send SMS" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="DS.Forms.sms.SendSMS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/reports/CommonBorder.css" rel="stylesheet" />
    <link href="/Styles/feeCollection.css" rel="stylesheet" />    
    <style>
        .tgPanel {
            width: 100%;
        }

        .controlLength {
            width: 200px;
            margin: 5px;
        }

        .controlLength2 {
            width: 300px;
            margin: 5px;
        }

        .littleMargin {
            margin: 5px;
        }

        input[type="checkbox"] {
            margin: 5px;
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
            <div class="tgPanelHead">Send SMS</div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnSendSMS" />
                    <asp:AsyncPostBackTrigger ControlID="dlTitel" />
                    <asp:AsyncPostBackTrigger ControlID="chkselect" />
                </Triggers>
                <ContentTemplate>

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift</td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength">
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Section</td>
                                    <td>
                                        <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary littleMargin"
                                            runat="server" OnClick="btnSearch_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnTodayAbsent" Text="Today Absent" ClientIDMode="Static"
                                            runat="server" CssClass="btn btn-success littleMargin" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" style="margin-top: -23px;">
                        <ProgressTemplate>
                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                <p style="margin-top: 39px">Wait&nbsp; Sending SMS</p>
                            </span>
                            <img style="width: 26px; height: 26px; cursor: pointer; float: left; margin-top: 39px; margin-left: 7px;" src="/images/wait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="tgPanel">
                                <div class="tgPanelHead">Roll List</div>
                                <div style="min-height: 416px; max-height: 416px; overflow: auto; overflow-x: hidden;">
                                    <asp:CheckBox ID="chkselect" Text="Select All" AutoPostBack="true" Visible="false"
                                        ClientIDMode="Static" runat="server" OnCheckedChanged="chkselect_CheckedChanged" />
                                    <asp:CheckBoxList ID="chklbRollNo" runat="server" ClientIDMode="Static"></asp:CheckBoxList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="tgPanel">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="tgPanelHead">SMS Template</div>
                                        <table class="tbl-controlPanel">
                                            <tr>
                                                <td>Title</td>
                                                <td>
                                                    <asp:DropDownList ID="dlTitel" ToolTip="Select Shift name" runat="server" CssClass="input controlLength2"
                                                        OnSelectedIndexChanged="dlTitel_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Body</td>
                                                <td>
                                                    <asp:TextBox ID="txtBody" ClientIDMode="Static" runat="server" CssClass="input controlLength2"
                                                        Height="300px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSendSMS" Text="Send SMS" ClientIDMode="Static" runat="server"
                                                        CssClass="btn btn-primary" OnClick="btnSendSMS_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

