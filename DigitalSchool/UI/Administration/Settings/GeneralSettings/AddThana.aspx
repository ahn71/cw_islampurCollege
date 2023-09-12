<%@ Page Title="Add Thana/Upazila" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddThana.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.AddThana" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         #tblThana_length {
             display: none;
            padding: 15px;
        }
         #tblThana_filter {
            display: none;
            padding: 15px;
        }
          #tblThana_info {
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
                <li><a runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">Add Thana/Upazila</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Thana/Upazila List</h4>
                <div class="dataTables_filter_New" style="float: right;margin-right:0px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New" style="width:193px;"  placeholder="type here" />
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
                        <asp:AsyncPostBackTrigger ControlID="dlDistrict" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divThanaList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                        </div>
                        <asp:HiddenField ID="lblThanaId" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel runat="server" ID="upThana" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Thana/Upazila</div>
                            
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-8 col-sm-offset-2">
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Select District</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="dlDistrict" ClientIDMode="Static" CssClass="input form-control" AutoPostBack="true" OnSelectedIndexChanged="dlDistrict_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Thana/Upazila</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtThana" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">থানা/উপজেলা</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtThanaBn" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4"></label>
                                        <div class="col-sm-8">
                                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <input type="button" class="btn btn-default" value="Clear" onclick="clearIt();" />
                                        </div>
                                      </div>
                                     
                                </div>
                            </div>                                                     
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        <%--<select class="dpe_txt" id="menu_one_list" name="menu_one_list" runat="server">
      
    <option value="">Select One</option><option value="20105">BHERAMARA</option><option value="20104">DAULATPUR</option><option value="20103">KHOKSHA</option><option value="20101">KUMARKHALI</option><option value="20102">KUSHTIA SADAR</option><option value="20106">MIRPUR</option></select>--%>
   
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblThana', '');
            });
            $('#tblThana').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblThana').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtThana', 1, 30, 'Enter the Thana/Upazila Name') == false) return false;
            if (validateText('txtThanaBn', 1, 30, 'Enter the Thana/Upazila Name in Bengali') == false) return false;
            return true;
        }
        function editThana(thanaId) {
            $('#lblThanaId').val(thanaId);
            var strDistrictID = $('#r_' + thanaId + ' td:first-child').html();
            var strThana = $('#r_' + thanaId + ' td:nth-child(2)').html();
            var strThanaBn = $('#r_' + thanaId + ' td:nth-child(3)').html();
            var strDistrict = $('#r_' + thanaId + ' td:nth-child(4)').html();            
                    
            $('#txtThana').val(strThana);
            $('#txtThanaBn').val(strThanaBn);
            $('#dlDistrict').val(strDistrictID);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblThanaId').val('');
            $('#txtThana').val('');
            $('#txtThanaBn').val('');
            setFocus('txtThana');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>
