<%@ Page Title="Academic Transcript" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AcademicTranscript.aspx.cs" Inherits="DS.Forms.AcademicTranscript" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength {
            width: 130px;
            margin: 5px;
        }
        .tgPanel {
            width: 100%;
        }

        .littleMargin {
            margin-right: 5px;
        }
        .tbl-controlPanel{
            width:600px;
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
                <div class="tgPanelHead">Academic Transcript</div>
                <table class="tbl-controlPanel">
                    <tr>
                        <td>Batch</td>
                        <td>
                            <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" CssClass="input controlLength"
                                OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                <asp:ListItem Selected="True">---Select---</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>Section</td>
                        <td>
                            <asp:DropDownList ID="dlSection" CssClass="input controlLength"
                                runat="server">
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
                        <td></td>
                    </tr>
                    <tr>
                        <td>Exam Id</td>
                        <td>
                            <asp:DropDownList ID="dlExamId" runat="server" CssClass="input controlLength"
                                ClientIDMode="Static" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td>Roll</td>
                        <td>
                            <asp:UpdatePanel runat="server" ID="upRoll" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength"></asp:TextBox>
                                    </td>
                        <td colspan="3">
                            <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success littleMargin"
                                runat="server" OnClick="btnSearch_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview"
                                CssClass="btn btn-success" OnClick="btnPrintPreview_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <div style="width: 595px; margin: 0 auto; border: 1px solid gray">
                            <div>
                                <div style="width: 595px; margin: 0 auto;">
                                    <div id="divAcademicTranscript" class="datatables_wrapper" runat="server"
                                        style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

