<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SetDependencyExam.aspx.cs" Inherits="DS.UI.Academic.Examination.SetDependencyExam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .tgPanel {
            width: 100%;
        }
        input[type="checkbox"]{
            margin: 3px;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblExId" ClientIDMode="Static" runat="server" />
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
                <li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Examination/ExamInfo.aspx">Exam Info</a></li>
                <li class="active">Add Exam Type</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right">Exam Details Information</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <div class="col-md-2"></div>
            <div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                        <asp:AsyncPostBackTrigger ControlID="ddlExamId" />
                        <asp:AsyncPostBackTrigger ControlID="chkExamTypeList" />
                        <asp:AsyncPostBackTrigger ControlID="chkSelectAll" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="tgPanel">
                        <div id="divExamList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;">

                            <asp:GridView ID="gvExamIdList" runat="server" AutoGenerateColumns="False" HeaderStyle-BackColor="Black"  HeaderStyle-ForeColor="White" HeaderStyle-Height="43px" OnRowCommand="gvExamIdList_OnRowCommand" OnRowDeleting="gvExamIdList_OnRowDeleting"  AllowPaging="True" PageSize="20" OnPageIndexChanging="gvExamIdList_PageIndexChanging"  >
                               <PagerStyle CssClass="gridview" />
                                 <Columns>
                                    <asp:BoundField DataField="ParentExInId" HeaderText="Final Exam Identity"  />
                                   
                                    <asp:TemplateField>
                                        <HeaderTemplate>Delete</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' OnClientClick="return confirm('Are you sure to delete ?')" CommandName="Delete" ImageUrl="~/Images/gridImages/delete.gif" runat="server" Width="20" style="margin-left:13px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                                      
                                </Columns>
                                <HeaderStyle BackColor="Black" ForeColor="White" Height="40px" />
                            </asp:GridView>
                        </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="tgPanelHead">Add Exam Type</div>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Batch Name
                                    </td>
                                    <td>
                                         <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" Width="261px"
                                            CssClass="input"  AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    
                                </tr>
                                <tr>
                                    <td>Exam Type
                                    </td>
                                    <td>
                                         <asp:DropDownList ID="ddlExamId" runat="server" ClientIDMode="Static" Width="261px"
                                            CssClass="input"  AutoPostBack="true" OnSelectedIndexChanged="ddlExamId_SelectedIndexChanged">
                                        </asp:DropDownList></td>
                                    
                                </tr>

                                <tr id="trSelectAll" runat="server" visible="false" >
                                    <td>
                                    </td>
                                    <td>
                                      <asp:CheckBox ID="chkSelectAll" runat="server" Text="Select All" AutoPostBack="True" OnCheckedChanged="chkSelectAll_CheckedChanged"  />
                                    </td>
                                    
                                </tr>

                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                      <asp:CheckBoxList ID="chkExamTypeList" runat="server" RepeatLayout="Flow" OnSelectedIndexChanged="chkExamTypeList_SelectedIndexChanged" AutoPostBack="true" ForeColor="Red"  ></asp:CheckBoxList>
                                    </td>
                                    
                                </tr>
                                 <tr>
                                    <td></td>
                                    <td>
                                       <asp:CheckBox runat="server" ClientIDMode="Static" ID="cbIsFinal" Checked="false" Text="As Final Exam" />
                                    </td>
                                </tr>
                                 
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button CssClass="btn btn-primary" ID="btnSave" runat="server" Text="Save" ClientIDMode="Static"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click"/>
                                        &nbsp;<asp:Button runat="server" ID="btnClear" Text="Reset" CssClass="btn btn-default" OnClientClick="return clearIt();"/>
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function clearIt() {
            $('#ddlBatch').val(0);
            $('#ddlExamId').val(0);
            //$('#txtDate').val('');
            //$('#dlExamType').val(0);
            // setFocus('txtDistrict');
            // $("#btnSaveDistrict").val('Save');
        }

        function editExam(Id) {
           
           
            $('#lblCourseId').val(Id);
            var strAT = $('#r_' + Id + ' td:first-child').html();
            var serverURL = window.location.protocol + "//" + window.location.host + "/";

            var result = confirm("Are you sure to delete ?");
            if (result) {
             //   alert(strAT);
                jx.load(serverURL + 'UI/Academic/Examination/ForUpdate.aspx?tbldata=ExamDependencyInfo,' + strAT + '&val=' + Id + '&do=GetSubjectStatus', function (data) {

                    $('#divExamList').html(data);

                });
            }
           

        }
        </script>
       
</asp:Content>
