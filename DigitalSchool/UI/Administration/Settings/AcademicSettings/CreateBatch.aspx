<%@ Page Title="Add Batch" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="CreateBatch.aspx.cs" Inherits="DS.UI.Administration.Settings.AcademicSettings.ManagedBatch.CreateBatch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        } 
        .controlLength{
            width: 200px;
        }        
        .tbl-controlPanel td{
            width: 50%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        input[type="checkbox"]{
            margin: 5px;
        }
           #tblBatch_length {
             display: none;
            padding: 15px;
        }
         #tblBatch_filter {
            display: none;
            padding: 15px;
        }
          #tblBatch_info {
             display: none;
            padding: 15px;
        }        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblbatchId" ClientIDMode="Static" Value="0" runat="server" />
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
                <li><a runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Settings/AcademicSettings/AcdSettingsHome.aspx">Academic Settings</a></li>                
                <li class="active">Add Batch</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="updatepanelFeesSett">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <h4 class="text-right" style="float:left">Batch List</h4>
                     <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
                 </div>                    
                </div>
                <div class="col-md-6"></div>
            </div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <div class="tgPanel">
                    <div id="divBatchList" class="datatables_wrapper" runat="server"
                        style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                    </div>
                        </div>
                </div>
                <div class="col-md-6">
                    <div class="tgPanel">
                        <div class="tgPanelHead">Add Batch</div>

                         <div class="row tbl-controlPanel">
                            <div class="col-sm-8 col-sm-offset-2">
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Class</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlClassName" ClientIDMode="Static" runat="server" CssClass="input controlLength form-control">
                                        <asp:ListItem Selected="True">...Select Class...</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">Session Year</label>
                                    <div class="col-sm-8">
                                          <asp:DropDownList ID="ddlSession" ClientIDMode="Static" runat="server" CssClass="input controlLength form-control">
                                    </asp:DropDownList>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4"></label>
                                    <div class="col-sm-8">
                                        
                                            <asp:CheckBox ID="chkIsUse" class="radio-inline" runat="server" Text="Is Use" Checked="True" RepeatLayout="Flow" CssClass="radiobuttonlist" RepeatDirection="Horizontal"/>
                                         
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <label class="col-sm-4">New Batch</label>
                                    <div class="col-sm-8">
                                        <asp:Label ID="lblbatch" ClientIDMode="Static" runat="server" Style="color: #1fb5ad" Font-Size="18px"
                                        Font-Bold="true" Width="293px"></asp:Label>
                                    </div>
                                  </div>
                                  <div class="row tbl-controlPanel">
                                    <div class="col-sm-offset-4 col-sm-8">
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                        OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                    &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                    </div>
                                  </div>
                            </div>
                        </div>

                        <%--<table class="tbl-controlPanel">
                            <tr>
                                <td>Class
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClassName" ClientIDMode="Static" runat="server" CssClass="input controlLength">
                                        <asp:ListItem Selected="True">...Select Class...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Session Year
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSession" ClientIDMode="Static" runat="server" CssClass="input controlLength">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:CheckBox ID="chkIsUse" runat="server" Text="Is Use" Checked="True" CssClass="controlLength"/>
                                </td>                                
                            </tr>
                            <tr>
                                <td>New Batch
                                </td>
                                <td>
                                    <asp:Label ID="lblbatch" ClientIDMode="Static" runat="server" Style="color: #1fb5ad" Font-Size="18px"
                                        Font-Bold="true" Width="293px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button CssClass="btn btn-primary" ID="btnSave" ClientIDMode="Static" runat="server" Text="Save"
                                        OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                    &nbsp;<input type="button" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                </td>
                            </tr>
                        </table> --%>                      
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblBatch', '');
            });
            $("#ddlClassName,#ddlSession").change(function () {
                var val1 = $("#ddlClassName :selected").text();
                var val2 = $("#ddlSession :selected").text();
                var val3 = val1+val2;
                $('#lblbatch').html(val3);
            });
            $('#tblBatch').dataTable({
                "iDisplayLength": 10000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblBatch').dataTable({
                "iDisplayLength": 10000,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateCombo('ddlClassName', "0", 'Select a Class') == false) return false;
            else if (validateCombo('ddlSession', "0", 'Select a Session') == false) return false;
            return true;
        }
        function clearIt() {
            $('#ddlClassName').val('');
            $('#ddlSession').val('');
            $('#lblbatch').val('');
            $("#btnSave").val('Save');
        }
        function success() {
            loaddatatable();
            showMessage('Save successfully', 'success');
        }
        function fail() {
            loaddatatable();
            showMessage('unable to save', 'warning');
        }
    </script>
</asp:Content>
