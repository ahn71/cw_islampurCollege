<%@ Page Title="Subject Question Pattern" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubjectQuestionPattern.aspx.cs" Inherits="DS.Forms.SubjectQuestionPattern" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }  
        .input{
            width: 200px;
        }   
        .tbl-controlPanel{
           width:900px;
        }
           
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblSubQPId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Subject Question Pattern</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlClassName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlsubjectName" />
                            <asp:AsyncPostBackTrigger ControlID="ddlQPattern" />
                            <asp:AsyncPostBackTrigger ControlID="ddlExamType" />
                            <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>
                                        Exam Type
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlExamType" CssClass="input" runat="server" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Class
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlClassName" CssClass="input" runat="server" ClientIDMode="Static"
                                            OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged" AutoPostBack="True">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>                                    
                                    <td ID="trGroup1" runat="server" Visible="true">
                                        Group
                                    </td>
                                    <td ID="trGroup2" runat="server" Visible="true">
                                        <asp:DropDownList ID="DropGrp" CssClass="input" runat="server" ClientIDMode="Static"
                                            AutoPostBack="True">
                                            <asp:ListItem Value="1">Science</asp:ListItem>
                                            <asp:ListItem Value="2">Commerce</asp:ListItem>
                                            <asp:ListItem Value="3">Arts</asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:CheckBox ID="chkScience" Text="Science" runat="server" Style="padding-right: 20px;" />
                                    <asp:CheckBox ID="chkCommerce" Text="Commerce" runat="server" Style="padding-right: 20px;" />
                                    <asp:CheckBox ID="chkArts" Text="Arts" runat="server" />--%>
                                    </td>                                    
                                </tr>
                                <tr>
                                    <td>
                                        Subject
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsubjectName" CssClass="input" runat="server" ClientIDMode="Static">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        Quest. Pattern
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlQPattern" runat="server" CssClass="input" ClientIDMode="Static">
                                            <asp:ListItem Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Pattern Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQMarks" CssClass="input" runat="server" ClientIDMode="Static"> </asp:TextBox>
                                    </td>
                                    <td>
                                        Pass Marks
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPassMark" CssClass="input" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <td>
                                        Convert To
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtConvertTo" CssClass="input" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="2">
                                        <asp:Button ID="btnAdd" runat="server" ClientIDMode="Static" CssClass="btn btn-success" OnClientClick="return validateInputs();"
                                            Text="Add" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" CssClass="btn btn-primary"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="btn btn-default" OnClick="btnClear_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
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
                        </Triggers>
                        <ContentTemplate>
                            <div id="divscroll" style="height: 200px; max-height: 200px; overflow: auto; overflow-x: hidden;">
                                <asp:GridView ID="gvSubQPattern" runat="server" AutoGenerateColumns="false" 
                                    CssClass="table table-striped table-bordered tbl-controlPanel"
                                    ClientIDMode="Static" OnRowCommand="gvSubQPattern_RowCommand">
                                    <Columns>
                                        <asp:ButtonField CommandName="Remove" Text="Remove" ButtonType="Button" />
                                        <asp:BoundField DataField="SubId" HeaderText="SubId" Visible="false" />
                                        <asp:BoundField DataField="SubName" HeaderText="Subject" />
                                        <asp:BoundField DataField="QPId" HeaderText="QPId" Visible="false" />
                                        <asp:BoundField DataField="QPName" HeaderText="Pattern" />
                                        <asp:BoundField DataField="QMarks" HeaderText="P.Marks" />
                                        <asp:BoundField DataField="PassMarks" HeaderText="Pa.Marks" />
                                        <asp:BoundField DataField="ConvertTo" HeaderText="Convert" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("SubId") %>' />
                                                <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Bind("QPId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>                    
            </div>            
        </div>
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
                            <asp:AsyncPostBackTrigger ControlID="ddlClassList" />
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlExamTypeList" runat="server"
                                            CssClass="input" ClientIDMode="Static" OnSelectedIndexChanged="ddlClassList_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">...Select Exam Type...</asp:ListItem>
                                        </asp:DropDownList>                                    
                                        <asp:DropDownList ID="ddlClassList" runat="server"
                                            CssClass="input" ClientIDMode="Static" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlClassList_SelectedIndexChanged">
                                            <asp:ListItem Selected="True">...Select Class...</asp:ListItem>
                                        </asp:DropDownList>                                    
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" Text="Search" ClientIDMode="Static" runat="server" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnEdit" CssClass="btn btn-success" Text="Edit" ClientIDMode="Static" runat="server" OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnDelete" CssClass="btn btn-danger" Text="Delete" ClientIDMode="Static" runat="server" OnClick="btnDelete_Click" />
                                    </td>
                                </tr>
                                <tr runat="server" id="trGrouplist" visible="false">
                                    <td>
                                        <label class="checkbox-inline">
                                          <input type="checkbox" id="chkSciencelist" runat="server"> Science
                                        </label>
                                        <label class="checkbox-inline">
                                          <input type="checkbox" id="chkCommercelist" runat="server"> Commerce
                                        </label>
                                        <label class="checkbox-inline">
                                          <input type="checkbox" id="chkArtslist" runat="server"> Arts
                                        </label>
                                        <%--<asp:CheckBox ID="chkSciencelist" Text="Science" runat="server" Style="padding-right: 20px" />--%>
                                        <%--<asp:CheckBox ID="chkCommercelist" Text="Commerce" runat="server" Style="padding-right: 20px;" />
                                        <asp:CheckBox ID="chkArtslist" Text="Arts" runat="server" />--%>
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
                                style="width: 100%; min-height: 150px; max-height: 500px; overflow: auto; overflow-x: hidden; margin-top: 5px;">
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
        function validateInputs() {
            if (validateText('txtDes_Name', 1, 30, 'Enter Designation Name') == false) return false;
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
    </script>
</asp:Content>
