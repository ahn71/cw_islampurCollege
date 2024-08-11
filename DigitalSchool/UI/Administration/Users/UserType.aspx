<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="UserType.aspx.cs" Inherits="DS.UI.Administration.Users.UserType" %>
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
        .controlLength{
            width: 200px;
        }
          #tblClassList_length {
             display: none;
            padding: 15px;
        }
         #tblClassList_filter {
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblUserTypeId" ClientIDMode="Static" runat="server"/>    
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Users/UsersHome.aspx">Control Panel</a></li>             
                <li class="active">User Type Name</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>    
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">User Type List</h4>
                 <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"   placeholder="type here" />
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
                        <div id="divList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="tgPanelHead">Add New User Type</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>User Type
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click"  />
                                        &nbsp;<input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                    </td>
                                </tr>                                
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <Triggers>                                
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />   
                          <%--      <asp:AsyncPostBackTrigger ControlID="btnSetPage" />  --%>                           
                            </Triggers>
                            <ContentTemplate>
                            <asp:ModalPopupExtender ID="showDependencyModal" runat="server" BehaviorID="modalpopup2" CancelControlID="btnClose"
                                OkControlID="Button1"
                                TargetControlID="Button2" PopupControlID="showParticular"  >
                            </asp:ModalPopupExtender>
                            <div id="showParticular" runat="server" style="display: none;width:55%; height:500px; background-color:white; top:60px"  >
                                <div style="background-color:#23282C;color:white"><h3>Chosen Page For Set In User Type</h3></div>  
                                                
                                <%--<div style=" width:100% ; overflow:scroll; height:100%">--%><asp:Panel runat="server" ScrollBars="Vertical" Width="100%" Height="100%">
                                  <asp:GridView ID="gvPageInfoList"  runat="server" Width="100%" DataKeyNames="PageNameId" CssClass="table table-bordered" AutoGenerateColumns="False" Height="250px">

                                                <RowStyle HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hideSubId" runat="server"
                                                                Value='<%# DataBinder.Eval(Container.DataItem, "PageNameId")%>' />
                                                            <%# Container.DataItemIndex+1%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField> 
                                                    <asp:TemplateField HeaderText="Chosen">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkChosen" runat="server" OnCheckedChanged="chkChosen_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Page Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubName" style="float:left"  runat="server"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "PageTitle")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Module">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtPassMarks" style="width:50px;text-align:start;"  runat="server"
                                                                Text='<%# DataBinder.Eval(Container.DataItem, "ModuleType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>   

                                                </Columns>
                                            </asp:GridView>
                                  <%--  </div>--%>
                                </asp:Panel>
                                <div style="margin-top:10px">
                                    <table>
                                        <tr>
                                            <td>
                                                <input id="Button1" runat="server" type="button" value="Set" style="width: 91px; font-weight: bold; color: black; display:none"  />
                                                <input id="Button2" runat="server" type="button" value="Ok" style="width: 91px; font-weight: bold; color: black; display:none" />
                                                <input id="btnClose" runat="server" type="button" value="Close" style="width: 91px; font-weight: bold; color: black" onclick="btnClose_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                                </ContentTemplate>
                         </asp:UpdatePanel>
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
            if (validateText('TxtName', 1, 50, 'Enter a Set Name') == false) return false;
            return true;
        }
        function editRow(Id) {
            $('#lblUserTypeId').val(Id);
            $('#TxtName').val($('#ClsTimeSetName' + Id).html());
            $("#btnSave").val('Update');
        }
        function NP(Id) {
           
            var id = Id;
            window.location.href = "ChangePageInfo.aspx?id="+id;
        }

        function clearIt() {
            $('input[type=text]').val('');
            $('#lblUserTypeId').val('');
            $("#btnSave").val('Save');
            setFocus('TxtName');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function SavedSuccess() {
            loaddatatable();
            showMessage('Saved successfully', 'success');
            clearIt();
        }
        function NegivatePage(Id) {
            alert(id);
        }
    </script>
</asp:Content>
