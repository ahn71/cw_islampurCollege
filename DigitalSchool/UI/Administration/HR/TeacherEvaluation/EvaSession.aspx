<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EvaSession.aspx.cs" Inherits="DS.UI.Administration.HR.TeacherEvaluation.EvaSession" %>
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
        input[type="checkbox"]{
            margin: 7px;
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
         #ckblCommittee tr td {
             text-align:left;
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
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a id="A2" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a id="A4" runat="server" href="~/UI/Administration/HR/TeacherEvaluation/EvaHome.aspx">Teacher Evaluation</a></li> 
                <li class="active">Create Session</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Session List</h4>
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
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
<%--                        <div class="tgPanel">
                        <div id="divCategoryList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                            </div>--%>
                         <div class="tgPanel">
                            <%--<div id="divCategoryList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>--%>
                             <asp:GridView ID="gvSessionList" DataKeyNames="SessionID" CellPadding="3"
                                 CssClass="table table-striped table-bordered table-condensed" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" runat="server" AutoGenerateColumns="false"  OnRowCommand="gvSessionList_RowCommand">
                                 <Columns>
                                     <asp:TemplateField HeaderText="SL">
                                         <ItemTemplate>
                                             <%# Container.DataItemIndex + 1 %>
                                         </ItemTemplate>
                                         <HeaderStyle HorizontalAlign="Center" />
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="Session" HeaderText="Session" />
                                     <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                     <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                                     <asp:BoundField DataField="NumPattern" HeaderText="Number Pattern" />
                                   <%--    <asp:TemplateField HeaderText="Committee Member">
                                         <ItemTemplate>
                                           <asp:DropDownList runat="server" ID="ddlMemberList" CssClass="input form-control"></asp:DropDownList>
                                         </ItemTemplate>

                                     </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="Edit">
                                         <ItemTemplate>
                                             <asp:Button runat="server" CommandName="Alter" CommandArgument="<%# Container.DataItemIndex%> " ID="btnAlter" CssClass="btn btnalt" Text="Edit" />
                                         </ItemTemplate>

                                     </asp:TemplateField>
                                 </Columns>
                             </asp:GridView>
                        </div>
                        <asp:HiddenField ID="lblCategoryId" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="tgPanelHead">Create Session</div>
                        <table class="tbl-controlPanel"> 
                                <tr>
                                <td>
                                    Session Name 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSessionName" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            ></asp:TextBox>
                                        
                                </td>
                            </tr>                            
                            <tr>
                                <td>
                                    Start 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtStartDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </td>
                            </tr> 
                                <tr>
                                <td>
                                    End 
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" CssClass="input form-control"
                                            ></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEndDate" Format="dd-MM-yyyy" runat="server"></asp:CalendarExtender>
                                </td>
                            </tr>   
                                <tr>
                                <td>
                                    Number Pattern 
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlNumberPattern" ClientIDMode="Static" CssClass="input form-control"></asp:DropDownList>
                                </td>
                            </tr>  
                              <tr>
                                <td>
                                    Evaluator Members
                                </td>
                                <td>
                                    <div  style="overflow-y: scroll;height:150px">
                                        <asp:CheckBoxList runat="server" ID="ckblCommittee" ClientIDMode="Static" CssClass="ckbl" ></asp:CheckBoxList>
                                    </div>
                                    
                                </td>
                            </tr>                        
                                                     
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                        OnClientClick="return validateInputs();" OnClick="btnSave_Click" 
                                         />
                                    <input id="tnReset" type="reset" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                </td>
                            </tr>                            
                        </table>                        
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
            if (validateText('txtSessionName', 1, 50, 'Enter Session Name') == false) return false;
            if (validateText('txtStartDate', 1, 50, 'Enter Start Date') == false) return false;
            if (validateText('txtEndDate', 1, 50, 'Enter End Date') == false) return false;
           
            return true;
        }
        function editCategory(Did) {
            $('#lblCategoryId').val(Did);
            var strCategory = $('#r_' + Did + ' td:first-child').html();
            $('#txtCategory').val(strCategory);
            var strO = $('#r_' + Did + ' td:nth-child(2)').html();
            $('#txtOrdering').val(strO);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblCategoryId').val('');
            $('#txtCategory').val('');
            $('#txtOrdering').val('');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>