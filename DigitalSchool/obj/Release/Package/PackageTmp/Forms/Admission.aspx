<%@ Page Title="Admission Form" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admission.aspx.cs" Inherits="DigitalSchool.Forms.Adminssion" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/AdminssionStyle.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.10.2.min.js" type="text/javascript"></script> 
    <script src="/Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <link href="/Styles/jquery-ui-datepekar.css" rel="stylesheet" />
    <link href="/Styles/popupStyle.css" rel="stylesheet" />
<style type="text/css">

    .tgInput {
        border:none!important;
    }
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>


<asp:UpdatePanel runat="server" ID="upPanel">
    <Triggers>
<%--       <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btnSaveNew" EventName="Click" />--%>
        <asp:AsyncPostBackTrigger ControlID="ddlPADistrict" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="ddlTADistrict" EventName="SelectedIndexChanged" />
    </Triggers>

<ContentTemplate>






<%--STUDENT INFORMATION--%>
<div class="tgPanel" style="margin-top:20px;border:1px solid #2E9FFF!important;">
<div class="tgPanelHead" style="background-color:#2E9FFF;padding: 2px 10px;">Student Information</div>
<div class="tgInput">

    <table style="float:left; display:block; width:62%; border:0" >
        <tr>
            <td class="label">Admission No <span class="required">*</span></td>
            <td class="input_box">
                <asp:TextBox ID="txtAdmissionNo" TabIndex="0" runat="server" ClientIDMode="Static" ></asp:TextBox>
            </td>
            <td >Date <span class="required">*</span></td>
            <td>              
                <asp:TextBox ID="txtAdmissionDate"  TabIndex="1" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAdmissionDate"></asp:CalendarExtender>
        </tr>
        

        <tr>
            <td class="label">Class <span class="required">*</span></td>
            <td>   
                <asp:DropDownList ID="ddlClass"  TabIndex="2" runat="server" Height="26px" ClientIDMode="Static" >
                </asp:DropDownList>
            </td>
            <td>Section <span class="required">*</span></td>
            <td>
                <asp:DropDownList ID="ddlSection"  TabIndex="3" runat="server" Height="26px" >
                </asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td >Student Name <span class="required">*</span></td>
            <td colspan="3"> <asp:TextBox ID="txtStudentName" runat="server" Width="100%" ClientIDMode="Static" ></asp:TextBox></td>
        </tr>


        <tr>
            <td class="label">Roll <span class="required">*</span></td>
            <td>
                <asp:TextBox ID="txtRoll" runat="server" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))" ></asp:TextBox>
            </td>
            <td >Date of Birth <span class="required">*</span></td>
            <td><asp:TextBox ID="txtDateOfBirth" runat="server" Width="100%" ></asp:TextBox> </td>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateOfBirth"></asp:CalendarExtender>
        </tr>


        <tr>
            <td>Gender<span class="required">*</span></td>
            <td>   
                <asp:DropDownList ID="ddlGender" runat="server" Height="26px" >
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >Mobile <span class="required">*</span></td>
            <td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>

    </table>

    <div style="float:left; margin-left:67px; display:block;width:20%"> 
        <div style="width:100%;text-align:center;">
            <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static"  runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" /> 
        </div>

        <asp:FileUpload ID="FileUpload1" style="margin-top:20px;" runat="server"  onchange="previewFile()" ClientIDMode="Static" />
    </div>

    <div style="clear:both;"></div>
</div>
        
