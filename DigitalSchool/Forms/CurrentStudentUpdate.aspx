<%@ Page Title="Student Current Information Update" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CurrentStudentUpdate.aspx.cs" Inherits="DS.Forms.CurrentStudentUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Styles/AdminssionStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upPanel" UpdateMode="Conditional">
        <ContentTemplate>
            <%--STUDENT INFORMATION--%>
            <div class="tgPanel" style="margin-top: 20px; border: 1px solid #2E9FFF!important;">
                <div class="tgPanelHead" style="background-color: #2E9FFF; padding: 2px 10px;">Student Information</div>
                <div class="tgInput">
                    <table style="float: left; display: block; width: 62%; border: 0">
                        <tr>
                            <td>Class <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlClass" TabIndex="2" runat="server" Height="26px" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>Section <span class="required">*</span></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlClass" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlSection" TabIndex="3" ClientIDMode="Static" runat="server" Height="26px">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td>Student Name <span class="required">*</span></td>
                            <td colspan="3">
                                <asp:TextBox ID="txtStudentName" runat="server" Width="100%" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Roll <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                            <td>Gender <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlGender" runat="server" Height="26px" ClientIDMode="Static">
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>Mobile<span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>Status <span class="required">*</span></td>
                            <td>
                                <asp:CheckBox runat="server" ID="chkStatus" Checked="true" Text="" /></td>
                        </tr>
                        <tr>
                            <td>Session<span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtSession" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>

                            <td>Blood Group </td>
                            <td>
                                <asp:DropDownList ID="dlBloodGroup" runat="server" class="ddl-box" Height="26px" ClientIDMode="Static">
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

                            </td>
                        </tr>

                        </tr>        
                        <td>Religion<span class="required">*</span></td>
                        <td>
                            <asp:DropDownList ID="dlReligion" runat="server" ClientIDMode="Static" Height="26px" TabIndex="2">
                                <asp:ListItem>Islam</asp:ListItem>
                                <asp:ListItem>Hindu</asp:ListItem>
                                <asp:ListItem>Christian</asp:ListItem>
                                <asp:ListItem>Buddhist</asp:ListItem>
                                <asp:ListItem>Upozati</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="label">Shift</td>
                        <td>
                            <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" Height="26px" TabIndex="2">
                                <asp:ListItem>Morning</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </table>
                    <div style="float: left; margin-left: 67px; display: block; width: 20%">
                        <div style="width: 100%; text-align: center;">
                            <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/profileImages/noProfileImage.jpg" />
                        </div>

                        <asp:FileUpload ID="FileUpload1" Style="margin-top: 20px;" runat="server" onclick="" onchange="previewFile()" ClientIDMode="Static" Visible="false" />
                    </div>
                    <div style="clear: both;"></div>
                </div>
            </div>
            <%--Present address--%>
            <div class="tgPanel" style="margin-top: 20px; border: 1px solid #9E4B9E!important">
                <div class="tgPanelHead" style="background-color: #9E4B9E; padding: 2px 10px;">Parents Information</div>
                <div class="tgInput">

                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td>Father&#39;s Name <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFatherName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Occupation <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFatherOccupation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Yearly Income <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtFatherYearlyIncome" runat="server" class="input" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Mother&#39;s Name <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMotherName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Occupation <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMotherOccupation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Yearly Income <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtMotherYearlyIncome" runat="server" ClientIDMode="Static" class="input" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Father's Mobile
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFathersMobile" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Mother's Mobile
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMothersMobile" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Home Phone
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtHomePhone" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--Guardian Information--%>
            <div class="tgPanel" style="margin-top: 20px; border: 1px solid #F86D52!important;">
                <div class="tgPanelHead" style="background-color: #F86D52; padding: 2px 10px">Guardian Information ( If parents are absent )</div>
                <div class="tgInput">
                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td>Guardian Name </td>
                            <td>
                                <asp:TextBox ID="txtGuardianName" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Relation </td>
                            <td>
                                <asp:TextBox ID="txtGuardianRelation" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td style="width: 80px">Mobile No</td>
                            <td>
                                <asp:TextBox ID="txtGurdianMobile" runat="server" class="input" ClientIDMode="Static" onKeyUp="$(this).val($(this).val().replace(/[^\d]/ig, ''))"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Guardian Address </td>
                            <td>
                                <asp:TextBox ID="txtGuardianAddress" runat="server" class="input" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
            <%--Present address--%>
            <div class="tgPanel" style="margin-top: 20px;">
                <div class="tgPanelHead" style="background-color: #008191; padding: 2px 10px;">Present address </div>
                <div class="tgInput">
                    <table style="display: block; width: 100%; border: 0">
                        <tr>
                            <td>Village <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtTAVillage" ClientIDMode="Static" runat="server" Width="160px"></asp:TextBox>
                            </td>
                            <td>Post Office <span class="required">*</span></td>
                            <td>
                                <asp:TextBox ID="txtTAPostOffice" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>Thana/Upazila <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlTAThana" runat="server" class="ddl-box" Height="26px" Width="190px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>District <span class="required">*</span></td>
                            <td>
                                <asp:DropDownList ID="ddlTADistrict" runat="server" AutoPostBack="True" ClientIDMode="Static" class="ddl-box" Height="26px" OnSelectedIndexChanged="ddlTADistrict_SelectedIndexChanged" Width="190px">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="bottom: 0px; height: auto; width: 188px; padding: 5px; margin-bottom: 10px; text-align: center; background-color: rgba(0, 168, 0, 0.6); border: 1px solid green; margin-left: 0px; float: right; left: 0px; margin-right: 40px; margin-top: 5px;">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="greenBtn" ClientIDMode="Static" OnClientClick="return validateInputs();" OnClick="btnUpdate_Click" Width="80px" />
        <input type="button" value="Back" class="blackBtn" onclick="backUrl();" />
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

        function backUrl() {
            goURL('/Forms/CurrentStudentInfo.aspx');
        }
    </script>
</asp:Content>
