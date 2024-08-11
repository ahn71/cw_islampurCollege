<%@ Page Title="Course wise student " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseWiseStudent.aspx.cs" Inherits="DS.Forms.CourseWiseStudent" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgInput" style="width: 680px; margin: 20px auto;">
            <asp:UpdatePanel runat="server" ID="upcws">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdoWithImage" />
                    <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatch" Style="width: 120px;" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" Style="width: 120px;" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false">
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
                                <asp:Button ID="btnSearch" CssClass="btn btn-primary litleMargin" Text="Search" ClientIDMode="Static" runat="server"
                                    OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td colspan="3">
                                <asp:RadioButton runat="server" ID="rdoWithImage" Checked="true" Text="With Image" ClientIDMode="Static"
                                    OnCheckedChanged="rdoWithImage_CheckedChanged"
                                    AutoPostBack="true" />
                                <asp:RadioButton runat="server" ID="rdoNoImage" ClientIDMode="Static" Text="Without Image"
                                    OnCheckedChanged="rdoNoImage_CheckedChanged"
                                    AutoPostBack="true" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
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
                    <div id="divCourseWiseStudentList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
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

