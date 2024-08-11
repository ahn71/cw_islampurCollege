<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master"  AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="DS.UI.Reports.StafforFaculty.EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .btn {
            margin: 3px;
        }
        input[type="radio"]{
            margin: 10px;
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
                <li><a runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">Employees Info </a></li>
                <li class="active">All List</li>               
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
        <div class="tgPanelHead">Employee List</div>
        <div class="widget">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>                    
                    <table class="tbl-controlPanel">
                         <tr>
                            <td>
                                Report Type 
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblReportType" RepeatColumns="3" AutoPostBack="true" 
                                     runat="server">
                                    <asp:ListItem Value="Department_wise" Selected="True" >Department wise</asp:ListItem>
                                    <asp:ListItem Value="Designation_wise" >Designation wise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Employee Type 
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdblEmpList" RepeatColumns="3" AutoPostBack="true" 
                                    OnSelectedIndexChanged="rdblEmpList_SelectedIndexChanged" runat="server">
                                   
                                </asp:RadioButtonList>
                            </td>
                            
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                           <td>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px" 
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click"/>
                            </td>
                        </tr>
                        </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rdblEmpList" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="employeeList" class="datatables_wrapper" runat="server"
                        style="margin: 0 auto; width: 100%; height: auto">
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

