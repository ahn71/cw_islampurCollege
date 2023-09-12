<%@ Page Title="Employee Registration Form" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EmpRegForm.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.EmpRegForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" >
    <style type="text/css">
        .tgPanel {
            width: 100%;
        }

        .tbl-controlPanel {
            width: 100%;
            font-size: 1em;
        }

            .tbl-controlPanel tr td {
                width: 35%;
            }

            .tbl-controlPanel td:first-child,
            .tbl-controlPanel td:nth-child(2n+1) {
                text-align: right;
                width: 15%;
                padding-right: 5px;
            }

        .controlLength {
            color: #000;
        }

        .tbl-controlPanel1 {
            font-family: Calibri;
            font-size: 1em;
        }

            .tbl-controlPanel1 td:first-child,
            .tbl-controlPanel1 td:nth-child(3),
            .tbl-controlPanel1 td:nth-child(5) {
            }

        .tbl-controlPanel2 {
            font-family: Calibri;
            width: 98%;
            margin: 10px 0px;
            font-size: 1em;
        }

            .tbl-controlPanel2 tr td {
                width: 19%;
                padding: 3px 0;
            }

            .tbl-controlPanel2 td:first-child {
                width: 18%;
                text-align: right;
                padding-right: 5px;
            }

            .tbl-controlPanel2 td:nth-child(3),
            .tbl-controlPanel2 td:nth-child(5) {
                text-align: right;
                width: 10%;
                padding-right: 5px;
            }

        .tgbutton {
            padding: 10px 0;
            margin-left: 39%;
        }

        .ajax__calendar_footer {
            height: auto !important;
        }

        input[type="checkbox"] {
            margin: 5px;
        }

        .extraMargin {
            margin-right: 2px;
        }

        .radiobuttonlist label {
        }

        .input {
            padding: 6px;
        }

        .in2 {
            float: left;
        }

        .erf-nxt-btn {
            margin-top: 15px;
        }

        .ec-title {
            text-align: left !important;
            margin-left: 10px;
        }

        .pimg-box {
            margin-bottom: 20px;
        }

        .emp-table-box {
            padding-left: 0 !important;
            padding-right: 0 !important;
        }

        .otherinfo-box {
            margin-bottom: 20px;
        }

        #EmpSignimgProfile {
            width: 100%;
            height: 80px;
        }

        #FileUpload1, #FileUpload2 {
            width: 95%;
        }

        #MainContent_etab_logic .form-control {
            color: #333 !important;
        }

        #MainContent_txtEIExamName, #MainContent_txtEIDepertment {
            color: #333 !important;
        }
         #MainContent_CalendarExtender3_container td,
        #MainContent_CalendarExtender3_container td:first-child,
        #MainContent_CalendarExtender3_container td:nth-child(3),
        #MainContent_CalendarExtender4_container td,
        #MainContent_CalendarExtender4_container td:first-child,
        #MainContent_CalendarExtender4_container td:nth-child(3),
        #MainContent_CalendarExtender3_daysTable td,
        #MainContent_CalendarExtender3_daysTable td:first-child,
        #MainContent_CalendarExtender3_daysTable td:nth-child(3){
            width: auto;
            margin: 0;
            padding: 0;
        }
         .overtgPanelHead{
            width:97%;
         }
         .checkboxwdt{
             width:100px;
         }
         
         .checkboxwdt label{
             margin-top:-5px!important;
         }
         .leftsidemnone{
             padding-left:0!important;
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
                <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpHome.aspx">Employee Management</a></li>
                <li class="active">Employee Registration Form</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <!-- =============EmpReg Form Start TAB=========== -->

    <div id="Tabs" role="tabpanel">
        
        <ul id="rowTab" class="nav nav-tabs  nav-pills nav-justified thumbnail setup-panel" role="tablist">
            <li id="litab1" class="active"><a aria-controls="empRegp" role="tab" data-toggle="tab" href="#empRegp">Personal Information</a></li>
            <li id="litab2"><a data-toggle="tab" href="#empRegp1" aria-controls="empRegp1" role="tab">Institutional Information</a></li>
            <li id="litab3"><a data-toggle="tab" href="#empRegp2" aria-controls="empRegp2" role="tab">Educational Information</a></li>
            <li id="litab4"><a data-toggle="tab" href="#empRegp3" aria-controls="empRegp3" role="tab">Experience Information</a></li>
            <li id="litab5"><a data-toggle="tab" href="#empRegp4" aria-controls="empRegp4" role="tab">Others Information</a></li>
        </ul>
   
                <div class="tab-content">
                        
                    <div id="empRegp" role="tabpanel" class="tab-pane active">
                        <div class="row setup-content">
                            <div class="col-xs-12">
                                <div class="col-md-12 well text-center">

                                    <!-- <form> -->
                                   <%--  <asp:UpdatePanel runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
               <asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />
                <asp:AsyncPostBackTrigger ControlID="chkSameAddress" />

            </Triggers>
            <ContentTemplate>--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="tgPanel">
                                                <div class="tgPanelHead">Personal Information</div>


                                                <div style="float: left;">
                                                    <div class="row tbl-controlPanel">
                                                          <asp:UpdatePanel runat="server" ID="up1" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                <asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />
                <asp:AsyncPostBackTrigger ControlID="chkSameAddress" />

            </Triggers>
            <ContentTemplate>
                                                        <div class="col-sm-10 leftsidemnone">
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Type</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:RadioButtonList ID="rblEmpType"  runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">                                    
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Principal</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:RadioButtonList ID="rblviceprincipal" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal" AutoPostBack="false">
                                                                            <asp:ListItem class="radio-inline" style="margin-left: 10px" Value="1">Yes</asp:ListItem>
                                                                            <asp:ListItem class="radio-inline" style="margin-left: 10px" Selected="True" Value="0">No</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">ID No.<span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_CardNo" runat="server"
                                                                            CssClass="input controlLength form-control" ClientIDMode="Static"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Name <span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_Name" runat="server" ClientIDMode="Static"
                                                                            CssClass="input in2" Width="69%"></asp:TextBox>
                                                                        <asp:TextBox ID="txtTCodeNo" runat="server" ClientIDMode="Static"
                                                                            CssClass="input form-control" Width="30%" PlaceHolder="T Code"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row tbl-controlPanel">

                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Name Bangla</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtENameBN" runat="server"
                                                                            ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Father's Name<span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_FathersName" runat="server"
                                                                            ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Mother's Name <span class="required"></span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_MothersName" runat="server"
                                                                            ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Religion</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="dlReligion" runat="server"
                                                                            ClientIDMode="Static" CssClass="input controlLength form-control">
                                                                            <asp:ListItem>Islam</asp:ListItem>
                                                                            <asp:ListItem>Hindu</asp:ListItem>
                                                                            <asp:ListItem>Buddha</asp:ListItem>
                                                                            <asp:ListItem>Christian</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Nationality</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="dlNationality" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                                                            <asp:ListItem>Bangladeshi</asp:ListItem>
                                                                            <asp:ListItem>Japanese</asp:ListItem>
                                                                            <asp:ListItem>Pakistani</asp:ListItem>
                                                                            <asp:ListItem>India</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>

                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Blood Group</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="dlBloodGroup" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                                                            <asp:ListItem>Unknown</asp:ListItem>
                                                                            <asp:ListItem>A+</asp:ListItem>
                                                                            <asp:ListItem>A-</asp:ListItem>
                                                                            <asp:ListItem>B+</asp:ListItem>
                                                                            <asp:ListItem>B-</asp:ListItem>
                                                                            <asp:ListItem>AB+</asp:ListItem>
                                                                            <asp:ListItem>AB-</asp:ListItem>
                                                                            <asp:ListItem>O+</asp:ListItem>
                                                                            <asp:ListItem>O-</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row tbl-controlPanel">

                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Marital Status</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="dlMaritalStatus" runat="server"
                                                                            ClientIDMode="Static" CssClass="input controlLength form-control">
                                                                            <asp:ListItem>Married</asp:ListItem>
                                                                            <asp:ListItem>Unmarried</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Date of Birth<span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_Birthday" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtE_Birthday"></asp:CalendarExtender>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Gender<span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="dlGender" runat="server" ClientIDMode="Static"
                                                                            CssClass="input controlLength form-control">
                                                                            <asp:ListItem>Male</asp:ListItem>
                                                                            <asp:ListItem>Female</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Phone</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_Phone" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                                                            onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Mobile <span class="required">*</span></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="lblMobile" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center extraMargin form-control in2"
                                                                            ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                                                        <asp:TextBox ID="txtE_Mobile" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))" runat="server" Width="80%"
                                                                            MaxLength="11" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4">Email</label>
                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="txtE_Email" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                              <div class="tgPanelHead overtgPanelHead">Emergency Contact</div>
                   
                                                            <div class="econtact-box">
                                                               
              
                                                                
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Name </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtECName" runat="server"
                                                                                ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Relation </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtECRelation" runat="server"
                                                                                ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6"></div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Mobile </label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtECMobile" runat="server"
                                                                                ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                     </div>
                                                          <div class="tgPanelHead overtgPanelHead"> Present Address </div>
                                                            <div class="econtact-box">                                                                
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6">

                                                                        <label class="col-sm-4">Address<span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtE_PresentAddress" runat="server"
                                                                                ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">District <span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlDistrict" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="DistrictName" DataValueField="DistrictId" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                                                                <asp:ListItem Selected="True" Value="-1">--Select--</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Thana/Upazila<span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlthana" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="ThanaName" DataValueField="ThanaId" AutoPostBack="true" OnSelectedIndexChanged="ddlthana_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Post Office <span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlPostOffice" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="PostOfficeName" DataValueField="PostOfficeID">
                                                                                 <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                             <div class="tgPanelHead overtgPanelHead">Permanent Address
                        <div class="col-sm-1 checkboxwdt" style="float: right;">
                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckedChanged"  runat="server" ID="chkSameAddress" ClientIDMode="Static" />
                        <label  for="chkSameAddress">Same</label>
                    </div>
                                                                   
                    </div>
                                                            <div class="econtact-box">                                                               
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6">

                                                                        <label class="col-sm-4">Address<span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:TextBox ID="txtE_ParmanentAddress" runat="server"
                                                                                ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                                        </div>

                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">District <span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlpAddress" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="DistrictName" DataValueField="DistrictId" AutoPostBack="true" OnSelectedIndexChanged="ddlpAddress_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row tbl-controlPanel">
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Thana/Upazila<span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlpThana" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="ThanaName" DataValueField="ThanaId" AutoPostBack="true" OnSelectedIndexChanged="ddlpThana_SelectedIndexChanged">
                                                                                 <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-sm-4">Post Office <span class="required"></span></label>
                                                                        <div class="col-sm-8">
                                                                            <asp:DropDownList ID="ddlpPostOffice" runat="server" ClientIDMode="Static"
                                                                                CssClass="input controlLength form-control" DataTextField="PostOfficeName" DataValueField="PostOfficeID">
                                                                                 <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row tbl-controlPanel">
                                                                <div class="col-sm-6">
                                                                    <label class="col-sm-4"></label>
                                                                    <div class="col-sm-8">
                                                                        <asp:RadioButtonList RepeatLayout="Flow" CssClass="checkboxlist" RepeatDirection="Horizontal">
                                                                            <asp:CheckBox ID="chkExaminer" class="checkbox-inline" runat="server" Checked="True" Text="Is Examiner" />
                                                                            <asp:CheckBox ID="chkIsActive" class="checkbox-inline" runat="server" Checked="True" Text="Is Active" TextAlign="Right" />
                                                                            <asp:CheckBox ID="chkForAllShift" class="checkbox-inline" runat="server" Checked="True" Text="Assign Both Shift" />
                                                                        </asp:RadioButtonList>
                                                                    </div>
                                                                </div>

                                                            </div>

                                                        </div>
                                                        </ContentTemplate>
                                         </asp:UpdatePanel>
                                                          <asp:UpdatePanel runat="server" ID="up2" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
             
             <%-- <asp:AsyncPostBackTrigger ControlID="rblEmpType" />--%>
            </Triggers>
            <ContentTemplate>
                                                        <div class="col-sm-2">
                                                            <div class="row pimg-box">
                                                                <div style="width: 100%; text-align: center;">
                                                                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                                                                </div>
                                                                <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                                            </div>
                                                            <div class="row pimg-box">

                                                                <asp:Image ID="EmpSignimgProfile" class="stdSignImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/EmpSign/noSignImage.jpg" />
                                                                <br />
                                                                <asp:FileUpload ID="FileUpload2" runat="server" onclick="" onchange="previewEmpSignFile()" ClientIDMode="Static" />
                                                            </div>
                                                            <%--<div class="row">
                                                <div class="col-md-4"><a href="#" class="btn btn-primary">New</a></div>
                                                <div class="col-md-4"><a href="#" class="btn btn-success">Save</a></div>
                                                <div class="col-md-4"><a href="#" class="btn btn-danger">Close</a></div>
                                            </div>
                                            <div class="row"><a href="#" class="btn btn-default btnsame">Education Information</a></div>
                                            <div class="row"><a href="#" class="btn btn-default btnsame">Institutional Information</a></div>
                                            <div class="row"><a href="#" class="btn btn-default btnsame">Experience Information</a></div>
                                            <div class="row"><a href="#" class="btn btn-default btnsame">Others Information</a></div>--%>
                                                        </div>
                    </ContentTemplate>
                                         </asp:UpdatePanel>
                                                    </div>

                                                </div>


                                                <div style="clear: both;"></div>
                                            </div>
                                        </div>
                                    </div>
                <%--</ContentTemplate>
                                         </asp:UpdatePanel>--%>
                                    <a data-toggle="tab" class="btn btn-primary btn-md erf-nxt-btn" onclick="tabchange(0)" href="#empRegp1">Go to Next Step >></a>
                                    <!-- </form> -->

                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="empRegp1" role="tabpanel" class="tab-pane">
                        <div class="row setup-content">
                            <div class="col-xs-12">
                                <div class="col-md-12 well text-center">
                                    <asp:UpdatePanel runat="server" ID="up3" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                <asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />

            </Triggers>
            <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="tgPanel">
                                                <div class="tgPanelHead">Institutional Information</div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Department <span class="required">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlDepartments" runat="server"
                                                                ClientIDMode="Static" CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Designation<span class="required">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlDesignation" runat="server"
                                                                ClientIDMode="Static" CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Joining Date <span class="required">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtE_JoiningDate" runat="server"
                                                                CssClass="input controlLength form-control" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtE_JoiningDate_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txtE_JoiningDate"></asp:CalendarExtender>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4">Duration in This College<span class="required"></span></label>
                                                        <div class="col-sm-3">
                                                            <b>
                                                                <asp:Label ID="dlcYear" runat="server"></asp:Label></b>


                                                        </div>
                                                        <div class="col-sm-3">

                                                            <asp:Label ID="dlcMonth" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <b>
                                                                <asp:Label ID="dlcDay" runat="server"></asp:Label></b>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Shift <span class="required">*</span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlShift" runat="server"
                                                                ClientIDMode="Static" CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Job Type</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlEStatus" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                                                <asp:ListItem>Permanent</asp:ListItem>
                                                                <asp:ListItem>Temporary</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-6">
                                                        <label class="col-sm-4">Last Degree<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="txtE_LastDegree" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4">Class Teacher of(if applicable)<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlClassTeacher" runat="server"
                                                                CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <%-- <div class="col-md-6">
                                                        <label class="col-sm-4">Group<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlGroup" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="dlGroup_SelectedIndexChanged" CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4">Subject<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="dlSubject" runat="server"
                                                                ClientIDMode="Static" CssClass="input controlLength form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                                </div>
                                                <div class="row tbl-controlPanel" style="display:none">
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4">Subject Teacher of Section: XI:<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="dlToSectionXI" runat="server" CssClass="input controlLength form-control"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label class="col-sm-4">XII<span class="required"></span></label>
                                                        <div class="col-sm-8">
                                                            <asp:TextBox ID="dlToSectionXII" runat="server" CssClass="input controlLength form-control"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                </ContentTemplate>
                                  </asp:UpdatePanel>
                                          <a data-toggle="tab" onclick="tabchange(1)" class="btn btn-primary btn-md erf-nxt-btn" href="#empRegp2">Go to Next Step >></a>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="empRegp2" role="tabpanel" class="tab-pane">
                        <div class="row setup-content">
                            <div class="col-xs-12">
                                <div class="col-md-12 well text-center">
                                    <div class="tgPanelHead">Educational Information</div>
                                     <asp:UpdatePanel runat="server" ID="up4" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
               <%-- <asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />

            </Triggers>
            <ContentTemplate>
                                    <div class="container col-xs-12">
                                        <div class="row clearfix">
                                            <div class="col-md-12 column emp-table-box">

                                                <div class="col-md-6">
                                                    <br />
                                                    <table runat="server" class="table table-bordered table-hover" style="background:#fff;" id="tab_logic">

                                                        <tr>
                                                            <th class="text-center">Examination Name
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtEIExamName" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Group/Depertment
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtEIDepertment" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Board/University
                                                            </th>

                                                            <td>
                                                                <asp:TextBox ID="txtEIBoard" CssClass="input controlLength form-control" runat="server"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Passing Year
                                                            </th>

                                                            <td>
                                                                <asp:TextBox ID="txtEIPassingYear" runat="server" CssClass="input controlLength form-control"></asp:TextBox>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Result
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtEIResult" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th></th>
                                                            <td>
                                                                <asp:Button ID="btnAddEducation" CssClass=" btn btn-success" runat="server" OnClick="btnAddEducation_Click" />

                                                            </td>

                                                        </tr>

                                                    </table>
                                                </div>
                                                <div class="col-md-6">
                                                    <br />
                                                    <asp:GridView ID="educationlist" runat="server" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                            <asp:BoundField DataField="SL" HeaderText="SL NO"></asp:BoundField>
                                                            <asp:BoundField DataField="EIExamName" HeaderText="Exam Name"></asp:BoundField>
                                                            <asp:BoundField DataField="EIDepertment" HeaderText="Depertment"></asp:BoundField>
                                                            <asp:BoundField DataField="EIBoard" HeaderText="Board/University"></asp:BoundField>
                                                            <asp:BoundField DataField="EIPassingYear" HeaderText="Passing Year"></asp:BoundField>
                                                            <asp:BoundField DataField="EIResult" HeaderText="Result"></asp:BoundField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn-link" CausesValidation="false" CommandName="" OnClick="LinkButton2_Click" Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkeduedit" runat="server" CssClass="btn-link" CausesValidation="false" CommandName="" OnClick="lnkeduedit_Click" Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                 
                                                </div>



                                            </div>
                                        </div>
                                    </div>
                </ContentTemplate>
                                         </asp:UpdatePanel>
                                    <a data-toggle="tab" onclick="tabchange(2)" class="btn btn-primary btn-md erf-nxt-btn" href="#empRegp3">Go to Next Step >></a>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="empRegp3" role="tabpanel" class="tab-pane">
                        <div class="row setup-content">
                            <div class="col-xs-12">
                                <div class="col-md-12 well text-center">
                                    <div class="tgPanelHead">Experience Information</div>
                                     <asp:UpdatePanel runat="server" ID="up5" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                <%--<asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />
                 <asp:AsyncPostBackTrigger ControlID="txtDateFromTo" />

            </Triggers>
            <ContentTemplate>
                                    <div class="container col-xs-12">
                                        <div class="row clearfix">
                                            <div class="col-md-12 column emp-table-box">
                                                <div class="col-md-6">
                                                    <br />
                                                    <table runat="server" class="table table-bordered table-hover" style="background:#fff;" id="etab_logic">

                                                        <tr>
                                                            <th class="text-center">Institute Name
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtinstituteName" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Designation
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtDesidnation" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Date(From)
                                                            </th>

                                                            <td>
                                                                <asp:TextBox ID="txtDateFromTo" ClientIDMode="Static" AutoPostBack="true" OnTextChanged="txtDateFromTo_TextChanged" CssClass=" input controlLength form-control" runat="server"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="txtDateFromTo"></asp:CalendarExtender>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Date(To)
                                                            </th>

                                                            <td>
                                                                <asp:TextBox ID="txt_toDate" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control" OnTextChanged="txt_toDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender4" CssClass="ajax__calendar" Format="dd-MM-yyyy" runat="server" TargetControlID="txt_toDate"></asp:CalendarExtender>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th class="text-center">Total Duration
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtTotalDuration" runat="server"  readonly="true" disabled="disabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th></th>
                                                            <td>
                                                                <asp:Button ID="btnAdd" CssClass=" btn btn-success" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                                            </td>

                                                        </tr>

                                                    </table>
                                                </div>
                                                <div class="col-md-6">
                                                    <br />
                                                    <asp:GridView ID="experienceList" runat="server" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>
                                                              
                                                            <asp:BoundField DataField="SL" HeaderText="SL NO"></asp:BoundField>
                                                            <asp:BoundField DataField="ExIInstName" HeaderText="Institution Name"></asp:BoundField>
                                                            <asp:BoundField DataField="ExIDesignation" HeaderText="Designation"></asp:BoundField>
                                                            <asp:BoundField DataField="ExIDDateFrom" HeaderText="Date (From)"></asp:BoundField>
                                                            <asp:BoundField DataField="ExIDateTO" HeaderText="Date (To)"></asp:BoundField>
                                                            <asp:BoundField DataField="ExIDuration" HeaderText="Duration"></asp:BoundField>
                                                           
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                     <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn-link" CausesValidation="false" CommandName="" OnClick="LinkButton1_Click" Text="Delete"></asp:LinkButton>
                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnlinkEdit" runat="server" CssClass="btn-link" CausesValidation="false" CommandName="" OnClick="btnlinkEdit_Click"  Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                           </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                   
                                                   </div>
                                            </div>
                                        </div>

                                    </div>
                </ContentTemplate>
                                         </asp:UpdatePanel>
                                    <a data-toggle="tab" onclick="tabchange(3)" class="btn btn-primary btn-md erf-nxt-btn" href="#empRegp4">Go to Next Step >></a>
                                </div>
                            </div>
                        </div>
                    </div>
               <%--  </ContentTemplate>
        </asp:UpdatePanel>--%>
                    <div id="empRegp4" role="tabpanel" class="tab-pane fade">

                        <div class="row setup-content">
                            <div class="col-xs-12">
                                <div class="col-md-12 well text-center">
                                    <div class="row">
                                        <asp:UpdatePanel runat="server" ID="up6" ClientIDMode="Static" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                <asp:AsyncPostBackTrigger ControlID="ddlpAddress" />
                <asp:AsyncPostBackTrigger ControlID="ddlDistrict" />
                <%--<asp:AsyncPostBackTrigger ControlID="txtE_JoiningDate" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlthana" />
                <asp:AsyncPostBackTrigger ControlID="ddlpThana" />
                <asp:AsyncPostBackTrigger ControlID="txt_toDate" />

            </Triggers>
            <ContentTemplate>
                                        <div class="col-md-12 otherinfo-box">
                                            <div class="tgPanel">
                                                <div class="tgPanelHead">Others Information</div>

                                                 <div class="container col-xs-12">
                                        <div class="row clearfix">
                                            <div class="col-md-12 column emp-table-box">
                                                <div class="col-md-6">
                                                     <asp:TextBox CssClass="input controlLength form-control" ID="txttraining" runat="server" Visible="false"></asp:TextBox>
                                                    <br />
                                                    <table runat="server" class="table table-bordered table-hover" style="background:#fff;" id="Table1">
                                                         
                                                        <tr>
                                                            <th class="text-center">Others Activities
                                                            </th>
                                                            <td>
                                                                <asp:TextBox CssClass="input controlLength form-control" ID="txtOthersInfo" runat="server"></asp:TextBox>
                                                              
                                                            </td>
                                                        </tr>                                                    
                                                        <tr>
                                                            <th></th>
                                                            <td>
                                                                <asp:Button ID="btnAddOthersInfo" CssClass=" btn btn-success" runat="server" Text="Add" OnClick="btnAddOthersInfo_Click"  />
                                                            </td>

                                                        </tr>

                                                    </table>
                                                </div>
                                                <div class="col-md-6">
                                                    <br />
                                                    <asp:GridView ID="gvOthersInfo" runat="server" CssClass="table table-bordered" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" AutoGenerateColumns="False">
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <Columns>                                                              
                                                            <asp:BoundField DataField="SL" HeaderText="SL NO"></asp:BoundField>
                                                            <asp:BoundField DataField="OthersInfo" HeaderText="Others Activities"></asp:BoundField>
                                                            <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                   <asp:LinkButton ID="lnkDeleteOthers" runat="server" CssClass="btn-link" CausesValidation="false" CommandName="" OnClick="lnkDeleteOthers_Click" Text="Delete"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkEditOthers" runat="server" CssClass="btn-link" CausesValidation="false" CommandName=""  OnClick="lnkEditOthers_Click" Text="Edit"></asp:LinkButton>
                                                                </ItemTemplate>
                                                           </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                    </asp:GridView>
                                                   
                                                   </div>
                                            </div>
                                        </div>

                                    </div>
                                            </div>
                                        </div>
                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        <div class="row tbl-controlPanel erf-nxt-btn">
                                            <div class="col-sm-2 col-sm-offset-5">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ClientIDMode="Static"
                                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                </div>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <style>
        .hid {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function tabchange(index) {
            if (index == 0) {
                $("#litab1").removeClass("active");
                $("#litab2").addClass("active");
            }
            else if (index == 1) {
                $("#litab2").removeClass("active");
                $("#litab3").addClass("active");
            }
            else if (index == 2) {
                $("#litab3").removeClass("active");
                $("#litab4").addClass("active");
            }
            else if (index == 3) {
                $("#litab4").removeClass("active");
                $("#litab5").addClass("active");
            }

        }
        function tabControl() {
            $("#empRegp").removeClass("active");
            $("#empRegp").removeClass("in");
            $("#empRegp3").addClass("active");
            $("#empRegp3").addClass("in");
        }

        function tabControl2() {
            $("#empRegp").removeClass("active");
            $("#empRegp").removeClass("in");
            $("#empRegp2").addClass("active");
            $("#empRegp2").addClass("in");
        }
        function tabControl33() {
            $("#empRegp").removeClass("active");
            $("#empRegp").removeClass("in");
            $("#empRegp1").addClass("active");
            $("#empRegp1").addClass("in");
        }
         function tabControl4() { //others
            $("#empRegp").removeClass("active");
            $("#empRegp").removeClass("in");
            $("#empRegp4").addClass("active");
            $("#empRegp4").addClass("in");
        }
        //$(document).ready(function () {
        //    var i = 0;
        //    $("#add_row").click(function () {
        //        $('#addr' + i).html("<td>" + (i + 1) + "</td><td><input name='insName" + i + "' type='text' placeholder='' class='form-control input-md'  /> </td><td><select type='text' name='edesignation" + i + "' class='form-control'><option name='vicep" + i + "' value='VicePrincipal'>Vice Principal</option><option name='AssTeacher" + i + "' value='asstteacher'>Asst. Teacher</option></select></td><td><input  name='date" + i + "' type='text' placeholder=''  class='form-control input-md'></td><td><input  name='eduration" + i + "' type='text' placeholder=''  class='form-control input-md'></td><td><input  name='eduration" + i + "' type='text' placeholder=''  class='form-control input-md'></td>");

        //        $('#MainContent_tab_logic').append('<tr id="addr' + (i + 1) + '"></tr>');
        //        i++;
        //    });
        //    $("#delete_row").click(function () {
        //        if (i > 1) {
        //            $("#addr" + (i - 1)).html('');
        //            i--;
        //        }
        //    });

        //});

        //$(document).ready(function () {
        //    var i = 0;
        //    $("#eadd_row").click(function () {
        //        $('#eaddr' + i).html("<td>" + (i + 1) + "</td><td><input name='insName" + i + "' type='text' placeholder='' class='form-control input-md'  /> </td><td><select type='text' name='edesignation" + i + "' class='form-control'><option name='vicep" + i + "' value='VicePrincipal'>Vice Principal</option><option name='AssTeacher" + i + "' value='asstteacher'>Asst. Teacher</option></select></td><td><input  name='date" + i + "' type='text' placeholder=''  class='form-control input-md'></td><td><input  name='eduration" + i + "' type='text' placeholder=''  class='form-control input-md'></td>");

        //        $('#MainContent_etab_logic').append('<tr id="eaddr' + (i + 1) + '"></tr>');
        //        i++;
        //    });
        //    $("#edelete_row").click(function () {
        //        if (i > 1) {
        //            $("#eaddr" + (i - 1)).html('');
        //            i--;
        //        }
        //    });

        //});





        function previewFile() {
            var preview = document.querySelector('#<%=imgProfile.ClientID %>');
            var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function previewEmpSignFile() {
            var preview = document.querySelector('#<%=EmpSignimgProfile.ClientID %>');
            var file = document.querySelector('#<%=FileUpload2.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
        function validateInputs() {
            try {
               <%-- if ($('#<%=rblEmpType.ClientID%> radio:selected').val() == "1") {
                    alert("YES");
                    if (validateText('txtTCodeNo', 1, 100, 'Enter Teacher Code') == false) return false;                    
                }--%>
                if (validateText('txtE_CardNo', 1, 20, 'Enter a Card number') == false) return false;
                if (validateText('txtE_JoiningDate', 1, 20, 'Enter a Joining date') == false) return false;
                if (validateText('txtE_Name', 1, 50, 'Enter Employee Name') == false) return false;
                if (validateText('txtE_FathersName', 1, 200, 'Enter Father Name') == false) return false;
                if (validateCombo('dlDepartments', "", 'Select a Department Name') == false) return false;
                if (validateCombo('dlDesignation', "", 'Select Designation Name') == false) return false;
                if (validateText('txtE_Mobile', 1, 100, 'Enter Mobile Number') == false) return false;
                if (validateText('txtE_Birthday', 1, 100, 'Enter Date of Birth') == false) return false;
                //if (validateText('txtE_PresentAddress', 1, 100, 'Enter Present Address') == false) return false;
                //if (validateText('txtE_ParmanentAddress', 1, 100, 'Enter Parmanent Address') == false) return false;
                if (validateCombo('ddlShift', "0", 'Select Shift Name') == false) return false;
                return true;
            }
            catch (e) {
                showMessage("Validation failed : " + e.message, 'error');
                console.log(e.message);
                return false;
            }
        }
        function updateSuccess() {
            showMessage('Update successfully', 'success');
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
        }
    </script>

</asp:Content>