</div>
   

 <%--Parents Information--%>
   <div class="tgPanel" style="margin-top:20px;border:1px solid #9E4B9E!important">
    <div class="tgPanelHead" style="background-color:#9E4B9E;padding: 2px 10px;">Parents Information</div>
    <div class="tgInput">

            <table  style="display:block; width:100%; border:0">
                <tr>
                    <td>Father&#39;s Name <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtFatherName" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td >Occupation <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtFatherOccupation" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td>Yearly Income <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtFatherYearlyIncome" runat="server" class="input"  ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Mother&#39;s Name <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtMotherName" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td >Occupation <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtMotherOccupation" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td>Yearly Income <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtMotherYearlyIncome" runat="server" class="input" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                </tr>
            </table>
    </div>
    </div>
        




 <%--Guardian Information--%>
   <div class="tgPanel" style="margin-top:20px;border:1px solid #F86D52!important;">
    <div class="tgPanelHead" style="background-color:#F86D52;padding: 2px 10px">Guardian Information ( If parents are absent )</div>
    <div class="tgInput">

            <table  style=" display:block; width:100%; border:0">
                <tr>
                    <td>Guardian Name </td>
                    <td>
                        <asp:TextBox ID="txtGuardianName" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td >Relation </td>
                    <td>
                        <asp:TextBox ID="txtGuardianRelation" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td style="width:80px">Mobile No</td>
                    <td>
                        <asp:TextBox ID="txtGurdianMobile" runat="server" class="input" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Guardian Address </td>
                    <td>
                        <asp:TextBox ID="txtGuardianAddress" runat="server" class="input"></asp:TextBox>
                    </td>
                    <td >&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td></td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
        



 <%--Permanent address--%>
   <div class="tgPanel" style="margin-top:20px;">
    <div class="tgPanelHead" style="background-color:#9DB92D;padding: 4px 10px;border:1px solid #9DB92D!important;">Permanent address</div>
    <div class="tgInput">

            <table class="auto-style1">
                <tr>
                    <td>Village <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtPAVillage" runat="server" Width="160px"></asp:TextBox>
                    </td>
                    <td >Post Office <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtPAPostOffice" runat="server" ></asp:TextBox>
                    </td>
                    <td>Thana/Upazila <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlPAThana" runat="server" class="ddl-box" Height="26px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>District </td>
                    <td>
                        <asp:DropDownList ID="ddlPADistrict" runat="server" AutoPostBack="True" class="ddl-box" Height="26px" OnSelectedIndexChanged="ddlPADistrict_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td >&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td></td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>



 <%--Present address--%>
   <div class="tgPanel" style="margin-top:20px;">
    <div class="tgPanelHead" style="background-color:#008191;padding: 2px 10px;">Present address</div>
    <div class="tgInput">

            <table  style="display:block; width:100%; border:0">
                <tr>
                    <td>Village <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtTAVillage" runat="server" Width="160px" ></asp:TextBox>
                    </td>
                    <td >Post Office <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtTAPostOffice" runat="server"></asp:TextBox>
                    </td>
                    <td>Thana/Upazila <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlTAThana" runat="server" class="ddl-box" Height="26px" Width="190px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>District <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlTADistrict" runat="server" AutoPostBack="True" class="ddl-box" Height="26px" OnSelectedIndexChanged="ddlTADistrict_SelectedIndexChanged" Width="190px">
                        </asp:DropDownList>
                        
                    </td>
                    <td >&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td></td>
                    <td>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>


 
 <%--Other Information--%>
   <div class="tgPanel" style="margin-top:20px;">
    <div class="tgPanelHead" style="background-color:#483D8B;padding: 2px 10px;">Other Information</div>
    <div class="Tag-Box">

            <table  style="display:block; width:100%; border:0">
                <tr>
                    <td style="width:200px">Select Exam <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlExam" runat="server" class="ddl-box" Height="26px" Width="190px">
                            <asp:ListItem>P.S.C</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>Roll <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtPSCRoll" runat="server" class="input" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                    <td>Passing Year <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlPassingYear" runat="server" class="ddl-box" Height="26px">
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>GPA <span class="required">*</span></td>
                    <td>
                        <asp:TextBox ID="txtGpa" runat="server" class="input" ></asp:TextBox>
                    </td>
                    <td >Board <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlBoard" runat="server" class="ddl-box" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td>Date <span class="required">*</span></td>
                    <td>                       
                        <asp:TextBox ID="txtTrDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtTrDate"></asp:CalendarExtender>
                        
                    </td>
                </tr>
                <tr>
                    <td>Previous School Name <span class="required">*</span></td>
                    <td colspan="3">
                        <asp:TextBox ID="txtPreviousSchoolName" runat="server" class="input" Width="100%"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Transfer Certificate No(if any) </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtTransferCNo" runat="server" class="input" Width="100%" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>That class would be admission <span class="required">*</span></td>
                    <td>
                        <asp:DropDownList ID="ddlThatClass" runat="server" class="ddl-box" Height="26px">
                        </asp:DropDownList>
                    </td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>    
                
                  
    </ContentTemplate>
