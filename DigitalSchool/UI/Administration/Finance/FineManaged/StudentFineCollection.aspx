<%@ Page Title="Student Fine Collection" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentFineCollection.aspx.cs" Inherits="DS.UI.Administration.Finance.FineManaged.StudentFineCollection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
        }
        .controlLength{
            width:150px;
            margin: 5px;
        }
        /*.tbl-controlPanel{
            width: 600px;            
        }*/
        .grandTotal{
            float:right; 
            width: 100%; 
            height: 28px; 
            padding-top: 4px; 
            padding-right: 40px; 
            font-size: 18px; 
            text-align: right;
            margin-top: 5px;
            color: #ff0000;
        }
        .head{
            border:none;
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
                <li class="active">Student Fine Collection</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="tgPanel">
                <div class="tgPanelHead">Fine Collection Panel</div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="dlGroup" />
                        <asp:AsyncPostBackTrigger ControlID="dlSection" />
                         <asp:AsyncPostBackTrigger ControlID="btnPayNow" />
                    </Triggers>
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-8 col-sm-offset-2">
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-4">
                                    <label class="col-sm-4">Shift</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" class="form-control"
                                             AutoPostBack="false"> 
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                   
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                <label class="col-sm-4">Batch</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlBatch" AutoPostBack="true" runat="server" class="form-control"
                                        ClientIDMode="Static" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" >
                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                    </div>
                                <div class="col-sm-4">
                                <label class="col-sm-4">Group</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlGroup" runat="server" CssClass="form-control" 
                                       AutoPostBack="true"  ClientIDMode="Static" OnSelectedIndexChanged="dlGroup_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                    </div>
                            </div>
                            <div class="row tbl-controlPanel">
                                
                                <div class="col-sm-4">
                                <label class="col-sm-4">Section</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlSection" runat="server" class="form-control"
                                     ClientIDMode="Static"  AutoPostBack="true"  OnSelectedIndexChanged="dlSection_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                </asp:DropDownList>
                                </div>
                                    </div>
                                <div class="col-sm-4">
                                <label class="col-sm-4">roll</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="dlRoll" runat="server" CssClass="form-control" 
                               AutoPostBack="false"  ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                           </asp:DropDownList>
                                </div>
                                    </div>
                                <div class="col-sm-4">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success" 
                                   OnClientClick="return validateInputs();"  OnClick="btnSearch_Click" />
                                </div>
                                    </div>
                               
                            </div>
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                   <%-- <table class="tbl-controlPanel">
                        <tr>
                            <td>
                                Shift
                            </td>
                            <td>
                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" class="input controlLength"
                                     AutoPostBack="false"> 
                                    <asp:ListItem Value="0">...Select...</asp:ListItem>                                   
                                </asp:DropDownList>
                            </td>
                            <td>
                                Batch
                            </td>
                            <td>
                                <asp:DropDownList ID="dlBatch" AutoPostBack="true" runat="server" class="input controlLength"
                                    ClientIDMode="Static" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" >
                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                </asp:DropDownList></td>
                           <td>Group</td>
                        <td>
                            <asp:DropDownList ID="dlGroup" runat="server" CssClass="input controlLength" 
                               AutoPostBack="true"  ClientIDMode="Static" OnSelectedIndexChanged="dlGroup_SelectedIndexChanged">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>
                        </td> 
                            
                        </tr>
                        <tr>
                            <td>
                                Section
                            </td>
                            <td>
                                <asp:DropDownList ID="dlSection" runat="server" class="input controlLength"
                                     ClientIDMode="Static"  AutoPostBack="true"  OnSelectedIndexChanged="dlSection_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="text-align:right">Roll
                            </td>
                            <td>
                               
                                       <asp:DropDownList ID="dlRoll" runat="server" CssClass="input controlLength" 
                               AutoPostBack="false"  ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                                           </asp:DropDownList>
                            </td>
                            <td colspan="2">
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success" 
                                   OnClientClick="return validateInputs();"  OnClick="btnSearch_Click" />
                                   
                            </td>                            
                        </tr>
                        <tr>                            
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                    </table>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-12">
            <div class="tgPanel">
                <div class="tgPanelHead">Searching Result</div>
                <asp:UpdatePanel ID="upPanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <table class="tbl-controlPanel">
                        <tr>
                            <td>
                                <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentName" Text=""></asp:Label>
                            </td>
                            <td>
                                <asp:Button runat="server" OnClick="btnPayNow_Click" ID="btnPayNow" 
                                     Visible="false" ClientIDMode="Static" 
                                    Text="Pay Now" CssClass="btn btn-primary" />
                            </td>
                        </tr>                        
                    </table>                    
                </ContentTemplate>
            </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnPayNow" />
                </Triggers>
                <ContentTemplate>
                    <div id="divFineInfo" class="datatables_wrapper" runat="server" 
                        style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;"></div>
                    <div id="divGrandTotal" class="head grandTotal" 
                        style=" "></div>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function makePayment() {
            $('#tblFine tbody tr td:nth-child(3) input[type=checkbox]').each(function () {
                if (this.checked) {
                    var id = this.id;
                    var famount = this.value;
                    jx.load('/ajax.aspx?id=' + id + '&todo=putFineDate' + '&amount= ' + famount + '', function (data) {
                    });
                }
                else {
                    // alert('no selected');
                }
            });
        }
        function checkFine(checkbox) {
            var id = checkbox.id;
            var famount = checkbox.value;
            var status = checkbox.checked;
            jx.load('/ajax.aspx?id=' + id + '&todo=fineCollection' + '&amount= ' + famount + '&status=' + status + ' ', function (data) {
                $('#divGrandTotal').html('Grand Total : ' + data);
            });
        }
        function validateInputs() {
            try {              
                if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
                if (validateCombo('dlBatch', "0", 'Select a Batch') == false) return false;
                if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
                if (validateCombo('dlRoll', "0", 'Select a Roll') == false) return false;
                return true;
            }
            catch (e) {
                showMessage("Validation failed : " + e.message, 'error');
                console.log(e.message);
                return false;
            }
        }
    </script>
</asp:Content>
