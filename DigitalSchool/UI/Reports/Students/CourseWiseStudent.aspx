<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="CourseWiseStudent.aspx.cs" Inherits="DS.UI.Reports.Students.CourseWiseStudent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        input[type="radio"] {
            margin: 10px;
        }
        .tgPanel {
            width: 100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5) {
            padding: 0px 5px;
        }
        .litleMargin {
            margin-left: 5px;
        }
        .btn {
            margin: 3px;
        } 
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                <li><a runat="server" href="~/UI/Reports/Students/StudentInfoHome.aspx">Student Information</a></li>
                <li class="active">Course Wise Student List</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <asp:UpdatePanel runat="server" ID="upcws">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoWithImage" />
                <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />
                <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                <asp:AsyncPostBackTrigger ControlID="dlGroup" />
            </Triggers>
            <ContentTemplate>
                <table class="tbl-controlPanel">
                    <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlGroup_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="false">                                    
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin"
                                    ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="4">
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
            <div class="clearfix"></div>
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
