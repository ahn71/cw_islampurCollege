<%@ Page Title="Employee Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EmpDetails.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.EmpDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel
        {
            width:100%;
        }
        input[type="checkbox"]
        {
            margin: 10px 5px 5px 0;
        }
        .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        .numeric img {
        height:20px;
        width:20px;
        }
        .mid {
            text-align:center;
        }
        @media (min-width: 320px) and (max-width: 480px) {
        .numeric img {
        height:20px;
        width:20px;
        }
            .pagination {
                width:100%;
                float:left;
            }
        }
        #rblEmpType label {
            margin-left:3px;
            margin-right:3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpHome.aspx">Employee Management</a></li> 
                <li class="active">Employee List</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <asp:UpdatePanel runat="server">
            <Triggers> 
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
            </Triggers>
            <ContentTemplate>
                 <div class="tgPanelHead">
            <asp:Label ID="lblTitle" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            List
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="head_title">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                        </Triggers>
                        <ContentTemplate>
                            <div>
                                 <asp:RadioButtonList ID="rblEmpType" ClientIDMode="Static"  runat="server" RepeatLayout="Flow"   RepeatDirection="Horizontal" CssClass="radiobuttonlist" AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">
                                     <%--<asp:ListItem class="radio-inline" style="margin-left:10px" Selected="True" Value="2">All</asp:ListItem>
                                     <asp:ListItem class="radio-inline" style="margin-left:10px"  Value="1">Teacher</asp:ListItem>
                                     <asp:ListItem class="radio-inline" style="margin-left:10px" Value="0">Staff</asp:ListItem>--%>
                                </asp:RadioButtonList>
                                <%--<asp:CheckBox runat="server" ID="chkIsTeacher" Text="Is Teacher" ClientIDMode="Static" AutoPostBack="true" Checked="true" 
                                    OnCheckedChanged="chkIsTeacher_CheckedChanged" />  --%>                   
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="dataTables_filter_New" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="Search_New" placeholder="type here..." />
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                </Triggers>
                <ContentTemplate>
                    <div id="divTeacherInfo" class="datatables_wrapper" runat="server" style="width: 100%; max-height:680px; overflow-y: scroll;overflow-x: hidden; "></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblTeacherInfo', '');
            });
            //- these are commented to stop paging 
            //$('#tblTeacherInfo').dataTable({
            //    "iDisplayLength": 10,
            //    "lengthMenu": [10, 20, 30, 40, 50, 100]
            //});
        });
        //function loaddatatable() {
        //    $('#tblTeacherInfo').dataTable({
        //        "iDisplayLength": 10,
        //        "lengthMenu": [10, 20, 30, 40, 50, 100]
        //    });
        //}
        function editTeacher(teacherId) {
            goURL('/UI/Administration/HR/Employee/EmpRegForm.aspx?TeacherId=' + teacherId + "&Edit=True");
        }
        function viewTeacher(teacherId) {
            goURL('/UI/Administration/HR/Employee/EmpProfile.aspx?TeacherId=' + teacherId);
        }
        function setSalary(teacherId) {
            goURL('/UI/Administration/HR/Employee/EmpDetails.aspx?TeacherId=' + teacherId + "&Save=True");
        }
        function existData() {
            alert("Salary Already Set");
            showMessage('Salary Already Set', 'warning');
        }
    </script>
</asp:Content>
