<%@ Page Title="Exam Info" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExamInfo.aspx.cs" Inherits="DS.UI.Academics.Examination.ExamInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        .LoadingImg_ {   
    margin-left: 10px;
    padding-top: 5px;
          }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
        #tblClassList {
        margin-top:0px!important;
        margin-bottom:0px!important;
        
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblDistrictId" ClientIDMode="Static" runat="server" />
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
                <li class="active">Exam Info</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2">
                </div>
            <div class="col-md-4">
                <h4 class="text-right" style="float: left;">Exam ID List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                    <label>
                        Search:
                        <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div> 
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divExamInfoId" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Exam Information Entry</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                            <asp:AsyncPostBackTrigger ControlID="dlExamType" />
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-8 col-sm-offset-2">
                                    <div class="form-group row">
                                        <label class="col-sm-4">Batch</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static"
                                            CssClass="input form-control" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        </div>                                        
                                    </div>                                    
                                    <div class="form-group row" runat="server" id="trGroup" visible="false">
                                        <label class="col-sm-4">Group</label>
                                        <div class="col-sm-8">
                                             <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                                             CssClass="input form-control">
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4">Start Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" AutoComplete="off" CssClass="input form-control"
                                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4">End Date</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" AutoComplete="off" CssClass="input form-control"
                                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4">ExamType</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="dlExamType" runat="server" ClientIDMode="Static"
                                            CssClass="input form-control" OnSelectedIndexChanged="dlExamType_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        </div>

                                    </div>
                                      <div class="form-group row">
                                        <label class="col-sm-4">Exam Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtExamName" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            ></asp:TextBox>
                                       
                                        </div>
                                    </div>
                                    <div class="form-group row">
                                        <label class="col-sm-4">Exam Id</label>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lblExamId" runat="server" ClientIDMode="Static" Font-Size="14px"
                                            Font-Bold="True" ForeColor="#1fb5ad"></asp:Label>
                                        </div>

                                    </div>
                                    
                                    <div class="form-group row">
                                        <label class="col-sm-4"></label>
                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="return clearIt();"/>
                                        <asp:LinkButton ID="lnkSetDependencyExam" runat="server" style="color: green; font-size:11px; font-weight: bold; margin: 2px;text-decoration: underline;" PostBackUrl="~/UI/Academic/Examination/SetDependencyExam.aspx">Set Dependency Exam </asp:LinkButton>
                                        </div>

                                    </div>
                                       <div class="form-group row">
                       <label class="col-sm-4"></label>
                                        <div class="col-sm-8">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>                           
                            <span style="font-family: 'Times New Roman'; font-size: 1.2em; padding:5px; color: #1fb5ad; font-weight: bold; float: left">
                                <p>Wait exam is processing</p>
                            </span>
                            <img class="LoadingImg_" src="../../../../../AssetsNew/images/input-spinner.gif" />
                            <div class="clearfix"></div>
                        </ProgressTemplate>                        
                    </asp:UpdateProgress>
                          </div>
                      </div>
                                    
                                    </div>
                                </div>
                            
                            <%--<table class="tbl-controlPanel">
                                <tr>
                                    <td>Batch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" ClientIDMode="Static"
                                            CssClass="input form-control" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList></td>
                                </tr>
                                <tr runat="server" id="trGroup" visible="false">
                                    <td>Group
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                                             CssClass="input form-control">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Start Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="MM-dd-yyyy" runat="server"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>End Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="MM-dd-yyyy" runat="server"></asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ExamType
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dlExamType" runat="server" ClientIDMode="Static"
                                            CssClass="input form-control" OnSelectedIndexChanged="dlExamType_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Exam Id
                                    </td>
                                    <td>
                                        <asp:Label ID="lblExamId" runat="server" ClientIDMode="Static" Font-Size="14px"
                                            Font-Bold="True" ForeColor="#1fb5ad"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn btn-default" OnClientClick="return clearIt();"/>
                                        <asp:LinkButton ID="lnkSetDependencyExam" runat="server" style="color: green; font-weight: bold; margin: 2px;text-decoration: underline;" PostBackUrl="~/UI/Academic/Examination/SetDependencyExam.aspx">Set Dependency Exam </asp:LinkButton>
                                        
                                    </td>
                                </tr>
                            </table> --%>                           
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblClassList', '');
            });
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {           
            if (validateCombo('dlBatch', '0', 'Select Batch Name') == false) return false;
            if ($('#ddlGroup').prop("disabled") == false) {
                if (validateCombo('ddlGroup', '0', 'Select Group Name') == false) return false;
            }
            if (validateText('txtDate', 1, 30, 'Select the exam date') == false) return false;
            if (validateCombo('dlExamType', '0', 'Select Exam Type') == false) return false;
            return true;
        }
        function editDistrict(districtId) {
            $('#lblDistrictId').val(districtId);
            var strDistrict = $('#r_' + districtId + ' td:first-child').html();
            $('#txtDistrict').val(strDistrict);
            $("#btnSaveDistrict").val('Update');
        }
        function clearIt() {           
            $('#dlBatch').val(0);
            $('#ddlGroup').val(0); 
            $('#txtDate').val('');
            $('#dlExamType').val(0);
           // setFocus('txtDistrict');
           // $("#btnSaveDistrict").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        $(document).ready(function () {
            $('#dlBatch').change(function () {
                if ($('#dlBatch').val() != "---Select" && $('#dlExamType').val() != "---Select---") {
                    $('#lblExamId').html($('#dlExamType').val() + "_" + $('#dlBatch').val() + "_" + $('#txtDate').val());
                }
            });
            $('#dlExamType').change(function () {
                if ($('#dlBatch').val() != "---Select" && $('#dlExamType').val() != "---Select---") {
                    $('#lblExamId').html($('#dlExamType').val() + "_" + $('#dlBatch').val() + "_" + $('#txtDate').val());
                   
                }
            });
        })
    </script>
</asp:Content>
