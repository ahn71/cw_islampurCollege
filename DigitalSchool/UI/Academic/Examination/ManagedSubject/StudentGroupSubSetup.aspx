<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentGroupSubSetup.aspx.cs" Inherits="DS.UI.Academic.Examination.ManagedSubject.StudentGroupSubSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .controlLength{
            min-width:200px;
            margin: 5px;
        }
        .tgPanel
        {
            width: 100%;
        } 
        #MainContent_gvstdgrpsubsetuplist
        {
            width:100%;            
        }
        #MainContent_gvstdgrpsubsetuplist th:nth-child(1), 
        #MainContent_gvstdgrpsubsetuplist th:nth-child(3),
        #MainContent_gvstdgrpsubsetuplist th:nth-child(4)
        {
            text-align: center;
        }
        .litleMargin{
            margin-right: 5px;
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
         @media only screen and (min-width: 320px) and (max-width: 479px) {

            #btnProcess {
                margin-left:5px;
            }
        
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
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
                <li><a id="A3" runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li><a id="A4" runat="server" href="~/UI/Academic/Examination/ManagedSubject/SubjectManageHome.aspx">Subject Management</a></li>
                <li class="active">Student Group Subject Setup</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="tgPanelHead">Set  Optional Subject</div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="row tbl-controlPanel">
                            <div class="col-sm-8 col-sm-offset-2">
                                 <div class="form-inline">
                                      <div class="form-group">
                                        <label for="exampleInputName2">Shift</label>
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                                        </asp:DropDownList>
                                      </div>
                                      <div class="form-group">
                                        <label for="exampleInputName2">Batch</label>
                                        <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" ClientIDMode="Static"
                                            OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" CssClass="input controlLength form-control">
                                        </asp:DropDownList>
                                      </div>
                                      <div class="form-group">
                                        <label for="exampleInputName2">Group</label>
                                        
                                          <asp:DropDownList ID="dlGroup" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:DropDownList>
                                      </div>
                                      <div class="form-group">
                                         <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server"
                                            OnClientClick="return validateInputs();" CssClass="btn btn-primary" OnClick="btnProcess_Click" />
                                        <span style="position: absolute">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="UpdatePanel2">
                                                <ProgressTemplate>
                                                    <img class="LoadingImg" src="../../../../AssetsNew/images/input-spinner.gif" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </span>
                                      </div>
                                      
                                    </div>
                                </div>
                            </div>
                            <%--<table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift</td>
                                    <td>
                                        <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" CssClass="input controlLength">
                                        </asp:DropDownList></td>
                                    <td>Batch</td>
                                    <td>
                                        <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" ClientIDMode="Static"
                                            OnSelectedIndexChanged="dlBatch_SelectedIndexChanged" CssClass="input controlLength">
                                        </asp:DropDownList>
                                    </td>
                                    <td>Group</td>
                                    <td>
                                        <asp:DropDownList ID="dlGroup" runat="server" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList></td>

                                    <td>
                                        <asp:Button ID="btnProcess" Text="Process" ClientIDMode="Static" runat="server"
                                            OnClientClick="return validateInputs();" CssClass="btn btn-primary" OnClick="btnProcess_Click" />
                                        <span style="position: absolute">
                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" ClientIDMode="Static" AssociatedUpdatePanelID="UpdatePanel2">
                                                <ProgressTemplate>
                                                    <img class="LoadingImg" src="../../../AssetsNew/images/input-spinner.gif" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </span>
                                    </td>
                                </tr>
                            </table>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <!--Student Group Subject Setup-->
        <div class="">
            <div class="row">
                <div class="col-md-12">
                    <div class="head">
                        <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnProcess').click();" />
                        <div class="dataTables_filter_New" style="float: right;">
                            <label>
                                Search:
                    <input type="text" class="Search_New" placeholder="type here" />
                            </label>
                        </div>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tgPanel">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnProcess" />
                                
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="admStdAssignPanel" runat="server" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                                    <asp:GridView ID="gvstdgrpsubsetuplist" runat="server" CssClass="table table-bordered" DataKeyNames="StudentId"
                                        OnRowDataBound="gvstdgrpsubsetuplist_RowDataBound"  AutoGenerateColumns="False" 
                                         OnPageIndexChanging="gvstdgrpsubsetuplist_PageIndexChanging">
                                        <PagerStyle CssClass="gridview" />
                                        <RowStyle CssClass="str" />
                                        <Columns>
                                            
                                            <asp:TemplateField HeaderText="S.No"   ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:HiddenField runat="server" ID="stdID" Value='<%# DataBinder.Eval(Container.DataItem, "StudentId")%>' />
                                                    <%# Container.DataItemIndex+1%>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Student Name">
                                                <ItemTemplate >
                                                    <asp:Label ID="lblStdName" runat="server"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Roll">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRoll" runat="server" CssClass="text-center"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Section">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSection" runat="server" CssClass="text-center"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "SectionName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mandatory Subject">
                                                <ItemTemplate>
                                                    <asp:CheckBoxList RepeatColumns="2"  ID="checkMendatory" Width="300px" runat="server" CssClass="input mend_sub">
                                                    </asp:CheckBoxList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Optional Subject">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList RepeatColumns="2"  ID="checkOptinal" Width="300px" runat="server" CssClass="input mend_sub">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnSave" OnClick="btnSave_Click" Text="Save" runat="server" CssClass="btn btn-success custom_save_btn"></asp:Button>
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
           </div>
        <!-- END-->
        <div class="row">
            <div class="col-md-12">
                <div class="tgPanel">
                    <div class="widget">
                   <%-- <div class="head">
                        <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnProcess').click();" />
                        <div class="dataTables_filter_New" style="float: right;">
                            <label>
                                Search:
                    <input type="text" class="Search_New" placeholder="type here" />
                            </label>
                        </div>
                    </div>--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnProcess" />
                            </Triggers>
                        <ContentTemplate>
                            <div runat="server" id="divStduentInfo" class="datatables_wrapper" style="width: 100%; height: auto"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {           
            $(document).on("keyup", '.Search_New', function () {
                searchTable1($(this).val(), 'MainContent_gvstdgrpsubsetuplist', '');
            });
        });
        function validateInputs() {
            if (validateCombo('dlShift', '0', 'Select Shift Name') == false) return false;
            if (validateCombo('dlBatch', '0', 'Select Batch Name') == false) return false;
            //if (validateCombo('dlGroup', '0', 'Select Group Name') == false) return false;
            return true;
        }
        function setOS(celldata) // os= Optional Subject
        {
            var getId = celldata.id;
            var splitedData = getId.split("_");
            var getVal = celldata.value;
            $('#opt_' + getId).val(getVal);
            jx.load('ForUpdate.aspx?tbldata=' + splitedData + '&val=' + getVal + '&do=attUpdate', function (data) {
            });
        }
    </script>
</asp:Content>
