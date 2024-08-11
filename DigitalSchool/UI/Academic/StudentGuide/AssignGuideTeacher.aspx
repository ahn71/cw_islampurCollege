<%@ Page Title="Assign Guide Teacher" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AssignGuideTeacher.aspx.cs" Inherits="DS.UI.Academic.StudentGuide.AssignGuideTeacher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        input[type="checkbox"] {
            margin: 5px;
        }

        .table tr th{
            background-color: #23282C;
            color: white;
        }

        .table th:nth-child(1),
        .table th:nth-child(3),
        .table th:nth-child(4), .table th:nth-child(5), .table th:nth-child(6) {
            text-align: center;
        }
        .dataTables_length, .dataTables_filter{
            display: none;
            padding: 15px;
        }
        #tblClassList_info {
             display: none;
            padding: 15px;
        }
         #tblClassList_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }

        .controlLength {
            min-width:115px;
            
        }
        .form-group {
            margin-top:10px;
        }

         @media only screen and (min-width: 320px) and (max-width: 479px) {
            
            .form-group {
                 margin:0px 10px;
             }
            .litleMargin {
                margin-top:10px;
            }
            .pagination {
            float:left;
            margin-top:10px;
            }
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <script src="../scripts/jquery-1.8.2.js"></script>
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
                <li class="active">Assign Guide Teacher</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    
    <div class="col-lg-12">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <Triggers>
            </Triggers>
            <ContentTemplate>
                <div class="tgPanel">           
                    <div class="row">
                        <div class="col-sm-8 col-sm-offset-2">
                            
                            <div class="form-inline">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">  
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                                    </Triggers>                                 
                                    <ContentTemplate>
                                <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                    <asp:DropDownList ID="ddlShiftList" runat="server" class="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">Batch</label>
                                    <asp:DropDownList ID="ddlBatch" runat="server" class="input controlLength form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divGroup" visible="false">
                                    <label for="exampleInputName2">Group</label>
                                    <asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength form-control" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ClientIDMode="Static" Enabled="true" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputName2">Section</label>
                                    <asp:DropDownList ID="ddlSection" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server"
                                    OnClientClick="return validateDropDown();" CssClass="btn btn-primary litleMargin" OnClick="btnSearch_Click" />
                                    </div>
                                  <div class="form-group">
                                    <label for="exampleInputName2">Guide Teacher</label>
                                    <asp:DropDownList ID="ddlTeacher" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>
                                  <div class="form-group">
                                <asp:Button ID="btnAssign" Text="Assign" ClientIDMode="Static" runat="server"
                                    OnClientClick="return validateDropDown();" CssClass="btn btn-primary litleMargin" OnClick="btnAssign_Click" />
                                    </div>            
                                        
                                            </ContentTemplate>
                                    </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="">
                    <div class="row">
                         <div class="dataTables_filter_New" style="float: right;margin-right:15px;">
                                    <label>
                                        Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                                    </label>
                                </div>
                   </div>
                        </div>
                    <div class="">
                    <div class="row">
                        <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">                               

                            <asp:GridView ID="gvStudentList" runat="server" Width="100%" DataKeyNames="StudentId"
                                 CssClass="table table-striped table-bordered dt-responsive nowrap"  CellSpacing="0" AllowPaging="true" AutoGenerateColumns="False" PageSize="25"
                                OnPageIndexChanging="gvStudentList_PageIndexChanging" OnRowDataBound="gvStudentList_RowDataBound">

                                <RowStyle HorizontalAlign="Center" />
                                <PagerStyle CssClass="gridview" />
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hidestdID" runat="server"
                                                Value='<%# DataBinder.Eval(Container.DataItem, "StudentId")%>' />
                                            <%# Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" Style="float: left" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roll">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoll" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gender">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgender" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Gender")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Mobile">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmobile" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Mobile")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderTemplate>
                                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="hdChk" Text="All" Checked="true" AutoPostBack="True" OnCheckedChanged="hdChk_CheckedChanged" /><br />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkStatus" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkStatus_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <%--  </div>--%>
                        </asp:Panel>
                            </div>
                    </div>
                        </div>
                    

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
           
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvStudentList', '');
                $('#MainContent_gvStudentList').dataTable({
                    "iDisplayLength": 10,
                    "lengthMenu": [10, 20, 30, 40, 50, 100]
                });
            });
        });
        function loadStudentInfo() {
            $('#MainContent_gvStudentList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
    </script>
</asp:Content>
