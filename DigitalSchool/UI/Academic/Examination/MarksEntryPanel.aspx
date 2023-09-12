<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MarksEntryPanel.aspx.cs" Inherits="DS.UI.Academic.Examination.MarksEntryPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width: 100%;
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
                 <li>  <a runat="server" id="aAcademicHome" >Academic Module</a></li>
                <li><a runat="server" id="aExamHome">Examination Module</a></li> 
                <li class="active">Marks Entry</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row" id="divSearchPanel" runat="server">
        <div class="col-md-12">
            <div class="tgPanel">
                <div class="tgPanelHead">

                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="ddlExamId" />
                        <asp:AsyncPostBackTrigger ControlID="btnHideSearchOptions" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="row tbl-controlPanel" >
                            <div class="col-sm-8 col-sm-offset-2" runat="server" id="tblOp">
                                <div class="form-group row">
                                    <label class="col-sm-2">Shift</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="input controlLengthMin form-control" ClientIDMode="Static">                                    
                                    </asp:DropDownList>
                                        </div>                                   
                                        
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Batch</label>                                  
                                       <div class="col-sm-10"> 
                                            <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLengthMin form-control"
                                        OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                    </div>
                                                                     
                                        
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Group</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" CssClass="input controlLengthMin form-control" Enabled="false" >
                                    </asp:DropDownList>
                                        </div>                                   
                                </div> 
                                <div class="form-group row">
                                    <label class="col-sm-2">Section</label>
                                    <div class="col-sm-10">
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
                                    <label class="col-sm-2">Subject</label>
                                    <div class="col-sm-10">
                                        <asp:DropDownList ID="ddlSubject" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">         
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2">Roll No</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtRollNo" CssClass="input controlLength" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group row">                                    
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <asp:CheckBox runat="server" ID="chkForCoutAsFinalResult" />
                                        <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" 
                                        OnClick="btnSearch_Click" CssClass="btn btn-primary" OnClientClick="return validateInputs();"/>
                                    <asp:Button ID="btnRefresh" Text="Refresh" ClientIDMode="Static" runat="server" 
                                        OnClick="btnRefresh_Click" CssClass="btn btn-default"/>
                                        <asp:Button ID="btnPrintPreview" runat="server" Text="Print Full Marksheet" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnPrintPreview_Click" />
                                         <asp:Button ID="btnResultProcessing" Text="Result Processing"  ClientIDMode="Static" runat="server" 
                                         OnClick="btnResultProcessing_Click" CssClass="btn btn-success" OnClientClick="return GetConfirmation();"/>
                                    </div>
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>                           
                            <span style="font-family: 'Times New Roman'; font-size: 1.2em; padding:5px; color: #1fb5ad; font-weight: bold; float: left">
                                <p>Wait please,It's working...</p>
                            </span>
                            <img class="LoadingImg_" src="../../../../../AssetsNew/images/input-spinner.gif" />
                            <div class="clearfix"></div>
                        </ProgressTemplate>                        
                    </asp:UpdateProgress>
                                    </div>
                                    
                                </div>
                                <div class="form-group row">                    
                                    
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
        function GetConfirmation(){
             if (validateCombo('ddlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('ddlBatch',0, 'Select Batch Name') == false) return false;
            if (validateCombo('ddlSectionName',0, 'Select Section Name') == false) return false;
            if (validateCombo('ddlExamId', 0, 'Select Exam Id') == false) return false;
         
      var reply = confirm("Ary you sure, want to process?");
      if(reply)
      {
         return true;
      }
      else
      {
         return false;
      }
            return true;
      }
    

        function saveData(celldata) {
            var dataID = celldata.id;
            var splitedData = dataID.split(":");
            var dataValue = celldata.value;
            jx.load('mark-entry/update?tbldata=' + splitedData + '&val=' + dataValue + '&do=attUpdate', function (data) {
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