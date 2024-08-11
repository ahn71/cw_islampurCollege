<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="MonthlyTest.aspx.cs" Inherits="DS.UI.Academic.Examination.MonthlyTest" %>
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
        #MainContent_gvStudentList_length{
            display: none;
            padding: 15px;
        }
        #MainContent_gvStudentList_info {
             display: none;
            padding: 15px;
        }
         #MainContent_gvStudentList_paginate {
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
        .monthly-test-table{
            margin:20px;
        }
        .monthly-test-table>tbody>tr>td{
            padding:8px;
        }
        .mtsb{
            margin-bottom:5px;
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
            .monthly-test-table > tbody > tr > td {
	            padding: 3px;
	            float: left;
	            width: 100%;
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
                 <li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Examination/ExamHome.aspx">Examination Module</a></li>
                <li class="active">Monthly Test</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    
    <div class="col-lg-12">  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">  
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlExamId" /> 
                                        <asp:AsyncPostBackTrigger ControlID="btnAssign" />                                       
                                    </Triggers>                                 
                                    <ContentTemplate>      
                <div class="tgPanel">                            
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <table class="monthly-test-table">
                                <tbody>
                                <tr>
                                    <td><label class="" for="exampleInputName2">Shift</label></td>
                                    <td><asp:DropDownList ID="ddlShiftList" runat="server" class="input controlLength form-control">
                                        </asp:DropDownList></td>
                                    <td><label class="" for="exampleInputEmail2">Batch</label></td>
                                    <td><asp:DropDownList ID="ddlBatch" runat="server"  class="input controlLength form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                     </td>
                                    <td><label class="" for="exampleInputName2">Group</label></td>
                                    <td><asp:DropDownList ID="ddlgroup" runat="server" class="input controlLength form-control" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ClientIDMode="Static" Enabled="true" AutoPostBack="True"></asp:DropDownList>
                                             </td>
                                    <td> <label class="" for="exampleInputName2">Section</label></td>
                                    <td><asp:DropDownList ID="ddlSection" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                     </td>
                                </tr>
                                <tr>
                                    <td><label class="" for="exampleInputName2">Exam Name</label></td>
                                    <td><asp:DropDownList ID="ddlExamId" OnSelectedIndexChanged="ddlExamId_SelectedIndexChanged" AutoPostBack="true" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                   </td>
                                    <td><label class="" for="exampleInputName2">Pattern Marks</label></td>
                                    <td><asp:TextBox ID="txtpatternmarks" runat="server" class="input controlLength form-control">
                                    </asp:TextBox></td>
                                    <td><label class="" for="exampleInputEmail2">Pass Marks</label></td>
                                    <td><asp:TextBox ID="txtpassmarks" runat="server" class="input controlLength form-control" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True"></asp:TextBox>
                                </td>
                                </tr>
                              </tbody>
                            </table>
                                                                  
                        </div>
                        <div class="col-md-1"></div>
                    </div>

                    <div class="">
                   <%-- <div class="row">
                         <div class="dataTables_filter_New" style="float: right;margin-right:15px;">
                                    <label>
                                        Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                                    </label>
                                </div>
                   </div>--%>
                        </div>
                    <div class="">
                    <div class="row">
                        <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">                               

                            <asp:GridView ID="gvStudentList" runat="server" Width="100%" DataKeyNames="StudentId"
                                 CssClass="table table-striped table-bordered dt-responsive nowrap"  CellSpacing="0" AllowPaging="false" AutoGenerateColumns="False"
                                OnRowDataBound="gvStudentList_RowDataBound">

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

                                    <asp:TemplateField HeaderText="Obtained Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtobtainedmarks"  CssClass="input controlLength form-control" Style="width: 50px; text-align: center;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "Obtainmarks")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    

                                </Columns>
                            </asp:GridView>
                            <%--  </div>--%>
                        </asp:Panel>
                            </div>
                    </div>
                        </div>                
                    <div class="row">
                        <div class="col-sm-10">
                            </div>
                         <div class="col-sm-2">
                            
                            <div class="form-inline">
                        <div class="form-group">
                                <asp:Button ID="btnAssign" Text="Submit" ClientIDMode="Static" runat="server"
                                    OnClientClick="return validateDropDown();" CssClass="btn btn-primary litleMargin mtsb" OnClick="btnAssign_Click" />
                                    </div> 
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
            loadStudentInfo();
        });
        function loadStudentInfo() {            
            $('#MainContent_gvStudentList').dataTable({
                "iDisplayLength": 1000
                //"lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function loadMessage() {
            showMessage('Submit Successfully', 'success');
            loadStudentInfo();
        }
    </script>
</asp:Content>
