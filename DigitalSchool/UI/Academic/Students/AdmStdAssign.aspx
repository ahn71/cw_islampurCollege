<%@ Page Title="Admission Student Assign" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdmStdAssign.aspx.cs" Inherits="DS.UI.Academic.Students.AdmStdAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .controlLength {
            width: 150px;
        }
        .tbl-controlPanel {
            
        }
        .tbl-controlPanel td:nth-child(1),
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5) {
            padding: 0px 5px;
        }
            .tbl-controlPanel tr:nth-child(1) {
                text-align:left;
            }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        .litleMargin {
            margin-left: 5px;
        }
        .btn {
            margin: 3px;
        }
        .btnRadio {
            padding: 3px;
        } 
        .tgbutton{            
            padding: 10px 0;
            margin-left: 46%;
        }
        .datatables_wrapper{
            min-height: 0;
            max-height: 400px;
        }
        input[type="checkbox"]{
            margin: 5px;
        }
        fieldset.scheduler-border {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 0.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.scheduler-border {
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
        }
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
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li>
                <li class="active">Admission Student Assign</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlClass" />
                <asp:AsyncPostBackTrigger ControlID="ddlsearchGroup" />               
            </Triggers>
            <ContentTemplate>
                <div class="tgPanelHead">Admission Student Assign</div>
                <div class="row">
                    <div class="col-lg-1"></div>
                    <div class="col-lg-5">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border"><b>Search Class Wise Student</b></legend>
                            <div class="row tbl-controlPanel">
                                <div class="col-md-6">
                                    <label class="col-md-3">Shift</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="dlShift" runat="server"  ClientIDMode="Static" CssClass="input form-control">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="col-md-3">Class</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="dlClass" runat="server"  ClientIDMode="Static"
                                            OnSelectedIndexChanged="dlClass_SelectedIndexChanged" AutoPostBack="true" CssClass="input form-control">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row tbl-controlPanel">
                                <div class="col-md-6">
                                    <label class="col-md-3">Group</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlsearchGroup" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlsearchGroup_SelectedIndexChanged" ClientIDMode="Static" CssClass="input form-control">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    
                                </div>
                                <div class="col-md-6">
                                    <label class="col-md-3">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin"
                                            OnClientClick="return btnSearch_validation();" OnClick="btnSearch_Click" />
                                        <asp:Label ID="lbltotal" ClientIDMode="Static" ForeColor="Green" runat="server" CssClass="control-label"></asp:Label>

                                    </label>
                                    <div class="col-md-9"></div>
                                </div>
                            </div>
                            
                        </fieldset>
                    </div>
                    <div class="col-lg-5">
                        <fieldset class="scheduler-border">
                            <legend class="scheduler-border">
                                <b>Assign Batch</b>
                            </legend>
                            <div class="row tbl-controlPanel">
                                <div class="col-md-6">
                                    <label class="col-md-3">Batch</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlBatch" runat="server" ClientIDMode="Static" CssClass="input form-control">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label class="col-md-3">Section</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlSection" runat="server" ClientIDMode="Static" CssClass="input form-control">
                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row tbl-controlPanel">
                                <div class="col-sm-6">
                                    <label class="col-sm-3"></label>
                                    <div class="col-xs-12 col-sm-9">
                                        <asp:Button ID="btnSave" runat="server" Text="Assign" class="btn btn-primary" ClientIDMode="Static"
                                            OnClientClick="return btnSave_validateInputs();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <label class="col-sm-3"></label>
                                    <div class="col-sm-9"></div>
                                </div>
                            </div>
                           
                        </fieldset>
                    </div>
                    <div>
                        <div class="col-lg-1"></div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" />            
        </Triggers>
        <ContentTemplate>
            <div class="tgPanel">
                <asp:Panel ID="admStdAssignPanel" runat="server" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="admStdAssignView" runat="server" DataKeyNames="ClassID" CssClass="table table-bordered"
                       OnRowDataBound="admStdAssignView_RowDataBound"  AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hideStdId" runat="server"
                                        Value='<%# DataBinder.Eval(Container.DataItem, "Student.StudentID")%>' />
                                    <%# Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission No">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmNo" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "AdmissionNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdmDate" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "AdmissionDate","{0:dd-MM-yyyy hh:mm:ss tt}")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStdName" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.FullName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Roll">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRoll" runat="server" ClientIDMode="Static"
                                        Width="50px" CssClass="input"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>                                                                                        
                                            <asp:CheckBox runat="server"  ID="hdChk" Text="All" Checked="true" AutoPostBack="True" OnCheckedChanged="hdChk_CheckedChanged" /><br />                                                     
                                        </HeaderTemplate>
                                        <ItemTemplate>                               
                                                    <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged" />                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <%-- <div class="tgbutton">
                    <table>
                        <tr>
                            <td>
                                
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" 
                                    OnClick="btnClear_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function btnSearch_validation() {
            if (validateCombo('dlShift', "0", 'Select a Shift Name') == false) return false;
            if (validateCombo('dlClass', "0", 'Select a Class Name') == false) return false;
            return true;
        };
        function btnSave_validateInputs() {
            if (validateCombo('dlShift', "0", 'Select a Shift Name') == false) return false;
            if (validateCombo('dlClass', "0", 'Select a Class Name') == false) return false;
            if (validateCombo('ddlBatch', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('ddlSection', "0", 'Select a Section Name') == false) return false;
            var chk = $('#chkAll').is(":checked");
            if (chk == false) {
                if (validateText('txtstdAmount', 1, 5, 'Enter How Many Student Assign this Section') == false) return false;
            }
            if ($('#admStdAssignView tr').length == 0) {
                showMessage("Please Search First Before Saved", 'warning');
                return false;
            }
            return true;
        };
    </script>
</asp:Content>
