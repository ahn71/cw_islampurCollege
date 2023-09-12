<%@ Page Title="Attendance Import" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="emp_import_data.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Machine.emp_import_data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tgPanel {
            width: 100%;
        }
        .alignment {
            text-align: center;
        } 
        .controlLength{
            width: 200px;
        }  
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        } 
         .table tr th{
            background-color: #23282C;
            color: white;
        } 
         .form-inline{ margin-left: 30px;}      
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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Teacher and Staff Attendance</a></li>
                <%--<li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Machine/MachineHome.aspx">Attendance By Machine</a></li>--%>
                <li class="active">Attendance Data Import</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <%--<asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnImport" />
        </Triggers>
        <ContentTemplate>--%>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <div class="tgPanel">
                        <div class="tgPanelHead">
                            <div runat="server"  class="row">
                                <div class="col-md-4">Teacher and Staff Attendance Import</div>
                                 <div class="col-md-4">
                                     <asp:FileUpload ID="fileupload" runat="server" CssClass="fileUpload" />
                                <asp:FileUpload ID="FileUpload1" Visible="false"
                                     runat="server" />
                                     </div>
                                 <div class="col-md-4"></div>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                </div>
                 <div class="row">         
         <div class="col-md-1"></div>
         <div class="col-md-5">
         <div class="tgPanel">  
                        <table class="tbl-controlPanel">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="lblFullImport" Font-Bold="True">Full Import</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Shift
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAttendanceDate" PLaceHolder="Click For Calendar" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength"></asp:TextBox>
                                    <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                        PopupButtonID="imgAttendanceDate" Enabled="True"
                                        TargetControlID="txtAttendanceDate" ID="CExtApplicationDate">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="Impirt" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtAttendanceDate" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnImport" ValidationGroup="Impirt" CssClass="btn btn-primary" runat="server" Text="Import"
                                        OnClientClick=" return InputValidationBasket2();" OnClick="btnImport_Click" />
                                    <asp:Button ID="Button3" runat="server" Text="Close" PostBackUrl="~/default.aspx" CssClass="btn btn-default" />
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: #1fb5ad; font-weight: bold; float: left">
                                                <p>Wait attendance&nbsp; processing</p>
                                            </span>
                                            <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>
                    </div>        
             </div>
               
                <div class="col-md-5">
         <div class="tgPanel">  
                        <table class="tbl-controlPanel">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="Label2" Font-Bold="True">Partial Import</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Card
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardNo" PLaceHolder="Type Card No" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartialAttDate" PLaceHolder="Click For Calendar" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtPartialAttDate_CalendarExtender" Format="dd-MM-yyyy" runat="server" TargetControlID="txtPartialAttDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnPartialImport" CssClass="btn btn-primary" runat="server" Text="Import"
                                        OnClientClick=" return InputValidationBasket();" OnClick="btnPartialImport_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Close" PostBackUrl="~/default.aspx" CssClass="btn btn-default" />
                                </td>
                            </tr>
                        </table>
                     </div>  
                    </div>
         </div>  
     <br />
    <div runat="server" visible="false" class="row"> 
        <div class="col-md-1"></div>      
        <div class="col-md-10">
        <div class="tgPanel">
            <div class="tgPanelHead">Attendance Details</div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />                         
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" EventName="Click" />      
                    
                </Triggers>
                <ContentTemplate>
                    <br />
                    <div class="row"> 
                        <div class="col-lg-1"></div>                   
                        <div class="col-md-11">
                    <div class="form-inline">
                         <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                <asp:DropDownList ID="ddlShiftList" runat="server" class="input controlLength form-control" AutoPostBack="false" >
                                    
                                   
                                </asp:DropDownList>
                             </div>
                        <div class="form-group">
                                    <label for="exampleInputEmail2">Department</label>
                            
                                <asp:DropDownList ID="dlDepartment"  runat="server" class="input controlLength form-control"  AutoPostBack="false" 
                                       ></asp:DropDownList>
                            </div>
                       
                          <div class="form-group">
                                    <label for="exampleInputName2">Designation</label>
                                <asp:DropDownList ID="dlDesignation" runat="server"  class="input controlLength form-control" AutoPostBack="false"> </asp:DropDownList>
                              </div>
                           <div class="form-group">
                                    <label for="exampleInputName2">Month</label>
                            <asp:DropDownList ID="dlSheetName" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                AutoPostBack="false" >
                            </asp:DropDownList>
                               </div> 
                        <div class="form-group">
                             <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-success litleMargin" ClientIDMode="Static" runat="server"
                                    OnClick="btnSearch_Click" />
                        </div>                   
                                                 
                               
                          </div>
                  <br />     
          
          <div class="form-inline">
               <div class="form-group">
                        <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                          OnClick="btnTodayAttendanceList_Click"   CssClass="btn btn-primary litleMargin"  />
                            </div>
                   <div class="form-group">
                        <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayPresentList_Click" />
                       </div>
                    <div class="form-group">
                        <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin"  OnClick="btnTodayAbsentList_Click" />
                        </div>
                               <div class="form-group">         
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin"  OnClick="btnPrintPreview_Click1"  /> 
                                   </div>                     
                 
                        </div>
                            <br />
                            </div>                       
                         </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-1"></div> 
       
        </div> 
    <br />
    <div runat="server" visible="false" class="row"> 
        <div class="col-md-1"></div>      
        <div class="col-md-10">
        <div class="tgPanel">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                </Triggers>
                <ContentTemplate>   
    <div class="widget">
                <div class="head">
                    <div class="dataTables_filter" style="float: right;">
                    </div>
                </div>                          
                         <div id="div1" class="datatables_wrapper" runat="server" style="width: 100%; height: auto">  
                                 <div style="margin:0px auto; width:748px">
                                    <asp:GridView ID="gvAttList" CssClass="table controlPanel"  AutoGenerateColumns="False" ClientIDMode="Static" runat="server">                                   
                                    <Columns>                                       
                                        <asp:BoundField HeaderText="Card No" DataField="ECardNo" />
                                        <asp:BoundField HeaderText="Name"  DataField="EName"/>
                                        <asp:BoundField HeaderText="Department"  DataField="DName"/>
                                        <asp:BoundField HeaderText="Designation"  DataField="DesName"/>
                                        <asp:BoundField HeaderText="Status"  DataField="AttStatus" />                                       
                                    </Columns>
                                </asp:GridView> 
                                </div>                                                                                     
                            </div>                
                        <div id="divMonthWiseAttendaceSheet" class="datatables_wrapper" runat="server" style="width: 100%; height: auto;margin-top:5px"></div>
                     </div>        
        </ContentTemplate>
    </asp:UpdatePanel>
            </div>
            </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function InputValidationBasket() {
            try {
                if ($('#txtCardNo').val().trim().length < 1) {
                    showMessage('Please type valid card no', 'error');
                    $('#txtCardNo').focus(); return false;
                }

                if ($('#txtPartialAttDate').val().trim().length == 0) {
                    showMessage('Please select date for partial attendance import', 'error');
                    $('#txtPartialAttDate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
        function InputValidationBasket2() {
            try {
                //if ($('#ddlShift').val().trim().length == 0) {
                //    showMessage('Please select a shift', 'error');
                //    $('#ddlShift').focus(); return false;
                //}

                if ($('#txtAttendanceDate').val().trim().length == 0) {
                    showMessage('Please select date for attendance import', 'error');
                    $('#txtAttendanceDate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
    </script>
</asp:Content>

