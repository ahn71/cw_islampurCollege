<%@ Page Title="Student Contact List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentContactList.aspx.cs" Inherits="DS.Forms.StudentContactList" %>

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
        <div class="tgInput" style="width: 700px;">
            <table>
                <tr>
                    <td>Class</td>
                    <td>
                        <asp:DropDownList ID="dlClass" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Section</td>
                    <td>
                        <asp:DropDownList ID="dlSection" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Shift</td>
                    <td>
                        <asp:DropDownList ID="dlShift" Style="width: 120px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                            <asp:ListItem>All</asp:ListItem>
                            <asp:ListItem>Morning</asp:ListItem>
                            <asp:ListItem>Day</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin"
                            ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" CssClass="btn btn-success pull-right"
                            OnClick="btnPrintPreview_Click" />
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
                    <div id="divStudentContactList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
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
