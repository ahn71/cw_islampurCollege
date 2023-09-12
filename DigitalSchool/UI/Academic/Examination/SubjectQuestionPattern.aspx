<%@ Page Title="Subject Question Pattern" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" 
    CodeBehind="SubjectQuestionPattern.aspx.cs" Inherits="DS.UI.Academic.Examination.SubjectQuestionPattern" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .input {
            width: 200px;
        }

        .tbl-controlPanel {
            width: 964px;
        }

            .tbl-controlPanel td:first-child,
            .tbl-controlPanel td:nth-child(2n+1) {
                text-align: right;
                padding-right: 7px;
            }

        .table tr th {
            background-color: #23282C;
            color: white;
        }

        .table td {
            text-align: left;
        }

        .table tr th:nth-child(5), table tr th:nth-child(6),
        table tr th:nth-child(7), table tr th:nth-child(8),
        tr td:nth-child(5), table tr td:nth-child(6),
        table tr td:nth-child(7), table tr td:nth-child(8) {
            text-align: center;
        }
        .table tr td:first-child, th:first-child {
            width:103px;
        }

        #btn_td {
            padding-right: 10%;
        }

        input[type="checkbox"] {
            margin: 5px;
        }
        .margin {
            margin:-21px 0 -6px 0;
        }
        .margin1 {
            margin-top:-21px;
        }
        .tblsubquepattern_length {
            padding:0px;
        }
        .dataTables_length, .dataTables_filter {
            padding:1px;
        }
        .dataTables_filter {
            margin-bottom:0px;
        }
        .dataTables_filter {
            margin: 22px 8px 0px 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSubQPId" ClientIDMode="Static" Value="0" runat="server" />
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">
                <li>
                   <%-- <a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>                
                 <%--<li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>--%>                
                <%--<li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>--%>
                 <li>  <a runat="server" id="aAcademicHome" >Academic Module</a></li>
                <li><a runat="server" id="aExamHome">Examination Module</a></li> 
                <li class="active">Subject Question Pattern</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Subject Question Pattern</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                            <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                            <asp:AsyncPostBackTrigger ControlID="ddlsubjectName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlCourse" />
                            <asp:AsyncPostBackTrigger ControlID="ddlQPattern" />
                            <asp:AsyncPostBackTrigger ControlID="ddlExamType" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="chkIsoptional" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Exam Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlExamType" CssClass="input" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Batch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatch" CssClass="input" runat="server" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td id="trGroup1" runat="server" visible="true">Group
                                    </td>
                                    <td id="trGroup2" runat="server" visible="true">
                                        <asp:DropDownList ID="ddlGroup" CssClass="input" runat="server"  Width="107px" ClientIDMode="Static" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                        </asp:DropDownList>

                                         <asp:TextBox ID="txtConvertTo" CssClass="input" Width="90px" ClientIDMode="Static" 
                                                runat="server" Placeholder="Convert To %"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subject
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsubjectName" CssClass="input" AutoPostBack="true" runat="server"
                                            OnSelectedIndexChanged="ddlsubjectName_SelectedIndexChanged" ClientIDMode="Static">
                                            <%--<asp:ListItem Selected="True">...Select...</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Course
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCourse" AutoPostBack="true" runat="server" CssClass="input"
                                            OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" ClientIDMode="Static">
                                            <%--<asp:ListItem Selected="True">...Select...</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td>Quest. Pattern
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQPattern" runat="server" CssClass="input" ClientIDMode="Static">
                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="tr" runat="server" visible="false">
                                    <td style="text-align: center" id="tdsubMarks" runat="server" visible="false" colspan="2">Subject Marks:
                                        <asp:TextBox runat="server" ID="txtsubMarks" ClientIDMode="Static"></asp:TextBox> 
                                        <%--<asp:Label ID="lblsubMarks" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                    </td>
                                    <td style="text-align: center" id="tdcourseMarks" runat="server" visible="false" colspan="2">Course Marks:
                                        <asp:TextBox runat="server" ID="txtcourseMarks" ClientIDMode="Static"></asp:TextBox>  
                                        <%--<asp:Label ID="lblcourseMarks" runat="server" ClientIDMode="Static"></asp:Label>--%>
                                    </td>
                                    <td style="text-align: center" id="tdisoptional" runat="server" visible="false" colspan="2">
                                        Is Optional:<asp:CheckBox ID="chkOptional" Enabled="false" runat="server" ClientIDMode="Static" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Pattern Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQMarks" CssClass="input" runat="server" ClientIDMode="Static"> </asp:TextBox>
                                    </td>
                                    <td>Pass Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassMark" CssClass="input" ClientIDMode="Static" runat="server"></asp:TextBox>
                                       
                                        <td>
                                           
                                            <asp:CheckBox  Visible="false" ID="chkIsoptional" Width="28%" AutoPostBack="true" ClientIDMode="Static"
                                                OnCheckedChanged="chkIsoptional_CheckedChanged" runat="server" Text="Optional" />
                                        </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td colspan="2" id="btn_td">
                                        <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" 
                                            CssClass="btn btn-success" OnClientClick="return validateInputs();"
                                            Text="Add" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnSave" runat="server" 
                                            ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                            OnClientClick="return validateInputsSave();" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" 
                                            CssClass="btn btn-default" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="dependencyPanel" runat="server" CssClass="datatables_wrapper"
                                Style="min-height: 90px; max-height: 200px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvSubQPattern" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-bordered"
                                    ClientIDMode="Static" OnRowCommand="gvSubQPattern_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="Remove" ControlStyle-CssClass ="btn btn-danger" Text="Remove" ButtonType="Button" />
                                        <asp:BoundField DataField="SubId" HeaderText="SubId" Visible="false" />
                                        <asp:BoundField DataField="SubName" HeaderText="Subject" />
                                        <asp:BoundField DataField="CourseId" HeaderText="CourseId" Visible="false" />
                                        <asp:BoundField DataField="CourseName" HeaderText="Course" />
                                        <asp:BoundField DataField="QPId" HeaderText="QPId" Visible="false" />
                                        <asp:BoundField DataField="QPName" HeaderText="Pattern" />
                                        <asp:BoundField DataField="QMarks" HeaderText="P.Marks" />
                                        <asp:BoundField DataField="PassMarks" HeaderText="Pa.Marks" />   
                                        <%--<asp:BoundField DataField="ConvertTo" HeaderText="Convert To (%)" />   --%>                                    
                                        <asp:BoundField DataField="IsOptional" HeaderText="Optional" />
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("SubId") %>' />
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("QPId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
         <!-- Dependency  modal -->
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <Triggers>                            
                            <asp:AsyncPostBackTrigger ControlID="btnDependency" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:ModalPopupExtender ID="ShowDependencyModal" runat="server" BehaviorID="modalpopup1" CancelControlID="Button2"
                                OkControlID="LinkButton1"
                                TargetControlID="button3" PopupControlID="showDependency" BackgroundCssClass="ModalPopupBG">
                            </asp:ModalPopupExtender>
                            <div id="showDependency" runat="server" style="display: none;" class="confirmationModal500">
                                <div class="modal-header">
                                    <button id="Button2" type="button" class="close white"></button>
                                    <div class="tgPanelHead">Dependency Details</div>
                                </div>
                                <div class="modal-body">
                                    <table class="tbl-controlPanel margin1">
                                        <tr>
                                            <td>
                                                Batch:
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblBatch"></asp:Label>
                                            </td>
                                            <td>
                                                Group:
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblGroup"></asp:Label>
                                            </td>
                                            </tr>
                                        </table>
                                    <div class="row">
                                        <asp:GridView ID="gvDependency" GridLines="Horizontal" CssClass="table table-bordered" OnRowDataBound="gvDependency_RowDataBound" AutoGenerateColumns="False" runat="server" >
                                            <PagerStyle CssClass="gridview" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Exam Name" ItemStyle-Width="180px" DataField="ExName" />                                                
                                                <asp:TemplateField HeaderText="Convert To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConvertTo" runat="server" Text='<%# Eval("ConvertTo")%>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="ConvertTo" runat="server" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>                                            
                                        </asp:GridView>

                                    </div>
                                    <div class="row">
                                           <table class="tbl-controlPanel margin">
                                        <tr>
                                            
                                            <td style="width:180px;">
                                               Total:
                                            </td>                                            
                                            <td style="padding-left:10px;">
                                                <asp:Label runat="server" ID="total"></asp:Label>
                                            </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button id="button3" type="button" runat="server" style="display: none;" />
                                    <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                        <i class="icon-remove"></i>                                    
                                        Close
                                    </asp:LinkButton>                                 
                                </div>
                            </div>
                            </ContentTemplate>
            </asp:UpdatePanel>
                            <!-- END Dependency modal -->
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Subject Question Pattern Details</div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="up10" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlBatchList" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel" style="width: 1050px;">
                                <tr>
                                    <td>Exam Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlExamTypeList" runat="server"
                                            CssClass="input" ClientIDMode="Static">
                                        </asp:DropDownList>

                                    </td>
                                    <td>Batch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBatchList" Width="150px" runat="server"
                                            CssClass="input" ClientIDMode="Static" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlBatchList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Group
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlGroupList" runat="server"
                                            CssClass="input" Width="150px" ClientIDMode="Static">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDependency" CssClass="btn btn-primary" Text="Dependency" ClientIDMode="Static"
                                            OnClientClick="return validateOutputDependency();" runat="server" OnClick="btnDependency_Click"/>
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" Text="Search" ClientIDMode="Static"
                                            OnClientClick="return validateOutput();" runat="server" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnEdit" CssClass="btn btn-success" Text="Edit" ClientIDMode="Static"
                                            OnClientClick="return validateOutput();" runat="server" OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Delete" ClientIDMode="Static"
                                            OnClientClick="return validateOutputDel(); "  runat="server" OnClick="btnDelete_Click" />
                                        <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-primary" Text="Print"  ClientIDMode="Static" OnClick="btnPrint_Click" /><br />
                                         <p runat="server" visible="false" id="msgForPatterStatus" style="color:red;  float:right">This pattern already used for exam process.</p>  
                                    </td>
                                </tr>
                                 </table>                                                  
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div id="divSQpattarn" class="datatables_wrapper" runat="server"
                                 style="width: 100%; min-height: 150px;
                                 max-height: 500px; overflow: auto; overflow-x: hidden; margin-top: -24px;">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#tblsubquepattern').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loadpatterninfo() {
            $('#tblsubquepattern').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {           
            if (validateCombo('ddlExamType', '0', 'Select Exam Type') == false) return false;
            if (validateCombo('ddlBatch', '0', 'Select Batch Name') == false) return false;
            if ($('#ddlGroup').prop("disabled") == false) {
                if (validateCombo('ddlGroup', '0', 'Select Group Name') == false) return false;                
               
            }
            if (validateText('txtConvertTo', 1, 100, 'Enter Convert To ') == false) return false;
            if ($("#btnSave").val() == 'Save') {
                if (validateCombo('ddlsubjectName', '0', 'Select Subject Name') == false) return false;
                //if ($('#ddlCourse').prop("disabled") == false) {
                //    if (validateCombo('ddlCourse', '0', 'Select Course Name') == false) return false;
                //}
                if (validateCombo('ddlQPattern', '0', 'Select Question Pattern') == false) return false;
                if (validateText('txtQMarks', 1, 100, 'Enter Question Mark') == false) return false;
                if (validateText('txtPassMark', 1, 100, 'Enter Pass Mark') == false) return false;
                //if (($("#ddlCourse option:selected").text() == '...Select...'))
                //{
                //    showMessage('Please Select Course Name', 'warning');
                //    return false;
                //}
                if(parseInt($('#txtsubMarks').val()) < parseInt($('#txtcourseMarks').val()))
                {
                    showMessage('Please Check Subject and Course Marks !', 'warning');
                    return false;
                }
            }
            return true;
        }
        function validateInputsSave() {
            if ($("#btnSave").val() == 'Update') {
                if (validateCombo('ddlExamType', '0', 'First click Edit then Update') == false) return false;
                if (validateCombo('ddlBatch', '0', 'First click Edit then Update') == false) return false;
            }
            if (validateCombo('ddlExamType', '0', 'Select Exam Type') == false) return false;
            if (validateCombo('ddlBatch', '0', 'Select Batch Name') == false) return false;
            if ($("#btnSave").val() == 'Save') {                
                return confirm('Do you want to Save?');
            }
            return true;
        }
        function validateOutput() {            
            if (validateCombo('ddlExamTypeList', '0', 'Select Exam Type') == false) return false;
            if (validateCombo('ddlBatchList', '0', 'Select Batch Name') == false) return false;
            if (($("#ddlGroupList option:selected").text() == '...Select...')) {
                showMessage('Please Select Group Name', 'warning');
                return false;
            }
            return true;
        }
        function validateOutputDependency() {            
            if (validateCombo('ddlBatchList', '0', 'Select Batch Name') == false) return false;
            if (($("#ddlGroupList option:selected").text() == '...Select...')) {
                showMessage('Please Select Group Name', 'warning');
                return false;
            }
            return true;
        }
        function validateOutputDel() {
            if (validateCombo('ddlExamTypeList', '0', 'Select Exam Type') == false) return false;
            if (validateCombo('ddlBatchList', '0', 'Select Batch Name') == false) return false;
            return confirm('Do you want to delete this Question pattern ?');
            return true;
        }
        function editEmployee(empId) {
            $('#lblDesignationId').val(empId);
            var strDesName = $('#r_' + empId + ' td:first-child').html();
            $('#txtDes_Name').val(strDesName);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#lblDesignationId').val('');
            $('#txtDes_Name').val('');
            setFocus('txtDes_Name');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearText();
        }
        function SaveSuccess() {
            showMessage('Save successfully', 'success');
        }
        function isoptional() {
            if ($('#chkOptional').attr('checked')) {
                $('#chkIsoptional').attr('checked', 'checked');
            } else {
                $('#chkIsoptional').attr('checked', false);
                showMessage('This Subject Not Optional', 'warning');
            }
        }
    </script>
</asp:Content>
