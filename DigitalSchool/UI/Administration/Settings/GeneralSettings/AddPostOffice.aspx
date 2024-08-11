<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddPostOffice.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.AddPostOffice" %>
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
                <li class="active">Add Post Office</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Post Office List</h4>
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
                        <asp:AsyncPostBackTrigger ControlID="dlThana" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divThanaList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                        </div>
                        <asp:HiddenField ID="lblPostOfficeId" ClientIDMode="Static" runat="server" />
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <asp:UpdatePanel runat="server" ID="upThana" UpdateMode="Conditional">
                      <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlDistrict" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                            <div class="tgPanelHead">Add Post Office</div>
                            
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-8 col-sm-offset-2">
                                      <div class="row tbl-controlPanel" runat="server" id="divDistrict">
                                        <label class="col-sm-4">Select District</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server"  ID="dlDistrict" ClientIDMode="Static" AutoPostBack="true" CssClass="input form-control" OnSelectedIndexChanged="dlDistrict_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                      </div>
                                    <div class="row tbl-controlPanel" runat="server" id="divThana">
                                        <label class="col-sm-4">Select Thana/Upazila</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList runat="server" ID="dlThana" ClientIDMode="Static" CssClass="input form-control" AutoPostBack="true" OnSelectedIndexChanged="dlThana_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                      </div>
                                      <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">Post Office Name</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPostOffice" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                        </div>
                                      </div>
                                    <div class="row tbl-controlPanel">
                                        <label class="col-sm-4">ডাকঘর</label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="txtPostOfficeBn" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
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
            if (validateText('txtPostOffice', 1, 100, 'Enter the Post Office Name') == false) return false;
            if (validateText('txtPostOfficeBn', 1, 100, 'Enter the Post Office Name in Bengali') == false) return false;
            return true;
        }
        function editPostOffice(PostOfficeID) {
            $('#lblPostOfficeId').val(PostOfficeID);
            var strDistrict = $('#r_' + PostOfficeID + ' td:first-child').html();
            var strThana = $('#r_' + PostOfficeID + ' td:nth-child(3)').html();
            var strPostOffice = $('#r_' + PostOfficeID + ' td:nth-child(5)').html();
            var strPostOfficeBn = $('#r_' + PostOfficeID + ' td:nth-child(6)').html();
            
            var did = strDistrict;                  
            $('#dlDistrict').val(strDistrict);
            $('#MainContent_divDistrict').hide();
            $('#MainContent_divThana').hide();
           
            
            //jx.load('/ajax.aspx?id=' + did + '&todo=loadthana', function (data) {

            //    $(data).find('Thana').each(function () {
            //        var OptionValue = $(this).find('ThanaId').text();
            //        var OptionText = $(this).find('ThanaName').text();
            //        var option = $("<option>" + OptionText + "</option>");
            //        option.attr("value", OptionValue);
            //        $("#dlThana").append(option);
            //        $('#dlThana').val(strThana);
            //    });
            //});
            
            $('#txtPostOffice').val(strPostOffice);
            $('#txtPostOfficeBn').val(strPostOfficeBn);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#MainContent_divDistrict').show();
            $('#MainContent_divThana').show();
            $('#dlDistrict').val(''); 
            $('#dlThana').val('');
            $('#txtPostOffice').val('');
            $('#txtPostOfficeBn').val('');
            setFocus('txtPostOffice');
            $("#btnSave").val('Save');
        }
    </script>
</asp:Content>