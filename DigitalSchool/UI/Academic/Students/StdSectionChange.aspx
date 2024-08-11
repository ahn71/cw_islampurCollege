<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StdSectionChange.aspx.cs" Inherits="DS.UI.Academic.Students.StdSectionChange" %>
<%@ Register Assembly="AjaxControlToolkit"  Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <%--<script src="../../../Scripts/jquery-1.8.2.js"></script>--%>
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
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlPreviousBatch" />                       
                        <asp:AsyncPostBackTrigger ControlID="ddlMainGroup" />
                    </Triggers>
        <ContentTemplate>
                <div class="tgPanelHead">Students Section Change</div>
               
                  
                
                
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
                    <label class="lal" for="exampleInputName2">Batch</label>
                    <asp:DropDownList ID="dlPreviousBatch" class="input controlLength form-control" runat="server" ClientIDMode="Static"
                       AutoPostBack="True" OnSelectedIndexChanged="dlPreviousBatch_SelectedIndexChanged">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divGroup" runat="server" visible="false" class="form-group">
                    <label class="lal" for="exampleInputName2">Group</label>
                    <asp:DropDownList ID="ddlMainGroup" class="input controlLength form-control" runat="server" ClientIDMode="Static"
                         AutoPostBack="True" OnSelectedIndexChanged="ddlMainGroup_SelectedIndexChanged">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="lal" for="exampleInputName2">Section</label>
                    <asp:DropDownList ID="ddlSection" class="input controlLength form-control" runat="server" ClientIDMode="Static">
                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                    </asp:DropDownList>
                </div>
                         <div class="form-group">
                    <label class="lal" for="exampleInputName2">Change Date</label>
                   <asp:TextBox ID="txtChangeDate" ClientIDMode="Static" runat="server" CssClass="input controlLength"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                    TargetControlID="txtChangeDate">
                                </asp:CalendarExtender>
                </div>
           
                <div class="form-group">

                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        OnClick="btnSearch_Click"/>
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
                        <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                                             
                    </Triggers>
        <ContentTemplate>
                <asp:Panel ID="admStdAssignPanel" runat="server" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                    <asp:GridView ID="gvstdlist" runat="server" CssClass="table table-bordered" DataKeyNames="ClsSecId"
                        AutoGenerateColumns="False" OnRowDataBound="gvstdlist_RowDataBound" >
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="stdID" Value='<%# DataBinder.Eval(Container.DataItem, "StudentId")%>' />
                                    <%# Container.DataItemIndex+1%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Roll No">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblRollNo" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblStdName" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Class">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblClass" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "ClassName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblGroup" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Current Section">
                                <ItemTemplate>                                    
                                    <asp:Label ID="lblSection" runat="server"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "SectionName")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                    
                            <asp:TemplateField HeaderText="New. Section">
                                <ItemTemplate>                                   
                                    <asp:DropDownList ID="ddlNewSection" Width="150px" runat="server" CssClass="input">
                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>                                                                                         
                                            <asp:CheckBox runat="server" ID="hdChk" Text="All" Checked="true" AutoPostBack="True" OnCheckedChanged="hdChk_CheckedChanged"  /><br />                                                     
                                        </HeaderTemplate>
                                        <ItemTemplate>                               
                                                    <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged"  />                                                
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <div class="tgbutton">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-primary"
                                     OnClick="btnSubmit_Click"  />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" ClientIDMode="Static" 
                                   />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
                    </asp:UpdatePanel>
            </div> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
