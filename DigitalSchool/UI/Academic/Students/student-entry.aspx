<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="student-entry.aspx.cs" Inherits="DS.UI.Academic.Students.student_entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .admission-form{
        padding: 10px;
    }
    .adform-title {
        color: #000;
        text-align: center;
        font-size: 25px;
        margin-bottom: 20px;
        background: #18a952;
        padding: 6px;
        color: #fff;
    }
    .group-info-box {
        border: 1px solid #c1c1c1;
        overflow: hidden;
        padding: 20px 5px 5px;
    }
    .group-info-box .control-label{
        padding-top: 6px;
    }
    .group-info-box .group-title {
        margin-top: -34px;
        position: absolute;
        background: #fff;
        padding: 0px 10px;
    }
    span.chkBox input {
        margin: 6px;
        transform: scale(1.5);
    }
    span.chkBox label {
        font-size:16px;
    }
        .required {
            color:red;
        }
        .mt-150 {
            margin-top:150px;
        }
        .mbm-10 {
            margin-bottom:20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
     <section class="page-section">
         <div class="row">
             <div class="col-md-12">
                 <!--breadcrumbs start -->
                 <ul class="breadcrumb">
                     <li>
                         <%--<a id="A1" runat="server" href="~/Dashboard.aspx">--%>
                         <a runat="server" id="aDashboard">
                             <i class="fa fa-dashboard"></i>
                             Dashboard
                         </a>
                     </li>
                     <%--<li><a id="A2" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>--%>
                     <li><a id="aAcademicHome" runat="server">Academic Module</a></li>
                     <%--<li><a id="A3" runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>--%>
                     <li><a id="aStudentHome" runat="server">Student Module</a></li>
                     <li class="active">Student Entry</li>
                 </ul>
                 <!--breadcrumbs end -->
             </div>
         </div>
         <div runat="server" id="divContainer" class="">
             <div class="page-inner admission-form">
                 
                 <div class="row">
                     <div class="col-md-4">
                         <div class="row mt-150 mbm-10" style="display:none">
                             <label for="name" class="col-sm-4 control-label">Bank Receipt No.<strong class="required">*</strong></label>
                             <div class="col-sm-8">
                                 <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMoneyReceiptNo" class="form-control" placeholder="Enter Bank Money Receipt No"></asp:TextBox>
                             </div>
                         </div>
                     </div>
                     <div class="col-md-4"></div>
                     <div class="col-md-4">

                         <div class="group-info-box student-info">
                             <h4 class="group-title">Student Photo</h4>
                             <div class="row form-group">
                                 <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                     <Triggers>
                                         <asp:PostBackTrigger ControlID="btnSubmit" />
                                     </Triggers>
                                     <ContentTemplate>
                                         <div class="col-md-6">
                                         </div>
                                         <div class="col-md-6">
                                             <div class="sphoto-view" style="">
                                                 <p>
                                                     <asp:Image ID="imgProfile" class="profileImage" Style="width: 100px; height: 120px; margin-right: 15px" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" /></p>
                                             </div>
                                         </div>
                                         <div class="col-md-12">
                                             <div class="row form-group">
                                                 <label for="name" class="col-sm-5 control-label">Student Photo<strong class="required">*</strong></label>
                                                 <div class="col-sm-7">
                                                     <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                                                 </div>
                                             </div>
                                         </div>
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                             </div>
                         </div>
                     </div>
                 </div>
                 <br />

                 <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
                     <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                         <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                         <asp:AsyncPostBackTrigger ControlID="ddlParentsDistrict" />
                         <asp:AsyncPostBackTrigger ControlID="ddlParentsUpazila" />
                         <asp:AsyncPostBackTrigger ControlID="ddlPermanentDistrict" />
                         <asp:AsyncPostBackTrigger ControlID="ddlPermanentUpazila" />
                         <asp:AsyncPostBackTrigger ControlID="ddlPresentDistrict" />
                         <asp:AsyncPostBackTrigger ControlID="ddlPresentUpazila" />
                         <asp:AsyncPostBackTrigger ControlID="chkFather" />
                         <asp:AsyncPostBackTrigger ControlID="chkMother" />
                         <asp:AsyncPostBackTrigger ControlID="chkOther" />
                         <asp:AsyncPostBackTrigger ControlID="ckbSameAsPermanentAddress" />
                         <asp:AsyncPostBackTrigger ControlID="ckbSameAsParentsAddress" />
                         <asp:PostBackTrigger ControlID="btnSubmit" />
                         <asp:AsyncPostBackTrigger ControlID="btnClear" />
                         <asp:AsyncPostBackTrigger ControlID="ckbTCInfo" />

                     </Triggers>
                     <ContentTemplate>
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Student Information</h4>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Student Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentName" class="form-control" placeholder="Enter Student Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">নাম<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Gender<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlGender" class="form-control">
                                                 <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                 <asp:ListItem Value="Male">Male</asp:ListItem>
                                                 <asp:ListItem Value="Female">Female</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Date of Birth<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtDateOfBirth" class="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Religion<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlReligion" class="form-control">
                                                 <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                 <asp:ListItem>Islam</asp:ListItem>
                                                 <asp:ListItem>Hindu</asp:ListItem>
                                                 <asp:ListItem>Christian</asp:ListItem>
                                                 <asp:ListItem>Buddhist</asp:ListItem>
                                                 <asp:ListItem>Upozati</asp:ListItem>
                                                 <asp:ListItem>Others</asp:ListItem>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Blood Group</label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlBloodGroup" class="form-control">
                                                 <asp:ListItem Value="0">Unknown</asp:ListItem>
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

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <div class="input-group">
                                                 <span class="input-group-addon">+88</span>
                                                 <asp:TextBox runat="server" ClientIDMode="Static" ID="txtStudentMobile" class="form-control" placeholder="Enter Student Mobile No"></asp:TextBox>
                                             </div>

                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Shift<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlShift" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Year<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlYear" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Class<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlClass" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Group<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlGroup" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Section<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlSection" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Roll<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                           <asp:TextBox runat="server" ClientIDMode="Static" ID="txtRollNo" class="form-control" placeholder="Enter Roll No"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Admission Date<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                           <asp:TextBox runat="server" ClientIDMode="Static" ID="txtAdmissionDate" class="form-control" placeholder="Enter Admission Date"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Parents Information</h4>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Father's Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherName" class="form-control" placeholder="Enter Father's Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">পিতার নাম <strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>

                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <div class="input-group">
                                                 <span class="input-group-addon">+88</span>
                                                 <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherMobile" class="form-control" placeholder="Enter Father's Mobile"></asp:TextBox>
                                             </div>

                                         </div>
                                     </div>
                                 </div>

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Occupation<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherOccupation" class="form-control" placeholder="Enter Father's Occupation"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">পেশা<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtFatherOccupationBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>


                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Mother's Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherName" class="form-control" placeholder="Enter Mother's Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">মাতার নাম<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherNameBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>

                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Mobile</label>
                                         <div class="col-sm-8">
                                             <div class="input-group">
                                                 <span class="input-group-addon">+88</span>
                                                 <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherMobile" class="form-control" placeholder="Enter Mother's Mobile"></asp:TextBox>
                                             </div>

                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Occupation<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherOccupation" class="form-control" placeholder="Enter Mother's Occupation"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">পেশা<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMotherOccupationBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                               <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Address<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtParentsVillage" class="form-control" placeholder="Enter Parents Address"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">ঠিকানা<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtParentsVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">District<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlParentsDistrict_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlParentsUpazila_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlParentsPostOffice" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                           

                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Guardian Information</h4>

                             <div class="row form-group">
                                 <div class="col-md-8"></div>
                                 <div class="col-md-4">
                                     <div class="pull-right">
                                         <asp:CheckBox runat="server" ID="chkFather" AutoPostBack="true" CssClass="chkBox"
                                             ClientIDMode="Static" Text=" Father ?" OnCheckedChanged="chkFather_CheckedChanged" />
                                         <asp:CheckBox runat="server" ID="chkMother" AutoPostBack="true" CssClass="chkBox"
                                             ClientIDMode="Static" Text="  Mother ?" OnCheckedChanged="chkMother_CheckedChanged" />
                                         <asp:CheckBox runat="server" ID="chkOther" AutoPostBack="true" CssClass="chkBox"
                                             ClientIDMode="Static" Text=" Other ?" OnCheckedChanged="chkOther_CheckedChanged" />
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Guardian Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianName" class="form-control" placeholder="Enter Guardian Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Relation<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianRelation" class="form-control" placeholder="Enter Guardian Relation"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>

                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Mobile<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <div class="input-group">
                                                 <span class="input-group-addon">+88</span>
                                                 <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianMobile" class="form-control" placeholder="Enter Guardian Mobile"></asp:TextBox>
                                             </div>

                                         </div>
                                     </div>
                                 </div>

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Address<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtGuardianAddress" class="form-control" placeholder="Enter Guardian Address"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Permanent Address</h4>
                             <div class="row form-group">
                                 <div class="col-sm-9"></div>
                                 <div class="col-sm-3 ">
                                     <div class="pull-right">
                                         <asp:CheckBox runat="server" ID="ckbSameAsParentsAddress" AutoPostBack="true" CssClass="chkBox"
                                             ClientIDMode="Static" Text=" Same as Parents Address ? " OnCheckedChanged="ckbSameAsParentsAddress_CheckedChanged"  />

                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Address<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPermanentVillage" class="form-control" placeholder="Enter Present Address"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">ঠিকানা<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPermanentVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">District<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentDistrict_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPermanentUpazila_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPermanentPostOffice" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>

                             </div>
                             

                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Present Address</h4>
                             <div class="row form-group">
                                 <div class="col-sm-9"></div>
                                 <div class="col-sm-3 ">
                                     <div class="pull-right">
                                         <asp:CheckBox runat="server" ID="ckbSameAsPermanentAddress" AutoPostBack="true" CssClass="chkBox"
                                             ClientIDMode="Static" Text=" Same as Permanent Address ? " OnCheckedChanged="ckbSameAsPermanentAddress_CheckedChanged" />

                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Address<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPresentVillage" class="form-control" placeholder="Enter Present Address"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">ঠিকানা<strong class="required"></strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPresentVillageBn" class="form-control" placeholder="বাংলায়"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">District <strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentDistrict" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentDistrict_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Upazila<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentUpazila" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentUpazila_SelectedIndexChanged"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Post Office<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPresentPostOffice" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                             </div>                             
                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">Previous Institute Info</h4>
                              <div class="row form-group">
                                 <div class="col-sm-9"></div>
                                 <div class="col-sm-3 ">
                                     <div class="pull-right">
                                         <asp:CheckBox runat="server" ID="ckbPreviousInstituteInfo" CssClass="chkBox"
                                             ClientIDMode="Static" Text="Not Applicable" />
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Institute Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamSchoolName" class="form-control" placeholder="Enter Institute Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Board<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamBoard" class="form-control"></asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Passing Year<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlPreviousExamPassingYear" class="form-control">
                                                 <%--<asp:ListItem Value="0">...Select...</asp:ListItem>
                                                 <asp:ListItem Value="2020">2020</asp:ListItem>
                                                 <asp:ListItem Value="2019">2019</asp:ListItem>
                                                 <asp:ListItem Value="2018">2018</asp:ListItem>
                                                 <asp:ListItem Value="2017">2017</asp:ListItem>
                                                 <asp:ListItem Value="2016">2016</asp:ListItem>
                                                 <asp:ListItem Value="2015">2015</asp:ListItem>--%>
                                             </asp:DropDownList>
                                         </div>
                                     </div>
                                 </div>

                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Registration No<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRegistrationNo" class="form-control" placeholder="Enter Registration No"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Roll No<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamRollNo" class="form-control" placeholder="Enter Roll No"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">GPA<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtPreviousExamGPA" class="form-control" placeholder="Enter GPA"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>

                             </div>

                         </div>
                         <br />
                         <div class="group-info-box student-info">
                             <h4 class="group-title">TC Information</h4>
                             <div class="row form-group">
                                 <div class="col-sm-9"></div>
                                 <div class="col-sm-3 ">
                                     <div class="pull-right">
                                         <asp:CheckBox runat="server" ID="ckbTCInfo" CssClass="chkBox"
                                             ClientIDMode="Static" AutoPostBack="true" Checked="true" Text="Not Applicable" OnCheckedChanged="ckbTCInfo_CheckedChanged" />
                                     </div>
                                 </div>
                             </div>
                             <div class="row form-group">
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="name" class="col-sm-4 control-label">Institute Name<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static" ID="txtTCCollegeName" class="form-control" Enabled="false" placeholder="Enter Institute Name"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="col-md-4">
                                     <div class="row">
                                         <label for="namebn" class="col-sm-4 control-label">Date<strong class="required">*</strong></label>
                                         <div class="col-sm-8">
                                             <asp:TextBox runat="server" ClientIDMode="Static"  Enabled="false" ID="txtTCDate" class="form-control" placeholder="dd-MM-yyyy"></asp:TextBox>
                                         </div>
                                     </div>
                                 </div>
                             </div>

                         </div>
                         <br />


                         <div class="row">
                             <div class="col-md-12">
                                 <div class="text-right">
                                     <p style="float: right; margin-left: 10px;">
                                         <asp:Button runat="server" CssClass="btn btn-primary" ClientIDMode="Static" ID="btnSubmit" Text="Save" OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                     </p>
                                     <p>
                                         <asp:Button runat="server" CssClass="btn btn-default" ClientIDMode="Static" ID="btnClear" Text="Clear" OnClick="btnClear_Click" />
                                     </p>
                                 </div>
                             </div>
                         </div>
                     </ContentTemplate>
                 </asp:UpdatePanel>
                 
             </div>
         </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
      <script type="text/javascript">
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
          function printDiv() {

            var divToPrint = document.getElementById('page-wrapper1');

            var newWin = window.open('', 'Print-Window');
            newWin.document.open();
            newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
            setTimeout(function () { newWin.close(); }, 10);

        }
        function validateInputs() {
            try {
                
                //  if (validateText('txtMoneyReceiptNo', 1, 50, 'Enter valid Money Receipt No') == false) return false;
        
                if ( $('#btnSubmit').val()=="Save" && document.getElementById("FileUpload1").files.length == 0) {
                   showMessage('Select Student Photo', 'error');return false;
                   }
                
                if (validateText('txtStudentName', 1, 100, 'Enter valid Student Name') == false) return false;
               // if (validateText('txtStudentNameBn', 1, 100, 'Enter valid Student Name in Bengali') == false) return false;               
                if (validateCombo('ddlGender', "0", 'Select Gender') == false) return false;
                if (validateText('txtDateOfBirth', 10, 10, 'Enter valid Date of Birth') == false) return false;
                if (validateCombo('ddlReligion', "0", 'Select Religion') == false) return false;
                if (validateText('txtStudentMobile', 11, 11, 'Enter valid Mobile') == false) return false;
                if (validateCombo('ddlShift', "0", 'Select a Shift') == false) return false;
                if (validateCombo('ddlYear', "0", 'Select Year') == false) return false;
                if (validateCombo('ddlClass', "0", 'Select Class') == false) return false;
                if (validateCombo('ddlGroup', "0", 'Select Group') == false) return false;                
                if (validateCombo('ddlSection', "0", 'Select Section') == false) return false;
                 if (validateText('txtRollNo', 1, 100, 'Enter valid Roll No.') == false) return false;
                 if (validateText('txtAdmissionDate', 1, 100, 'Enter valid Admission Date') == false) return false;

                if (validateText('txtFatherName', 1, 100, 'Enter Father\'s Name') == false) return false;
               // if (validateText('txtFatherNameBn', 1, 100, 'Enter Father\'s Name in Bengali') == false) return false;
                if (validateText('txtFatherMobile', 1, 100, 'Enter Father\'s Mobile') == false) return false;
                if (validateText('txtFatherOccupation', 1, 100, 'Enter Father\'s Occupation') == false) return false;
               // if (validateText('txtFatherOccupationBn', 1, 100, 'Enter Father\'s Occupation in Bengali') == false) return false;
                if (validateText('txtMotherName', 1, 100, 'Enter Mother\'s Name') == false) return false;
               // if (validateText('txtMotherNameBn', 1, 100, 'Enter Mother\'s Name in Bengali') == false) return false;
               // if (validateText('txtMotherOccupation', 1, 100, 'Enter Mother\'s Occupation') == false) return false;
               // if (validateText('txtMotherOccupationBn', 1, 100, 'Enter Mother\'s Occupation in Bengali') == false) return false;

                if (validateText('txtParentsVillage', 1, 100, 'Enter Parents Address') == false) return false;
               // if (validateText('txtParentsVillageBn', 1, 100, 'Enter a Village in Bengali for Parents Address') == false) return false;
                if (validateCombo('ddlParentsDistrict', "0", 'Select a District for Parents Address') == false) return false; 
                if (validateCombo('ddlParentsUpazila', "0", 'Select a Upazila for Parents Address') == false) return false; 
                if (validateCombo('ddlParentsPostOffice', "0", 'Select a Post Office for Parents Address') == false) return false;                 

                if (validateText('txtGuardianName', 1, 100, 'Enter Guardian Name') == false) return false;
                if (validateText('txtGuardianRelation', 1, 100, 'Enter Guardian Relation') == false) return false;
                if (validateText('txtGuardianMobile', 11, 11, 'Enter Guardian Mobile') == false) return false;
                if ($("#txtStudentMobile").val() == $("#txtGuardianMobile").val()) {                    
                     showMessage('Student Mobile and Guardian Mobile No can\'t be the same', 'error');return false;
                }
                if (validateText('txtGuardianAddress', 1, 200, 'Enter Guardian Address') == false) return false;                

                if (validateText('txtPermanentVillage', 1, 100, 'Enter Permanent Address') == false) return false;
               // if (validateText('txtPermanentVillageBn', 1, 100, 'Enter a Village in Bengali for Permanent Address') == false) return false;
                if (validateCombo('ddlPermanentDistrict', "0", 'Select a District for Permanent Address') == false) return false; 
                if (validateCombo('ddlPermanentUpazila', "0", 'Select a Upazila for Permanent Address') == false) return false; 
                if (validateCombo('ddlPermanentPostOffice', "0", 'Select a Post Office for Permanent Address') == false) return false; 
                
                if (validateText('txtPresentVillage', 1, 100, 'Enter Present Address') == false) return false;
               // if (validateText('txtPresentVillageBn', 1, 100, 'Enter a Village in Bengali for Present Address') == false) return false;
                if (validateCombo('ddlPresentDistrict', "0", 'Select a District for Present Address') == false) return false; 
                if (validateCombo('ddlPresentUpazila', "0", 'Select a Upazila for Present Address') == false) return false; 
                if (validateCombo('ddlPresentPostOffice', "0", 'Select a Post Office for Present Address') == false) return false; 
                

                if ($("#ckbPreviousInstituteInfo").is(':checked')) {
                }
                else {
                    if (validateText('txtPreviousExamSchoolName', 1, 150, 'Enter Previous Institute Name') == false) return false;
                    if (validateCombo('ddlPreviousExamBoard', "0", 'Select Education Board') == false) return false; 
                    if (validateCombo('ddlPreviousExamPassingYear', "0", 'Select Passing Year') == false) return false; 
                    if (validateText('txtPreviousExamRegistrationNo', 1, 20, 'Enter Registration No') == false) return false;
                    if (validateText('txtPreviousExamRollNo', 1, 20, 'Enter Previous Exam/Class Roll No') == false) return false;
                    if (validateText('txtPreviousExamGPA', 1,4, 'Enter Previous Exam GPA') == false) return false;
                }
                 if ($("#ckbTCInfo").is(':checked')) {
                }
                else {
                    if (validateText('txtTCCollegeName', 1, 150, 'Enter Institute Name for TC') == false) return false;
                    if (validateText('txtTCDate', 1, 150, 'Enter TC Date') == false) return false;
                }
                
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
