<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="DesignationwiseReport.aspx.cs" Inherits="DS.UI.Reports.StafforFaculty.DesignationwiseReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .btn {
            margin: 3px;
        }
        .controlLength{
            width: 250px;
        }
        .tbl-controlPanel td:first-child{
            padding-right: 5px;
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
                <li><a runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">Employees Info</a></li>
                <li class="active">Designation Wise List</li>               
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rdoWithImage" />
                <asp:AsyncPostBackTrigger ControlID="rdoNoImage" />
            </Triggers>
            <ContentTemplate>
                <table class="tbl-controlPanel">
                     <tr>
                        <td></td>
                        <td colspan="3">
                            <asp:RadioButton runat="server" ID="rdoWithImage" Checked="true" ClientIDMode="Static" Text="With Image"
                                AutoPostBack="true" OnCheckedChanged="rdoWithImage_CheckedChanged" />
                            <asp:RadioButton runat="server" ID="rdoNoImage" ClientIDMode="Static" Text="Without Image"
                                AutoPostBack="true" OnCheckedChanged="rdoNoImage_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>Designation</td>
                        <td>
                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-primary litleMargin"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                   
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" 
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                        <div class="dataTables_filter" style="float: right;">
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divTeacherList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlDesignation").select2();
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {
            $("#ddlDesignation").select2();
        }
    </script>
</asp:Content>
