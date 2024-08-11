<%@ Page Title="Student Daily Attendance" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentAttendance.aspx.cs" Inherits="DS.UI.Academics.Attendance.Student.Manually.StudentAttendance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength {
            min-width: 120px;
            margin: 5px;
        }
        .tgPanel {
            width: 100%;
        }        
        #tblStudentAttendance thead tr th{
            padding:10px;
            background: #23282c;
            color: white;
        }  
         #tblStudentAttendance thead tr th:nth-child(3){
            text-align:left!important;
        } 
          #fixed-row tr th:nth-child(3){
            text-align:left!important;
        }
        #tblStudentAttendance tbody tr td:nth-child(2){
            padding-left:1%;
        } 
        .litleMargin {
            margin-left: 5px;
        }
        .LoadingImg{
            width:26px;
            height:26px;
        }
        .form-inline {
            margin-left:40px;
            margin-right: 40px;
        }
       .fixed-row {
        position: fixed!important;
        z-index: 999;
        margin-top: -280px!important;
        display: inline-table;
        width: 44.2%;
        overflow: hidden;
       }
       
       #MainContent_divTable{
           overflow:hidden;
       }
      
        .dpwidth{
            width:120px;
        }

        #tblStudentAttendance {
            width: 83%;          
            margin: 0 auto;
        }
        .table-hover>tbody>tr:hover {
    background-color: #FFEB95;
}    #tblStudentAttendance {
    width: 45%;
}
       
        
        @media screen and (max-width: 1024px){
                .fixed-row {
    width: 79.5%;
    margin-top:-323px!important;
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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/AttendanceHome.aspx">Attendance Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Attendance/Student/StdAttnHome.aspx">Student Attendance</a></li>
               <%-- <li><a runat="server" href="~/UI/Academic/Attendance/Student/Manually/ManuallyHome.aspx">Student Attendance By Manually</a></li>--%>
                <li class="active">Attendance Entry By Manually</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Daily Attendance Count of Students</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                    <asp:AsyncPostBackTrigger ControlID="btnProcess" />
                </Triggers>
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-11">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                    <asp:DropDownList ID="ddlShiftList"  runat="server" class="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">Batch</label>
                                    <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divGroup" visible="false">
                                    <label for="exampleInputName2">Group</label>
                                    <asp:DropDownList ID="ddlgroup"  runat="server" class="input controlLength form-control" ClientIDMode="Static" Enabled="true" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputName2">Section</label>
                                    <asp:DropDownList ID="ddlSection"  runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group ">
                                    <label for="exampleInputName2">Date</label>
                                   <asp:TextBox ID="txtdate" runat="server"
                                                                CssClass="input controlLength form-control dpwidth" ClientIDMode="Static" AutoPostBack="false"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" runat="server" TargetControlID="txtdate"></asp:CalendarExtender>
                                </div>
                                 <div class="form-group ">
                                    <label runat="server" id="lblexecutionMsg"  >---</label>                                  
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-1"></div>
                            <div class="col-md-9">
                            <div class="form-inline">
                                 <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server"
                                   OnClientClick="return validateDropDown();"  CssClass="btn btn-primary litleMargin" OnClick="btnProcess_Click" />
                                
                                 <div class="form-group">
                                   <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Attendance List"
                                   CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click"  />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Present List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayPresentList_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Absent List"
                            CssClass="btn btn-primary litleMargin" OnClick="btnTodayAbsentList_Click" />
                                </div>
                                <asp:Button ID="btnPreview" Text="Preview & Print " ClientIDMode="Static" runat="server"
                                 OnClientClick="return validateDropDown();"   CssClass="btn btn-success litleMargin" OnClick="btnPreview_Click" />
                                </div>
                                </div>
                            <div class="col-md-2"></div>
                            
                        </div>
                    </div>
                    <br />
                  <div class="row">
                      <div class="col-md-12">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>                           
                            <span style="font-family: 'Times New Roman'; font-size: 1.2em; padding:5px; color: #1fb5ad; font-weight: bold; float: left">
                                <p>Wait attendance sheet is processing</p>
                            </span>
                            <img class="LoadingImg" src="../../../../../AssetsNew/images/input-spinner.gif" />
                            <div class="clearfix"></div>
                        </ProgressTemplate>                        
                    </asp:UpdateProgress>
                          </div>
                      </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnProcess" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="AttendancPanel" runat="server" CssClass="tgPanel" Visible="false" ScrollBars="Auto">
                <h3 runat="server" id="AttendanceSheetTitle" style="font-weight: bold; font-size: 22px; text-align:center"></h3>            
                <div runat="server" id="divTable" style="width: 100%; height: 0 auto;" visible="false"></div>
            </asp:Panel>                   
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateDropDown() {
            if (validateCombo('ddlShiftList', 0, 'Select a Shift') == false) return false;
            if (validateCombo('ddlBatch', 0, 'Select a Batch') == false) return false;
            if (validateCombo('ddlSection', 0, 'Select a Section') == false) return false;
            if (validateCombo('ddlMonths', 0, 'Select a Month') == false) return false;
            return true;
        }

        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                //jx.load('ForUpdate.aspx?tbldata=' + splitedData + ', function (data) {

                if (data == "0 rows affected") alert('Please Search by this date then attendance count !');

            });
        }
        function acceptValidCharacter(e, targetInput) {
            try {
                if (e.keyCode == 9) return true; // t
                if (e.keyCode != 65) {//a
                    if (e.keyCode != 80) {//p
                        if (e.keyCode != 8) {//backspace

                            alert('Please Type a or p');
                            //alert(targetInput.id);
                            $('#' + targetInput.id).val('').focus();
                            return false;
                        }
                        return false;
                    }
                    return true;
                }
                return true;
            }
            catch (e) {
            }
        }


        $(window).scroll(function () {
            if ($(this).scrollTop() > 250) {
                $('#att-head-row').addClass('fixed-row');
            } else {
                $('#att-head-row').removeClass('fixed-row');
            }
        });



    </script>
</asp:Content>
