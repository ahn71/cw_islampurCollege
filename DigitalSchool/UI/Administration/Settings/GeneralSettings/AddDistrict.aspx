<%@ Page Title="Add District" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddDistrict.aspx.cs" Inherits="DS.UI.Administration.Settings.GeneralSettings.AddDistrict" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
          #tblDistrictList_length {
             display: none;
            padding: 15px;
        }
         #tblDistrictList_filter {
            display: none;
            padding: 15px;
        }
          #tblDistrictList_info {
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
                <li class="active">Add District</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <h4 class="text-right" style="float:left">District List</h4>
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
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSaveDistrict" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divDistrictList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">
                        </div>
                            </div>
                         <asp:HiddenField ID="lblDistrictId" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add District</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>District Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDistrict" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>জেলা 
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDistrictBn" runat="server" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSaveDistrict" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSaveDistrict_Click" />
                                        <input id="tnReset" type="reset" onclick="clearIt();" value="Reset" class="btn btn-default" />
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <%--<div>
            <select  class="dpe_txt" id="menu_one" name="menu_one" runat="server">
    	<option selected="" value="">Select District</option> <option value="210">BAGERHAT</option> <option value="415">BANDARBAN</option> <option value="504">BARGUNA</option> <option value="501">BARISAL</option> <option value="506">BHOLA</option> <option value="110">BOGRA</option> <option value="405">BRAHMONBARIA</option> <option value="407">CHANDPUR</option> <option value="411">CHITTAGONG</option> <option value="203">CHUADANGA</option> <option value="406">COMILLA</option> <option value="412">COX`S BAZAR</option> <option value="310">DHAKA</option> <option value="703">DINAJPUR</option> <option value="314">FARIDPUR</option> <option value="410">FENI</option> <option value="708">GAIBANDHA</option> <option value="307">GAZIPUR</option> <option value="317">GOPALGONJ</option> <option value="603">HOBIGONJ</option> <option value="109">JAIPURHAT</option> <option value="301">JAMALPUR</option> <option value="206">JESSORE</option> <option value="503">JHALOKATHI</option> <option value="204">JHENAIDAH</option> <option value="413">KHAGRACHHARI</option> <option value="209">KHULNA</option> <option value="305">KISHORGONJ</option> <option value="707">KURIGRAM</option> <option value="201">KUSHTIA</option> <option value="706">LALMONIRHAT</option> <option value="408">LUXMIPUR</option> <option value="315">MADARIPUR</option> <option value="205">MAGURA</option> <option value="309">MANIKGONJ</option> <option value="202">MEHERPUR</option> <option value="604">MOULVIBAZAR</option> <option value="312">MUNSHIGONJ</option> <option value="303">MYMENSINGH</option> <option value="111">NAOGAON</option> <option value="207">NARAIL</option> <option value="311">NARAYANGONJ</option> <option value="308">NARSINGDI</option> <option value="114">NATORE</option> <option value="112">NAWABGONJ</option> <option value="304">NETROKONA</option> <option value="704">NILPHAMARI</option> <option value="409">NOAKHALI</option> <option value="116">PABNA</option> <option value="701">PANCHAGARH</option> <option value="505">PATUAKHALI</option> <option value="502">PIROJPUR</option> <option value="313">RAJBARI</option> <option value="113">RAJSHAHI</option> <option value="414">RANGAMATI</option> <option value="705">RANGPUR</option> <option value="208">SATKHIRA</option> <option value="316">SHARIATPUR</option> <option value="302">SHERPUR</option> <option value="115">SIRAJGONJ</option> <option value="601">SUNAMGONJ</option> <option value="602">SYLHET</option> <option value="306">TANGAIL</option> <option value="702">THAKURGAON</option>
    </select>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'tblDistrictList', '');
            });
            $('#tblDistrictList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblDesignationList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtDistrict', 1, 30, 'Enter the District Name') == false) return false;
            if (validateText('txtDistrictBn', 1, 30, 'Enter the District Name in Bengali') == false) return false;
            return true;
        }
        function editDistrict(districtId) {

            $('#lblDistrictId').val(districtId);

            var strDistrict = $('#r_' + districtId + ' td:first-child').html();
            var strDistrictBn = $('#r_' + districtId + ' td:nth-child(2)').html();
            $('#txtDistrict').val(strDistrict);
            $('#txtDistrictBn').val(strDistrictBn);
            $("#btnSaveDistrict").val('Update');
        }
        function clearIt() {
            $('#lblDistrictId').val('');
            $('#txtDistrict').val('');
            $('#txtDistrictBn').val('');
            setFocus('txtDistrict');
            $("#btnSaveDistrict").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
        function acceptValidCharacter(e, targetInput) {
            try {
                if (e.keyCode != 65) {
                    if (e.keyCode != 80) {
                        if (e.keyCode != 8) {
                            alert(e.keyCode);
                            $('#' + targetInput.id).val('');
                        }
                    }
                }
            }
            catch (e) {
            }
        }
    </script>
</asp:Content>
