<%@ Page Title="Exam Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExamSetup.aspx.cs" Inherits="DS.Admin.ExamSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="btnAdd" />
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblExamSettId" ClientIDMode="Static" runat="server" />
    <div class="main-div">
        <asp:UpdatePanel runat="server" ID="updatepanelExam">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>
            <ContentTemplate>
                <div class="leftSide-div">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlClasses" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSubjectList" />
                            <asp:AsyncPostBackTrigger ControlID="ddlParrern" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="tgPanel">
                                <div class="tgPanelHead">Exam Settings</div>
                                <table class="tbl-controlPanel">
                                    <tr>
                                        <td>Select Exam Type</td>
                                        <td>
                                            <asp:DropDownList ID="dlExam" runat="server" ClientIDMode="Static" Height="26px" Width="246px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Select Class</td>
                                        <td>
                                            <asp:DropDownList ID="dlClasses" runat="server" ClientIDMode="Static" Height="26px" Width="246px"
                                                AutoPostBack="True" OnSelectedIndexChanged="dlClasses_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Subject</td>
                                        <td>
                                            <asp:DropDownList ID="ddlSubjectList" runat="server" ClientIDMode="Static" Height="26px" Width="246px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlSubjectList_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">...Select Subject...</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Pattern</td>
                                        <td>
                                            <asp:DropDownList ID="ddlParrern" runat="server" ClientIDMode="Static" Height="26px" Width="246px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlParrern_SelectedIndexChanged">
                                                <asp:ListItem Selected="True">...Selet Pattern...</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Marks</td>
                                        <td>
                                            <asp:TextBox ID="txtMarks" runat="server" ReadOnly="true" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Convert to</td>
                                        <td>
                                            <asp:TextBox ID="txtEx_Marks_Convert_To" runat="server" Width="246px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td>
                                            <div class="AddBox-SubQpattern" style="width: 242px">
                                                <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="greenBtn"
                                                    OnClientClick="return validateInputs();" Text="Add" Width="90px" Style="margin: 0 0 0 95px" OnClick="btnAdd_Click" />
                                            </div>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save"
                                                CssClass="greenBtn" OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="blackBtn" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClear" />

                                    </Triggers>
                                    <ContentTemplate>
                                        <div id="divscroll" style="height: 240px; max-height: 500px; overflow: auto; overflow-x: hidden;">
                                            <asp:GridView ID="gvAddExamSetup" runat="server" Width="481px"
                                                AutoGenerateColumns="false" Style="margin-left: 15px;" BackColor="White"
                                                BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3"
                                                CellSpacing="1" GridLines="None" Height="5px" ClientIDMode="Static"
                                                OnRowCommand="gvAddExamSetup_RowCommand">
                                                <Columns>
                                                    <asp:ButtonField CommandName="Remove" Text="Remove" ButtonType="Button" />
                                                    <asp:BoundField DataField="SubId" HeaderText="SubId" Visible="false" />
                                                    <asp:BoundField DataField="SubName" HeaderText="Sub.Name" />
                                                    <asp:BoundField DataField="QPId" HeaderText="QPId" Visible="false" />
                                                    <asp:BoundField DataField="QPName" HeaderText="Q.Pattern" />
                                                    <asp:BoundField DataField="Marks" HeaderText="Marks" />
                                                    <asp:BoundField DataField="ConvertTo" HeaderText="ConvertTo" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("SubId") %>' />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("QPId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                                <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Left" />
                                                <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#33276A" />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="rightSide-div">
            <div style="text-align: center; font-weight: 700; font-size: 16px">Exam Setup Details</div>
            <br />
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlClassList" runat="server" Width="180px" Height="30px" CssClass="input"
                            ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlClassList_SelectedIndexChanged">
                            <asp:ListItem Selected="True">---Select Class---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="ddlExamType" runat="server" Width="200px" Height="30px" CssClass="input" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                            <asp:ListItem Selected="True">---Select Exam Type---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 30px"></td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" CssClass="greenBtn" Text="Edit" OnClick="btnEdit_Click" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="up10" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlClassList" />
                    <asp:AsyncPostBackTrigger ControlID="ddlExamType" />
                </Triggers>
                <ContentTemplate>
                    <div id="divExamSetup" class="datatables_wrapper" runat="server" style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtAmount', 1, 30, 'Enter Exam Amount Taka') == false) return false;
            else if (validateText('txtEx_Marks', 1, 30, 'Enter Exam Marks') == false) return false;
            else if (validateText('txtEx_Marks_Convert_To', 1, 30, 'Enter Exam Marks Convert To') == false) return false;
            return true;
        }
        function editExamSettings(examSetId) {
            $('#lblExamSettId').val(examSetId);
            var strExam = $('#r_' + examSetId + ' td:nth-child(1)').html();
            var strClass = $('#r_' + examSetId + ' td:nth-child(2)').html();
            var strFees = $('#r_' + examSetId + ' td:nth-child(3)').html();
            var strMarks = $('#r_' + examSetId + ' td:nth-child(4)').html();
            var strConvetTo = $('#r_' + examSetId + ' td:nth-child(5)').html();
            $('#dlClasses').val(strClass);
            $('#dlExam').val(strExam);
            $('#txtAmount').val(strFees);
            $('#txtEx_Marks').val(strMarks);
            $('#txtEx_Marks_Convert_To').val(strConvetTo);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblExamSettId').val('');
            $('#txtAmount').val('');
            $('#txtEx_Marks').val('');
            $('#txtEx_Marks_Convert_To').val('');
            setFocus('txtAmount');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
