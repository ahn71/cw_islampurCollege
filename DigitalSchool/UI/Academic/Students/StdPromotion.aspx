<%@ Page Title="Students Promotion" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdPromotion.aspx.cs" Inherits="DS.UI.Academic.Students.StdPromotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
        }
        .controlLength {
            /*width: 150px;*/
        }
        .tbl-controlPanel td:first-child{
            text-align:right;
            padding-right: 5px;
        }
        .tbl-controlPanel {
            /*width:740px;*/
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .tgbutton{            
            padding: 10px 0;
            margin-left: 46%;
        }
        .datatables_wrapper{
            min-height: 0;
            max-height: 400px;
        }
         input[type="radio"]{
            margin: 5px;
        }
        .form-inline {
            text-align:center;
            margin:0px 10px;
        }
        .lal{text-align: left}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <script src="../../../Scripts/jquery-1.8.2.js"></script>
       <script type="text/javascript">

           var oldgridcolor;
           function SetMouseOver(element) {
               oldgridcolor = element.style.backgroundColor;
               element.style.backgroundColor = '#ffeb95';
               element.style.cursor = 'pointer';
               // element.style.textDecoration = 'underline';
           }
           function SetMouseOut(element) {
               element.style.backgroundColor = oldgridcolor;
               // element.style.textDecoration = 'none';

           }

</script>
    <asp:HiddenField ID="lblStudentId" ClientIDMode="Static" Value="" runat="server" />
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
                    <%--<a id="A1" runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <%--<li><a id="A2" runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>--%>
                <li><a runat="server" id="aAcademicHome">Academic Module</a></li>
                <li><a runat="server" id="aStudentHome">Student Module</a></li>
                <li class="active">Students Section Change</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    
            <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlPreviousBatch" />
                        <asp:AsyncPostBackTrigger ControlID="rdblStudentStatus" />
                        <asp:AsyncPostBackTrigger ControlID="ddlMainGroup" />
                    </Triggers>
        <ContentTemplate>
                <div class="tgPanelHead">Students Promotion Settings</div>
               
                    <div class="row tbl-controlPanel">
                        <div class="col-sm-6 col-sm-offset-3">
                            <asp:RadioButtonList ID="rdblStudentStatus" RepeatLayout="Flow" RepeatDirection="Horizontal" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="rdblStudentStatus_SelectedIndexChanged" RepeatColumns="3" >
                                <asp:ListItem class="radio-inline" Value="1" Selected="True">Regular Student Promotion</asp:ListItem>
                                <asp:ListItem class="radio-inline" Value="0">Fail Student Promotion</asp:ListItem>
                                <asp:ListItem class="radio-inline" Value="00">Fail Student Batch Assign</asp:ListItem>
                            </asp:RadioButtonList>                   
                      </div>
                   </div>
                </div>
                
            <br />
            <div class="row">
                <div class="col-sm-10 col-sm-offset-1">
            <div class="form-inline">
                <div class="form-group box">
                    <label class="lal" for="exampleInputName2">Shift</label>
                    <asp:DropDownList ID="dlShift" class="input controlLength form-control" runat="server" ClientIDMode="Static"
                        AutoPostBack="false">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="lal" for="exampleInputName2">Current Batch</label>
                    <asp:DropDownList ID="dlPreviousBatch" class="input controlLength form-control" runat="server" ClientIDMode="Static"
                        OnSelectedIndexChanged="dlPreviousBatch_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divGroup" runat="server" visible="false" class="form-group">
                    <label class="lal" for="exampleInputName2">Current Group</label>
                    <asp:DropDownList ID="ddlMainGroup" class="input controlLength form-control" runat="server" ClientIDMode="Static"
                        OnSelectedIndexChanged="ddlMainGroup_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="lal" for="exampleInputName2">Current Section</label>
                    <asp:DropDownList ID="ddlSection" class="input controlLength form-control" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="lal" for="exampleInputName2">New Batch</label>
                    <asp:DropDownList ID="dlCurrentBatch" runat="server" ClientIDMode="Static"
                        class="input controlLength form-control">
                    </asp:DropDownList>
                </div>
                <div class="form-group">

                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        OnClientClick="return btnSearch_validateInputs();" OnClick="btnSearch_Click" />
                </div>
            </div>
            </div>
         </div>            
        </ContentTemplate>
    </asp:UpdatePanel>
                  <br />
            </div>  
            <div class="tgPanel">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnUpdateStd" />
                         <asp:AsyncPostBackTrigger ControlID="rdblStudentStatus" />                       
                    </Triggers>
        <ContentTemplate>
                <asp:Panel ID="admStdAssignPanel" runat="server" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="gvstdlist" runat="server" CssClass="table table-bordered" DataKeyNames="NewBatchID,NewClassID"
                        AutoGenerateColumns="False" OnRowDataBound="gvstdlist_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="stdID" Value='<%# DataBinder.Eval(Container.DataItem, "Student.StudentId")%>' />
                                    <%# Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblStdName" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.FullName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crnt. Class">
                                <ItemTemplate>
                                    <asp:Label ID="lblPreCls" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.ClassName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crnt. Roll">
                                <ItemTemplate>
                                    <asp:Label ID="lblPreRoll" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.RollNo")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crnt. Group">
                                <ItemTemplate>
                                    <asp:Label ID="lblPreGroup" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Group.GroupName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Crnt. Section">
                                <ItemTemplate>
                                    <asp:Label ID="lblPreSection" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.SectionName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exam GPA">
                                <ItemTemplate>
                                    <asp:Label ID="lblExamGPA" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "GPA")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New. Roll">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNewRollNo" runat="server" CssClass="input" Width="50px"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "NewRoll")%>'></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New. Class">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblPromoClassName" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "NewClassName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New. Batch">
                                <ItemTemplate>
                                    <asp:Label ID="lblPromoBatch" runat="server" CssClass="text-center"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "NewBatchName")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                            
                             <asp:TemplateField HeaderText="New. Group">                                  
                                <ItemTemplate>                                   
                                    <asp:DropDownList OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" 
                                        AutoPostBack="true" ID="ddlGroup" Width="150px" runat="server" CssClass="input">
                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>           
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="New. Section">
                                <ItemTemplate>                                   
                                    <asp:DropDownList ID="ddlSection" Width="150px" runat="server" CssClass="input">
                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>                                                                                        
                                            <asp:CheckBox runat="server" ID="hdChk" Text="All" Checked="true" AutoPostBack="True" OnCheckedChanged="hdChk_CheckedChanged" /><br />                                                     
                                        </HeaderTemplate>
                                        <ItemTemplate>                               
                                                    <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged" />                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <div class="tgbutton">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnUpdateStd" runat="server" Text="Submit" CssClass="btn btn-primary"
                                    OnClientClick="return btnUpdateStd_validateInputs();" OnClick="btnUpdateStd_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" 
                                    OnClick="btnClear_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
                    </asp:UpdatePanel>
            </div>       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function btnSearch_validateInputs() {
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('dlPreviousBatch', "0", 'Select a Current Batch') == false) return false;
            if (validateCombo('dlCurrentBatch', "0", 'Select a New Batch') == false) return false;
            return true;
        };
        function btnUpdateStd_validateInputs() {
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('dlPreviousBatch', "0", 'Select a Current Batch') == false) return false;
            if (validateCombo('dlCurrentBatch', "0", 'Select a New Batch') == false) return false;
            if ($('#MainContent_gvstdlist tr').length == 0) {
                showMessage("Please Search First Before Saved", 'warning');
                return false;
            }
            return true;
        };
    </script>
</asp:Content>
