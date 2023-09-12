<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetExamTimings.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetExamTimings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;   
            border: none;          
        }
         .table tr th{
            background-color: #23282C;
            color: white;
        }
        /*.gvExamSchedule table {
            width:670px;
        }*/
          .gvExamSchedule td:nth-child(5),th:nth-child(5),td:nth-child(6),th:nth-child(6),td:nth-child(7),th:nth-child(7),td:nth-child(8),th:nth-child(8){
            width:200px;
            text-align:center;
        }
        .gvExamSchedule th:first-child,td:first-child{
            text-align:center;
        }
        .gvExamSchedule td:nth-child(2),th:nth-child(2),td:nth-child(3),th:nth-child(3)  {
            width:45px;
            text-align:center;
        }
          .gvExamSchedule td:nth-child(4),th:nth-child(4)  {
            width:100px;
            text-align:center;
        }
         .controlLength{
            width: 200px;
        }
        .tbl-controlPanel{
            width: 670px;
        }
        /*.tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(3){
            text-align:right;
            padding-right: 5px;
        }*/
        .DivStyle{
            padding: 10px;
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
            width: 150px;
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
          / Something you can count on /
          height: 100%;
          white-space: nowrap;          
        }
        .table-defination th.rotate > div {
          transform: 
            / Magic Numbers /
            translate(2px, 0px)
            / 45 is really 360 - 45 /
            rotate(-90deg);
          width: 65px;        
        } 
        .table > thead > tr > th, 
        .table > tbody > tr > th, 
        .table > tfoot > tr > th{
            padding : 0px;
            vertical-align : middle;
        } 

         .table tr th:first-child, tr td:first-child, tr th:nth-child(1), tr td:nth-child(1),tr th:nth-child(2), tr td:nth-child(2), tr th:nth-child(3),tr td:nth-child(3), tr th:nth-child(4),tr td:nth-child(4),tr th:nth-child(5),tr td:nth-child(5), tr th:nth-child(6),tr td:nth-child(devil), tr th:nth-child(7),tr td:nth-child(7),tr th:nth-child(8),tr td:nth-child(music) {
            text-align: center;        
        }
        .form-group {
          margin-left:5px;
        }
         .radioButtonLists label{
          padding-left:2px;
          margin-right:3px;
          margin-left:2px;
}
     
        .col-lg-10 {
            width:auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <script src="../../../../../AssetsNew/js/jsUpdateProgress.js" type="text/javascript"></script>--%>
    <asp:ModalPopupExtender ID="ModalProgress" runat="server" TargetControlID="panelUpdateProgress"
                            BackgroundCssClass="modalBackground" PopupControlID="panelUpdateProgress" />
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" EnableViewState="false">
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
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Timetable/TimetableHome.aspx">Timetable Module</a></li>                
                <li class="active">Exam Routine</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlTimingSet" />
            <asp:AsyncPostBackTrigger ControlID="dlBatchName" />
            <asp:AsyncPostBackTrigger ControlID="rblPeriodList" />
            <asp:AsyncPostBackTrigger ControlID="ddlClsGrpId" />
        </Triggers>       
        <ContentTemplate>
            <div class="">
                <div class="tgPanel">             
                    <div class="row">
                        <div class="col-lg-1"></div>
                        <div class="col-lg-10">
                     <div class="form-inline" >
                         <div class="form-group" >
                                    <label for="exampleInputName2">Timing</label>
                               <asp:DropDownList ID="ddlTimingSet" runat="server" OnSelectedIndexChanged="ddlTimingSet_SelectedIndexChanged" ClientIDMode="Static" CssClass="input controlLength" AutoPostBack="true">
                                    </asp:DropDownList>
                             </div>
                        <div class="form-group" >
                                    <label for="exampleInputEmail2">Date</label>
                            <asp:TextBox ID="txtExamDate" runat="server" CssClass="input controlLength" ClientIDMode="Static" Width="100px"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtExamDate"></asp:CalendarExtender>                                
                            </div>                       
                          <div class="form-group" >
                                    <label for="exampleInputName2">Batch</label>
                                <asp:DropDownList ID="dlBatchName" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="input controlLength" OnSelectedIndexChanged="dlBatchName_SelectedIndexChanged" Width="120px">
                                        
                                    </asp:DropDownList>

                          </div>
                           <div runat="server" id="divGroup" visible="false" class="form-group" >
                                    <label  for="exampleInputName2">Group</label>                              
                             <asp:DropDownList Enabled="false"  ID="ddlClsGrpId" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="input controlLength"  Width="120px" OnSelectedIndexChanged="ddlClsGrpId_SelectedIndexChanged">
                                        
                                    </asp:DropDownList>                                 
                               </div> 
                        <div class="form-group" >
                            <label  for="exampleInputName2">Subject</label>
                             <asp:DropDownList ID="ddlSubject" AutoPostBack="false" runat="server" ClientIDMode="Static" CssClass="input controlLength" Width="150px">                                      
                                    </asp:DropDownList>
                        </div> 
                           <div class="form-group">
                               <div >
                            <asp:ImageButton runat="server" ID="btnAdd" ToolTip="For Add List" AlternateText="Add" ImageUrl="~/Images/ADD.ico" Width="40px" OnClick="btnAdd_Click"/>
                                   </div>
                        </div>  
                           <div class="form-group">
                               <div >
                             <asp:ImageButton runat="server" ID="btnNewDate" ToolTip="For Add New Row" AlternateText="New" ImageUrl="~/Images/add_row.ico" Width="40px" OnClick="btnNewDate_Click"/>
                        </div>  
                               </div> 
                           <div class="form-group">
                               <div >
                             <asp:Button ID="btnSave" CssClass="btn btn-primary " runat="server" Text="Save" OnClientClick="return confirm('Do you want ot save ?');" OnClick="btnSave_Click" />
                        </div>              
                         </div> 
                      
                    </div>
                            </div>
                    <div class="col-lg-1"></div>
                        </div>
                     
            </div>
              </div>  
            
           <div  class="col-md-12">
              <div style="margin-left:40%"> <asp:RadioButtonList ID="rblPeriodList" CssClass="radioButtonLists" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="0" OnSelectedIndexChanged="rblPeriodList_SelectedIndexChanged"  ></asp:RadioButtonList></div>
                            <asp:GridView ID="gvExamSchedule" HeaderStyle-BackColor="#23282C" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White"  CssClass="tbl-controlPanel gvExamSchedule"   runat="server"  GridLines="Both" AutoGenerateColumns="true"  EditRowStyle-HorizontalAlign="Center" OnRowDataBound="gvExamSchedule_RowDataBound" OnRowCommand="gvExamSchedule_RowCommand" OnRowDeleting="gvExamSchedule_RowDeleting" >
                                
                                <HeaderStyle Height="45px"  />
                              
                                <Columns>
                                    <asp:TemplateField AccessibleHeaderText="Chosen" HeaderText="Chosen">                                       
                                        <ItemTemplate>                                            
                                            <asp:CheckBox ID="chkChosen" runat="server" AutoPostBack="true" OnCheckedChanged="chkChosen_Checked" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="Delete">
                                      <ItemTemplate>
                                          <%--<asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" Text="Delete" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' />--%>
                                          <asp:ImageButton runat="server" ID="btnDelete"  ImageUrl="~/Images/DeleteIcon.ico" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' />
                                      </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="Clear">                                        
                                      <ItemTemplate >
                                          <%--<asp:Button runat="server" ID="btnClear" CssClass="btn clear" Text="Clear" CommandName="Clear" CommandArgument='<%# Container.DataItemIndex %>' />--%>
                                          <asp:ImageButton runat="server" ID="btnClear"  ImageUrl="~/Images/Clear.ico" CommandName="Clear" CommandArgument='<%# Container.DataItemIndex %>'  />
                                      </ItemTemplate>
                                    </asp:TemplateField>                                                                  
                                </Columns>                                
                                    </asp:GridView>
                      </div>    
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script>
         <%-- var ModalProgress = '<%= ModalProgress.ClientID %>';--%>
         $(document).ready(function () {
             $("#ddlSubject").select2();
         });
         function load() {
             $("#ddlSubject").select2();
         }

        function validationInput() {
            if (validateCombo('dlBatchName', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('dlClassTimingSet', "0", 'Select a Class Timing Set') == false) return false;
        }
        function pageLoad() {
            $('#subTeacher div.external-event').each(function () {               
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
                    var classTimeSetNameId = $('#ddlTimingSet option:selected').val();
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
                    var st = '';
                    var br = '';
                    var flag = '';
                    var TSAId='';
                    stbr = stbr.split('_');
                  
                    if (stbr[0] == 'st') {
                        st = stbr[1];
                        
                        TSAId=stbr[2]
                        var r = confirm("Do you want to add this Subject and Teacher!");
                        if (r != true) {
                            return false;
                        }
                        flag = 'st';
                        br = $('#' + td + ' div.dropped ul > li:nth-child(4) > span.label a').attr('id');
                    } else {
                        
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
                        data: "{clsRoutineId: '" + clsRoutineId + "',subTeacherId : '" + st + "'," +
                              "dayId :'" + dayId + "',clsTimeSetNameId: '" + classTimeSetNameId + "',"+
                              "clsTimeId : '" + timeId + "',batch : '" + batch + "',section : '" + section + "',"+
                              "shift : '" + shift + "',rmId : '" + br + "',flag:'" + flag + "',TSAId:'" + TSAId + "',groupId:'" + groupId + "',batchName:'" + batchName + "'}",
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
                            if (data.d == 'st-conflict') {
                                
                                showMessage('This teacher and subject are not able to asign at this time, beacouse this teacher has been booked by another subject, please try another teacher and subject', 'warning');

                                return false;
                            }
                            var msg = data.d.split('_');
                            if (msg[0] == "occupied-t")
                            {
                               
                                var title =(st ==0)?'room':'teacher';
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
                            if (msg[0] == 'st') {
                                stbrtext = stbrtext.split(':');
                                $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label').get(0).firstChild.nodeValue = stbrtext[0];
                                $('#' + td + ' div.dropped ul > li:nth-child(2) > span.label').get(0).firstChild.nodeValue = stbrtext[1];
                                $('#' + td + ' div.dropped ul').attr('id', 'cr_' + msg[1]);
                                $('#' + td + ' div.dropped ul > li:nth-child(1) > span.label a').attr('id', st);
                                showMessage('The teacher ' + stbrtext[1] + ' and Subject ' + stbrtext[0] + ' are being successfully asign to class routine', 'success');
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