</asp:UpdatePanel>


    <div style="bottom: 0px; height: auto; width: 165px; padding: 5px; margin-bottom:10px; text-align: center; background-color: rgba(0, 168, 0, 0.6); border: 1px solid green; margin-left: 0px; float: right; left: 0px; margin-right: 40px; margin-top: 5px;">

   
        <asp:Button ID="btnSave" runat="server" Text="Save" class="greenBtn" OnClick="btnSave_Click" />

         <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="blackBtn" OnClick="btnClear_Click" />

   </div>



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

    </script>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>
    <script type="text/javascript">
        function ifSubmitted() {
            var xclear = document.getElementById('#allClear').value;
            if (parseInt(xclear) == 1) {


                if (validateText('txtAdmissionNo', 1, 5, 'Enter a Admission No') == false) return false;
                if (validateText('txtDate', 1, 5, 'Enter a Admission Date') == false) return false;
                if (validateText('ddlClass', 1, 3, 'Enter a Class') == false) return false;
                if (validateText('ddlSection', 1, 1, 'Enter a Section') == false) return false;
                if (validateText('txtRoll', 1, 1, 'Enter a Roll No') == false) return false;
                if (validateText('txtStudentName', 1, 5, 'Enter a Full Name') == false) return false;
                if (validateText('ddlGender', 1, 4, 'Enter a Gender') == false) return false;
                if (validateText('txDateOfBirth', 1, 5, 'Enter a Date Of Birth') == false) return false;

                if (validateText('txtMobile', 1, 11, 'Enter a Mobile') == false) return false;
                // if (validateText('FileUpload1', 1, 100, 'Enter a Image Name') == false) return false;
                if (validateText('txtFatherName', 1, 5, 'Enter a Fathers Name') == false) return false;
                if (validateText('txtFatherOccupation', 1, 5, 'Enter a Fathers Profession') == false) return false;
                if (validateText('txtFatherYearlyIncome', 1, 4, 'Enter a Fathers Yearly Income') == false) return false;

                if (validateText('txtMotherName', 1, 100, 'Enter a Mothers Name') == false) return false;
                if (validateText('txtMotherOccupation', 1, 100, 'Enter a Mothers Profession') == false) return false;
                if (validateText('txtMotherYearlyIncome', 1, 100, 'Enter a Mothers Yearly Income') == false) return false;

                if (validateText('txtPAVillage', 1, 100, 'Enter a PA Village') == false) return false;
                if (validateText('txtPAPostOffice', 1, 100, 'Enter a PA Post Office') == false) return false;
                if (validateText('ddlPAThana', 1, 100, 'Enter a PA Thana') == false) return false;
                if (validateText('txtPADistrict', 1, 100, 'Enter a PA District') == false) return false;

                if (validateText('txtTAVillage', 1, 100, 'Enter a TA Vi Ilage') == false) return false;
                if (validateText('txtTAPostOffice', 1, 100, 'Enter a TA Post Office') == false) return false;
                if (validateText('ddlTAThana', 1, 100, 'Enter a TA Thana') == false) return false;
                if (validateText('ddlTADistrict', 1, 100, 'Enter a TA District') == false) return false;

                if (validateText('txtGuardianName', 1, 100, 'Enter a Guardian Name') == false) return false;
                if (validateText('txtGuardianRelation', 1, 100, 'Enter a Guardian Relation') == false) return false;
                if (validateText('txtGurdianMobile', 1, 100, 'Enter a Guardian Mobile No') == false) return false;
                if (validateText('txtGuardianAddress', 1, 100, 'Enter a GuardianAddress') == false) return false;

                if (validateText('txtMotherTongue', 1, 100, 'Enter a Mother Tongue') == false) return false;
                if (validateText('txtNationality', 1, 100, 'Enter a Nationality') == false) return false;

                if (validateText('txtPreviousSchoolName', 1, 100, 'Enter a Previous School Name') == false) return false;
                if (validateText('txtTransferCNo', 1, 100, 'Enter a Transfer Certified No') == false) return false;
                if (validateText('txtTrDate', 1, 100, 'Enter a Certified Date') == false) return false;
                if (validateText('ddlExam', 1, 100, 'Enter a Preferred Class') == false) return false;
                if (validateText('txtGpa', 1, 100, 'Enter a P S C G PA') == false) return false;
                if (validateText('txtPSCRoll', 1, 100, 'Enter a P S C Roll No') == false) return false;
                if (validateText('ddlBoard', 1, 100, 'Enter a P S C Board') == false) return false;
                if (validateText('ddlPassingYear', 1, 100, 'Enter a P S C Passing Year') == false) return false;
                return true;
            } else {
                return false;
            }
        }

</script>
    <script type="text/javascript" >


        $("#txtRoll").keypress(function (e) {
            if (String.fromCharCode(e.keyCode).match(/[^0-9]/g)) return false;
        });

        $(function () {
           // var element = this.document.getElementById('txtAdmissionNo');
            $('#txtFatherYearlyIncome').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey || e.altKey) {
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                        e.preventDefault();
                        
                        alert(key);
                    }
                }
            });










        });


    </script>

    <script src="/Scripts/popupjquery.min.js"></script>
    <script src="/Scripts/popupjavascript.js"></script>
    <script src="/Scripts/jquery.js"></script>
    <script src="/Scripts/jquery-ui-datepekar.js"></script>
</asp:Content>
