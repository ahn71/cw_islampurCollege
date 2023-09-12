<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmitCardGenerator.aspx.cs" Inherits="DS.UI.Reports.Students.AdmitCardGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Styles/reports/CommonBorder.css" rel="stylesheet" />
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
           min-height:400px;
        }
        .controlLength {
            
        }
        .tbl-controlPanel td:nth-child(1){
            text-align: right;
            padding-right: 5px;
        }

        #MainContent_UpdatePanel4 {
        box-sizing: border-box;
        }
        .1box{
        min-height:500px;
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
                <li><a runat="server" href="~/UI/Reports/Students/StudentInfoHome.aspx">Student Information</a></li>
                <li class="active">Admid ID Card List</li>               
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
            <div class="col-md-6 1box">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                         <asp:AsyncPostBackTrigger ControlID="dlGroup" />
                    </Triggers>
        <ContentTemplate>
                <div class="tgPanel">
                    <div class="tgPanelHead">Admit Card Generate For All Students</div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Exam</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlExamType" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Shift</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlShiftAdmit" runat="server"
                                    ClientIDMode="Static" AutoPostBack="false" class="input controlLength form-control">                                    
                                </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Batch</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                        OnSelectedIndexChanged="dlBatch_SelectedIndexChanged"  AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Group</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength form-control"
                                    OnSelectedIndexChanged="dlGroup_SelectedIndexChanged"  AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                              </div>
                               <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Section</label>
                                  <div class="col-sm-8">
                                        <asp:DropDownList ID="dlSection" ClientIDMode="Static" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                  </div>
                              </div>
                              
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                <asp:Button ID="btnACGenerate" class="btn btn-primary" runat="server" Text="Process"
                                    OnClick="btnACGenerate_Click" />
                                </div>
                              </div>
                        </div>
                    </div>
                   <%-- <table class="tbl-controlPanel">
                        <tr>
                            <td>Exam</td>
                            <td>
                                <asp:DropDownList ID="dlExamType" runat="server" class="input controlLength"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftAdmit" runat="server"
                                    ClientIDMode="Static" AutoPostBack="false" class="input controlLength">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlBatch_SelectedIndexChanged"  AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlGroup_SelectedIndexChanged"  AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" ClientIDMode="Static" runat="server" class="input controlLength"></asp:DropDownList></td>
                        </tr>                        

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnACGenerate" class="btn btn-primary" runat="server" Text="Process"
                                    OnClick="btnACGenerate_Click" />
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
                    </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="dlBatchForAdmintcardByRoll" />
                         <asp:AsyncPostBackTrigger ControlID="dlGroupForAdmintcardByRoll" />
                         <asp:AsyncPostBackTrigger ControlID="dlSectionForAdmintcardByRoll" />
                     </Triggers>
        <ContentTemplate>
                <div class="tgPanel">
                    <div class="tgPanelHead">Admit Card Generate For Individual Student</div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Exam</label>
                                <div class="col-sm-8">
                                     <asp:DropDownList ID="dlExamForAdmintcardByRoll"
                                        runat="server" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Shift</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlShiftForAdmitRoll" runat="server" ClientIDMode="Static"
                                        AutoPostBack="false" CssClass="input controlLength form-control">                                    
                                    </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Batch</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlBatchForAdmintcardByRoll" AutoPostBack="true"
                                      OnSelectedIndexChanged="dlBatchForAdmintcardByRoll_SelectedIndexChanged"   runat="server" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                              </div>
                               <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Group</label>
                                  <div class="col-sm-8">
                                        <asp:DropDownList ID="dlGroupForAdmintcardByRoll" runat="server" CssClass="input controlLength form-control"
                                            OnSelectedIndexChanged="dlGroupForAdmintcardByRoll_SelectedIndexChanged"  AutoPostBack="true" ClientIDMode="Static">
                                             <asp:ListItem>All</asp:ListItem>
                                        </asp:DropDownList>
                                  </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Section</label>
                                  <div class="col-sm-8">
                                        <asp:DropDownList ID="dlSectionForAdmintcardByRoll" AutoPostBack="true"
                                       OnSelectedIndexChanged="dlSectionForAdmintcardByRoll_SelectedIndexChanged"  runat="server" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                  </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Roll</label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="dlRollForAdmitCard" AutoPostBack="false"
                                    runat="server" CssClass="input controlLength form-control">
                                </asp:DropDownList>
                             
                                    </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button ID="btnAdmitCardProcessByRoll" OnClientClick="checking();" CssClass="btn btn-primary"
                                    ClientIDMode="Static" runat="server" Text="Porcess" OnClick="btnAdmitCardProcessByRoll_Click" />
                                </div>
                              </div>
                        </div>
                    </div>
                   <%-- <table class="tbl-controlPanel">
                        <tr>
                            <td>Exam</td>
                            <td>
                                <asp:DropDownList ID="dlExamForAdmintcardByRoll"
                                    runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForAdmitRoll" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatchForAdmintcardByRoll" AutoPostBack="true"
                                  OnSelectedIndexChanged="dlBatchForAdmintcardByRoll_SelectedIndexChanged"   runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="dlGroupForAdmintcardByRoll" runat="server" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlGroupForAdmintcardByRoll_SelectedIndexChanged"  AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForAdmintcardByRoll" AutoPostBack="true"
                                   OnSelectedIndexChanged="dlSectionForAdmintcardByRoll_SelectedIndexChanged"  runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                        </tr>                        
                        <tr>
                            <td>Roll</td>
                            <td>
                                 <asp:DropDownList ID="dlRollForAdmitCard" AutoPostBack="false"
                                    runat="server" CssClass="input controlLength">
                                </asp:DropDownList></td>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnAdmitCardProcessByRoll" OnClientClick="checking();" CssClass="btn btn-primary"
                                    ClientIDMode="Static" runat="server" Text="Porcess" OnClick="btnAdmitCardProcessByRoll_Click" />
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-6">
                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="dlBatchForIdCard" />
                          <asp:AsyncPostBackTrigger ControlID="dlGroupForIdCard" />
                     </Triggers>
        <ContentTemplate>
                <div class="tgPanel">
                    <div class="tgPanelHead">Id Card Generate For All Students</div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Shift</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlShiftForIdCard" runat="server" ClientIDMode="Static"
                                        AutoPostBack="false" CssClass="input controlLength form-control">                                    
                                    </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Batch</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlBatchForIdCard" runat="server" AutoPostBack="true"
                                       OnSelectedIndexChanged="dlBatchForIdCard_SelectedIndexChanged"  CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Group</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlGroupForIdCard" runat="server" CssClass="input controlLength form-control"
                                       OnSelectedIndexChanged="dlGroupForIdCard_SelectedIndexChanged"   AutoPostBack="true" ClientIDMode="Static">
                                         <asp:ListItem>All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                              </div>
                               <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Section</label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="dlSectionForIdCard" runat="server"
                                        CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                  </div>
                              </div
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button ID="btnIdCardGenerate" CssClass="btn btn-primary" runat="server"
                                    Text="Porcess" OnClick="btnIdCardGenerate_Click" />
                                </div>
                              </div>
                        </div>
                    </div>
                    <%--<table class="tbl-controlPanel">
                         <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForIdCard" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatchForIdCard" runat="server" AutoPostBack="true"
                                   OnSelectedIndexChanged="dlBatchForIdCard_SelectedIndexChanged"  CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="dlGroupForIdCard" runat="server" CssClass="input controlLength"
                                   OnSelectedIndexChanged="dlGroupForIdCard_SelectedIndexChanged"   AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForIdCard" runat="server"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>                       
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnIdCardGenerate" CssClass="btn btn-primary" runat="server"
                                    Text="Porcess" OnClick="btnIdCardGenerate_Click" />
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="dlBatchForIdCardByROll" />
                         <asp:AsyncPostBackTrigger ControlID="dlGroupForIDCardRoll" />
                         <asp:AsyncPostBackTrigger ControlID="dlSectionForIdCardByRoll" />
                     </Triggers>
        <ContentTemplate>
                <div class="tgPanel">
                    <div class="tgPanelHead">Id Card Generate For Individual Student</div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-8 col-sm-offset-2">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Shift</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlShiftForIdCardRoll" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength form-control">                                    
                                </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Batch</label>
                                <div class="col-sm-8">
                                        <asp:DropDownList ID="dlBatchForIdCardByROll" runat="server" AutoPostBack="true"
                                   OnSelectedIndexChanged="dlBatchForIdCardByROll_SelectedIndexChanged"  CssClass="input controlLength form-control">
                                </asp:DropDownList>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Group</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlGroupForIDCardRoll" runat="server" CssClass="input controlLength form-control"
                                   OnSelectedIndexChanged="dlGroupForIDCardRoll_SelectedIndexChanged"
                                       AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                              </div>
                               <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Section</label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="dlSectionForIdCardByRoll" runat="server" AutoPostBack="true"
                                  OnSelectedIndexChanged="dlSectionForIdCardByRoll_SelectedIndexChanged"   CssClass="input controlLength form-control">
                                </asp:DropDownList>
                                  </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                  <label class="col-sm-4">Roll</label>
                                  <div class="col-sm-8">
                                    <asp:DropDownList ID="dlRollForIDCard" runat="server" AutoPostBack="false"
                                    CssClass="input controlLength form-control">
                                </asp:DropDownList>
                             
                                 </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Porcess"
                                    OnClientClick="return validateInputs();" OnClick="Button2_Click" />
                                </div>
                              </div>
                        </div>
                    </div>
                    <%--<table class="tbl-controlPanel">
                         <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShiftForIdCardRoll" runat="server" ClientIDMode="Static"
                                    AutoPostBack="false" CssClass="input controlLength">                                    
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatchForIdCardByROll" runat="server" AutoPostBack="true"
                                   OnSelectedIndexChanged="dlBatchForIdCardByROll_SelectedIndexChanged"  CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>                       
                        <tr>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="dlGroupForIDCardRoll" runat="server" CssClass="input controlLength"
                                   OnSelectedIndexChanged="dlGroupForIDCardRoll_SelectedIndexChanged"
                                       AutoPostBack="true" ClientIDMode="Static">
                                     <asp:ListItem>All</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSectionForIdCardByRoll" runat="server" AutoPostBack="true"
                                  OnSelectedIndexChanged="dlSectionForIdCardByRoll_SelectedIndexChanged"   CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>                        
                        <tr>
                            <td>Roll</td>
                            <td>
                                <asp:DropDownList ID="dlRollForIDCard" runat="server" AutoPostBack="false"
                                    CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Porcess"
                                    OnClientClick="return validateInputs();" OnClick="Button2_Click" />
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
                     </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.search', function () {
                searchTable($(this).val(), 'tblStudentInfo', '');
            });
        });
        function validateInputs() {
            if (validateText('txtIdCardRoll', 1, 50, 'Enter a roll number') == false) return false;
            return true;
        }
        function editBoards(BoardId) {
            $('#lblBoardId').val(BoardId);

            var strBoardName = $('#r_' + BoardId + ' td:first-child').html();
            $('#txtBoardName').val(strBoardName);
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
