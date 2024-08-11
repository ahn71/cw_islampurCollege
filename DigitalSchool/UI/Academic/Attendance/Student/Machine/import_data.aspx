<%@ Page Title="Attendance Data Import" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="import_data.aspx.cs" Inherits="DS.UI.Academics.Attendance.Student.Machine.import_data" %>
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
            /*width: 200px;*/
            min-width: 150px;
        }  
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .tgheight {
            height:244px;
        }
        .form-inline {
            margin-left: 34px;
    margin-right: 31px;
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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/Student/StdAttnHome.aspx">Student Attendance</a></li>
                <%--<li><a runat="server" href="~/UI/Academic/Attendance/Student/Machine/MachineHome.aspx">Student Attendance By Machine</a></li>--%> 
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
                            <div class="row">
                                <div class="col-md-4">Student Attendance Import</div>
                                 <div class="col-md-4">
                                     <asp:FileUpload ID="fileupload" runat="server" CssClass="fileUpload" />
                                <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
                                     </div>
                                 <div class="col-md-4"></div>
                                </div>
                            </div>
                           
                        </div>
                    </div>
                <div class="col-md-1"></div>
                </div>
     <div class="row">    
   
         <div class="col-md-1"></div>
         <div class="col-md-5">
         <div class="tgPanel tgheight">                                
                        <table class="tbl-controlPanel">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Label runat="server" ID="lblFullImport" Font-Bold="True">Full Process</asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td>Shift</td>
                                <td>
                                    <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                              <tr>
                                <td>Batch</td>
                                <td>
                                    <asp:DropDownList ID="ddlFullImportBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                           
                            <tr>
                                <td>Date</td>
                                <td>
                                    <asp:TextBox ID="txtAttendanceDate" PLaceHolder="Click For Calendar" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength form-control"></asp:TextBox>
                                    <asp:CalendarExtender runat="server" Format="dd-MM-yyyy"
                                        PopupButtonID="imgAttendanceDate" Enabled="True"
                                        TargetControlID="txtAttendanceDate" ID="CExtApplicationDate">
                                    </asp:CalendarExtender>
                                    <asp:RequiredFieldValidator ForeColor="Red" ValidationGroup="Impirt" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtAttendanceDate"
                                        ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                       
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnImport" ValidationGroup="Impirt" CssClass="btn btn-primary" runat="server" Text="Process"
                                        OnClientClick=" return InputValidationBasket2();" OnClick="btnImport_Click" />
                                    <asp:Button ID="Button3" runat="server" Text="Close" PostBackUrl="~/UI/Academic/Attendance/Student/StdAttnHome.aspx" CssClass="btn btn-danger " />
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
                                    <asp:Label runat="server" ID="Label2" Font-Bold="True">Partial Process</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Shift</td>
                                <td>
                                    <asp:DropDownList ID="ddlPartialShift" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td>Batch</td>
                                <td>
                                    <asp:DropDownList ID="ddlPartialImportBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Adm. No
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCardNo" PLaceHolder="Type Admission No" runat="server"
                                        ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartialAttDate" PLaceHolder="Click For Calendar" runat="server"
                                        ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtPartialAttDate_CalendarExtender" Format="dd-MM-yyyy" runat="server" TargetControlID="txtPartialAttDate">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnPartialImport" CssClass="btn btn-primary" runat="server" Text="Process"
                                        OnClientClick=" return InputValidationBasket();" OnClick="btnPartialImport_Click" />
                                    <asp:Button ID="Button2" runat="server" Text="Close" PostBackUrl="~/UI/Academic/Attendance/Student/StdAttnHome.aspx" CssClass="btn btn-danger" />
                                </td>
                            </tr>
                        </table>
                    </div>  
                    </div>
         <div class="col-md-1"></div>
         </div>             
          <br />
    <div runat="server" visible="false" class="row"> 
        <div class="col-md-1"></div>      
        <div class="col-md-10">
        <div class="tgPanel">
            <div class="tgPanelHead">Students Attendance Details</div>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlShiftList" />
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSection" />                  
                    <asp:AsyncPostBackTrigger ControlID="ddlMonths" />                     
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAbsentList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayAttendanceList" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnTodayPresentList" EventName="Click" />      
                    
                </Triggers>
                <ContentTemplate>
                    <br />
                    <div class="row">                    
                        <div class="col-md-12">
                    <div class="form-inline">
                         <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                <asp:DropDownList ID="ddlShiftList" runat="server" class="input controlLength form-control" AutoPostBack="True" >
                                    
                                   
                                </asp:DropDownList>
                             </div>
                        <div class="form-group">
                                    <label for="exampleInputEmail2">Batch</label>
                            
                                <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength form-control" AutoPostBack="True" 
                                    OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"   ></asp:DropDownList>
                            </div>
                         <div class="form-group" runat="server" id="divGroup" visible="false">
                                    <label for="exampleInputName2">Group</label>
                          
                                <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength form-control" ClientIDMode="Static"  Enabled="true"  AutoPostBack="True"
                                     OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ></asp:DropDownList>
                             </div>
                          <div class="form-group">
                                    <label for="exampleInputName2">Section</label>
                                <asp:DropDownList ID="ddlSection" runat="server"  class="input controlLength form-control"  AutoPostBack="True"> </asp:DropDownList>
                              </div>
                           <div class="form-group">
                                    <label for="exampleInputName2">Month</label>
                            <asp:DropDownList ID="ddlMonths" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                AutoPostBack="True" >
                            </asp:DropDownList>
                               </div>                    
                                                 
                               
                          </div>
                  <br />     
          
          <div class="form-inline">
               <div class="form-group">
                        <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                             CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click" />
                            </div>
                   <div class="form-group">
                        <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin"  OnClick="btnTodayPresentList_Click" />
                       </div>
                    <div class="form-group">
                        <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click"  />
                        </div>
                               <div class="form-group">         
                                <asp:Button runat="server" ID="btnPrintPreview" Text="Print Preview"
                                    CssClass="btn btn-primary litleMargin" OnClick="btnPrintPreview_Click"   /> 
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
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function InputValidationBasket() {
            try {
                if ($('#ddlPartialShift').val() == 0) {
                    showMessage('Please select a shift', 'error');
                    $('#ddlPartialShift').focus(); return false;
                }
                if ($('#ddlPartialImportBatch').val() == 0) {
                    showMessage('Please select a Batch', 'error');
                    $('#ddlPartialImportBatch').focus(); return false;
                }
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
                if ($('#ddlShift').val()== 0) {
                    showMessage('Please select a shift', 'error');
                    $('#ddlShift').focus(); return false;
                }
                if ($('#ddlFullImportBatch').val() == 0) {
                    showMessage('Please select a Batch', 'error');
                    $('#ddlFullImportBatch').focus(); return false;
                }
                if ($('#txtAttendanceDate').val().trim().length == 0) {
                    showMessage('Please select date for attendance import', 'error');
                    $('#txtAttendanceDate').focus(); return false;
                }
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
        function ClearInputBox() {
            try {
                $("#txtAttendanceDate").val('');
                $('#txtPartialAttDate').val('');
                $('#txtCardNo').val('');
                $('#ddlShift').val('');
            }
            catch (exception) {
                showMessage(exception, error)
            }
        }
        
    </script>
</asp:Content>
