<%@ Page Title="Guardian Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GuardianInformation.aspx.cs" Inherits="DS.Forms.GuardianInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .controlLength{
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgInput" style="width: 592px; margin: 0 auto;">
        <table class="tbl-controlPanel">
            <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="dlClass" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                         AutoPostBack="false"></asp:DropDownList>
                </td>

                <td>Section</td>
                <td>
                    <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength"
                        ClientIDMode="Static" AutoPostBack="false">
                        <asp:ListItem>All</asp:ListItem>
                    </asp:DropDownList>
                </td>

                <td>Shift</td>
                <td>
                    <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" AutoPostBack="false" CssClass="input controlLength">
                        <asp:ListItem>All</asp:ListItem>
                        <asp:ListItem>Morning</asp:ListItem>
                        <asp:ListItem>Day</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>

                    <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success" 
                        OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div class="widget">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="head">                    
                    <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" 
                        CssClass="btn btn-primary" OnClick="btnPrintPreview_Click" />
                    <div class="dataTables_filter" style="float: right;">
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <div id="lblSectionDiv" runat="server"></div>
                <div id="divGuardianList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });

        });
    </script>
</asp:Content>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           