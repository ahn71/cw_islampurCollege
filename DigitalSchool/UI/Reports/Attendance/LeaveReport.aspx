<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="LeaveReport.aspx.cs" Inherits="DS.UI.Reports.Attendance.LeaveReport" %>
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
                <li class="active">Teacher and Sfatt Leave Report</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>        
    </asp:UpdatePanel>
    <div id="ManiDivDailyAtt_L" style="border:1px black solid">
        <div class="tgPanelHead">Leave List Report</div>
        <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShift_L" />
            <asp:AsyncPostBackTrigger ControlID="ddlDepartment_L" />  
             <asp:AsyncPostBackTrigger ControlID="ddlDesignation_L" />
            <asp:AsyncPostBackTrigger ControlID="rblReportType_L" />  
             <asp:AsyncPostBackTrigger ControlID="btnPrint_L" />   
            <asp:AsyncPostBackTrigger ControlID="lnkNew_L" />                              
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >       
         <div style="margin:0px auto; width: 325px;  height: 35px; margin-top:10px" ">
                   <table style="height:35px"><tr><td>
        <asp:RadioButtonList ID="rblReportType_L" runat="server" AutoPostBack="true" CssClass="radiobuttonlist"  RepeatDirection="Horizontal"  OnSelectedIndexChanged="rblReportType_L_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">All</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Individual</asp:ListItem>           
        </asp:RadioButtonList>
                       </td>
                       <td>&nbsp;&nbsp;</td>                       
                       <td runat="server" id="tdtxtCardNo_L" visible="false" >                          
                   <asp:TextBox ID="txtCardNo_L" Width="100px"  runat="server" PlaceHolder="Card No" CssClass="form-control text_box_width_import" Font-Bold="true" ForeColor="#414141"  ></asp:TextBox>                          
                    </td>
                       <td>
                            &nbsp;<asp:LinkButton runat="server" Visible="false" Text="New" ID="lnkNew_L"  ClientIDMode="Static" ForeColor="blue" Font-Bold="true" OnClick="lnkNew_L_Click"></asp:LinkButton>
                           </td> </tr></table>
                     </div> 
        <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server" >
            <tr>
                <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShift_L" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" >                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Department &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment_L"  runat="server" class="input controlLength" Width="140px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                             <td> &nbsp;Designation &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation_L" runat="server" class="input controlLength" Width="160px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" ></asp:DropDownList>
                            </td>             
                
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_L" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right"  OnClick="btnPrint_L_Click"/> </td>                       
                </tr>          
            </table> 
        <br />
        <table style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate_L" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate_L" ID="CalendarExtender2">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate_L" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate_L" ID="CalendarExtender3">
              </asp:CalendarExtender>
             </td>              
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>
    <div id="ManiDivDailyAtt_Ap" style="border:1px black solid">
        <div class="tgPanelHead">Approved and Rejected Leave List </div>
        <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShift_Ap" />
            <asp:AsyncPostBackTrigger ControlID="ddlDepartment_Ap" />  
             <asp:AsyncPostBackTrigger ControlID="ddlDesignation_Ap" />
            <asp:AsyncPostBackTrigger ControlID="rblReportType_Ap" />  
             <asp:AsyncPostBackTrigger ControlID="btnPrint_Ap" />   
            <asp:AsyncPostBackTrigger ControlID="lnkNew_Ap" />                              
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >   
        <asp:RadioButtonList runat="server" ID="rblApprovedRejected" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">All</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Approved</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Rejected</asp:ListItem>
        </asp:RadioButtonList>    
         <div style="margin:0px auto; width: 325px;  height: 35px; margin-top:10px" ">
                   <table style="height:35px"><tr><td>
        <asp:RadioButtonList ID="rblReportType_Ap" runat="server" AutoPostBack="true" CssClass="radiobuttonlist"  RepeatDirection="Horizontal"  OnSelectedIndexChanged="rblReportType_Ap_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">All</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Individual</asp:ListItem>           
        </asp:RadioButtonList>
                       </td>
                       <td>&nbsp;&nbsp;</td>                       
                       <td runat="server" id="tdtxtCardNo_Ap" visible="false" >                          
                   <asp:TextBox ID="txtCardNo_Ap" Width="100px"  runat="server" PlaceHolder="Card No" CssClass="form-control text_box_width_import" Font-Bold="true" ForeColor="#414141"  ></asp:TextBox>                          
                    </td>
                       <td>
                            &nbsp;<asp:LinkButton runat="server" Visible="false" Text="New" ID="lnkNew_Ap"  ClientIDMode="Static" ForeColor="blue" Font-Bold="true" OnClick="lnkNew_Ap_Click"></asp:LinkButton>
                           </td> </tr></table>
                     </div> 
        <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server" >
            <tr>
                <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShift_Ap" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" >                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Department &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment_Ap"  runat="server" class="input controlLength" Width="140px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                             <td> &nbsp;Designation &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation_Ap" runat="server" class="input controlLength" Width="160px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" ></asp:DropDownList>
                            </td>             
                
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_Ap" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right"  OnClick="btnPrint_Ap_Click"/> </td>                       
                </tr>          
            </table> 
        <br />
        <table style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate_Ap" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate_Ap" ID="CalendarExtender4">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate_Ap" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate_Ap" ID="CalendarExtender5">
              </asp:CalendarExtender>
             </td>              
            </tr>
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
  </div>
    <div id="ManiDivDailyAtt" style="border:1px black solid">
        <div class="tgPanelHead">Leave Balance Report</div>
        <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShift" />
            <asp:AsyncPostBackTrigger ControlID="ddlDepartment" />  
             <asp:AsyncPostBackTrigger ControlID="ddlDesignation" />
            <asp:AsyncPostBackTrigger ControlID="rblReportType_B" />  
             <asp:AsyncPostBackTrigger ControlID="btnPrint" />   
            <asp:AsyncPostBackTrigger ControlID="lnkNew_B" />                              
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >
         <%--
        <asp:RadioButtonList runat="server" ID="rblEmpType_D" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">Both</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Faculty</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Staff</asp:ListItem>
        </asp:RadioButtonList>--%>  
         <%--<div style="text-align:center; width:600px; margin:0px auto; margin-top:10px">
            <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0">Attendance Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="1"> Present Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="2"> Absent Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="3">Log in-out Time</asp:ListItem>
        </asp:RadioButtonList>                   
                     </div>  --%> 
         <div style="margin:0px auto; width: 325px;  height: 35px; margin-top:10px" ">
                   <table style="height:35px"><tr><td>
        <asp:RadioButtonList ID="rblReportType_B" runat="server" AutoPostBack="true" CssClass="radiobuttonlist"  RepeatDirection="Horizontal"  OnSelectedIndexChanged="rblReportType_B_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">All</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Individual</asp:ListItem>           
        </asp:RadioButtonList>
                       </td>
                       <td>&nbsp;&nbsp;</td>                       
                       <td runat="server" id="tdtxtCardNo_B" visible="false" >                          
                   <asp:TextBox ID="txtCardNo_B" Width="100px"  runat="server" PlaceHolder="Card No" CssClass="form-control text_box_width_import" Font-Bold="true" ForeColor="#414141"  ></asp:TextBox>                          
                    </td>
                       <td>
                            &nbsp;<asp:LinkButton runat="server" Visible="false" Text="New" ID="lnkNew_B"  ClientIDMode="Static" ForeColor="blue" Font-Bold="true" OnClick="lnkNew_B_Click"></asp:LinkButton>
                           </td> </tr></table>
                     </div> 
        <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server" >
            <tr>
                <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShift" Width="100px" runat="server" class="input controlLength" AutoPostBack="True" >                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Department &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment"  runat="server" class="input controlLength" Width="140px" AutoPostBack="True"></asp:DropDownList>
                            </td>
                             <td> &nbsp;Designation &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDesignation" runat="server" class="input controlLength" Width="160px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" ></asp:DropDownList>
                            </td>             
                
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_Click"/> </td>                       
                </tr>          
            </table> 
        <br />
        <table style="margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
             
            <tr>               
                <td>&nbsp;From Date&nbsp;</td>
             <td><asp:TextBox ID="txtFromDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtFromDate" ID="txtFromDate_Calendar">
              </asp:CalendarExtender>
             </td>
                 <td>&nbsp;To Date&nbsp;</td>
             <td><asp:TextBox ID="txtToDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                 <asp:CalendarExtender runat="server" Format="dd-MM-yyyy" TargetControlID="txtToDate" ID="CalendarExtender1">
              </asp:CalendarExtender>
             </td>
              
               <%-- <td>&nbsp;To Date&nbsp;</td>
                <td><asp:TextBox ID="txtTDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="form-control text_box_width_import" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtTDate">
                               </asp:CalendarExtender> </td> --%>
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
          <asp:AsyncPostBackTrigger ControlID="rblReportType" />
            <asp:AsyncPostBackTrigger ControlID="lnkNew" />   
            <%--  <asp:AsyncPostBackTrigger ControlID="rblEmpType" />          --%>
        </Triggers>
        <ContentTemplate>
            <div class="tgPanelHead">Yearly Leave Status Report</div>
           <div class="tgPanel">
            <%--    <asp:RadioButtonList runat="server" ID="rblEmpType"  AutoPostBack="true" CssClass="radiobuttonlist" RepeatDirection="Horizontal" >
            <asp:ListItem style="margin-left:10px" Selected="True" Value="2">Both</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="1">Faculty</asp:ListItem>
             <asp:ListItem style="margin-left:10px"  Value="0">Staff</asp:ListItem>
        </asp:RadioButtonList>--%>
               <div style="margin:0px auto; width: 325px;  height: 35px; margin-top:10px" ">
                   <table style="height:35px"><tr><td>
        <asp:RadioButtonList ID="rblReportType" runat="server" AutoPostBack="true" CssClass="radiobuttonlist"  RepeatDirection="Horizontal"  OnSelectedIndexChanged="rblReportType_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">All</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Individual</asp:ListItem>           
        </asp:RadioButtonList>
                       </td>
                       <td>&nbsp;&nbsp;</td>                       
                       <td runat="server" id="tdtxtCardNo" visible="false">                          
                   <asp:TextBox ID="txtCardNo" Width="100px"  runat="server" PlaceHolder="Card No" CssClass="form-control text_box_width_import" Font-Bold="true" ForeColor="#414141"  ></asp:TextBox>                          
                    </td>
                       <td>
                            &nbsp;<asp:LinkButton runat="server" Visible="false" Text="New" ID="lnkNew"  ClientIDMode="Static" ForeColor="blue" Font-Bold="true" OnClick="lnkNew_Click"></asp:LinkButton>
                           </td> </tr></table>
                     </div>
                <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px">
                        <tr> 
                              <td>Shift&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" Width="100px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>                          
                            <td>&nbsp;Department&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlDepartmentList" Width="140px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;Designation&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlDesignationList" Width="170px" CssClass="input controlLength" runat="server" ClientIDMode="Static"
                                    AutoPostBack="True" >
                                </asp:DropDownList>
                            </td>
                             <td runat="server" id="tdMonth">&nbsp;Year&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlYear" Width="130px" CssClass="input controlLength" runat="server" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>                          
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" ClientIDMode="Static" CssClass="btn btn-success pull-right" Width="120px" OnClick="btnPrintPreview_Click"/>                            
                            </td>
                        </tr>
                    </table>                      
    </div>
     </ContentTemplate>
   </asp:UpdatePanel>    
<%--    <div class="tgPanel">        
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
    </div>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function InputValidation()
        {
            if (validateText('txtFromDate', 1, 20, 'Enter a admission date') == false) return false;
            if (validateText('txtToDate', 1, 20, 'Enter a admission number') == false) return false;           
            // if (validateCombo('ddlClass', "0", 'Select a class') == false) return false;
            return true;
        }      
    </script>
</asp:Content>
