<%@ Page Title="Question Pattern" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="QuestionPattern.aspx.cs" Inherits="DS.UI.Academic.Examination.QuestionPattern" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }
        .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
        #tblClassList_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
         #btnSave {
         margin-left:115px;
         }
          @media only screen and (min-width: 320px) and (max-width: 479px) {

            #btnSave {
                margin-left:initial;
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
    <asp:HiddenField ID="lblQPId" ClientIDMode="Static" runat="server" />
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
                <li class="active">Question Pattern</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">Question Pattern</h4>
            </div>
            <div class="col-md-6"></div>
            <div class="col-md-12">
                <div class="row">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="tgPanel">
                                <div id="divQuestionPattern" class="datatables_wrapper" runat="server" 
                                    style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                                    </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-6">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Question Pattern</div>
                            <%--<table class="tbl-controlPanel">                                
                                <tr>
                                    <td>Question Pattern</td>
                                    <td>
                                        <asp:TextBox ID="txtQPName" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr> 
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearText();" />
                                    </td>    
                               </tr>                           
                            </table>--%>
                            <div class="row tbl-controlPanel"> 
		                        <div class="col-sm-10 col-sm-offset-1">
			                        <div class="form-inline">
				                         <div class="form-group">
					                         <label for="exampleInputName2">Question Pattern</label>
					                         <asp:TextBox ID="txtQPName" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                             <asp:CheckBox ID="chkIsActive" ClientIDMode="Static" runat="server" Text="Is Active" />
				                         </div>
			                        </div>
	                          </div>
                         </div>
                             
                            <div class="row tbl-controlPanel"> 
		                        <div class="col-sm-10 col-sm-offset-1">
			                        <div class="form-inline">
				                         <div class="form-group">
					                         <label for="exampleInputName2" disabled="true"></label>
					                         <asp:Button ID="btnSave" ClientIDMode="Static" CssClass="btn btn-primary" runat="server" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearText();" />
				                         </div>
			                        </div>
	                          </div>
                         </div>

                        </div>
                    </div>                    
                </div>
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
            if (validateText('txtQPName', 1, 30, 'Enter Question Pattern') == false) return false;
            return true;
        }
        function editQuestionPattern(QPId) {
            $('#lblQPId').val(QPId);
            var strQP = $('#r_' + QPId + ' td:first-child').html();
            $('#txtQPName').val(strQP);
             strQP = $('#r_' + QPId + ' td:nth-child(2)').html();
             if (strQP == "Yes") {              
                $('#chkIsActive').removeProp('checked');
                $('#chkIsActive').click();

            }
            else $('#chkIsActive').removeProp('checked');
            $("#btnSave").val('Update');
        }
        function clearText() {
            $('#lblQPId').val('');
            $('#txtQPName').val('');
            setFocus('txtQPName');
            $('#btnSave').val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Updated successfully', 'success');
            clearText();
        }
        function SaveSuccess() {
            loaddatatable();
            showMessage('Save successfully', 'success');
            clearText();
        }
    </script>
</asp:Content>
