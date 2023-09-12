<%@ Page Title="Student Admission" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdAdmission.aspx.cs" Inherits="DS.UI.Academics.Students.StdAdmission" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../../../../Styles/AdminssionStyle.css" rel="stylesheet" type="text/css" />
    <link href="../../../../Styles/jquery-ui-datepekar.css" rel="stylesheet" />
    <link href="../../../../Styles/popupStyle.css" rel="stylesheet" />--%>
    <style type="text/css">
        .tgPanel{
            width: 100%;
        }

        .tgPanelHead{background: #23282c none repeat scroll 0 0;
        color: white;
        font-family: "Open Sans",sans-serif;
        font-size: 12px;
        font-weight: bold;
        overflow: hidden;
        padding: 10px 0;
        text-align: left;}

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
            margin-left: 46%;
        }       
        .extraMargin {
            margin-right : 2px;
        }
       input[type="checkbox"]{
            margin: 5px;
        }
        .check {
            float:right;
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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>
                <li class="active">Student Admission Form</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <%--STUDENT INFORMATION--%>
    <div class="tgPanel">
        <div class="tgPanelHead">
            <div class="col-sm-12">
                Student Information
            </div>
          </div>
        <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
            </Triggers>
            <ContentTemplate>
                <div>
                    <div class="col-sm-10">
                      <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Admission Form No<span class="required">*</span></label>
                              <div class="col-md-8">
                                    <asp:TextBox ID="txtAdmissionNo" TabIndex="0" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="F1" runat="server" FilterType="Numbers" 
                                TargetControlID="txtAdmissionNo" ValidChars=""></asp:FilteredTextBoxExtender>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Date<span class="required">*</span></label>
                              <div class="col-md-8">
                                    <asp:TextBox ID="txtAdmissionDate" ClientIDMode="Static" TabIndex="1" runat="server" CssClass="input controlLength"></asp:TextBox>
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
                                <asp:TextBox ID="txtStudentName" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Name Bangla</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtFullNameBn" style="font-family:SutonnyMJ;" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Class<span class="required">*</span></label>
                              <div class="col-md-8">
                                    <asp:DropDownList ID="ddlClass" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Group<span class="required">*</span></label>
                              <div class="col-md-8">
                                    <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Shift<span class="required">*</span></label>
                              <div class="col-md-8">
                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                          <div class="group">
                              <label class="col-sm-4">Section</label>
                              <div class="col-md-8">
                                <asp:DropDownList Enabled="false" ID="ddlSection" ClientIDMode="Static" runat="server" CssClass="input controlLength">
                                    </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Gender<span class="required">*</span></label>
                              <div class="col-md-8">
                                    <asp:DropDownList ID="ddlGender" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Roll</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                          <div class="group">
                              <label class="col-sm-4">Date of Birth</label>
                              <div class="col-md-8">
                                     <asp:TextBox ID="txtDateOfBirth" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Format="dd-MM-yyyy" TargetControlID="txtDateOfBirth">
                                </asp:CalendarExtender>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Religion</label>
                              <div class="col-md-8">
                                 <asp:DropDownList ID="dlReligion" runat="server" ClientIDMode="Static" CssClass="input controlLength">
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
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Mobile</label>
                              <div class="col-md-8">
                                    <asp:TextBox ID="lblMobile" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center extraMargin" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                <asp:TextBox ID="txtMobile" runat="server"  MaxLength="11" Width="80%" CssClass="input"></asp:TextBox>
                              </div>
                          </div>
                       </div>
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Admission Year</label>
                              <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSession" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>2001</asp:ListItem>
                                    <asp:ListItem>2002</asp:ListItem>
                                    <asp:ListItem>2003</asp:ListItem>
                                    <asp:ListItem>2004</asp:ListItem>
                                    <asp:ListItem>2005</asp:ListItem>
                                    <asp:ListItem>2006</asp:ListItem>
                                    <asp:ListItem>2007</asp:ListItem>
                                    <asp:ListItem>2008</asp:ListItem>
                                    <asp:ListItem>2009</asp:ListItem>
                                    <asp:ListItem>2010</asp:ListItem>
                                    <asp:ListItem>2011</asp:ListItem>
                                    <asp:ListItem>2012</asp:ListItem>
                                    <asp:ListItem>2013</asp:ListItem>
                                    <asp:ListItem>2014</asp:ListItem>
                                </asp:DropDownList>
                              </div>
                          </div>
                       </div>
                      </div>
                        <div class="row tbl-controlPanel">
                        <div class="col-md-6">
                           <div class="group">
                              <label class="col-sm-4">Blood Group</label>
                              <div class="col-md-8">
                                    <asp:DropDownList ID="dlBloodGroup" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
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
                            <%-- <div class="col-md-6">
                            <div class="group">
                              <label class="col-sm-4">Id Card</label>
                              <div class="col-md-8">
                                <asp:TextBox ID="TextBox1" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                              </div>
                          </div>
                       </div>--%>
                       
                      </div>
                    </div>
                    <div class="col-sm-2">
                         <div style="float: left; width: 25%; margin: 10px 0; padding: 0px 25px">
                            <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                            <br />
                            <asp:FileUpload ID="FileUpload1" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" />
                        </div>
                    </div>
                   
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
       
        <div class="clearfix"></div>
    </div>

    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="chkFather" />
            <asp:AsyncPostBackTrigger ControlID="chkMother" />
            <asp:AsyncPostBackTrigger ControlID="chkOther" />
        </Triggers>
        <ContentTemplate>
            <%--Parents Information--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    <div class="col-sm-12">
                      Parents Information
                   </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Father&#39;s Name<span class="required">*</span></label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtFatherName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		               <div class="group">
			                <label class="col-sm-4">Name Bangla</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtFatherNameBn" style="font-family:SutonnyMJ;" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                 <div class="group">
			                <label class="col-sm-4">Father&#39;s Occupation</label>
			                <div class="col-md-8">
				                <%--<asp:TextBox ID="txtFatherOccupation" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlFatherOccupation" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Job</asp:ListItem>
                                    <asp:ListItem>Farmer</asp:ListItem>
                                    <asp:ListItem>Business</asp:ListItem>
                                    <asp:ListItem>Worker</asp:ListItem>                                   
                                </asp:DropDownList>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Yearly Income</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtFatherYearlyIncome" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                Text="0" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		               <div class="group">
			                <label class="col-sm-4">Mother&#39;s Name<span class="required">*</span></label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMotherName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Name Bangla</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMotherNameBn" style="font-family:SutonnyMJ;" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Mother&#39;s Occupation</label>
			                <div class="col-md-8">
				                <%--<asp:TextBox ID="txtMotherOccupation" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>--%>
                                  <asp:DropDownList ID="ddlMotherOccupation" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>House Wife</asp:ListItem>
                                    <asp:ListItem>Job</asp:ListItem>
                                    <asp:ListItem>Business</asp:ListItem>                                 
                                </asp:DropDownList>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Yearly Income</label>
			                <div class="col-md-8">
			                    <asp:TextBox ID="txtMotherYearlyIncome" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                Text="0" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
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
                            <asp:TextBox runat="server" ID="txtFathersMobile" MaxLength="11" Width="80%" CssClass="input" ClientIDMode="Static"></asp:TextBox>

			                    <asp:TextBox ID="txtHP" Visible="false" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox Visible="false" runat="server" ID="txtHomePhone" Width="80%" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                 <div class="group">
			                <label class="col-sm-4">Mother's Mobile</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtMM" runat="server" Width="18%" Text="+88" CssClass="input text-danger text-center" 
                                    ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                            <asp:TextBox runat="server" ID="txtMothersMobile" MaxLength="11" Width="80%" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Father's Email</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtFatherEmail" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Mother's Email</label>
			                <div class="col-md-8">
				                <asp:TextBox runat="server" ID="txtMotherEmail" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
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
                <div class="tgPanelHead">
                    <div class="col-xs-12 col-sm-9">
                        Guardian Information
                    </div>
                    
                    <div class="col-xs-12 col-sm-3" style="float: right">
                     <div class="row">
                       <div class="col-sm-12 check">    
                         <asp:CheckBox runat="server" ID="chkFather" AutoPostBack="true" CssClass="chkBox" OnCheckedChanged="chkFather_CheckedChanged" ClientIDMode="Static" Text=" Father ?" />
                         <asp:CheckBox runat="server" ID="chkMother" AutoPostBack="true" CssClass="chkBox" OnCheckedChanged="chkMother_CheckedChanged" ClientIDMode="Static" Text="  Mother ?" />
                         <asp:CheckBox runat="server" ID="chkOther" AutoPostBack="true" CssClass="chkBox" OnCheckedChanged="chkOther_CheckedChanged" ClientIDMode="Static" Text=" Other ?" />
                       </div>
                     </div> 
                    </div>                   
                </div>
                <asp:Panel runat="server" ID="pnlGuardian">
                    <div class="row tbl-controlPanel1">
	                    <div class="col-md-4">
		                    <div class="group">
			                    <label class="col-sm-4">Guardian Name<span class="required">*</span> </label>
			                    <div class="col-md-8">
				                    <asp:TextBox ID="txtGuardianName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                    </div>
		                    </div>
	                    </div>
	                    <div class="col-md-4">
		                    <div class="group">
			                    <label class="col-sm-4">Relation <span class="required">*</span></label>
			                    <div class="col-md-8">
				                    <asp:DropDownList ID="ddlRelation" runat="server" class="ddl-box" ClientIDMode="Static" CssClass="input controlLength">
                                    <asp:ListItem>Father</asp:ListItem>
                                    <asp:ListItem>Mother</asp:ListItem>
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
                                <asp:TextBox ID="txtGurdianMobile" Width="78%" runat="server" MaxLength="11" CssClass="input controlLength" ClientIDMode="Static"
                                    onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
			                    </div>
		                    </div>
	                    </div>
                    </div>
                    <div class="row tbl-controlPanel1">
	                    <div class="col-md-4">
		                    <div class="group">
			                    <label class="col-sm-4">Guardian Address </label>
			                    <div class="col-md-8">
				                    <asp:TextBox ID="txtGuardianAddress" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
			                    </div>
		                    </div>
	                    </div>
	                    <div class="col-md-4"></div>
	                    <div class="col-md-4"></div>
                    </div>
                    
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPADistrict" />
        </Triggers>
        <ContentTemplate>
            <%--Permanent address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    <div class="col-sm-12">
                        Permanent address
                    </div>
                    </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Village</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtPAVillage" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Post Office</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtPAPostOffice" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Thana/Upazila </label>
			                <div class="col-md-8">
			                    
				                <asp:DropDownList ID="ddlPAThana" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">District</label>
			                <div class="col-md-8">
                                <asp:DropDownList ID="ddlPADistrict" runat="server" AutoPostBack="True"
                                ClientIDMode="Static" CssClass="input controlLength"
                                OnSelectedIndexChanged="ddlPADistrict_SelectedIndexChanged">
                            </asp:DropDownList>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                
	                </div>
	                <div class="col-md-4">
		                
	                </div>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlTADistrict" />
            <asp:AsyncPostBackTrigger ControlID="chkSameAddress" />
        </Triggers>
        <ContentTemplate>
            <%--Present address--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    <div class="col-sm-11">
                         Present address
                    </div>
                    <div class="col-sm-1" style="float: right">
                        <asp:CheckBox AutoPostBack="true" OnCheckedChanged="chkSameAddress_CheckedChanged" runat="server" ID="chkSameAddress" ClientIDMode="Static" />
                        <label for="chkSameAddress">Same</label>
                    </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Village</label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtTAVillage" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Post Office </label>
			                <div class="col-md-8">
				                <asp:TextBox ID="txtTAPostOffice" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">Thana/Upazila</label>
			                <div class="col-md-8">
			                    <asp:DropDownList ID="ddlTAThana" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem Value="0" Selected="True">...Select...</asp:ListItem>
                            </asp:DropDownList>
			                </div>
		                </div>
	                </div>
                </div>
                <div class="row tbl-controlPanel1">
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4">District</label>
			                <div class="col-md-8">
				                <asp:DropDownList ID="ddlTADistrict" runat="server" AutoPostBack="True" ClientIDMode="Static" CssClass="input controlLength"
                                OnSelectedIndexChanged="ddlTADistrict_SelectedIndexChanged">
                                <asp:ListItem>...Select...</asp:ListItem>
                                 </asp:DropDownList>
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4"></label>
			                <div class="col-md-8">
				 
			                </div>
		                </div>
	                </div>
	                <div class="col-md-4">
		                <div class="group">
			                <label class="col-sm-4"></label>
			                <div class="col-md-8">
			   
			                </div>
		                </div>
	                </div>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlClass" />
        </Triggers>
        <ContentTemplate>
            <%--Other Information--%>
            <div class="tgPanel">
                <div class="tgPanelHead">
                    <div class="col-sm-10">
                        Other Information
                     </div>
                    <div class="col-sm-2" style="float: right">
                        <asp:CheckBox runat="server" ID="chkNotApplicable" ClientIDMode="Static" />
                        <label for="chkNotApplicable">Not applicable</label>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Select Exam</label>
                            <div class="col-md-8">
                                 <asp:DropDownList ID="ddlExam" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem>P.S.C</asp:ListItem>
                                <asp:ListItem>J.S.C</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Roll</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtPSCRoll" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Passing Year</label>
                            <div class="col-md-8">
                               <asp:DropDownList ID="ddlPassingYear" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                <asp:ListItem>2013</asp:ListItem>
                                <asp:ListItem>2014</asp:ListItem>
                                <asp:ListItem>2015</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">GPA</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtGpa" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Board</label>
                            <div class="col-md-8">
                                 <asp:DropDownList ID="ddlBoard" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                            </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="group">
                            <label class="col-sm-4">Date</label>
                            <div class="col-md-8">
                               <asp:TextBox ID="txtTrDate" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy" TargetControlID="txtTrDate"></asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-sm-12 ">
                        <label class="col-sm-2">Previous School Name</label>
                        <div class="col-md-10">
                             <asp:TextBox ID="txtPreviousSchoolName" runat="server" CssClass="input controlLength" ClientIDMode="Static"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-sm-12 ">
                        <label class="col-sm-2">Registraton</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtRegistration" CssClass="input controlLength" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-sm-12 ">
                        <label class="col-sm-2">Transfer Certificate No(if any)</label>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtTransferCNo" runat="server" CssClass="input controlLength" ClientIDMode="Static"
                                onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row tbl-controlPanel2">
                    <div class="col-sm-12 ">
                        <label class="col-sm-2">That class would be admission</label>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlThatClass" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                            </asp:DropDownList>
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
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" ClientIDMode="Static"
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
                if (validateCombo('ddlClass',"0", 'Select a class') == false) return false;
                if (validateCombo('ddlGender',"0", 'Select a gender') == false) return false;
                if (validateCombo('dlShift', "0",'Select a Shift') == false) return false;
                if (validateText('txtFatherName', 1, 200, 'Enter  father name') == false) return false;
                if (validateText('txtMotherName', 1, 150, 'Enter  mother name') == false) return false;
                if (validateText('txtGuardianName', 1, 100, 'Enter Guardian Name') == false) return false;
                if (validateText('txtGurdianMobile', 1, 15, 'Enter Guardian Mobile Number') == false) return false;              
              

                if ($("#chkNotApplicable").is(':checked')) {
                }
                else {
                    if (validateText('txtPreviousSchoolName', 1, 200, 'Enter a previous school name') == false) return false;
                    if (validateText('txtTrDate', 1, 100, 'Enter a certified date') == false) return false;
                    if (validateText('ddlExam', 1, 100, 'Enter a preferred class') == false) return false;
                    if (validateText('txtGpa', 1, 100, 'Enter a P S C G PA') == false) return false;
                    if (validateText('txtPSCRoll', 1, 100, 'Enter a P S C roll number') == false) return false;
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
    </script>
</asp:Content>
