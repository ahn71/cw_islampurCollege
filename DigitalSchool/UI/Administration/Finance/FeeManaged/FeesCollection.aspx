<%@ Page Title="Student Fees Collection" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FeesCollection.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.FeesCollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width:100%;
        }
        .controlLength {
            min-width:180px;
        }
        /*.tbl-controlPanel {
            width: 913px;
        }*/
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
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
        .textalign
        {
            text-align:center;
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
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Student Fees Collection</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanelHead">Fees Collection Panel</div>
                        <div class="row tbl-controlPanel"> 
	                        <div class="col-sm-9 col-sm-offset-3">
		                        <div class="form-inline">
                                     <div class="form-group">
				                         <label for="exampleInputName2">Student Type</label>
				                            <asp:DropDownList ID="ddlStudentType" runat="server" CssClass="input form-control" ClientIDMode="Static"
                                                    AutoPostBack="false">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                </asp:DropDownList>
			                         </div>
			                         <div class="form-group">
				                         <label for="exampleInputName2">Batch</label>
				                            <asp:DropDownList ID="dlBatch" AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" runat="server">
                                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                            </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
				                         <label for="exampleInputName2">Fees Category</label>
                                        <asp:DropDownList ID="dlFeesCategory" runat="server" ClientIDMode="Static"
                                            CssClass="input controlLength form-control">
                                            <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                        </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
                                        <asp:Button ID="btnSearch" Text="Search" CssClass="btn btn-primary" OnClientClick="return btnSearch_Validation();"
                                        ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
			                         </div>
			                        <div class="form-group">
                                        <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="Maroon"></asp:Label>
			                         </div>
			                        
		                        </div>
                            </div>
                        </div> 
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="upPanel2" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="dlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="dlSection" />
                        
                    </Triggers>
                    <ContentTemplate>
                        <div class="row tbl-controlPanel"> 
	                        <div class="col-sm-10 col-sm-offset-1">
		                        <div class="form-inline">
			                         <div class="form-group">
				                         <label for="exampleInputName2">Shift</label>
				                            <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                                AutoPostBack="false">
                                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>                                       
                                            </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
				                         <label for="exampleInputName2">Group</label>
                                        <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength form-control"
                                            OnSelectedIndexChanged="dlGroup_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" >
                                            <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                        </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
				                         <label for="exampleInputName2">Section</label>
                                        <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                            OnSelectedIndexChanged="dlSection_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                        </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
				                         <label for="exampleInputName2">Roll</label>
                                        <asp:DropDownList ID="dlRollNo" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="dlRollNo_SelectedIndexChanged" >
                                            <asp:ListItem Selected="True" Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
			                         </div>
			                        <div class="form-group">
				                         <label for="exampleInputName2"></label>
                                        <asp:Button runat="server" ID="btnSearchByRoll" Text="Search" ClientIDMode="Static"
                                        OnClientClick="return btnSearchByRoll_Validation();"
                                        OnClick="btnSearchByRoll_Click" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success"
                                         ClientIDMode="Static" Text="Pay Now" OnClick="btnSave_Click" />
			                         </div>
			
			                         <div class="form-group">
				                        <asp:Label ID="lblName" runat="server" ForeColor="#1fb5ad"></asp:Label>
			                         </div>
                                    <div class="form-group">
				                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFineNote" ForeColor="Maroon"></asp:Label>
			                         </div>
		                        </div>
                            </div>
                        </div> 

                                               
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="divPrint" style="margin-top: 10px;">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                            <asp:AsyncPostBackTrigger ControlID="btnSearchByRoll" />
                            <asp:AsyncPostBackTrigger ControlID="dlRollNo" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divParticularCategoryList" class="datatables_wrapper" runat="server"
                                style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearchByRoll" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divFineInfo" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                        </div>
                        <div id="divGrandTotal" class="head grandTotal">
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
        function btnSearch_Validation() {
            if (validateCombo('dlBatch', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlFeesCategory', "0", 'Select a Fee Category') == false) return false;
        }
        function btnSearchByRoll_Validation()
        {
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
            if (validateCombo('dlRollNo', "0", 'Select a Roll No') == false) return false;
            return true;
        }
        function makePayment() {
            if (btnSearchByRoll_Validation())
            {
                $('#tblFine tbody tr td:nth-child(3) input[type=checkbox]').each(function () {
                    if (this.checked) {
                        var id = this.id;
                        var famount = this.value;
                        jx.load('/ajax.aspx?id=' + id + '&todo=putDate' + '&amount= ' + famount + '', function (data) {
                        });
                    }
                    else {
                        // alert('no selected');
                    }
                });
            }            
        }                
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
                var rowCount = $('#tblParticularCategory tr').length;
                console.log('validating inputs');
                if (validateText('txtRoll', 1, 20, 'Enter a roll number') == false) return false;
                if (rowCount < 1) {
                    return false;
                }
                else {
                    goURL('/Report/FeeCollectionReport.aspx');
                }
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
        function OthersAddition(inputvalue) {
            var type = inputvalue.id;
            var v = inputvalue.value;
            jx.load('/ajax.aspx?id=' + v + '&todo=OthersParticularsAddition' + '&type= ' + type + ' ', function (data) {
                var spdata = data.split('_');
                $('#Total').val(spdata[0]);
               $('#Payble').val(spdata[1]);
                $('#Due').val(spdata[2]);               

            });
        }
        function Otherstext(inputvalue) {
            var type = inputvalue.id;
            var v = inputvalue.value;
            jx.load('/ajax.aspx?id=' + v + '&todo=OthersParticularsAddition' + '&type= ' + type + ' ', function () {                

            });
        }
    </script>
</asp:Content>
