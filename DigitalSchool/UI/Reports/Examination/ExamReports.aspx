<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true"
     CodeBehind="ExamReports.aspx.cs" Inherits="DS.UI.Reports.Examination.ExamReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">   
    <style type="text/css">
        .controlLength{
            width:195px;            
        }
        .tgPanel
        {
            width: 100%;
            min-height: 300px;
        }
        .tbl-controlPanel tr td:first-child {
            width: 30%;
            text-align: right;
            padding-right: 8px;               
        }
        
        .littleMargin{
            margin-right: 5px;
        }
        .titleHeader{
            padding : 10px;
        }
        #btnPrintPreview{
            margin: 3px;
        }
        @media (min-width: 320px) and (max-width: 480px) {
            .btn-primary {
            margin-top:5px;
            }
        }
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
                <li><a runat="server" href="~/UI/Reports/Examination/ExaminationHome.aspx">Examination</a></li>
                <li class="active">Exam Reports</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>  
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
        <div class="row">
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <div class="col-md-12">
                    <div class="tgPanelHead">Exam Reports</div>
                </div>                
                <div class="col-md-6">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <Triggers>                               
                                <asp:AsyncPostBackTrigger ControlID="ddlSectionName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBatch" /> 
                                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />                                                            
                                <asp:AsyncPostBackTrigger ControlID="btnClear" />                                                            
                            </Triggers>
                            <ContentTemplate>
                                <div class="row tbl-controlPanel">
                                    <div class="col-sm-10 col-sm-offset-1">
                                          <div class="row tbl-controlPanel">
                                            <label class="col-sm-4">Shift</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlShift" runat="server" ClientIDMode="Static"
                                                 CssClass="input controlLength form-control">   
                                                                                           
                                            </asp:DropDownList>
                                            </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                            <label class="col-sm-4">Batch</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static"
                                                 CssClass="input controlLength form-control"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            </div>
                                          </div>
                                              <div class="row tbl-controlPanel">
                                              <label class="col-sm-4">Group</label>
                                              <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" 
                                              OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"   AutoPostBack="True" CssClass="input controlLength form-control"
                                               >
                                            </asp:DropDownList>
                                              </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                            <label class="col-sm-4">Exam Id</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static"
                                                 CssClass="input controlLength form-control">
                                            </asp:DropDownList>
                                                 <asp:CheckBox runat="server" ClientIDMode="Static" ID="cbIsFinal" Checked="false" Text="As Final Result" Visible="false" />
                                            </div>
                                          </div>
                                     
                                          <div class="row tbl-controlPanel">
                                              <label class="col-sm-4">Section</label>
                                              <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlSectionName" runat="server" ClientIDMode="Static"
                                             OnSelectedIndexChanged="ddlSectionName_SelectedIndexChanged"
                                                 AutoPostBack="True" CssClass="input controlLength form-control">
                                            </asp:DropDownList>
                             
                                          </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                              <label class="col-sm-4">Roll No</label>
                                              <div class="col-sm-8">
                                                  <asp:TextBox runat="server" ClientIDMode="Static" ID="txtRollNo" CssClass="input controlLength" ></asp:TextBox>
                                                  <asp:Button ClientIDMode="Static" runat="server" ID="btnClear" Text="Clear" ForeColor="Red"  OnClick="btnClear_Click" />
                             
                                          </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                              <label class="col-sm-4"></label>
                                              <div class="col-sm-8">
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
                                
                                         <%--<div class="row tbl-controlPanel">
                                              <label class="col-sm-4">Roll</label>
                                              <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlRoll" runat="server" ClientIDMode="Static" 
                                                CssClass="input controlLength" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                              </div>
                                        </div>--%>
                                    </div>
                                </div>
                               
                                <div style="text-align: center; font-size: -25px">
                                    <p runat="server" id="MarkSheetTitle" visible="false"></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                            </Triggers>
                            <ContentTemplate>
                                <div class="row tbl-controlPanel">
                                    <div class="col-sm-10 col-sm-offset-1">
                                          
                                          <div class="row tbl-controlPanel">
                                            <div class="col-sm-6">
                                                <asp:Button Text="Pass List" CssClass="btn btn-primary form-control" ID="btnPassList" 
                                                runat="server" OnClientClick="return validateDropDown();" OnClick="btnPassList_Click"  Visible="false" />
                                            </div>
                                            <div class="col-sm-6">
                                                 <asp:Button Text="Fail List" CssClass="btn btn-primary form-control" ID="btnFailList" 
                                                runat="server" OnClientClick="return validateDropDown();" OnClick="btnFailList_Click"  Visible="false" />
                                            </div>
                                          </div>



                                        <div class="row tbl-controlPanel">    
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnResultSummary" runat="server" CssClass="btn btn-primary form-control"
                                               OnClientClick="return validateDropDown();" OnClick="btnResultSummary_Click" Text="Result Summary" />
                                            </div>

                                            <div class="col-sm-6">
                                                <asp:Button ID="btnFailedStudent" runat="server" CssClass="btn btn-primary form-control"
                                               OnClientClick="return validateDropDown();" OnClick="btnFailedStudent_Click" Text="Failed Student Report" />
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Button Text="Student Wise" CssClass="btn btn-primary form-control" 
                                                ID="btnStudentWiseMarkList" runat="server"
                                                OnClientClick="return validateDropDown();"  Visible="false" OnClick="btnStudentWiseMarkList_Click" />
                                            </div>
                                        
                                            


                                          </div>



                                          <div class="row tbl-controlPanel" style="display:none">
                                            <div class="col-sm-6">
                                                 <asp:TextBox ID="txtTopStudent" runat="server" CssClass="input controlLength form-control" 
                                                PlaceHolder="Number of Student" Visible="false"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                 <asp:Button Text="Find Top Student" ClientIDMode="Static" CssClass="btn btn-primary form-control"
                                                 Visible="false"
                                                ID="btnFindTopStudent" runat="server"   OnClick="btnFindTopStudent_Click" />   
                                            </div>
                                          </div>
                                           <div class="row tbl-controlPanel"  style="display:none">
                                              <div class="col-sm-6">
                                            <asp:Button ID="btnprogressreport" runat="server" Text="Progress Report" 
                                              OnClientClick="return validateDropDown();"  Visible="false"  CssClass="btn btn-primary form-control" OnClick="btnprogressreport_Click" />
                                            </div>
                                            <div class="col-sm-6">
                             
                                            </div>
                                          </div>
                                         <div class="row tbl-controlPanel">
                                              <div class="col-sm-6">
                                            <asp:Button ID="btnAcademicTranscript" runat="server" Text="Academic Transcript" 
                                              OnClientClick="return validateDropDown();"  Visible="false"  CssClass="btn btn-primary form-control" OnClick="btnAcademicTranscript_Click" />
                                            </div>
                                            <div class="col-sm-6">
                             
                                            </div>
                                          </div>
                                         <div class="row tbl-controlPanel">
                                              <div class="col-sm-6">
                                            <asp:Button ID="btnAcademicTranscriptWithMarks" runat="server" Text="Report Card With Marks" 
                                              OnClientClick="return validateDropDown();"  CssClass="btn btn-primary form-control" OnClick="btnAcademicTranscriptWithMarks_Click" />
                                              </div>
                                             
                                             <div class="col-sm-6">
                             <asp:Button ID="btnSubjectWiseFailedStudent" runat="server" Text="Subject Wise Failed List" 
                                              OnClientClick="return validateDropDown();"  CssClass="btn btn-primary form-control"  OnClick="btnSubjectWiseFailedStudent_Click" />
                                            </div>
                                          </div>
                                        <div class="row tbl-controlPanel">
                                            <div class="col-sm-6">
                             <asp:Button ID="btnMeritList" runat="server" Text="Merit Position List" 
                                              OnClientClick="return validateDropDown();"  CssClass="btn btn-primary form-control" OnClick="btnMeritList_Click" />
                                            </div>

                                             <div class="col-sm-6">
                                                <asp:Button ID="btnShowResult" runat="server" CssClass="btn btn-primary form-control"
                                               OnClick="btnShowResult_Click"  Text="Result Sheet"  />
                                            </div>

                                            </div>
                                           <div class="row tbl-controlPanel">
                                              <div class="col-sm-6">
                                            <asp:Button ID="btnTabulationSheet" runat="server" Text="Tabulation Sheet" 
                                              OnClientClick="return validateDropDown();" Visible="false" CssClass="btn btn-primary form-control" OnClick="btnTabulationSheet_Click" />
                                            </div>
                                            <div class="col-sm-6">
                             
                                            </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                              <div class="col-sm-6">
                                                <asp:TextBox ID="txtStudentRoll" runat="server" CssClass="input controlLength form-control"
                                                 Width="195" Visible="false"
                                                PlaceHolder="Roll of Student"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnFindGPA" runat="server" 
                                                CssClass="btn btn-primary form-control" Visible="false"
                                                Text="Find GPA" OnClick="btnFindGPA_Click" />
                                            </div>
                                          </div>
                                          <div class="row tbl-controlPanel">
                                            <div class="col-sm-6">
                             
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Button ID="btnDetailsMarks" runat="server" CssClass="btn btn-primary form-control"
                                                 Visible="false"
                                                Text="Details Select Exam"/>
                                            </div>
                                          </div>
                                         
                                    </div>
                                </div>
                                
                                <div style="text-align: center; font-size: 20px">
                                    <p runat="server" id="P1" visible="false"></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>



                    </div>
                </div>

            </div>
            
                  
            <div class="col-md-1"></div>
        </div>       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
      
         
       
         
        function validateDropDown() {
            if (validateCombo('ddlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('ddlBatch', 0, 'Select Batch Name') == false) return false;
            if (validateCombo('ddlExamId', 0, 'Select Exam Id') == false) return false;
            //if (validateCombo('ddlSectionName', 0, 'Select Section Name') == false) return false;           
            return true;
        }
    
  


    </script>
</asp:Content>
