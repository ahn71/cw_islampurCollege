<%@ Page Title="Attendance Sheet" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MonthWiseAttendanceSheet.aspx.cs" Inherits="DS.UI.Reports.Attendance.MonthWiseAttendanceSheet" %>
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
           min-width: 110px;
        }        
      
        .btn {
            margin: 3px;
        }
        .radiobuttonlist label {
            margin-left:5px;
        }
        .boX { text-align: center;} 
        div.col-sm-10.col-sm-offset-1.boX div.form-inline div.form-group label{ text-align: left; }
        div.col-xs-12.col-sm-12.boX div.form-inline div.form-group label { text-align: left; } 
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
                <li class="active">Student Attendance</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <div id="ManiDivDailyAtt" style="border:1px black solid">
        <div class="tgPanelHead">Daily Attendance Report (Students)</div>
        <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShit_D" />
            <asp:AsyncPostBackTrigger ControlID="ddlBatch_D" />  
             <asp:AsyncPostBackTrigger ControlID="ddlGroup_D" />
            <asp:AsyncPostBackTrigger ControlID="ddlSection_D" />  
             <asp:AsyncPostBackTrigger ControlID="btnPrint_D" /> 
            <asp:AsyncPostBackTrigger ControlID="ddlRollNo_D" />                                
        </Triggers>
        <ContentTemplate>
    <div class="tgPanel" >
        <div class="row tbl-controlPanel">
         <div class="col-sm-6 col-sm-offset-3">
            <asp:RadioButtonList ID="rblReportType" RepeatLayout="Flow" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal">
            <asp:ListItem class="radio-inline" Selected="True" style="margin-left:10px">Attendance Status</asp:ListItem>
            <asp:ListItem class="radio-inline" style="margin-left:10px"> Present Status</asp:ListItem>
            <asp:ListItem class="radio-inline" style="margin-left:10px"> Absent Status</asp:ListItem>
           <%-- <asp:ListItem class="radio-inline" style="margin-left:10px">Log in-out Time</asp:ListItem>--%>
                 <asp:ListItem class="radio-inline" style="margin-left:10px">Summary</asp:ListItem>
        </asp:RadioButtonList>                   
                     </div>
            </div>     
        <%--<table class="tbl-controlPanel" style="width:auto" runat="server" >
            <tr>
                <td>Shift &nbsp;</td>
                            <td>
                                <asp:DropDownList ID="ddlShit_D" Width="100px" runat="server" class="input controlLength"  AutoPostBack="true" OnSelectedIndexChanged="ddlShit_D_SelectedIndexChanged" >                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Batch &nbsp;
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlBatch_D"  runat="server" class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_D_SelectedIndexChanged"  ></asp:DropDownList>
                            </td>
                             <td id="tdGrpTitle" runat="server" visible="false"> &nbsp;Group &nbsp;
                            </td>
                            <td id="tdGrpName" runat="server" visible="false">
                                <asp:DropDownList ID="ddlGroup_D" runat="server" class="input controlLength" Width="100px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_D_SelectedIndexChanged"  ></asp:DropDownList>
                            </td>
                            <td>&nbsp;Section&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSection_D" runat="server"  class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_D_SelectedIndexChanged"> </asp:DropDownList>
                            </td>
                 <td>&nbsp;Roll No&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRollNo_D" runat="server"  class="input controlLength" ClientIDMode="Static" Width="80px" > </asp:DropDownList>
                            </td>                
                <td>&nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click"  /> </td>                       
                </tr>
            </table>
        <table class="tbl-controlPanel" style="width:auto" runat="server">
            <tr>
                <td>&nbsp;From Date&nbsp;</td>
                <td><asp:TextBox ID="txtFromDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate">
                               </asp:CalendarExtender>
                </td> 
                <td>
                   To Date 
                </td>
                <td><asp:TextBox ID="txtToDate" Width="170px"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength"  ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                               </asp:CalendarExtender>
                </td> 
            </tr>
        </table> --%> 
        <div class="row tbl-controlPanel"> 
		        <div class="col-sm-10 col-sm-offset-1 boX">
			        <div class="form-inline">
				         <div class="form-group">
					         <label for="exampleInputName2">Shift &nbsp;</label>
					         <asp:DropDownList ID="ddlShit_D" runat="server" class="input controlLength form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlShit_D_SelectedIndexChanged" >                                   
                                </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Batch &nbsp;</label>
                            <asp:DropDownList ID="ddlBatch_D"  runat="server" class="input controlLength form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_D_SelectedIndexChanged"  ></asp:DropDownList>
				         </div>
				        <div id="tdGrpName" runat="server" Visible="False" class="form-group">
					         <label for="exampleInputName2">&nbsp;Group &nbsp;</label>
                            <asp:DropDownList ID="ddlGroup_D" runat="server" class="input controlLength form-control" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_D_SelectedIndexChanged"  ></asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Section &nbsp;</label>
                            <asp:DropDownList ID="ddlSection_D" runat="server"  class="input controlLength form-control"  AutoPostBack="True" OnSelectedIndexChanged="ddlSection_D_SelectedIndexChanged"> </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Roll No&nbsp;</label>
					        <asp:DropDownList ID="ddlRollNo_D" runat="server"  class="input controlLength form-control"  ClientIDMode="Static"  > </asp:DropDownList>
				         </div>
				        
				         <div class="form-group">
					        <asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrint_D_Click"  />
				         </div>
			        </div>
	          </div>
         </div> 
         <div class="row tbl-controlPanel"> 
		        <div class="col-sm-6 col-sm-offset-3">
			        <div class="form-inline">
				         <div class="form-group">
					         <label for="exampleInputName2">&nbsp;From Date&nbsp;</label>
                             <asp:TextBox ID="txtFromDate"   runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength form-control" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate">
                               </asp:CalendarExtender>
                        </div>
                        <div class="form-group">
					         <label for="exampleInputName2">To Date </label>
                            <asp:TextBox ID="txtToDate"   runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength form-control"  ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender1" runat="server" TargetControlID="txtToDate">
                               </asp:CalendarExtender>
                        </div>
                        
                    </div>
               </div>
        </div>
            
    </div>
           </ContentTemplate>
   </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="ManiDivMonthlyAtt" style="border:1px black solid">
       <div class="tgPanelHead">Monthly Attendance Report (Students)</div>
    <asp:UpdatePanel runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />
            <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
            <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
            <asp:AsyncPostBackTrigger ControlID="ddlMonths" />  
            <asp:AsyncPostBackTrigger ControlID="rblRepotMontly" />   
            <asp:AsyncPostBackTrigger ControlID="ddlSection" />  
            <asp:AsyncPostBackTrigger ControlID="ddlRollNo" />                        
        </Triggers>
        <ContentTemplate>                        
    <div class="tgPanel" > 
        <div class="row tbl-controlPanel">
         <div class="col-sm-6 col-sm-offset-3">
        <asp:RadioButtonList ID="rblRepotMontly" RepeatLayout="Flow" runat="server" CssClass="radiobuttonlist" AutoPostBack="true" RepeatDirection="Horizontal"  OnSelectedIndexChanged="rblRepotMontly_SelectedIndexChanged">
            <asp:ListItem class="radio-inline" Selected="True" Value="0" style="margin-left:10px">Attendance Sheet</asp:ListItem>
            <asp:ListItem class="radio-inline" Value="1" style="margin-left:10px">Attendance Summary</asp:ListItem>
            <asp:ListItem class="radio-inline" Value="2" style="margin-left:10px">Absent List</asp:ListItem>
        </asp:RadioButtonList>
            </div>
        </div>
      <%--  <table  style="  margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px" runat="server" >
            <tr>
                <td>Shift &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShiftList" Width="100px" runat="server"  class="input controlLength" AutoPostBack="true" OnSelectedIndexChanged="ddlShiftList_SelectedIndexChanged">
                                   
                                </asp:DropDownList>
                            </td>
                            <td> &nbsp;Batch &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"  ></asp:DropDownList>
                            </td>
                             <td id="tdGrpMtitle" runat="server" visible="false"> &nbsp;Group &nbsp;
                            </td>
                            <td id="tdGrpMName" runat="server" visible="false">
                                <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength" Width="100px" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td> &nbsp;Section &nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSection" runat="server"  class="input controlLength" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"> </asp:DropDownList>
                            </td>
                <td>&nbsp;Roll No&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRollNo" runat="server"  class="input controlLength" ClientIDMode="Static" Width="80px"  > </asp:DropDownList>
                            </td>
                        <td runat="server" id="tdMonth" > &nbsp;Month &nbsp;</td>
                        <td>
                            <asp:DropDownList Visible="false" ID="dlSheetName" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:DropDownList  ID="ddlMonths" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="150px"
                              >
                            </asp:DropDownList>                            
                        </td>                
                <td>
                    &nbsp;<asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                    <asp:Button ID="btnSearch" Visible="false" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                        runat="server" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>--%>
        	<div class="row tbl-controlPanel"> 
		        <div class="col-xs-12 col-sm-12 boX">
			        <div class="form-inline">
				         <div class="form-group">
					         <label for="exampleInputName2">Shift &nbsp;</label>
					            <asp:DropDownList ID="ddlShiftList" runat="server"  class="input controlLength form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlShiftList_SelectedIndexChanged">
                                </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Batch &nbsp;</label>
                            <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"  ></asp:DropDownList>
				         </div>
				        <div id="tdGrpMName" Visible="False" runat="server" class="form-group">
					         <label for="exampleInputName2">&nbsp;Group &nbsp;</label>
                            <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength form-control" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ></asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Section &nbsp;</label>
                            <asp:DropDownList ID="ddlSection" runat="server"  class="input controlLength form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"> </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">&nbsp;Roll No&nbsp;</label>
                            <asp:DropDownList ID="ddlRollNo" runat="server"  class="input controlLength form-control" ClientIDMode="Static"  > </asp:DropDownList>
					
				         </div>
				        <div class="form-group">
					         <label id="tdMonth" runat="server" for="exampleInputName2"> &nbsp;Month &nbsp;</label>
					
					        <asp:DropDownList Visible="false" ID="dlSheetName" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:DropDownList  ID="ddlMonths" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control" 
                              >
                            </asp:DropDownList> 
				         </div>
				         <div class="form-group">
					        <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview" Width="120px"
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                    <asp:Button ID="btnSearch" Visible="false" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                        runat="server" OnClick="btnSearch_Click" />
				         </div>
			        </div>
	          </div>
         </div>
        <div class="row tbl-controlPanel"> 
		        <div class="col-sm-6 col-sm-offset-3">
			        <div class="form-inline" visible="false" runat="server" id="tblDateRange">
				         <div class="form-group">
					         <label for="exampleInputName2">From Date</label>
                             <asp:DropDownList Visible="false" ID="DropDownList1" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:DropDownList Visible="false" ID="DropDownList2" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control" Width="150px"
                                AutoPostBack="True" >
                            </asp:DropDownList>
                            <asp:TextBox ID="txtFdate"  runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength form-control" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender2" runat="server" TargetControlID="txtFdate">
                               </asp:CalendarExtender>
                        </div>
                        <div class="form-group">
					         <label for="exampleInputName2">To Date</label>
                            <asp:TextBox ID="txtTdate"   runat="server" PlaceHolder="Click For Calander" CssClass="input controlLength form-control" ></asp:TextBox>
                               <asp:CalendarExtender Format="dd-MM-yyyy" ID="CalendarExtender3" runat="server" TargetControlID="txtTdate">
                               </asp:CalendarExtender>
                        </div>
                    </div>
              </div>
        </div>
       <%-- <table  style=" margin: 10px auto;font-family: Calibri;font-size: 15px;padding: 5px" visible="false" runat="server" id="tblDateRange">
            <tr>
                  <td>From Date</td>
                        <td>
                            <asp:DropDownList Visible="false" ID="DropDownList1" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            <asp:DropDownList Visible="false" ID="DropDownList2" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="150px"
                                AutoPostBack="True" >
                            </asp:DropDownList>
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
        </table>--%>
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
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div id="lblSectionDiv" runat="server"></div>
                    <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                    <div style="width: 100%; overflow: auto">
                        <asp:GridView runat="server" ID="gvAttendanceSheet" ClientIDMode="Static"
                            CssClass="table table-striped table-bordered tbl-controlPanel">
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>            
        </div>
    </div>           
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">        
        $(document).ready(function () {
            $("#ddlRollNo_D").select2();
            $("#ddlRollNo").select2();
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function inputValidation()
        {

        }
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {
            $("#ddlRollNo_D").select2();
            $("#ddlRollNo").select2();
        }
    </script>
</asp:Content>
