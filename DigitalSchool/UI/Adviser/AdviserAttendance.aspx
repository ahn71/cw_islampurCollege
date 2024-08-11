<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="AdviserAttendance.aspx.cs" Inherits="DS.UI.Adviser.AdviserAttendance" %>
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
     <asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <p class="message" id="P1" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        DashBoard
                    </a>
                </li>  
                <li><a runat="server" href="~/UI/Adviser/AttLeaveHome.aspx">Attendance And Leave </a></li>              
                <li class="active">Attendance</li>                               
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
        <div class="tgPanelHead">Daily Attendance Report</div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>           
             <asp:AsyncPostBackTrigger ControlID="btnPrint_D" />                                 
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >       
         <div style="text-align:center; width:600px; margin:0px auto; margin-top:10px">
            <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="0">Attendance Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="1"> Present Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="2"> Absent Status</asp:ListItem>
            <asp:ListItem style="margin-left:10px" Value="3">Log in-out Time</asp:ListItem>
        </asp:RadioButtonList>                   
                     </div>           
        <table id="Table2" style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px;width:auto" runat="server">
            <tr>
                <td>&nbsp;From Date&nbsp;</td>
                <td><asp:TextBox ID="txtDate" Width="170px"  runat="server" ClientIDMode="Static" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtDate">
                               </asp:CalendarExtender> </td>                
                <td>&nbsp;To Date&nbsp;</td>
                <td><asp:TextBox ID="txtToDate" Width="170px"  runat="server" ClientIDMode="Static" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                               </asp:CalendarExtender> </td> 
                 <td>&nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click"/> </td>                              
                </tr>             
        </table> 
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
                 
        </Triggers>
        <ContentTemplate>
            <div class="tgPanelHead">Monthly Attendance Report</div>
           <div class="tgPanel" style="height:170px">              
               <div style="margin:0px auto; width:415px; margin-top:10px" ">
        <asp:RadioButtonList ID="rblRepotMontly" runat="server" AutoPostBack="true" CssClass="radiobuttonlist" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblRepotMontly_SelectedIndexChanged" >
            <asp:ListItem Selected="True" Value="0" style="margin-left:10px">Attendance Sheet</asp:ListItem>
            <asp:ListItem Value="1" style="margin-left:10px">Attendance Summary</asp:ListItem>
            <asp:ListItem Value="2" style="margin-left:10px">Absent List</asp:ListItem>
        </asp:RadioButtonList>
            </div>
                    <table style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px">
                        <tr>                              
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
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                                             
                    </Triggers>
                    <ContentTemplate>                        
                        <div class="dataTables_filter" style="float: right;">
                        </div>                  
            
              </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            <div class="clearfix"></div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
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
             $(document).on("keyup", '.search', function () {
                 searchTable($(this).val(), 'tblStudentInfo', '');
             });
         });
         function InputValidation() {
             if (validateCombo('ddlShiftList', "0", 'Select a Shift') == false) return false;
             if (validateCombo('ddlBatch', "0", 'Select a Batch') == false) return false;
             if (validateCombo('ddlMonths', "0", 'Select a class') == false) return false;
             // if (validateCombo('ddlClass', "0", 'Select a class') == false) return false;
             return true;
         }
         function goToNewTab(url) {
             window.open(url);         
         }       
    </script>
</asp:Content>
