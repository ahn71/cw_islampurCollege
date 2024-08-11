<%@ Page Title="Department Ways Salary Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SalaryDetailsInfo.aspx.cs" Inherits="DS.UI.Administration.HR.Payroll.SalaryDetailsInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn{
            margin: 2px 0;
        }
        .controlLength {
            min-width:120px;
        }
        .tgPanel {
            width: 100%;
        }
        #tblSetRollOptionalSubject {
            width: 100%;
        }
        #tblSetRollOptionalSubject th,
        #tblSetRollOptionalSubject td,
        #tblSetRollOptionalSubject td input,
        #tblSetRollOptionalSubject td select {
            padding: 5px 5px;
            margin-left: 10px;
            text-align: center;
        }
        .litleMargin {
            margin-left: 5px;
        }

        .inbox {
        min-width:130px;
        }
         @media (min-width: 320px) and (max-width: 480px) {
      
            .inbox {
                margin-top:10px;
            }
            #btnSearch {
                margin-left:15px;
                margin-top:5px;
            }
            #btnSalaryRange {
                margin-top:5px;
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
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>
                <li><a runat="server" href="~/UI/Administration/HR/Payroll/PayrollHome.aspx">Payroll Management</a></li>
                <li class="active">Department Ways Salary Details</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Department Ways Salary Details Information</div>
                    <div class="row tbl-controlPanel"> 
                        <div class="col-sm-12">
                            <div class="">
                                 <div class="col-sm-5">
                                     <label class="col-sm-5">Department</label>
                                     <div class="col-sm-7">
                                        <asp:DropDownList runat="server" ID="dlDepartment" ClientIDMode="Static" CssClass="input controlLength form-control"
                                    AutoPostBack="true" OnSelectedIndexChanged="dlDepartment_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                </asp:DropDownList>
                                     </div>
                                 </div>
                                <div class="col-sm-5">

                                     <label class="col-sm-5">Name</label>
                                    <div class="col-sm-7">
                                        <asp:UpdatePanel ID="up1" runat="server">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dlDepartment" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="dlTeacher" runat="server" CssClass="input controlLength form-control"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                        </div>
                                 </div>
                                <div class="col-sm-2">
                                     <asp:Button runat="server" ID="btnSearch" ClientIDMode="Static" Text="Search" CssClass="btn btn-success" OnClientClick="return validateInputs();"
                                    OnClick="btnSearch_Click" />
                
                                 </div>
                                
                            </div>
                        </div>
                    </div>

                 
                    <br />
                </div>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Searching by Salary</div>
                    <div class="row tbl-controlPanel"> 
                        <div class="col-sm-12">
                            <div class="">
                                 <div class="col-sm-3">
                                     <asp:DropDownList ID="dlSalaryType" runat="server" CssClass="form-control inbox">
                                        <asp:ListItem>Basic Salary</asp:ListItem>
                                        <asp:ListItem>School Salary</asp:ListItem>
                                        <asp:ListItem>Total Salary</asp:ListItem>
                                    </asp:DropDownList>
                    
                                 </div>
                                <div class="col-sm-3">
                                      <asp:TextBox runat="server" ID="txtFromSalary" CssClass="form-control inbox" PlaceHolder="From Salary"></asp:TextBox>
                
                                 </div>
                                <div class="col-sm-3">
                                    <asp:TextBox runat="server" ID="txtToSalary" CssClass="form-control inbox" PlaceHolder="To Salary"></asp:TextBox>
                
                                 </div>
                                <div class="col-sm-3">
                                     <asp:Button runat="server" ID="btnSalaryRange" ClientIDMode="Static" CssClass="btn btn-success" Text="Search" OnClientClick="return validateInput2();"
                                    OnClick="btnSalaryRange_Click" />
                
                                 </div>
                                
                            </div>
                        </div>
                    </div>

                 
                    <br />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-10"></div>
            <div class="col-md-2">
                <asp:UpdatePanel runat="server" ID="up2">
                    <ContentTemplate>
                        <asp:Button runat="server" ID="btnPrintPreview" ClientIDMode="Static" Text="Print Preview"
                            CssClass="btn btn-success pull-right" OnClick="btnPrintPreview_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Details</div>
            <hr style="margin: 0px;" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalaryRange" />
                </Triggers>
                <ContentTemplate>
                    <div id="divSalaryDetailsInfo" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
       <script type="text/javascript">     
        function validateInputs() {                        
            if (validateCombo('dlDepartment',"0", 'Select Department') == false) return false;
            if (validateCombo('MainContent_dlTeacher', "0", 'Select Name') == false) return false;
                return true;        
        }
        function validateInput2(){
            if (validateText('MainContent_txtFromSalary', 1, 20, 'Enter  Salary Range') == false) return false;
            if (validateText('MainContent_txtToSalary', 1, 20, 'Enter Salary Range') == false) return false;
            return true;
        }
    </script>
</asp:Content>
