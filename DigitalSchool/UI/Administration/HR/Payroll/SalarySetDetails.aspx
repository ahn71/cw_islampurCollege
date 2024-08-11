<%@ Page Title="Employee Salary Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SalarySetDetails.aspx.cs" Inherits="DS.UI.Administration.HR.Payroll.SalarySetDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        input[type="checkbox"] {
            margin-left: 10px;
            margin-right: 5px;
        }
         .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }  
         @media (min-width: 320px) and (max-width: 480px) {
      
            .pagination {
                width:100%;
                float:left;
            }
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
                <li><a runat="server" href="~/UI/Administration/HR/Payroll/PayrollHome.aspx">Payroll Management</a></li>
                <li class="active">Salary Set Details</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <div class="tgPanelHead">Employee Salary Details</div>
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="dataTables_filter_New" style="float: right;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here..." />
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="divSalarySetInfo" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblEmployeeSalary', '');
            });
            $('#tblEmployeeSalary').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function editSalary(employeeId) {
            goURL('/UI/Administration/HR/Payroll/SetSalary.aspx?TeacherId=' + employeeId + "&Edit=True");
        }
    </script>
</asp:Content>
