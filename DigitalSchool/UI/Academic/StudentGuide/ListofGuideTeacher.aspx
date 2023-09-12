<%@ Page Title="List of Guide Teacher" EnableEventValidation="false" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ListofGuideTeacher.aspx.cs" Inherits="DS.UI.Academic.StudentGuide.ListofGuideTeacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }

        input[type="checkbox"] {
            margin: 5px;
        }

        .table tr th {
            background-color: #23282C;
            color: white;
        }       

        .table th:nth-child(1),
        .table th:nth-child(3),
        .table th:nth-child(4), 
        .table th:nth-child(5), 
        .table th:nth-child(6),
        .table th:nth-child(7),
        .table th:nth-child(8) {
            text-align: center;
        }
         .controlLength {
         min-width:170px;
         
         }
         .form-inline {
        margin-top:5px;
        margin-bottom:5px;
         }
         @media only screen and (min-width: 320px) and (max-width: 479px) {
             .form-group {
             margin-right:10px;
             margin-left:10px;
             }
             .pagination {
                float:left;
             }

         
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
                <li><a id="A3" runat="server" href="~/UI/Academic/StudentGuide/StudentGuideHome.aspx">Guide Teacher Module</a></li> 
                <li class="active">Guide Teacher Wise Student list</li>
            </ul>
            <!--breadcrumbs end -->
        </div>   
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
       <Triggers>
           <asp:AsyncPostBackTrigger ControlID="ddlDept" />
            <asp:AsyncPostBackTrigger ControlID="ddlAdviserName" />
       </Triggers>
        <ContentTemplate>
           
                <div class="col-md-12">

                
         <div class="row">
        <div class="col-md-12">
        <div class="tgPanel" style="width:100%">        
              <div class="row"> 
                  <div class="col-md-3"></div>
                  <div class="col-md-6">  
                      <div class="form-inline">
                          <div class="form-group">
                                    <label for="exampleInputName2">Department</label>
                               <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged"
                                 AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"></asp:DropDownList>
                             </div>
                           <div class="form-group">
                                    <label for="exampleInputName2">Guide Teacher Name</label>
                               <asp:DropDownList ID="ddlAdviserName" OnSelectedIndexChanged="ddlAdviserName_SelectedIndexChanged"
                               AutoPostBack="true"  runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"></asp:DropDownList>
                             </div>
                          </div>                   
                          </div>
                  <div class="col-md-3"></div>
                  </div>                         
        </div>       
        </div>
             </div>          
           
       <div class="row">    
    <div class="col-sm-12">
     
         <div id="showParticular" runat="server" style="display: block; width: 100%; height: 100% auto; background-color: white; top: 60px">             
                        <asp:Panel ID="Panel2" runat="server" Width="100%" Height="100%">  
                            
                            <div class="tgPanel">                               
                            <asp:GridView ID="gvStudentList" runat="server" DataKeyNames="StudentId" 
                               OnRowCommand="gvStudentList_RowCommand"  CssClass="table table-striped table-bordered dt-responsive nowrap"
                                cellspacing="0" Width="100%" OnRowDeleting="gvStudentList_RowDeleting" AutoGenerateColumns="False" >

                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="StudentId" runat="server"
                                                Value='<%# DataBinder.Eval(Container.DataItem, "StudentId")%>' />
                                            <%# Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FullName">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStdName" Style="float: left" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBatch" Style=" text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "BatchName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShift" Style="text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ShiftName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Group">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroup" Style="text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "GroupName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Section">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsection" Style="width: text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "SectionName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Roll">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoll" Style="text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <asp:TemplateField >
                                        <HeaderTemplate >
                                            Delete
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="Delete" CssClass="btn btn-danger"
                                              ClientIDMode="Static" ID="btnAttendance" Text="Delete"  
                                                CommandArgument='<%#Eval("StudentId")%>' OnClientClick="return confirm('Are you sure, you want to delete the record?')" />
                                        </ItemTemplate>
                                    </asp:TemplateField>  

                                </Columns>
                            </asp:GridView>
                                  <%--  </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                </div>
                            <%--  </div>--%>
                        </asp:Panel>

                    </div>
            </div>
</div>
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
             //$(document).on("keyup", '.Search_New', function () {
             //    searchTable($(this).val(), 'MainContent_gvStudentList', '');
                 $('#MainContent_gvStudentList').dataTable({
                     "iDisplayLength": 10,
                     "lengthMenu": [10, 20, 30, 40, 50, 100]
                 });
             //});
         });
         function loadStudentInfo() {
             $('#MainContent_gvStudentList').dataTable({
                 "iDisplayLength": 10,
                 "lengthMenu": [10, 20, 30, 40, 50, 100]
             });
         }
    </script>
</asp:Content>
