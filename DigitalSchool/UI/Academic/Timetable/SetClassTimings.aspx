<%@ Page Title="Class Routine" Language="C#" MasterPageFile="~/main.Master"  CodeBehind="SetClassTimings.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetClassTimings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;   
            border: none;          
        }
        .controlLength{
            min-width: 120px;
        }
        /*.tbl-controlPanel{
            width: 860px;
        }*/
        /*.tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(3){
            text-align:right;
            padding-right: 5px;
        }*/
        .DivStyle{
            padding: 10px;
        }       
        a:link {
            color: #fff;
        }
        a::before {
            color: #fff;
        }
        .drg-event-title{
            border-bottom: none;
            font-weight: normal;
            padding-bottom: 0;
            margin-top: 5px;
            margin-bottom: 5px;
        }
        #buildings{
            margin: 5px auto;
        }
        .table-defination {
            width: 100%
        }
        .dropped {
           /* width: 150px;*/
            text-align: center;
        }
        ul, ol {
            margin-bottom:0px;
        }
        .table-defination th,
        .table-defination td:first-child {
            background-color: #f6eded;
        }
        .table-defination td div.dropped{
            background-color: #e2dddd;
            padding:10px;
        }
        .table-defination td div.dropped ul{
            list-style: none;
        }
        .table-defination td div.dropped ul li{           
            margin-bottom: 8px;
        }
        .table-defination td div.dropped ul li:last-child{           
            margin-bottom: 0px;
        }
        .table-defination td div.dropped ul li span a {
            margin-left: 5px;
            color: white;
        }
        ul, .list-unstyled {
            padding-left: 0px;
        }
        .table-defination th.rotate {
          /* Something you can count on */
          height: 100%;
          white-space: nowrap;          
        }
        .table-defination th.rotate > div {
          transform: 
            /* Magic Numbers */
            translate(2px, 0px)
            /* 45 is really 360 - 45 */
            rotate(-90deg);
          width: 65px;        
        } 
        .table > thead > tr > th, 
        .table > tbody > tr > th, 
        .table > tfoot > tr > th{
            padding : 0px;
            vertical-align : middle;
        }
        @media only screen and (min-width: 320px) and (max-width: 479px) {
        .controlLength{
            width: 230px;
        }
            .controlLength1 {
                width: 230px;
                margin-left:15px;
            }
           
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../../../../AssetsNew/js/jsUpdateProgress.js" type="text/javascript"></script>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
                            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
            <ProgressTemplate>
                <center>
                    <img id="Img5" runat="server" src="~/AssetsNew/images/input-spinner.gif" alt="loading..." /><br />               
                </center>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblClsTimeSetId" ClientIDMode="Static" runat="server" />    
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
                <li><a style="color:black;" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Routine Module</a></li>                
                <li class="active">Class Routine</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>     
            <div class="">               
                <div class="tgPanel">
                    <div class="col-md-9">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                                <asp:AsyncPostBackTrigger ControlID="dlBatchName"/>
                                <asp:AsyncPostBackTrigger ControlID="ddlClsGroup" />
                                <asp:AsyncPostBackTrigger ControlID="ddlDepartment" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="row tbl-controlPanel"> 
		                            <div class="col-sm-12 boX">
			                            <div class="form-inline">
				                             <div class="form-group">
					                             <label for="exampleInputName2">Shift</label>
					                                <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static"  CssClass="input controlLength form-control">
                                            </asp:DropDownList>
				                             </div>
				                            <div class="form-group">
					                             <label for="exampleInputName2">Batch</label>
                                                <asp:DropDownList ID="dlBatchName" AutoPostBack="true" runat="server" ClientIDMode="Static"  CssClass="input controlLength form-control"
                                                OnSelectedIndexChanged="dlBatchName_SelectedIndexChanged">
                                                <asp:ListItem Value="0">...Select Batch...</asp:ListItem>
                                            </asp:DropDownList>
				                             </div>
				                            <div class="form-group">
					                             <label for="exampleInputName2">Group</label>
                                                
                                            <asp:DropDownList ID="ddlClsGroup" runat="server" ClientIDMode="Static" AutoPostBack="true" CssClass="input controlLength form-control" OnSelectedIndexChanged="ddlClsGroup_SelectedIndexChanged">
                                            </asp:DropDownList>
				                             </div>
				                            <div class="form-group">
					                             <label for="exampleInputName2">Section</label>
                                                <asp:DropDownList ID="ddlClsSection" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                            </asp:DropDownList>
				                             </div>
				                            <div class="form-group boX">
					                             <label for="exampleInputName2"></label>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary"
                                                    OnClientClick="return validationInput();" OnClick="btnSearch_Click" />
                                                <asp:Button ID="btnPrint" runat="server" Text="Print" ClientIDMode="Static" CssClass="btn btn-warning"
                                                    OnClick="btnPrint_Click" />
					
				                             </div>
				                           
			                            </div>
	                              </div>
                             </div>
                                <div class="row tbl-controlPanel"> 
		                            <div class="col-xs-12 col-sm-12">
			                            <div class="form-inline">
				                             <div class="form-group">
					                             <label for="exampleInputName2">Department:</label>
					                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="input controlLength form-control"
                                                AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                            </asp:DropDownList>
                                                 
				                             </div>
				                          <div class="form-group">
                                              <asp:Panel ID="TeacherPanel" runat="server" CssClass="" ClientIDMode="Static"></asp:Panel>
				                          </div>
				        
			                            </div>
	                              </div>
                             </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dlBuildingName" />
                               <%-- <asp:AsyncPostBackTrigger ControlID="btnPrint" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="BuildingPanel" runat="server" CssClass="" ClientIDMode="Static">
                                   
                                    <asp:DropDownList ID="dlBuildingName" runat="server" style="margin-top: 14px;" CssClass="input form-control controlLength1" AutoPostBack="true"
                                        ClientIDMode="Static" OnSelectedIndexChanged="dlBuildingName_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Panel ID="buildings" runat="server" style="margin-left: 62px;" ClientIDMode="Static"></asp:Panel>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="clearfix"></div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                    <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                                </Triggers>
                                <ContentTemplate>
                <asp:Panel ID="TimeTablePanel" runat="server" Visible="false">
                    <div class="tgPanel">                       
                        <div class="col-md-12">
                            <asp:Panel ID="SubjectPanel" runat="server" CssClass="" ClientIDMode="Static"></asp:Panel>
                        </div>
                        <div class="col-md-12">
                            
                                    <asp:Panel ID="DayTimePanel" runat="server" CssClass="" Height="450"
                                        BorderStyle="Double" BorderColor="LightGray" BorderWidth="2px" ClientIDMode="Static" ScrollBars="Auto">
                                    </asp:Panel>                                
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </asp:Panel>
    </ContentTemplate>
                            </asp:UpdatePanel>
            </div>       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        var ModalProgress = '<%= ModalProgress.ClientID %>';
        function validationInput() {
            if (validateCombo('dlBatchName', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;            
        }
        function pageLoad() {
            $('#subject div.external-event').each(function () {
                // make the event draggable using jQuery UI
                $(this).draggable({
                    helper: 'clone',
                    zIndex: 999,
                    revert: true,      // will cause the event to go back to its
                    revertDuration: 0  //  original position after the drag
                });
            });
            $('#teacher div.external-event').each(function () {
                // make the event draggable using jQuery UI
                $(this).draggable({
                    helper: 'clone',
                    zIndex: 999,
                    revert: true,      // will cause the event to go back to its
                    revertDuration: 0  //  original position after the drag
                });
            });           
            $('#buildings div.external-event').each(function () {                
                // make the event draggable using jQuery UI
                $(this).draggable({
                    helper: 'clone',
                    zIndex: 999,
                    revert:function (dropped) {
                            var $draggable = $(this),
                                hasBeenDroppedBefore = $draggable.data('hasBeenDropped'),
                                wasJustDropped = dropped && dropped[0].id == "droppable";
                            if (wasJustDropped) {
                                // don't revert, it's in the droppable
                                return false;
                            } else {
                                if (hasBeenDroppedBefore) {
                                    // don't rely on the built in revert, do it yourself
                                    $draggable.animate({ top: 0, left: 0 }, 'slow');
                                    return false;
                                } else {
                                    // just let the build in work, although really, you could animate to 0,0 here as well
                                    return true;
                                }
                            }
                        },      // will cause the event to go back to its
                    revertDuration: 0  //  original position after the drag
                });
            });
            $('.dropped').droppable({
                activeClass: "ui-state-default",
                hoverClass: "ui-state-hover",                
                drop: function (event, ui) {                   
                    var batch = $('#dlBatchName option:selected').val();
                    var batchName = $('#dlBatchName option:selected').text();
                    var groupId = $('#ddlClsGroup option:selected').val();
                    var shift = $('#ddlShift option:selected').val();
                    var section = $('#ddlClsSection option:selected').val();
                    var buildingName = $('#dlBuildingName option:selected').text();
                    var td = $(this).parent().closest('td').attr('id');
                    var clsRoutine = $(this).children('ul').attr('id');
                    clsRoutine = clsRoutine.split('_');
                    var clsRoutineId = clsRoutine[1];
                    var colTd = td.split('_');
                    var dayId = colTd[1];
                    var timeId = colTd[2];
                    var stbr = ui.draggable.attr('id');
                    var stbrtext = ui.draggable.text();
                    var subid = '';//SubjectId
                    var courseid = '';//Course Id
                    var eid = '';//TeacherId
                    var br = '';//building and room
                    var flag = '';
                    stbr = stbr.split('_');
                  
                    if (stbr[0] == 's') {
                        subid = stbr[1];
                        
                        courseid = stbr[2]
                        var r = confirm("Do you want to add this Subject!");
                        if (r != true) {
                            return false;
                        }
                        flag = 's';
                        br = $('#' + td + ' div.dropped ul > li:nth-child(4) > span.label a').attr('id');
                    }
                    else if (stbr[0] == 't') {
                        eid = stbr[1];
                        var r = confirm("Do you want to add this Teacher!");
                        if (r != true) {
                            return false;
                        }
                        flag = 't';
                        br = $('#' + td + ' div.dropped ul > li:nth-child(4) > span.label a').attr('id');
                    }
                    else {
                        
                        br = stbr[1];
                       
                        var r = confirm("Do you want to add this Room!");
                        if (r != true) {
                            return false;
                        }
                        flag = 'br';
                        st = $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label a').attr('id');
                    }
                    $.ajax({
                        url: "/UI/Academic/Timetable/SetClassTimings.aspx/chkAndInsertClsRoutine",
                        data: "{clsRoutineId: '" + clsRoutineId + "',EId : '" + eid + "'," +
                              "dayId :'" + dayId + "',SubId: '" + subid + "',courseId: '" + courseid + "'," +
                              "timeid : '" + timeId + "',batch : '" + batch + "',section : '" + section + "'," +
                              "shift : '" + shift + "',rmId : '" + br + "',flag:'" + flag + "',groupId:'" + groupId + "',batchName:'" + batchName + "'}",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset= utf-8",
                        success: function OnSuccess(data) {
                            if (data.d == 'failed') {
                                showMessage(data.d, 'error');
                                return false;
                            }
                            if (data.d == 'RM-conflict')
                            {
                                showMessage('This room is already booked by another class, please try another room', 'warning');
                                return false;
                            }
                            if (data.d == 's-conflict') {
                                
                                showMessage('This subject are not able to asign at this time, because this subject has been booked by another subject, please try another subject', 'warning');

                                return false;
                            }
                            else if (data.d == 't-conflict') {
                                showMessage('This teacherare not able to asign at this time, because this teacher has been booked by another subject, please try another teacher', 'warning');

                                return false;
                            }
                            var msg = data.d.split('_');
                            if (msg[0] == "occupied-t")
                            {                                                   
                                if (flag == 't')
                                    var title = 'teacher';
                                else if (flag == 's')
                                    var title = 'subject';
                                else
                                    var title = 'room';
                                showMessage('This ' + title + ' is already occupied for ' + msg[1] + ' of ' + msg[2] + ' , please try another '+title+'', 'warning');
                                return false;
                            }
                          
                            if (msg[0] == 'RM')
                            {                               
                                $('#' + td + ' div.dropped ul > li:nth-child(3) > span.label').get(0).firstChild.nodeValue = buildingName;
                                $('#' + td + ' div.dropped ul > li:nth-child(4) > span.label').get(0).firstChild.nodeValue = stbrtext;
                                $('#' + td + ' div.dropped ul').attr('id', 'cr_' + msg[1]);
                                $('#' + td + ' div.dropped ul > li:nth-child(4) > span.label a').attr('id', br);
                                showMessage('The ' + stbrtext + ' classroom of ' + buildingName + ' is being successfully asign to class routine', 'success');
                                return false;
                            }
                            else if(msg[0] == 's') {                                
                                $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label').get(0).firstChild.nodeValue = stbrtext;                                
                                $('#' + td + ' div.dropped ul').attr('id', 'cr_' + msg[1]);
                                $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label a').attr('id', subid+'_'+courseid);
                                showMessage('This Subject ' + stbrtext + '  successfully asign to class routine', 'success');
                                return false;
                            }
                            else
                            {                              
                                $('#' + td + ' div.dropped ul > li:nth-child(2) > span.label').get(0).firstChild.nodeValue = stbrtext;
                                $('#' + td + ' div.dropped ul').attr('id', 'cr_' + msg[1]);
                                $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label a').attr('id', eid);
                                showMessage('The teacher ' + stbrtext + '  successfully asign to class routine', 'success');
                                return false;
                            }
                        },                      
                        error: OnError
                    });                    
                }            
            });            
            function OnError() {
                showMessage(data.d, 'error');
            };          
        }
        function deleteData(value) {
            alert(data);
            var r = confirm("Do you want to remove!");
            if (r != true) {
                return false;
            }
            var td = $(this).parent().closest('td').attr('id');
            var clsRoutine = $(td + ' div.dropped ul').attr('id');
            clsRoutine = clsRoutine.split('_');
            var clsRoutineId = clsRoutine[1];
            var st = '';
            var br = '';
            var cls = $(this).attr('class');
            if (cls == 'st') {
                var txt = $(td + ' div.dropped ul li:nth-child(1) >span.label').get(0).firstChild.nodeValue;
                if (txt == 'No Suject') {
                    showMessage('This has been not asigned yet, please asign first then delete it', 'warning');
                    return false;
                }
                st = $('.table-defination td div.dropped ul li:nth-child(1) >span.label a').attr('id');
                $.ajax({
                    url: "/UI/Academic/Timetable/SetClassTimings.aspx/chkDelete",
                    data: "{clsRoutine: '" + clsRoutine[1] + "',subTeacherId : '" + st + "',rmId : '" + br + "'}",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset= utf-8",
                    success: function OnSuccess(data) {
                        if (data.d == 'failed') {
                            showMessage(data.d, 'error');
                            return false;
                        }
                        if (data.d == 'ok') {
                            $('#cr_' + clsRoutine[1] + ' > li:nth-child(1) >span.label').get(0).firstChild.nodeValue = 'No Suject';
                            $('#cr_' + clsRoutine[1] + ' > li:nth-child(2) >span.label').get(0).firstChild.nodeValue = 'No Teacher';
                            $('.table-defination td div.dropped ul li:nth-child(1) >span.label a').attr('id', '0');
                            showMessage('Successfully deleted', 'success');
                            return false;
                        }
                    },
                    error: OnError
                });
            }
            if (cls == 'br') {

                var txt = $('.table-defination td div.dropped ul li:nth-child(3) >span.label').text();
                if (txt == 'No Building') {
                    showMessage('This has been not asigned yet, please asign first then delete it', 'warning');
                    return false;
                }
                br = $('.table-defination td div.dropped ul li:nth-child(4) >span.label a').attr('id');
                $.ajax({
                    url: "/UI/Academic/Timetable/SetClassTimings.aspx/chkDelete",
                    data: "{clsRoutine: '" + clsRoutine[1] + "',subTeacherId : '" + st + "',rmId : '" + br + "'}",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset= utf-8",
                    success: function OnSuccess(data) {
                        if (data.d == 'failed') {
                            showMessage(data.d, 'error');
                            return false;
                        }
                        if (data.d == 'ok') {
                            $('#cr_' + clsRoutine[1] + ' > li:nth-child(3) >span.label').get(0).firstChild.nodeValue = 'No Building';
                            $('#cr_' + clsRoutine[1] + ' > li:nth-child(4) >span.label').get(0).firstChild.nodeValue = 'No Room';
                            $('.table-defination td div.dropped ul li:nth-child(4) >span.label a').attr('id', '0');
                            showMessage('Successfully deleted', 'success');
                            return false;
                        }
                    },
                    error: OnError
                });
            }
        };        
    </script>
</asp:Content>
