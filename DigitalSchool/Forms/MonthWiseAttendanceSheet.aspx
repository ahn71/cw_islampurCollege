<%@ Page Title="Month Wise Attendance Sheet" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MonthWiseAttendanceSheet.aspx.cs" Inherits="DS.Forms.MonthWiseAttendanceShee" %>
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
        table td{
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
        <div class="tgInput" style="width: 500px;">
            <table>
                <tr>
                    <td>Month</td>
                    <td>
                        <asp:DropDownList ID="dlSheetName" Style="width: 280px;" runat="server" ClientIDMode="Static" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin" 
                            runat="server" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <div class="head">
                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" 
                    CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                <div class="dataTables_filter" style="float: right;">
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                    <div style="width: 100%; overflow: auto">
                        <asp:GridView runat="server" ID="gvAttendanceSheet" ClientIDMode="Static" 
                            CssClass="table table-striped table-bordered tbl-controlPanel">
                        </asp:GridView>
                    </div>
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
