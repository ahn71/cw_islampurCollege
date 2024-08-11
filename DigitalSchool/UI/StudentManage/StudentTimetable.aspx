<%@ Page Title="" Language="C#" MasterPageFile="~/Student.Master" AutoEventWireup="true" CodeBehind="StudentTimetable.aspx.cs" Inherits="DS.UI.StudentManage.StudentTimetable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }

        .input {
            width: 200px;
        }

        .tbl-controlPanel {
            width: 400px;
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
                    <a runat="server" href="~/UI/StudentManage/StudentManage.aspx">
                        <i class="fa fa-dashboard"></i>
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </a>
                </li>                
                <li class="active">Timetable</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row"> 
        <div class="col-md-12">
        <div class="tgPanel" style="width:100%">        
              <div class="row"> 
                  <div class="col-md-4">         
            <table class="tbl-controlPanel" style="text-align:left">
                <tr>
                    <td>
                        Exam Name
                    </td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlExam" runat="server" Width="260px" ClientIDMode="Static" CssClass="input controlLength"></asp:DropDownList>
                    </td>
                </tr>
            </table> 
                      </div>
                  </div>                         
        </div>       
        </div>           
    </div>
   <br />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>               
                <div class="col-md-3">
                    <a id="A3" runat="server" onserverclick="A1_ServerClick">
                        <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="classroutine" src="../../../../Images/moduleicon/Reports/Class Schedule.ico">
                            </span>
                            <span>Class Routine</span>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a id="A2" runat="server" onserverclick="A2_ServerClick" onclick="return validateDropDown();">                       
                         <div class="mini-stat sub_mini_stat clearfix btn3d btn custom_menu_btn">
                            <span>
                                <img width="30" alt="classroutine" src="../../../../Images/moduleicon/Reports/Exam Schedule.ico">
                            </span>
                            <span>Exam Routine</span>
                        </div>
                    </a>                  
                </div>                            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     <div runat="server" id="divGv" class="col-md-12" style="margin-top:10px">             
                            <asp:GridView ID="gvExamSchedule"  CssClass="tbl-controlPanel1 gvExamSchedule"   runat="server"  GridLines="Both" AutoGenerateColumns="true"  EditRowStyle-HorizontalAlign="Center" >                                
                                <HeaderStyle Height="45px" HorizontalAlign="Center" />                               
                                    </asp:GridView>
                      </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateDropDown() {           
            if (validateCombo('ddlExam', 0, 'Select Exam Name') == false) return false;          
            return true;
        }
    </script>
</asp:Content>
