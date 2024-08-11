<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="OldStudentEntry.aspx.cs" Inherits="DS.UI.Academic.Students.OldStudentEntry" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../../../../Styles/AdminssionStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Styles/jquery-ui-datepekar.css" rel="stylesheet" />
    <link href="../../../../Styles/popupStyle.css" rel="stylesheet" />--%>
    <style type="text/css">
        .tgPanel{
            width: 100%;
        }
        .tbl-controlPanel{
            width: 100%;
            font-size:1em;
        }
        .tbl-controlPanel tr td{
            width: 35%;
        }        
        .tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(2n+1)
        {
            text-align: right;
            width: 15%;
            padding-right: 5px;
        }
        .controlLength{
            width: 100%;
        } 
        .tbl-controlPanel1{
            font-family: Calibri;            
            width: 98%;
            margin: 10px 0px;
            font-size:1em;
        }  
        .tbl-controlPanel1 tr td{
            width: 20%;
            padding: 3px 0;
        }
        .tbl-controlPanel1 td:first-child,
        .tbl-controlPanel1 td:nth-child(3),
        .tbl-controlPanel1 td:nth-child(5)
        {
            text-align: right;
            width: 12%;
            padding-right: 5px;            
        }         
        .tbl-controlPanel2{
            font-family: Calibri;            
            width: 98%;
            margin: 10px 0px;
            font-size:1em;
        }  
        .tbl-controlPanel2 tr td{
            width: 19%;
            padding: 3px 0;
        }
        .tbl-controlPanel2 td:first-child {
            width: 18%;
            text-align: right;
            padding-right: 5px;           
        }
        .tbl-controlPanel2 td:nth-child(3),
        .tbl-controlPanel2 td:nth-child(5)
        {
            text-align: right;
            width: 10%;
            padding-right: 5px;            
        }        
         
        .tgbutton{            
            padding: 10px 0;
            margin-left: 50%;
        }
        #MainContent_CalendarExtender1_daysTable tr td{
            padding: 0px;
            width: 0px;
        }
        #MainContent_CalendarExtender3_daysTable tr td{
            padding: 0px;
            width: 0px;
        }  
        #MainContent_CalendarExtender4_daysTable tr td{
            padding: 0px;
            width: 0px;
        } 
        #MainContent_CalendarExtender2_popupDiv,#MainContent_CalendarExtender4_popupDiv,#MainContent_CalendarExtender1_popupDiv{
            width:200px;
        }
        #MainContent_CalendarExtender2_body,#MainContent_CalendarExtender1_body,#MainContent_CalendarExtender4_body{
            height:144px;
        }
        .ajax__calendar_footer {
            height: auto!important;
        }
        .extraMargin {
            margin-right : 2px;
        }
       input[type="checkbox"]{
            margin: 5px;
        }
        .tgPanelHead {
            margin-bottom: 10px;
        }
        #imgProfile{
            float:none;
            margin-bottom:15px;
        }
        #txtIdCard{
            cursor:not-allowed;
        }
        #txtGuardianAddress {
            width:164%;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblstdId" Value="0" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdfAdmissionNo" Value="0" ClientIDMode="Static" runat="server" />
     <asp:HiddenField ID="hdfClassId" Value="0" ClientIDMode="Static" runat="server" />
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
                <li><a id="A2" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>
                <li class="active">Student Entry</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <%--STUDENT INFORMATION--%>

    <div class="row tgPanel">
        <div class="col-sm-12 tgPanelHead">Student Information</div>
        <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                <asp:AsyncPostBackTrigger ControlID="txtRoll" />
            </Triggers>
            <ContentTemplate>
                <div class="col-sm-10">
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Admission Form No<span class="required">*</span></label>
                              <div class="col-md-8">
                              <asp:TextBox  ID="txtAdmissionNo" TabIndex="1" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Date<span class="required">*</span></label>
                              <div class="col-md-8">
                              <asp:TextBox ID="txtAdmissionDate" ClientIDMode="Static" TabIndex="2" runat="server" CssClass="input controlLength"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtAdmissionDate">                                
                                </asp:CalendarExtender>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Student Name<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtStudentName" TabIndex="3" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                         <div class="group">
                              <label class="col-sm-4">Name Bangla</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtFullNameBn" TabIndex="4" style="font-family:SutonnyMJ;" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                             <div class="group">
                              <label class="col-sm-4">Shift<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="dlShift" TabIndex="5" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Class<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="ddlClass" TabIndex="7" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                          </div>
                         
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Batch<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="ddlBatch" TabIndex="6" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Group<span class="required">*</span></label>
                              <div class="col-md-8">
                              <asp:DropDownList ID="ddlGroup" TabIndex="7" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Section<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:DropDownList  ID="ddlSection" TabIndex="8" ClientIDMode="Static" runat="server" CssClass="input controlLength">
                                    </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Roll<span class="required">*</span> </label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtRoll" runat="server" TabIndex="9" OnTextChanged="txtRoll_TextChanged" AutoPostBack="true"  ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Gender<span class="required">*</span></label>
                              <div class="col-md-8">
                              <asp:DropDownList ID="ddlGender" runat="server" TabIndex="10" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Date of Birth</label>
                              <div class="col-md-8">
                              <asp:TextBox ID="txtDateOfBirth" runat="server" TabIndex="11" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MM-yyyy" TargetControlID="txtDateOfBirth">
                                </asp:CalendarExtender>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Religion</label>
                              <div class="col-md-8">
                              <asp:DropDownList ID="dlReligion" runat="server" TabIndex="12" ClientIDMode="Static" CssClass="input controlLength">
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
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Mobile</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="lblMobile" runat="server" Width="17%" Text="+88" CssClass="input text-danger text-center extraMargin" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtMobile" TabIndex="13" runat="server"  MaxLength="11" Width="81%" CssClass="input"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                    </div>
                    <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Admission Year</label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="ddlSession" TabIndex="14" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                     
                                    <asp:ListItem>2015</asp:ListItem>   
                                    <asp:ListItem>2016</asp:ListItem>
                                    <asp:ListItem Selected="True">2017</asp:ListItem>                                      
                                    <asp:ListItem>2018</asp:ListItem>                                       
                                    <asp:ListItem>2019</asp:ListItem>                                       
                                    <asp:ListItem>2020</asp:ListItem>                                       
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                          <div class="group">
                              <label class="col-sm-4">Blood Group</label>
                              <div class="col-md-8">
                              <asp:DropDownList ID="dlBloodGroup" TabIndex="15" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
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
                    </div>
                 <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Id No</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtIdCard" runat="server"  ClientIDMode="Static" CssClass="input controlLength"  disabled></asp:TextBox>
                              </div>
                          </div>
                       </div>
                      <div class="col-md-6">
                          <div class="group">
                              <label class="col-sm-4">Student Type<span class="required">*</span></label>
                              <div class="col-md-8">
                              <asp:DropDownList ID="ddlStdType" runat="server" TabIndex="16" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">                                   
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                       
                    </div>
                    
                    
                </div>
                 </ContentTemplate>
        </asp:UpdatePanel>
         <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
            <Triggers>            
            </Triggers>
            <ContentTemplate>
                <div class="col-sm-2">
                    <div style="float: left; width: 25%; margin: 10px 0; padding: 0px 25px">
                        <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" onclick=""  onchange="previewFile()" ClientIDMode="Static" />
                    </div>
                </div>          
            </ContentTemplate>
        </asp:UpdatePanel>        
        <div class="clearfix"></div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1"  UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkFather" />
            <asp:AsyncPostBackTrigger ControlID="chkMother" />
            <asp:AsyncPostBackTrigger ControlID="chkOther" />
        </Triggers>
        <ContentTemplate>
            <%--Parents Information--%>
            <div class="row tgPanel">
                <div class="col-sm-12 tgPanelHead">
                    Parents Information                   
                </div>
               <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Father&#39;s Name<span class="required">*</span></label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtFatherName" TabIndex="17" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		               <div class="group">
			                <label class="col-sm-4">Name Bangla</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtFatherNameBn" TabIndex="18" style="font-family:SutonnyMJ;" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                 <div class="group">
			                <label class="col-sm-4">Father&#39;s Occupation</label>
			                <div class="col-md-8">
				                <%--<asp:TextBox ID="txtFatherOccupation" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFatherOccupation" TabIndex="19" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Private Service Holder</asp:ListItem>
                                    <asp:ListItem>Government Service Holder</asp:ListItem>
                                    <asp:ListItem>Teacher</asp:ListItem>
                                    <asp:ListItem>Banker</asp:ListItem> 
                                    <asp:ListItem>Doctor</asp:ListItem>
                                    <asp:ListItem>Engineer</asp:ListItem>
                                    <asp:ListItem>Lawyer</asp:ListItem>
                                    <asp:ListItem>Journalist</asp:ListItem>
                                    <asp:ListItem>Immigrant</asp:ListItem>
                                    <asp:ListItem>Defense</asp:ListItem>
                                    <asp:ListItem>Police</asp:ListItem>
                                    <asp:ListItem>Farmer</asp:ListItem>
                                    <asp:ListItem Selected="True">Business</asp:ListItem>
                                    <asp:ListItem>Foreigner</asp:ListItem>
                                    <asp:ListItem>Retired</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>                                  
                                </asp:DropDownList>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Designation</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtFatherDesignation" TabIndex="19" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Organization</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtFatherOrganization" TabIndex="20" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Yearly Income</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtFatherYearlyIncome" TabIndex="21" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                Text="0" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Father's Email</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtFatherEmail" TabIndex="22" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <%--<label class="col-sm-4">Home Phone</label>--%>
                             <label class="col-sm-4">Father's Mobile</label>
			                <div class="col-md-8">
                                 <asp:TextBox ID="txtFM" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtFathersMobile" TabIndex="23" MaxLength="11" Width="80%" CssClass="input" ClientIDMode="Static"></asp:TextBox>

			                    <asp:TextBox ID="txtHP" Visible="false" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox Visible="false" runat="server" ID="txtHomePhone" Width="80%" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Father's Phone</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtFP" TabIndex="24" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		               <div class="group">
			                <label class="col-sm-4">Mother&#39;s Name<span class="required">*</span></label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMotherName" TabIndex="24" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Name Bangla</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMotherNameBn" TabIndex="25" style="font-family:SutonnyMJ;" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Mother&#39;s Occupation</label>
			                <div class="col-md-8">
				                <%--<asp:TextBox ID="txtMotherOccupation" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlMotherOccupation" TabIndex="26" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem Selected="True">Housewife</asp:ListItem>
                                    <asp:ListItem>Private Service Holder</asp:ListItem>
                                    <asp:ListItem>Government Service Holder</asp:ListItem> 
                                    <asp:ListItem>Teacher</asp:ListItem>
                                    <asp:ListItem>Banker</asp:ListItem>
                                    <asp:ListItem>Doctor</asp:ListItem>
                                    <asp:ListItem>Engineer</asp:ListItem>
                                    <asp:ListItem>Lawyer</asp:ListItem>
                                    <asp:ListItem>Journalist</asp:ListItem>
                                    <asp:ListItem>Immigrant</asp:ListItem>
                                    <asp:ListItem>Defense</asp:ListItem> 
                                    <asp:ListItem>Police</asp:ListItem>
                                    <asp:ListItem>Foreigner</asp:ListItem>
                                    <asp:ListItem>Retired</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>                                
                                </asp:DropDownList>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Designation</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtMotherDesignation" TabIndex="27" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Organization</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtMotherOrganization" TabIndex="28" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Yearly Income</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtMotherYearlyIncome" TabIndex="29" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                Text="0" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                
                </div>
                <div class="row tbl-controlPanel1">
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Mother's Email</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtMotherEmail" TabIndex="30" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                 <div class="group">
			                <label class="col-sm-4">Mother's Mobile</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMM" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtMothersMobile" TabIndex="31" MaxLength="11" Width="80%" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                    <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Mother's Phone</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtMP" TabIndex="32" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>

                </div>
               
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Guardian Information--%>
    <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkFather" />
            <asp:AsyncPostBackTrigger ControlID="chkMother" />
            <asp:AsyncPostBackTrigger ControlID="chkOther" />
        </Triggers>
        <ContentTemplate>
            <div runat="server" id="dviGuardian" class="tgPanel">
                <div class="col-sm-12">
                <div class="row tgPanelHead">
                <div class="col-sm-9 ">
                    Guardian Information
                </div>
                    <div class="col-sm-3" style="float: right">
                        <asp:CheckBox  runat="server" ID="chkFather" AutoPostBack="true" CssClass="chkBox"
                            OnCheckedChanged="chkFather_CheckedChanged" ClientIDMode="Static" Text=" Father ?" />
                         <asp:CheckBox runat="server" ID="chkMother" AutoPostBack="true" CssClass="chkBox"
                            OnCheckedChanged="chkMother_CheckedChanged" ClientIDMode="Static" Text="  Mother ?" />
                           <asp:CheckBox runat="server" ID="chkOther" AutoPostBack="true" CssClass="chkBox"
                            OnCheckedChanged="chkOther_CheckedChanged" ClientIDMode="Static" Text=" Other ?" />
                    </div>                   
                </div>
                    </div>
                <asp:Panel runat="server" ID="pnlGuardian">
                    <div class="row tbl-controlPanel1">
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Guardian Name<span class="required">*</span></label>
                              <div class="col-md-8">
                               <asp:TextBox ID="txtGuardianName" TabIndex="33" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                      <div class="col-md-4">
                         <div class="group">
                             <label class="col-sm-4">Relation <span class="required">*</span></label>
                             <div class="col-md-8">
                               <asp:DropDownList ID="ddlRelation" TabIndex="34" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Father</asp:ListItem>
                                    <asp:ListItem>Mother</asp:ListItem> 
                                    <asp:ListItem>Husband</asp:ListItem>
                                    <asp:ListItem>Uncle</asp:ListItem>
                                    <asp:ListItem>Aunt</asp:ListItem>
                                    <asp:ListItem>GrandFather</asp:ListItem>
                                    <asp:ListItem>GrandMother</asp:ListItem>
                                    <asp:ListItem>Brother</asp:ListItem>
                                    <asp:ListItem>Sister</asp:ListItem>
                                    <asp:ListItem>Cousin</asp:ListItem>
                                </asp:DropDownList>
                             </div>
                          </div>
                      </div>
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Mobile No<span class="required">*</span></label>
                              <div class="col-md-8">
                               <asp:TextBox ID="lblgdMobile" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center extraMargin" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtGurdianMobile" TabIndex="35" Width="78%" runat="server" MaxLength="11" CssClass="input controlLength" ClientIDMode="Static"
                                    onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                    </div>
                    <div class="row tbl-controlPanel1">
                        <div class="col-md-8">
                          <div class="group row" style="margin-left:-6px; margin-right:0">
                              <label class="col-sm-2">Guardian Address</label>
                              <div class="col-md-10">
                                <asp:TextBox ID="txtGuardianAddress" TabIndex="36" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                    </div>
                    
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPADistrict" />
             <asp:AsyncPostBackTrigger ControlID="ddlPAThana" />
        </Triggers>
        <ContentTemplate>
            <%--Permanent address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">Permanent address</div>
                <div class="row tbl-controlPanel1">
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Address</label>
                              <div class="col-md-8">
                               <asp:TextBox ID="txtPAVillage" runat="server" TabIndex="37" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                     <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">District</label>
                              <div class="col-md-8">
                                <asp:DropDownList  ID="ddlPADistrict" TabIndex="38" runat="server" AutoPostBack="True"
                                ClientIDMode="Static" CssClass="input controlLength"
                                OnSelectedIndexChanged="ddlPADistrict_SelectedIndexChanged">
                            </asp:DropDownList>
                              </div>
                          </div>
                      </div>                     
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Thana/Upazila</label>
                              <div class="col-md-8">
                               <asp:DropDownList  ID="ddlPAThana" TabIndex="39" runat="server" OnSelectedIndexChanged="ddlPAThana_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" CssClass="input controlLength">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>
                              </div>
                          </div>
                      </div>
                    </div>
                    <div class="row tbl-controlPanel1">
                        <div class="col-md-4">
                         <div class="group">
                             <label class="col-sm-4">Post Office</label>
                             <div class="col-md-8">                              
                                 <asp:DropDownList ID="ddlPAPostOffice" TabIndex="40" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>   
                             </div>
                          </div>
                      </div>
                    </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server"  ID="UpdatePanel3" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlTADistrict" />
             <asp:AsyncPostBackTrigger ControlID="ddlTAThana" />
            <asp:AsyncPostBackTrigger ControlID="chkSameAddress" />
        </Triggers>
        <ContentTemplate>
            <%--Present address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    Present address
                    <div style="float: right">
                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckedChanged" runat="server" ID="chkSameAddress" ClientIDMode="Static" />
                        <label for="chkSameAddress">Same</label>
                    </div>
                </div>
                <div class="row tbl-controlPanel1">
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Address</label>
                              <div class="col-md-8">
                               <asp:TextBox ID="txtTAVillage" TabIndex="41" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                      </div>
                     <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">District</label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="ddlTADistrict" TabIndex="42" runat="server" AutoPostBack="True" ClientIDMode="Static" CssClass="input controlLength"
                                OnSelectedIndexChanged="ddlTADistrict_SelectedIndexChanged">
                                <asp:ListItem>...Select...</asp:ListItem>
                            </asp:DropDownList>
                              </div>
                          </div>
                      </div>                     
                        <div class="col-md-4">
                          <div class="group">
                              <label class="col-sm-4">Thana/Upazila</label>
                              <div class="col-md-8">
                               <asp:DropDownList ID="ddlTAThana" TabIndex="43" runat="server" CssClass="input controlLength" OnSelectedIndexChanged="ddlTAThana_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>
                              </div>
                          </div>
                      </div>
                    </div>
                    <div class="row tbl-controlPanel1">
                        <div class="col-md-4">
                         <div class="group">
                             <label class="col-sm-4">Post Office</label>
                             <div class="col-md-8">
                                  <asp:DropDownList ID="ddlTAPostOffice" TabIndex="44" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>                               
                             </div>
                          </div>
                      </div>
                    </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
        </Triggers>
        <ContentTemplate>--%>
            <%--Other Information--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    SSC Information
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="chkNotApplicable" ClientIDMode="Static" />
                        <label for="chkNotApplicable">Not applicable</label>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Select Exam</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlExam" TabIndex="45" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem>SSC</asp:ListItem>
                                    <asp:ListItem>Dakhil</asp:ListItem>
                                    <asp:ListItem>Equivalent</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Board</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBoard" TabIndex="46" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Passing Year</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlPassingYear" TabIndex="47" runat="server" CssClass="input controlLength" ClientIDMode="Static">                 
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-md-4">  
                        <div class="group">
                            <label class="col-sm-4">Registraton</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtRegistration" TabIndex="48" CssClass="input controlLength" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">  
                        <div class="group">
                            <label class="col-sm-4">Roll</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtSSCRoll" TabIndex="49" CssClass="input controlLength" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">GPA</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtGpa" runat="server" TabIndex="50" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                           
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                        <label class="col-md-2" style="width:148px;padding-right:0">Previous School Name</label>
                        <div class="col-md-10" style="padding-right:0;width:1113px;">
                            <asp:TextBox ID="txtPreviousSchoolName" TabIndex="51"  runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                        </div>
                 </div>
            </div>
   <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>

  
            <div class="tgPanel">
                <div class="tgPanelHead">
                    Ride Information
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="CheckBox1" ClientIDMode="Static" />
                        <label for="chkNotApplicable">Not applicable</label>
                    </div>
                </div>

                <div class="row tbl-controlPanel2"> 
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Bus Name</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlBusName" TabIndex="52" runat="server" CssClass="input controlLength" ClientIDMode="Static">                               
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Location</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlLocation" TabIndex="53" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                               
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Bus Stand</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlPlace" TabIndex="54" runat="server" CssClass="input controlLength" ClientIDMode="Static">                                
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    
                </div>
                
             </div>
     <asp:UpdatePanel runat="server" ID="UpdatePanel4"  UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkTCFrom"  />
                             <asp:AsyncPostBackTrigger ControlID="chkTCTo"  />
                             <asp:AsyncPostBackTrigger ControlID="chkTCNotApplicable"  />

                        </Triggers>
                        <ContentTemplate>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    TC Information
                    <div style="float: right">
                        <asp:CheckBox runat="server"  ID="chkTCFrom" AutoPostBack="true" ClientIDMode="Static" OnCheckedChanged="chkTCFrom_CheckedChanged" />
                        <label for="chkTCFrom">From</label>
                    </div>
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="chkTCTo" AutoPostBack="true" ClientIDMode="Static" OnCheckedChanged="chkTCTo_CheckedChanged" />
                        <label for="chkTCTo">To</label>
                    </div>
                    <div style="float: right">
                        <asp:CheckBox runat="server" ID="chkTCNotApplicable" AutoPostBack="true" ClientIDMode="Static" OnCheckedChanged="chkTCNotApplicable_CheckedChanged" />
                        <label for="chkTCNotApplicable">Not applicable</label>
                    </div>
                </div>
            <div class="row tbl-controlPanel2"> 
                    <label class="col-md-1" style="padding-right:0;">College Name</label>
                    <div class="col-md-11" style="padding-left:55px;padding-right:30px">
                        <asp:TextBox ID="txtTCCollegeName" TabIndex="55" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                            onKeyUp=""></asp:TextBox>
                    </div>
            </div>
            <div class="row tbl-controlPanel2"> 
                <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Class</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlTCClass" TabIndex="56" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem Selected="True"></asp:ListItem>
                                <asp:ListItem>XI</asp:ListItem>
                                <asp:ListItem>X</asp:ListItem>
                                <asp:ListItem>XII</asp:ListItem>
                                </asp:DropDownList>
                           </div>
                      </div>
                 </div>
                <div class="col-md-4">
                    <div class="group">
                        <label class="col-sm-4">Semester</label>
                        <div class="col-md-8">
                            <asp:DropDownList ID="ddlTCSemister" TabIndex="57" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                            <asp:ListItem Selected="True"></asp:ListItem>
                            <asp:ListItem>1st Semester</asp:ListItem>
                            <asp:ListItem>2nd Semester</asp:ListItem>
                            <asp:ListItem>3rd Semester</asp:ListItem>
 <asp:ListItem> Pre - Test Exam</asp:ListItem>
 <asp:ListItem>Test Exam</asp:ListItem>
                        </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Date</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtTCDate" runat="server"  TabIndex="58" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MM-yyyy" TargetControlID="txtTCDate"></asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
                            </ContentTemplate>
         </asp:UpdatePanel>
   <div class="tgbutton">       
        <table class="tbl-controlPanel">
            <tr>
                <td>
                    <asp:Button ID="btnSave" TabIndex="59" runat="server" Text="Save" class="btn btn-primary" ClientIDMode="Static"
                        OnClientClick="return validateInputs();" OnClick="btnSave_Click" />                    
                </td>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />

                        </Triggers>
                        <ContentTemplate>
                            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" OnClick="btnClear_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>           
    </div>
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
        function initAll() {
            $('input').click(function () {
                setTimeout($('.datepicker').css('top', 'auto'), 25);
            });
        }
        function sameData() {
            $("txtTAVillage").val() = $("txtPAVillage").val();
        }
        $(document).ready(function () {
            $("#ddlPAThana").select2();
            $("#ddlPADistrict").select2();
            $("#ddlTAThana").select2();
            $("#ddlTADistrict").select2();
            $(document).on('keypress', 'input[type="text"]', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    var controlId = $(this).attr('id');

                    if (controlId == "txtAdmissionNo") setFocus("txtAdmissionDate");
                }
            });
        });
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
        }
        function saveSuccess() {
            showMessage('Save successfully', 'success');
        }
        function validateInputs() {
            try {
                console.log('validating inputs');
                if (validateText('txtAdmissionNo', 1, 20, 'Enter a admission number') == false) return false;
                if (validateText('txtAdmissionDate', 1, 20, 'Enter a admission date') == false) return false;
                if (validateText('txtStudentName', 1, 200, 'Enter a full name') == false) return false;
                if (validateCombo('ddlBatch', "0", 'Select a Batch') == false) return false;
                if (validateCombo('ddlClass', "0", 'Select a class') == false) return false;
                if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
                if (validateCombo('ddlSection', "0", 'Select Section Name') == false) return false;
                if (validateCombo('ddlGender', "0", 'Select a gender') == false) return false;
                if (validateText('txtRoll', 1, 200, 'Enter  Roll Number') == false) return false;
                if (validateCombo('ddlStdType', "0", 'Select Student Type') == false) return false;
                if (validateText('txtFatherName', 1, 200, 'Enter  father name') == false) return false;
                if (validateText('txtMotherName', 1, 150, 'Enter  mother name') == false) return false;
                if (validateText('txtGuardianName', 1, 100, 'Enter Guardian Name') == false) return false;
                if (validateText('txtGurdianMobile', 1, 15, 'Enter Guardian Mobile Number') == false) return false;

               
                if ($("#chkNotApplicable").is(':checked')) {
                }
                else {
                    if (validateText('txtPreviousSchoolName', 1, 200, 'Enter a previous school name') == false) return false;
                    if (validateText('ddlExam', 1, 100, 'Enter a preferred class') == false) return false;
                    if (validateText('txtGpa', 1, 100, 'Enter a P S C G PA') == false) return false;
                    if (validateText('ddlBoard', 1, 100, 'Enter a P S C board') == false) return false;
                    if (validateText('ddlPassingYear', 1, 100, 'Enter a P S C passing year') == false) return false;
                }
                return true;
            }
            catch (e) {
                showMessage("Validation failed : " + e.message, 'error');
                console.log(e.message);
                return false;
            }
        }
        function acceptValidCharacter(event) {
            alert("ol");
            // var a = $(this).val(String.fromCharCode(event.keyCode()));
            alert(event.fromCharCode(onkeyup.keyCode));
            try {
                alert(String.fromCharCode(event.keyCode()));
            }
            catch (e) {

            }
        }
        function load() {
            $("#ddlPAThana").select2();
            $("#ddlPADistrict").select2();
            $("#ddlTAThana").select2();
            $("#ddlTADistrict").select2();
        }
        $("#txtRoll").keyup(function (event) {
            var v = $("#txtRoll").val();
            $("#txtIdCard").val(v);

        });
        $("#ctl00$MainContent$txtRoll").keyup(function (event) {
            var v = $("#ctl00$MainContent$txtRoll").val();
            $("ctl00$MainContent$txtIdCard").val(v);
        });
    </script>
</asp:Content>


