<%@ Page Title="Attendance Entry By Manually" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FacultyAttendance.aspx.cs" Inherits="DS.UI.Academics.Attendance.StafforFaculty.Manually.FacultyAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        .controlLength{
            width:150px;
            margin: 5px;
        }
        .form-inline{ margin-left: 20px;}
        .tgPanel
        {
            width: 100%;
        }
        #tblSetRollOptionalSubject
        {
            width:100%;            
        }
        #tblSetRollOptionalSubject th,  
        #tblSetRollOptionalSubject td, 
        #tblSetRollOptionalSubject td input,
        #tblSetRollOptionalSubject td select
        {
            padding: 5px 5px;
            margin-left: 10px;
            text-align: center;
        }
        .litleMargin{
            margin-left: 5px;
        }
        @media (max-width: 767px) {
            .controlLength {
              width: 90%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/StafforFacultyHome.aspx">Staff or Faculty Attendance</a></li>
               <%-- <li><a runat="server" href="~/UI/Academic/Attendance/StafforFaculty/Manually/ManuallyHome.aspx">Attendance By Manually</a></li>--%>
                <li class="active">Attendance Entry By Manually</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Daily Attendance Count of Faculty and Staff</div>
            <div class="row">
                <div class="col-md-1"></div>               
                <div class="col-md-11">
                    <div class="form-inline">
                        <div class="form-group">
                            <label for="exampleInputName2">Shift</label>
                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="input controlLength"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputName2">Departments</label>
                            <asp:DropDownList ID="dlDepartments" runat="server" CssClass="input controlLength"></asp:DropDownList>
                        </div>
                         <div class="form-group">
                            <label for="exampleInputName2">Designation</label>
                            <asp:DropDownList ID="dlDesignation" runat="server" CssClass="input controlLength"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputName2">Month</label>
                            <asp:DropDownList ID="dlMonths" runat="server" CssClass="input controlLength">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">

                            <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server" CssClass="btn btn-primary litleMargin"
                                OnClick="btnProcess_Click" />
                        </div>
                    </div>
                </div>
            </div>

           <div class="row">                      
                             <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                             <div class="col-md-1"></div>   
                            <div class="col-md-11">
                            <div class="form-inline">
                                 <div class="form-group">
                                   <asp:Button ID="btnTodayAttendanceList" runat="server" ClientIDMode="Static" Text="Todays Attendance List"
                                   CssClass="btn btn-primary litleMargin" OnClick="btnTodayAttendanceList_Click"  />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnTodayPresentList" runat="server" ClientIDMode="Static" Text="Todays Present List"
                            CssClass="btn btn-primary litleMargin"  OnClick="btnTodayPresentList_Click" />
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnTodayAbsentList" runat="server" ClientIDMode="Static" Text="Todays Absent List"
                            CssClass="btn btn-primary litleMargin"   OnClick="btnTodayAbsentList_Click"/>
                                </div>
                                 <div class="form-group">
                                    <asp:Button ID="btnPreview" Text="Preview & Print " ClientIDMode="Static" runat="server"
                                 OnClientClick="return validateDropDown();"   CssClass="btn btn-success litleMargin" OnClick="btnPreview_Click1"  />
                                </div>
                                </div>
                                </div>  
                    </ContentTemplate>
                                 </asp:UpdatePanel>                         
                       
                    </div>
                   
           
            <div class="col-md-2"></div>
                </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <br />
                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left">
                                <p>Wait attendance sheet is processing</p>
                            </span>
                            <img style="width: 26px; height: 26px; cursor: pointer; float: left" src="/images/wait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
       
    </div>
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnProcess" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="AttendanceSheetTitle" style="font-weight: bold; font-size: 22px;"></div>
            <br />
            <div runat="server" id="divTable" style="width: 100%; height: 0 auto; overflow-x: scroll; overflow-y: scroll" visible="false"></div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                if (data == "0 rows affected") alert('Please Search by this date then attendance count !');
                else if (data == "Leave Are Not Exists") alert('Any type of levae are not exists');
                else if (data == "Leave Are Exists") alert('Any type of levae are exists');
            });
        }

        function acceptValidCharacter(e, targetInput) {
            try {
              
                if (e.keyCode == 9) return true; // t

                if (e.keyCode != 65) {//a
                    if (e.keyCode != 80) {//p
                        if (e.keyCode != 76) {//l
                            if (e.keyCode != 86) {//v

                                if (e.keyCode != 8) {//backspace
                                    alert('Please Type a or p or l or v');
                                    //alert(targetInput.id);
                                    $('#' + targetInput.id).val('').focus();
                                    return false;
                                }

                                return false;
                            }
                            return fal;
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

    </script>
</asp:Content>
