<%@ Page Title="Grading" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Grading.aspx.cs" Inherits="DS.UI.Academics.Examination.Grading" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width:100%;
        }
        .dataTables_length, .dataTables_filter {
          display: none;
          
        }
        #tblClassList_info {
             display: none;
           
        }
        #tblClassList_paginate {
            display: none;
           
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
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
    <asp:HiddenField ID="lblGId" ClientIDMode="Static" runat="server" />
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
                <li class="active">Grading</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float: left;">Grading Details</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
             
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divGradingList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Grading</div>
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-10 col-sm-offset-1">
                            <div class="form-group row">
                                <label class="col-sm-3">Grade</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtGrade" runat="server" ClientIDMode="Static"
                                     CssClass="input form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3">Mark Min</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGradeMin" runat="server" ClientIDMode="Static"
                                     CssClass="input form-control"></asp:TextBox>
                                </div>
                                <label class="col-sm-3">Mark Max</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGradeMax" runat="server"
                                    ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3">Point Min</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGPointMin" runat="server" ClientIDMode="Static"
                                     CssClass="input form-control"></asp:TextBox>
                                </div>
                                <label class="col-sm-3">Point Max</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGPointMax"
                                        runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                </div>
                            </div>
                             <div class="form-group row">
                                <label class="col-sm-3">Comment</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtComment" runat="server" ClientIDMode="Static"
                                     CssClass="input form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-3"></label>
                                <div class="col-sm-9">
                                    <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                                    runat="server" Text="Save" OnClick="btnSave_Click" />
                                <input type="button" value="Clear" class="btn btn-default" onclick="clearIt();" />
                                </div>
                            </div>
                           
                        </div>
                    </div>
                                      
                </div>
            </div>
            <div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
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
            if (validateText('txtGrade', 1, 15, 'Enter Grade Name') == false) return false;
            if (validateText('txtGradeMin', 1, 15, 'Enter Grade Minimum Range') == false) return false;
            if (validateText('txtGradeMax', 1, 15, 'Enter Grade Maximum Range') == false) return false;
            if (validateText('txtGPointMin', 1, 15, 'Enter Grade Point Min') == false) return false;
            if (validateText('txtGPointMax', 1, 15, 'Enter Grade Point Max') == false) return false;
            return true;
        }
        function editGrading(Id) {
            $('#lblGId').val(Id);
            var strGrade = $('#r_' + Id + ' td:first-child').html();
            var strGradeMin = $('#r_' + Id + ' td:nth-child(2)').html();
            var strGradeMax = $('#r_' + Id + ' td:nth-child(3)').html();
            var strGradePointMin = $('#r_' + Id + ' td:nth-child(4)').html();
            var strGradePointMax = $('#r_' + Id + ' td:nth-child(5)').html();
            var strcomment = $('#r_' + Id + ' td:nth-child(6)').html();
            $('#txtGrade').val(strGrade);
            $('#txtGradeMin').val(strGradeMin);
            $('#txtGradeMax').val(strGradeMax);
            $('#txtGPointMin').val(strGradePointMin);
            $('#txtGPointMax').val(strGradePointMax);
            $('#txtComment').val(strcomment);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblGId').val('');
            $('#txtGrade').val('');
            $('#txtGradeMin').val('');
            $('#txtGradeMax').val('');
            $('#txtGPointMin').val('');
            $('#txtGPointMax').val('');
            $('#txtComment').val('');
            setFocus('txtGrade');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            clearIt();
        }
        function SaveSuccess() {
            loaddatatable();
            showMessage('Save successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
