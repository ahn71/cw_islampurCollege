<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="IndivisualStudentList.aspx.cs" Inherits="DS.UI.Reports.Students.IndivisualStudentList" %>
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
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5),
        .tbl-controlPanel td:nth-child(7){
            padding: 0px 5px;
        }
        .controlLength{
            min-width: 130px;
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
                <li class="active">Individual Student Profile</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
       
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlBatch" />
             <asp:AsyncPostBackTrigger ControlID="dlGroup" />
            <asp:AsyncPostBackTrigger ControlID="dlSection" />
           
        </Triggers>
        <ContentTemplate>
            <div class="tgPanel">
                <div class="row tbl-controlPanel"> 
                    <div class="col-sm-10 col-sm-offset-1">
                        <div class="form-inline">
                             <div class="form-group">
                                 <label for="exampleInputName2">Shift</label>
                                    <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" AutoPostBack="false">                                                            
                                </asp:DropDownList>
                             </div>
                            <div class="form-group">
                                 <label for="exampleInputName2">Batch</label>
                                    <asp:DropDownList ID="dlBatch" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" AutoPostBack="true"
                                        OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" >
                                
                                    </asp:DropDownList>
                             </div>
                            <div class="form-group">
                                 <label for="exampleInputName2">Group</label>
                                   <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength form-control" 
                              OnSelectedIndexChanged="dlGroup_SelectedIndexChanged"  AutoPostBack="true"  ClientIDMode="Static">
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList> 
                             </div>
                            <div class="form-group">
                                 <label for="exampleInputName2">Section</label>
                                    <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength form-control"  ClientIDMode="Static" AutoPostBack="True"
                                OnSelectedIndexChanged="dlSection_SelectedIndexChanged">
                                <asp:ListItem>All</asp:ListItem>
                            </asp:DropDownList>
                             </div>
                            <div class="form-group">
                                 <label for="exampleInputName2">Roll</label>
                                    <asp:DropDownList ID="dlRoll" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" AutoPostBack="false">
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

               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <div class="tgPanelHead">Searching Result</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="head">
                        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" ClientIDMode="Static"
                            CssClass="btn btn-success pull-right" style="width:120px;position: absolute;right: 30px;" OnClick="btnPrintPreview_Click" />
                    </div>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divIndivisualStudentList" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="clearfix"></div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dlRoll").select2();
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
            $('#tblStudentInfo').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblStudentInfo').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function goToNewTab(url) {
            window.open(url);            
            load();
        }
        function load() {
            $("#dlRoll").select2();
        }
    </script>
</asp:Content>
