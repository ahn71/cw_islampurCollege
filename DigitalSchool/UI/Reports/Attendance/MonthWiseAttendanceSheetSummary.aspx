<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MonthWiseAttendanceSheetSummary.aspx.cs" Inherits="DS.UI.Reports.Attendance.MonthWiseAttendanceSheetSummary" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        .controlLength{
            width: 350px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .btn {
            margin: 3px;
        }
         .radiobuttonlist label {
            margin-left:5px;
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
                <li><a runat="server" href="~/UI/Reports/Attendance/AttendanceHome.aspx">Attendance</a></li>
                <li class="active">Staff/Teacher Attendance</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>        
    </asp:UpdatePanel>
    <div id="ManiDivDailyAtt" style="border:1px black solid">
        <div class="tgPanelHead">Daily Attendance Report (Staff & Teacher)</div>
        <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShift_D" />
            <asp:AsyncPostBackTrigger ControlID="ddlDepartment_D" />  
             <asp:AsyncPostBackTrigger ControlID="ddlDesignation_D" />
            <asp:AsyncPostBackTrigger ControlID="rblReportType" />  
             <asp:AsyncPostBackTrigger ControlID="btnPrint_D" />  
            <asp:AsyncPostBackTrigger ControlID="rblEmpType_D" />                               
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >
        <asp:RadioButtonList runat="server" ID="rblEmpType_D" CssClass="radiobuttonlist" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_D_SelectedIndexChanged">
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">Both</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Teacher</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Staff</asp:ListItem>
        </asp:RadioButtonList>
         <div style="text-align:center; width:600px; margin:0px auto; margin-top:10px">
            <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0">Attendance Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="1"> Present Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="2"> Absent Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="3">Log in-out Time</asp:ListItem>
        </asp:RadioButtonList>                   
                     </div>     
        <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server" >
            <tr>
                <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShift_D" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" OnSelectedIndexChanged="ddlShift_D_SelectedIndexChanged" >                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Department &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment_D"  runat="server" class="input controlLength" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_D_SelectedIndexChanged"   ></asp:DropDownList>
                            </td>
                             <td> &nbsp;Designation &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation_D" runat="server" class="input controlLength" Width="160px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_D_SelectedIndexChanged" ></asp:DropDownList>
                            </td>  
                <td> &nbsp;Card No &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCardNo_D" runat="server" class="input controlLength" Width="170px" ClientIDMode="Static"  Enabled="true"  ></asp:DropDownList>
                            </td>
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click"/> </td>                              
                </tr>
            </table> 
        <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
            <tr>
                <td>&nbsp;From Date&nbsp;</td>
                <td><asp:TextBox ID="txtDate" Width="170px"  runat="server" ClientIDMode="Static" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                               </asp:CalendarExtender> </td>                
                <td>&nbsp;To Date&nbsp;</td>
                <td><asp:TextBox ID="txtToDate" Width="170px"  runat="server" ClientIDMode="Static" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                               </asp:CalendarExtender> </td>               
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShiftList" /> 
            <asp:AsyncPostBackTrigger ControlID="ddlDepartment" />
            <asp:AsyncPostBackTrigger ControlID="ddlDesignation" />  
            <asp:AsyncPostBackTrigger ControlID="rblRepotMontly" />   
            <asp:AsyncPostBackTrigger ControlID="rblEmpType" />          
        </Triggers>
        <ContentTemplate>
            <div class="tgPanelHead">Monthly Attendance Report (Staff & Teacher)</div>
           <div class="tgPanel" style="height:170px">
                <asp:RadioButtonList runat="server" ID="rblEmpType"  AutoPostBack="true" CssClass="radiobuttonlist" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">Both</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Teacher</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Staff</asp:ListItem>
        </asp:RadioButtonList>
               <div style="margin:0px auto; width:415px; margin-top:10px" ">
        <asp:RadioButtonList ID="rblRepotMontly" runat="server" AutoPostBack="true" CssClass="radiobuttonlist" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRepotMontly_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">Attendance Sheet</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Attendance Summary</asp:ListItem>
            <asp:ListItem Value="2" style="margin-left:10px">Absent List</asp:ListItem>
        </asp:RadioButtonList>
            </div>
                    <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px">
                        <tr> 
                              <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" Width="100px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlShiftList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>                          
                            <td>&nbsp;Department&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" Width="140px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;Designation&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation" Width="170px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td> &nbsp;Card No &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCardNo" runat="server" class="input controlLength" Width="170px" ClientIDMode="Static"  Enabled="true" ></asp:DropDownList>
                            </td>
                             <td runat="server" id="tdMonth">&nbsp;Month&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlMonth" Width="130px" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>                          
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" ClientIDMode="Static" CssClass="btn btn-success pull-right" Width="120px" OnClick="btnPrintPreview_Click"/>                            
                            </td>
                        </tr>
                    </table>  
                <table  style=" margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px" visible="false" runat="server" id="tblDateRange">
            <tr>
                  <td>From Date</td>
                        <td>                            
                            <asp:TextBox ID="txtFdate" Width="174"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtFdate">
                               </asp:CalendarExtender>  
                        </td>
                <td>To Date</td>
                <td>
                    <asp:TextBox ID="txtTdate" Width="174"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtTdate">
                               </asp:CalendarExtender> </td>              
            </tr>
        </table>
    </div>
     </ContentTemplate>
   </asp:UpdatePanel>
    <div class="tgPanel">        
        <div class="widget">
            <div class="head">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                                             
                    </Triggers>
                    <ContentTemplate>                        
                        <div class="dataTables_filter" style="float: right;">
                        </div>                  
            
              </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            <div class="clearfix"></div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                   
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                    <div style="width: 100%; overflow: auto">
                        <asp:GridView runat="server" ID="gvAttendanceSummary" ClientIDMode="Static"
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
            $("#ddlCardNo_D").select2();
            $("#ddlCardNo").select2();
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function InputValidation()
        {
            if (validateCombo('ddlShiftList', "0", 'Select a Shift') == false) return false;
            if (validateCombo('ddlBatch', "0", 'Select a Batch') == false) return false;
            if (validateCombo('ddlMonths', "0", 'Select a class') == false) return false;
            // if (validateCombo('ddlClass', "0", 'Select a class') == false) return false;
            return true;
        }
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {
            $("#ddlCardNo_D").select2();
            $("#ddlCardNo").select2();
        }
    </script>
</asp:Content>
