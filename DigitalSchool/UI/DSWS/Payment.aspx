<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="DS.UI.DSWS.Payment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength {
            width: 120px;
            margin: 5px;
        }

        .tgInput td:first-child,
        .tgInput td:nth-child(3),
        .tgInput td:nth-child(5),
        .tgInput td:nth-child(4) {
            padding: 0px;
            width: 20px;
        }

        .tgPanel {
            width: 100%;
        }

        .littleMargin {
            margin-right: 5px;
        }

        #btnPrintPreview {
            margin: 3px;
        }

        .form-control, .table, .table-bordered > thead > tr > th, .table-bordered > tbody > tr > td, .table-bordered > tbody > tr > th {
            border: 1px solid #aaa;
            color: #000;
            font-size: 16px;
        }

        .std_result_show {
            margin: 0 auto;
            width: 40%;
        }

            .std_result_show tr {
                border: 1px solid #ddd
            }

                .std_result_show tr:nth-child(even) {
                    background: #fafafa;
                }

                .std_result_show tr td {
                    padding: 5px !important;
                }

                    .std_result_show tr td:first-child {
                        font-weight: bold;
                        color: #1CA59E;
                    }

        .control-label {
            text-align: right;
        }
        .nagadBtn{
            border: 1px solid #337ab7;
            padding: 12px;
            font-size: 20px;
            color: #000;
            border-radius: 3px;
            font-weight: 600;
            text-transform: capitalize;
            display: inline-block;
            margin-top: 15px;
        }
        .linkbt{
            text-decoration:underline;
            font-size:20px;
            color:blue;
        }
    </style>
    <script type="text/javascript">
        function searchAgain() {
            $('#txtRollNo').val('');
            $('#txtRollNo').focus();
            $("#ForLeftSideMenuList_divResultView").empty();

        }
        function printDiv() {

            var divToPrint = document.getElementById('page-wrapper1');

            var newWin = window.open('', 'Print-Window');
            newWin.document.open();
            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 10);

        }
        function goToNewTab(url) {
            window.open(url);
        }
        function validateDropDown() {
            if (validateCombo('ddlYear', 0, 'Select Year') == false) return false;
            if (validateCombo('ddlBatch', 0, 'Select Class Name') == false) return false;
            if (validateText('txtRollNo', 1, 30, 'Type Admissino No') == false) return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    <section class="template-section">
        <div class="container">
            <div runat="server" id="divBoardDirContacts" class="main-content">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                            <asp:AsyncPostBackTrigger ControlID="btnPayment" />
                            <asp:AsyncPostBackTrigger ControlID="ddlFeeCategories" />
                            <asp:AsyncPostBackTrigger ControlID="ckbIsAdmission" />
                            <asp:AsyncPostBackTrigger ControlID="rblPaywith" />
                            <asp:AsyncPostBackTrigger ControlID="btnPayNow" />
                            <asp:AsyncPostBackTrigger ControlID="ddlClassForOpen" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="result-top-box">
                                <div class="row">
                                    <div class="col-md-2 col-xs-2">
                                        <img class="img-responsive" src="/websitedesign/assets/images/logo.png" />
                                    </div>
                                    <div class="col-md-10 col-xs-10">
                                        <h4 class="title-4">Islampur college WEB BASED PAYMENT SYSTEM</h4>
                                        <h2 runat="server" id="hIsOpenPayment" class="title-4" style="color:yellow;font:bold">(Open Payment)</h2>
                                        <h2 runat="server" id="hIsTest" class="title-4 text-warning">(Under Testing!!!)</h2>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default cform-panel">

                                <div class="panel-heading">
                                    <h4>Please provide your fee corresponding Info  </h4>
                                </div>
                                <div class="panel-body">
                                    <div class="col-md-10 col-md-offset-1">
                                        <asp:Panel runat="server" ID="pnlSearcingArea" ClientIDMode="Static">
                                        <div class="form-group row">
                                            <label class="col-md-3 control-label"></label>
                                            <div class="col-md-9">
                                                <asp:CheckBox  runat="server" ClientIDMode="Static" AutoPostBack="true" ID="ckbIsAdmission" Text="_ Admission Fee?" OnCheckedChanged="ckbIsAdmission_CheckedChanged" />
                                              
                                            </div>
                                          
                                        </div>
                                        <asp:Panel runat="server" ClientIDMode="Static" ID="pnlYearClassArea"> 
                                        <div class="form-group row">
                                            <label class="col-md-3 control-label">Year</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"
                                                    ClientIDMode="Static">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-md-3 control-label">Class</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control" ClientIDMode="Static">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                            </asp:Panel>
                                        <div class="form-group row">
                                            <label class="col-md-3 control-label" runat="server" id="lblAdmissionOrRollCaption">Admission Form No/SSC/HSC Roll/Registration No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtRollNo" placeholder="Enter your admission no" runat="server" CssClass="form-control"
                                                    ClientIDMode="Static">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group row">
                                            <label class="col-md-3 "></label>
                                            <div class="col-md-1 ">
                                                <asp:Label runat="server" style="color: #1CA59E;font-size:18px" ID="lblMsg" ClientIDMode="Static"></asp:Label>
                                            </div>
                                            <div class="col-md-8 text-right">
                                                <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-primary littleMargin" ID="btnFind" Text="Search Student" OnClientClick="return validateDropDown();" OnClick="btnFind_Click" />
                                            </div>
                                        </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" Visible="false" ClientIDMode="Static" ID="pnlPayment">

                                            <div class="row">
                                                <div class="col-md-3"></div>
                                                <div class="col-md-9">
                                                    <h3 class="text-primary">Student Information</h3>
                                                    <div class="student-info-by-pay" runat="server" id="divExistingStudentViewArea">
                                                        <div class="table-responsive">
                                                            <table class="table table-bordered">
                                                                <tbody>
                                                                    <tr>
                                                                        <th style="width: 120px;">Admission No/SSC/HSC Roll</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblAdmissionNo"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                        <td colspan="3" style="text-align: center;">
                                                                            <div runat="server" id="divImage">
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="width: 120px;">Name</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblStudentName"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                        <th style="width: 120px;">Father Name</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblFathersName"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="width: 120px;">Year</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblYear"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                        <th style="width: 120px;">Class</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblClass"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <th style="width: 120px;">Group</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblGroup"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                        <th style="width: 120px;">Class Roll</th>
                                                                        <td style="width: 5px;">:</td>
                                                                        <td>
                                                                            <asp:Label runat="server" ID="lblClassRoll"
                                                                                ClientIDMode="Static"></asp:Label></td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                     <div class="form-group row" style="display:none" >
                                                        <label class="col-md-12">Pay with</label>
                                                        <div class="col-md-12">
                                                            <asp:RadioButtonList Visible="false" runat="server" ID="rblPaywith" ClientIDMode="Static" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblPaywith_SelectedIndexChanged">
                                                                <asp:ListItem Selected="True" Value="nagad">
                                                                     <img src="../../Images/nagad.svg" />
                                                                </asp:ListItem>
                                                                <asp:ListItem  Value="ssl">
                                                                    <img src="../../Images/ssl-icon.png" />
                                                                </asp:ListItem>
                                                                 
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div> 
                                                    
                                                    <div runat="server" id="divOpenStudentInfoArea">
                                                         <div class="form-group row">
                                                        <label class="col-md-12">Class  <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="ddlClassForOpen" runat="server" ClientIDMode="Static"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClassForOpen_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Session <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtYear" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Group Name <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                             <asp:DropDownList ID="ddlGroupForOpen" runat="server" ClientIDMode="Static"
                                                                CssClass="form-control">
                                                                  </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Registration Number <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtRegNo" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Class Roll <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtClassRoll" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                         
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Full Name<strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtStudentName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                       
                                                        
                                                        <div class="form-group row">
                                                        <label class="col-md-12">Father's Name <strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtFathersName" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                        <div class="form-group row">
                                                        <label class="col-md-12">Mobile Number(+88)<strong style="color:red">*</strong> </label>
                                                        <div class="col-md-12">
                                                            <asp:TextBox runat="server" ID="txtStudentMobileNo" CssClass="form-control"  MaxLength="11"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                       
                                                    </div>
                                                    
                                                    <div class="form-group row" runat="server" id="ddlCatagory">
                                                        <label class="col-md-12">Fee Category<strong style="color:red">*</strong></label>
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="ddlFeeCategories" runat="server" ClientIDMode="Static"
                                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFeeCategories_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>                                                   

                                                    <div runat="server" id="divParticularCategoryList">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-md-3"></div>
												<div class="col-md-9">
													<div class="before-resv-note text-center">
													   <input for="iagree" style="color: #333" type="checkbox" name="vehicle" class="" checked="checked" disabled=""><span id="iagree"> I agree with <a href="terms-conditions" target="_blank">Terms &amp; Condition</a></span>
													</div>
												</div>
                                                <div class="col-md-3"></div>
                                                <div class="col-md-9">
                                                    <div class="text-center">                                                       
                                                        <asp:LinkButton ID="btnPaymentSSL" CssClass="nagadBtn"  runat="server" ClientIDMode="Static"  OnClick="btnPaymentSSL_Click" >
                                                            Payment By
                                                            <img src="../../Images/ssl-icon.png" />
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="btnPayment" CssClass="nagadBtn" runat="server" ClientIDMode="Static" OnClick="btnPayment_Click" >
                                                            Payment By
                                                            <img src="../../Images/nagad.svg" />
                                                        </asp:LinkButton>

                                                        <asp:LinkButton Visible="false" ID="btnPayNow" CssClass="nagadBtn" runat="server" ClientIDMode="Static" OnClick="btnPayNow_Click">
                                                            Pay Now                                           
                                                        </asp:LinkButton>


                                                        <h1 runat="server" visible="false" style="color:green;" id="hAlreadyPaid"> Already Paid!</h1>

                                                        <h1 runat="server" visible="false" style="color:red;" id="hPreviousDue"></h1>
                                                         <h3 runat="server" id="subscriptionMessage" visible="false" class="text-danger">এই ক্লাসের জন্য আপনার সফটওয়্যার এর সাবস্ক্রিপশন করা নেই</h3>
                                                         <a runat="server" id="btnSubsreicption" visible="false" class="linkbt">সাবস্ক্রিপশন করতে এখানে ক্লিক করুন</a>

                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                       

                                    </div>
                                </div>
                            </div>
                            <%--  Result View  --%>

                            <%--  Result View  --%>
                            <br />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
