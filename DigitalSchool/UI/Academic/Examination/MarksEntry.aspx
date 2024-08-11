<%@ Page Title="Marks Entry" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="MarksEntry.aspx.cs" Inherits="DS.UI.Academics.Examination.MarksEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width: 100%;
        }
        #btnPrintPreview{
            margin: 3px;
        }
        .controlLength{
            /*width: 302px;*/
        }
        .controlLengthMin{
            /*width: 150px;*/
        }
        .LoadingImg{
            width: 80%;
            margin-left: 10px;
            padding-top: 5px;
        }
        .checkBox label{
            margin-left:4px;
        }
        @media only screen and (min-width: 320px) and (max-width: 479px) {

            #ddlSectionName {
            margin-top:10px;
            }

            #ddlBatch{
            margin-top:10px;
            }

            #MainContent_btnDetailsMarks{
            margin-top:10px;
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
                <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>                
                <%--<li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>--%>                
                <%--<li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>--%>
                <li><a runat="server" id="aAcademicHome" >Academic Module</a></li>
                <li><a runat="server" id="aExamHome">Examination Module</a></li> 
                <li class="active">Marks Entry</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row" id="divSearchPanel" runat="server">
        <div class="col-md-6">
            <div class="tgPanel">
                <div class="tgPanelHead">

                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                         <asp:AsyncPostBackTrigger ControlID="btnPreviewMarksheet" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="btnHideSearchOptions" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="row tbl-controlPanel" >
                            <div class="col-sm-8 col-sm-offset-2" runat="server" id="tblOp">
                                <div class="form-group row">
                                    <label class="col-sm-2">Shift</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static">                                    
                                    </asp:DropDownList>
                                        </div>
                                    
                                        <div class="col-sm-5"> 
                                            <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLengthMin form-control"
                                        OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Group</label>
                                    <div class="col-sm-5">
                                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" CssClass="input controlLengthMin form-control" Enabled="false" >
                                    </asp:DropDownList>
                                        </div>
                                    <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlSectionName" runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Exam Id</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                         AutoPostBack="True" OnSelectedIndexChanged="ddlExamId_SelectedIndexChanged">
                                        <asp:ListItem Value="0">...Select Exam Id...</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" 
                                        OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return validateInputs();"/>
                                    <asp:Button ID="btnRefresh" Text="Refresh" ClientIDMode="Static" runat="server" 
                                        OnClick="btnRefresh_Click" CssClass="btn btn-default"/>
                                       
                                    </div>
                                    <div class="col-sm-8 col-sm-offset-2">
                                      
                                    </div>
                                    
                                </div>
                                <div class="form-group row">
                                    
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <asp:CheckBox ID="chkForCoutAsFinalResult" runat="server" CssClass="checkBox" Text="Process Result As Final" AutoPostBack="true" OnCheckedChanged="chkForCoutAsFinalResult_CheckedChanged" Visible="false" Checked="false" />
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-3"></label>
                                    <div class="col-sm-9">
                                        <p runat="server" id="MarkSheetTitle" visible="false"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                                                                      
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-6">
            <div class="tgPanel" style="min-height: 200px;">
                <div class="tgPanelHead"></div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlExamId" /> 
                        <asp:AsyncPostBackTrigger ControlID="btnTotalResultProcess" />  
                        <asp:AsyncPostBackTrigger ControlID="chkForCoutAsFinalResult" /> 
                        <asp:AsyncPostBackTrigger ControlID="ddlsubjectName" /> 
                         <asp:AsyncPostBackTrigger ControlID="ddlBatch" /> 
                        <asp:AsyncPostBackTrigger ControlID="btnSearch"     />             
                    </Triggers>
                    <ContentTemplate>
                        <div class="row tbl-controlPanel">
	                        <div class="col-sm-10 col-sm-offset-1">
		                        <div class="form-group row">
			                        <label class="col-sm-3">Subject</label>
			                        <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlsubjectName" runat="server" CssClass="input controlLength form-control"
                                       AutoPostBack="true"   OnSelectedIndexChanged="ddlsubjectName_SelectedIndexChanged" ClientIDMode="Static">
                                    </asp:DropDownList>
			                        </div>
		                        </div>
                                <div class="form-group row">
			                        <label class="col-sm-3">Course</label>
			                        <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlCourse" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">
                                    </asp:DropDownList>
			                        </div>
		                        </div>
                                <div class="form-group row">
			                        <label class="col-sm-3"></label>
			                        <div class="col-sm-9">
                                         <asp:Button Text="Preview Marks list" Width="149px" CssClass="btn btn-primary" ID="btnPreviewMarksheet" runat="server"
                                       OnClientClick="return validSubject();"  OnClick="btnPreviewMarksheet_Click" />
                                    <asp:Button Text="Details Select Exam" Width="149px"  CssClass="btn btn-primary" ID="btnDetailsMarks" runat="server" 
                                        OnClick="btnDetailsMarks_Click" />
			                        </div>
		                        </div>
                                <div class="form-group row">
			                        <label class="col-sm-3"></label>
			                        <div class="col-sm-9">
                                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Full Marksheet" CssClass="btn btn-success" ClientIDMode="Static"
                                      style="float:left !important;width:149px;margin:0px;" OnClick="btnPrintPreview_Click" Visible="false" />

                                    <asp:Button Text="Total Result Process" CssClass="btn btn-primary" ID="btnTotalResultProcess" runat="server"
                                        OnClick="btnTotalResultProcess_Click" Visible="False" style="width:149px;margin-left:4px;"/>
			                        </div>
		                        </div>
                                <div class="form-group row">
			                        <label class="col-sm-3"></label>
			                        <div class="col-sm-9">
                                         <asp:Button Text="Delete" CssClass="btn btn-primary" ID="btnDeleteCurrentResult" runat="server" Visible="false" style="width:135px" OnClientClick="return confirm ('Do you want to delete this final result ?')" OnClick="btnDeleteCurrentResult_Click" />
                                    <span style="position: absolute">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <img class="LoadingImg" src="../../../AssetsNew/images/input-spinner.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </span>
			                        </div>
		                        </div>
                               
	                        </div>
                        </div>
                       <%-- <table class="tbl-controlPanel">
                            <tr>
                                <td>Subject </td>
                                <td>
                                    <asp:DropDownList ID="ddlsubjectName" runat="server" CssClass="input controlLength"
                                       AutoPostBack="true"   OnSelectedIndexChanged="ddlsubjectName_SelectedIndexChanged" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Course </td>
                                <td>
                                    <asp:DropDownList ID="ddlCourse" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button Text="Preview Marks list" Width="149px" CssClass="btn btn-primary" ID="btnPreviewMarksheet" runat="server"
                                       OnClientClick="return validSubject();"  OnClick="btnPreviewMarksheet_Click" />
                                    <asp:Button Text="Details Select Exam" Width="149px"  CssClass="btn btn-primary" ID="btnDetailsMarks" runat="server" 
                                        OnClick="btnDetailsMarks_Click" />
                                   
                                     
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnPrintPreview" runat="server" Text="Print Full Marksheet" CssClass="btn btn-success" ClientIDMode="Static"
                                      style="float:left !important;width:149px;margin:0px;" OnClick="btnPrintPreview_Click" Visible="false" />

                                    <asp:Button Text="Total Result Process" CssClass="btn btn-primary" ID="btnTotalResultProcess" runat="server"
                                        OnClick="btnTotalResultProcess_Click" Visible="False" style="width:149px;margin-left:4px;"/>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    
                                   <asp:Button Text="Delete" CssClass="btn btn-primary" ID="btnDeleteCurrentResult" runat="server" Visible="false" style="width:135px" OnClientClick="return confirm ('Do you want to delete this final result ?')" OnClick="btnDeleteCurrentResult_Click" />
                                    <span style="position: absolute">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="UpdatePanel1">
                                            <ProgressTemplate>
                                                <img class="LoadingImg" src="../../../AssetsNew/images/input-spinner.gif" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </span>
                                </td>
                            </tr>
                        </table> --%>                       
                        <div style="text-align: center; font-size: 20px">
                            <p runat="server" id="P1" visible="false"></p>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
                     
            <div class="tgPanelHead" >             
            </div>
       
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPrintPreview" />
                </Triggers>
                <ContentTemplate>
                    
                    
                <asp:Button ID="btnHideSearchOptions" runat="server" Text="Hide Searcha Panel" style="width:142px;margin-top:3px" CssClass="btn btn-success pull-right" ClientIDMode="Static"
                        Width="120px" OnClick="btnHideSearchOptions_Click" Visible="false"  />
            <asp:Panel ID="Panel1" runat="server" Visible="false">
                <%--<asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnTotalResultProcess" />
                    </Triggers>
                    <ContentTemplate>--%>
                        <div runat="server" id="divDisplayFinalResult" style="width: 100%; padding: 5px; height: 30px; overflow: auto;">
                            <asp:Label ID="lblClassTitle" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                            <asp:Label ID="lblShiftTitle" runat="server" Font-Bold="True" Font-Size="15px"></asp:Label><br />
                            <asp:GridView ID="gvDisplayTotalFinalResult" ClientIDMode="Static" CssClass="display" runat="server"></asp:GridView>
                        </div>
                   <%-- </ContentTemplate>
                </asp:UpdatePanel>--%>
            </asp:Panel>  
                    </ContentTemplate>
            </asp:UpdatePanel>          
            <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <br />
                    <div id="divMarksheet" runat="server" 
                        style="width: 100%; height:auto; overflow-x: auto; overflow-y: auto; float: left;" visible="false"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">    
    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#tblMarkEntryPoint').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblMarkEntryPoint').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateCombo('ddlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('ddlBatch',0, 'Select Batch Name') == false) return false;
            if (validateCombo('ddlSectionName',0, 'Select Section Name') == false) return false;
            if (validateCombo('ddlExamId', 0, 'Select Exam Id') == false) return false;
           
            return true;
        }
        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
                if (data == "Not Save")
                    alert("Please type valid marks");
                this.focus();
            });
        }
        function validSubject() {
            if (validateCombo('ddlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('ddlBatch',0, 'Select Batch Name') == false) return false;
            if (validateCombo('ddlSectionName',0, 'Select Section Name') == false) return false;
            if (validateCombo('ddlExamId',0, 'Select Exam Id') == false) return false;
            if (validateCombo('ddlsubjectName', 0, 'Select Subject') == false) return false;
            return true;
        }
      
    </script>
</asp:Content>
