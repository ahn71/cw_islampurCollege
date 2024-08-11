<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="ParentsInformationList.aspx.cs" Inherits="DS.UI.Reports.Students.ParentsInformationList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .controlLength {
            min-width: 170px;
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
                <li class="active">Parents Information List</li>               
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
       <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                <asp:AsyncPostBackTrigger ControlID="dlGroup" />
            </Triggers>
                <ContentTemplate>
                    <div class="row tbl-controlPanel"> 
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="form-inline">
                                 <div class="form-group">
                                     <label for="exampleInputName2">Shift</label>
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2">Bacth</label>
                                        <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                            OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2">Group</label>
                                        <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength form-control"
                                            OnSelectedIndexChanged="dlGroup_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                             <asp:ListItem>All</asp:ListItem>
                                        </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2">Section</label>
                                        <asp:DropDownList ID="dlSection" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                            AutoPostBack="false">
                                            <asp:ListItem>All</asp:ListItem>
                                        </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2"></label>
                                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary litleMargin"
                                    ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                                 </div>
                            </div>
                        </div>
                    </div> 

                    
                </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                        <div class="clearfix"></div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divParentsInfoList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
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
