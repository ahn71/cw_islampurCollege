<%@ Page Title="Employee Info" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeInformation.aspx.cs" Inherits="DigitalSchool.Forms.EmployeeInformation" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 10px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>


<%--<asp:UpdatePanel runat="server" ID="upPanel">
<ContentTemplate>--%>




<%--STUDENT INFORMATION--%>
<div class="EmPanel" style="margin-top:20px;border:1px solid #2E9FFF!important; overflow: hidden;">
<div class="EmPanelHead" style="background-color:#2E9FFF;padding: 2px 10px;">Employee Information</div>
<div class="EmInput">

    <table >
        <tr>
            <td class="label">Index Number</td>
            <td style="width: 210px;">
                <asp:TextBox ID="txtE_CardNo" TabIndex="0" runat="server" ClientIDMode="Static" ></asp:TextBox>
            </td>
            <td >Joining Date <span class="required">*</span></td>
            <td class="auto-style1">              
                <asp:TextBox ID="txtE_JoiningDate" TabIndex="0" runat="server" ClientIDMode="Static" ></asp:TextBox>
                  <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtE_JoiningDate"></asp:CalendarExtender>
        </tr>

        <tr>
            <td class="label">Name <span class="required">*</span></td>
            <td>
                <asp:TextBox ID="txtE_Name" runat="server" ></asp:TextBox>
            </td>
            <td >Gender <span class="required">*</span></td>
            <td class="auto-style1">
                <asp:DropDownList ID="dlGender" runat="server">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                </asp:DropDownList></td>
        </tr>

        <tr>
            <td>Father's Name<span class="required">*</span></td>
            <td>   
                <asp:TextBox ID="txtE_FathersName" runat="server"></asp:TextBox>
            </td>
            <td >Mother's Name <span class="required">*</span></td>
            <td class="auto-style1"><asp:TextBox ID="txtE_MothersName" runat="server"></asp:TextBox></td>
            <td></td>
        </tr>

        <tr>
            <td class="label">Department <span class="required">*</span></td>
            <td class="input_box">
                <asp:DropDownList ID="dlDepartments" runat="server">
                    
                </asp:DropDownList>
            </td>
            <td >Designation<span class="required">*</span></td>
            <td class="auto-style1">              
                <asp:DropDownList ID="dlDesignation" runat="server">
                    
                </asp:DropDownList>
        </tr>

        <tr>
            <td class="label">Religion</td>
            <td class="input_box">
                <asp:DropDownList ID="dlReligion" runat="server">
                    <asp:ListItem>Islam</asp:ListItem>
                    <asp:ListItem>Hindu</asp:ListItem>
                    <asp:ListItem>Christian</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >Marital Status</td>
            <td class="auto-style1">              
                <asp:DropDownList ID="dlMaritalStatus" runat="server" Height="26px" >
                    <asp:ListItem>Married</asp:ListItem>
                    <asp:ListItem>Unmarried</asp:ListItem>
                </asp:DropDownList>
        </tr>


        <tr>
            <td class="label">Phone</td>
            <td>
                <asp:TextBox ID="txtE_Phone" runat="server" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))" ></asp:TextBox>
            </td>
            <td >Mobile <span class="required">*</span></td>
            <td class="auto-style1"><asp:TextBox ID="txtE_Mobile" runat="server" Width="100%" ></asp:TextBox> </td>
        </tr>

           <tr>
            <td class="label">Email</td>
            <td>
                <asp:TextBox ID="txtE_Email" runat="server"  ></asp:TextBox>
            </td>
            <td >Birth Day<span class="required">*</span></td>
            <td class="auto-style1"><asp:TextBox ID="txtE_Birthday" runat="server" Width="100%" ></asp:TextBox> </td>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtE_Birthday"></asp:CalendarExtender>
        </tr>


        <tr>
            <td style="vertical-align:top;">Present Address<span class="required">*</span></td>
            <td>   
                <asp:TextBox ID="txtE_PresentAddress" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td style="vertical-align:top;">Parmanent Address <span class="required">*</span></td>
            <td class="auto-style1"><asp:TextBox ID="txtE_ParmanentAddress" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            <td></td>
        </tr>


        <tr>
            <td class="label">Blood Group</td>
            <td class="input_box">
                <asp:DropDownList ID="dlBloodGroup" runat="server">
                    <asp:ListItem>A+</asp:ListItem>
                    <asp:ListItem>A-</asp:ListItem>
                    <asp:ListItem>B+</asp:ListItem>
                    <asp:ListItem>B-</asp:ListItem>
                    <asp:ListItem>AB+</asp:ListItem>
                    <asp:ListItem>AB-</asp:ListItem>
                    <asp:ListItem>O+</asp:ListItem>
                    <asp:ListItem>O-</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >Last Degree<span class="required">*</span></td>
            <td class="auto-style1">              
                <asp:TextBox ID="txtE_LastDegree" TabIndex="0" runat="server" ClientIDMode="Static" ></asp:TextBox>
        </tr>

        <tr>
            <td class="label">Examiner</td>
            <td class="input_box">
                <asp:CheckBox ID="chkExaminer" runat="server" Checked="True" />
            </td>
            <td >Nationality</td>
            <td class="auto-style1">              
                <asp:DropDownList ID="dlNationality" runat="server">
                    <asp:ListItem>Bangladeshi</asp:ListItem>
                    <asp:ListItem>Japanese</asp:ListItem>
                    <asp:ListItem>Pakistani</asp:ListItem>
                    <asp:ListItem>India</asp:ListItem>
                </asp:DropDownList>
        </tr>

    </table>

</div>


<div class="EmPanelRight"> 
        <div style="width:100%;text-align:center;">
             <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static"  runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" /> 
   
        </div>
          <asp:FileUpload ID="FileUpload1" onchange="previewFile()" runat="server" />
    </div>
        
</div>

    
    <div style="bottom: 0px; height: auto; width: 165px; padding: 5px; text-align: center; background-color: rgba(0, 168, 0, 0.6); border: 1px solid green; margin-left: 0px; float: right; left: 0px; margin-right: 40px; margin-top: 5px;">

   
        <asp:Button ID="btnSave" runat="server" Text="Save" class="greenBtn" OnClick="btnSave_Click"  />

         <asp:Button ID="btnClear" runat="server" Text="Clear"  CssClass="blackBtn"  />

   </div>
   


                
<%--                  
    </ContentTemplate>
</asp:UpdatePanel>--%>

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

        function updateSuccess() {
            showMessage('Updated successfully', 'success');
        }


    </script>


</asp:Content>
