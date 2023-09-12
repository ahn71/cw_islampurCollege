<%@ Page Title="Teacher Work Allotment" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="WorkAllotment.aspx.cs" Inherits="DS.UI.Academic.Timetable.WorkAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel{
            width: 100%;
        }
        .tbl-controlPanel td{
            width: 50%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;           
        }
        .controlLength{
            width: 230px;
        }   
        optgroup{
            color: #1fb5ad;
        } 
        option{
            color: #424242;
        }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblClsTimeSetId" ClientIDMode="Static" runat="server"/>    
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
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Timetable Module</a></li>                
                <li class="active">Teacher Subject Assign</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-4">
                <h4 class="text-left">Teacher Subject Assign</h4>             
            </div>
            <div class="col-md-4">
            
                  <asp:DropDownList ID="ddlShift" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="220px" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged">
                                                            
                  </asp:DropDownList>
                <asp:Button runat="server" ClientIDMode="Static" ID="btnLoadRepot" Text="Teacher Load Report" CssClass="btn btn-success " OnClick="btnLoadRepot_Click" />

                

            </div>
            <div class="col-md-4">
              <%--  <h4 class="text-right">Teacher List And No. of Assigned</h4>     --%>    
                 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <br />
                            <span style="font-family: 'Times New Roman'; font-size: 20px; color: green; font-weight: bold; float: left;margin-top: -15px;">
                                <p>Loading</p>
                            </span>
           
                            <img style="width: 26px; height: 26px; cursor: pointer; float: left;margin-top: -15px; margin-left: 3px;" src="/images/wait.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>       
            </div>

        </div>
         
        <div class="row">            
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID="ddlShift" />  
                        <asp:AsyncPostBackTrigger ControlID="btnLoadRepot" />                     
                    </Triggers>
                    <ContentTemplate>
                        <div id="SubList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto;  overflow: auto; overflow-x: hidden;">
                        </div>       
                                              
                    </ContentTemplate>

                     

                </asp:UpdatePanel>
               
            </div>
            <%--<div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="TeacherList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>

        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                //jx.load('ForUpdate.aspx?tbldata=' + splitedData + ', function (data) {

                if (data == "0 rows affected") alert('Please Search by this date then attendance count !');

            });
        }

        function SaveInput(id) {
            var subId = [];
            var teacherId = [];
            var classId = id;
            $('#t_' + id + ' td:first-child').each(function (index) {
                subId[index] = $(this).attr('id');
                teacherId[index] = $('#t_' + id + ' td:nth-child(2) > select#s_' + subId[index] + ' Option:selected').val();                
            });
            $.ajax({
                url: "/UI/Academic/Timetable/WorkAllotment.aspx/insertSubTeacher",
                data: "{subId : '" + subId + "', teacherId :'" + teacherId + "', classId : '" + classId + "'}",
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset= utf-8",
                success: OnSuccess,
                error: OnError
            });
            return true;
        }
        function OnSuccess(data)
        {            
            showMessage(data.d, 'success');
        }
        function OnError()
        {
            alert("failed");           
        }
        function goToNewTab(url) {
            window.open(url);            
        }
    </script>
</asp:Content>
