<%@ Page Title="Absent Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IndividualAbsentDetails.aspx.cs" Inherits="DS.Forms.IndividualAbsentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

        table td {
            margin: 4px !Important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlSheetName" />
            </Triggers>
            <ContentTemplate>
                <div class="tgInput" style="width: 715px;">
                    <table>
                        <tr>
                            <td>Month</td>
                            <td>
                                <asp:DropDownList ID="dlSheetName" Style="width: 280px;" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="dlSheetName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>Roll No</td>
                            <td>
                                <asp:DropDownList ID="dlRollNo" Style="width: 136px;" runat="server" ClientIDMode="Static">
                                    <asp:ListItem>...Select Roll...</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin"
                                    ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                            Width="120px" CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
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
                    <div id="divIndividualAbsent" class="datatables_wrapper" runat="server"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
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
