<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MonthlyTestReport.aspx.cs" Inherits="DS.UI.Reports.Examination.MonthlyTestReport" %>
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
                <li class="active">Monthly Test Report</li>               
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
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <div class="col-md-8">
                    <div class="tgPanelHead">Monthly Test Report</div>
                </div>  
                <div class="col-md-2">
                    </div>              
                <div class="col-md-8">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <Triggers>                               
                                <asp:AsyncPostBackTrigger ControlID="ddlSectionName" />
                                <asp:AsyncPostBackTrigger ControlID="ddlBatch" /> 
                                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />                                                            
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
                                            <label class="col-sm-4">Exam Id</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static"
                                                 CssClass="input controlLength form-control">
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
                                              <label class="col-sm-4">Section</label>
                                              <div class="col-sm-8">
                                                  <asp:DropDownList ID="ddlSectionName" runat="server" ClientIDMode="Static"
                                             OnSelectedIndexChanged="ddlSectionName_SelectedIndexChanged"
                                                 AutoPostBack="True" CssClass="input controlLength form-control">
                                            </asp:DropDownList>
                             
                                          </div>
                                          </div>                                        
                                    </div>
                                </div>                               
                               
                                <div style="text-align: center; font-size: -25px">
                                    <p runat="server" id="MarkSheetTitle" visible="false"></p>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div> 
                <div class="col-md-2">
                    </div>                 
            </div>
            <div class="col-md-2"></div>
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
