<%@ Page Title="Admission Fees Collection" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmFeesCollection.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.AdmFeesCollection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width:100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel {
            width: 840px;
        }
        .grandTotal {
            float: right;
            width: 100%;
            height: 28px;
            padding-top: 4px;
            padding-right: 40px;
            font-size: 18px;
            text-align: right;
            margin-top: 5px;
            color: #ff0000;
        }
        .head {
            border: none;
        }
        .textalign
        {
            text-align:center;
        }  
        .lengh{
             width: 14%;
        }      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdfstdId" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdfFeeCatId" Value="0" ClientIDMode="Static" runat="server" />
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
                <li><a id="A3" runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Admission Collection</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                        <asp:AsyncPostBackTrigger ControlID="btnPayNow" />
                        <asp:AsyncPostBackTrigger ControlID="ddlAdmissionNo" />
                        <asp:AsyncPostBackTrigger ControlID="dlFeesCategory" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanelHead">Admission Fees Collection Panel</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Admission No</td>
                                <td>
                                   <asp:DropDownList ID="ddlAdmissionNo" AutoPostBack="true" CssClass="input controlLength" ClientIDMode="Static"
                                          OnSelectedIndexChanged="ddlAdmissionNo_SelectedIndexChanged" runat="server">                                       
                                    </asp:DropDownList>
                                </td>
                                <td>Class Name</td>
                                <td>
                                    <asp:DropDownList ID="ddlClass" AutoPostBack="true" CssClass="input controlLength" ClientIDMode="Static"
                                          Enabled="false" runat="server">
                                        <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>  
                                <td>Fees Category </td>
                                <td>
                                    <asp:DropDownList ID="dlFeesCategory" runat="server" ClientIDMode="Static"
                                        CssClass="input controlLength" AutoPostBack="true" OnSelectedIndexChanged="dlFeesCategory_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>                      
                                                          
                            </tr>
                            <tr>
                                <td colspan="7" class="text-center">
                                    <asp:Label ID="lblUserNamePassword" runat="server"  
                                        BorderColor="White" Font-Size="20px" ForeColor="Maroon"></asp:Label>
                                </td>
                            </tr> 
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="tgPanel">
                <div class="tgPanelHead">Admission Particular Category List</div>               
                <div id="divPrint" style="margin-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnPayNow" /> 
                            <asp:AsyncPostBackTrigger ControlID="dlFeesCategory" /> 
                             <asp:AsyncPostBackTrigger ControlID="ddlAdmissionNo" />                          
                        </Triggers>
                        <ContentTemplate>
                            <div id="divParticularCategoryList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     <table class="tbl-controlPanel lengh">
                         <tr>
                             <td>

                             </td>
                             <td>

                             </td>
                             <td>
                                   <td>
                                    <asp:Button ID="btnPayNow" Text="PayNow" CssClass="btn btn-primary" OnClientClick="return validateInputs();"
                                        ClientIDMode="Static" runat="server" OnClick="btnPayNow_Click" />
                                    <asp:Button ID="btnClose" Text="Close" CssClass="btn btn-primary"
                                        ClientIDMode="Static" runat="server" PostBackUrl="~/Dashboard.aspx" />
                                </td>    
                             </td>
                         </tr>
                         </table>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSearchByRoll" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div id="divFineInfo" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                        </div>
                        <div id="divGrandTotal" class="head grandTotal">
                        </div>
                          <div id="divdiscount" class="head grandTotal">
                        </div>
                         <div id="divPayble" class="head grandTotal">
                        </div>
                         <div id="divPaid" class="head grandTotal">
                        </div>
                          <div id="divDue" class="head grandTotal">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script src="../../../../Scripts/jx.js"></script>
    <script type="text/javascript">             
        function CallPrint() {
            var prtContent = document.getElementById('divPrint');
            var WinPrint = window.open('', '', 'letf=0,top=0,width=1024,height=680,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }
        function validateInputs() {
            try {                            
                if (validateCombo('ddlAdmissionNo', "0", 'Select Admission No') == false) return false;
                if (validateCombo('ddlClass', "0", 'Select Class Name') == false) return false;
                if (validateCombo('dlFeesCategory', "0", 'Select Fees Category') == false) return false;
                return true;
            }
            catch (e) { };
        }

        function checkFine(checkbox) {
            var id = checkbox.id;
            var famount = checkbox.value;
            var status = checkbox.checked;
            jx.load('/ajax.aspx?id=' + id + '&todo=storeFineInfo' + '&amount= ' + famount + '&status=' + status + ' ', function (data) {
                $('#divGrandTotal').html('Grand Total : ' + data);
            });
        }
        function Commonfunction(inputvalue) {
            var type = inputvalue.id;
            var v = inputvalue.value;           
            jx.load('/ajax.aspx?id=' + v + '&todo=AdmissionCollection' + '&type= ' + type + ' ', function (data) {
                var spdata = data.split('_');
                $('#Discount').val(spdata[0]);
                $('#Payble').val(spdata[1]);
                $('#Paid').val(spdata[2]);
                $('#Due').val(spdata[3]);
              

               // alert($('#Payble').val());

            });
        }
    </script>
</asp:Content>
