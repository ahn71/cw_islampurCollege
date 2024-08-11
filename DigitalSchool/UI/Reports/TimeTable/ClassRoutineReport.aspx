<%@ Page Title="Class Routine Report" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ClassRoutineReport.aspx.cs" Inherits="DS.UI.Reports.TimeTable.ClassRoutineReport" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
         .ajax__tab_tab
        {
            -webkit-box-sizing: content-box!important;
            -moz-box-sizing: content-box!important;
            box-sizing: content-box!important;
        }  
        .ajax__tab_default .ajax__tab_tab
        {
            display: inline!important;            
        } 
        .ajax__tab_xp .ajax__tab_header .ajax__tab_outer{
            height: 20px!important;
        }
        .Box {
            min-width:130px;
        }
        .box {
            min-width:220px;
        }
        div.col-sm-10.col-sm-offset-1 div.form-inline div.form-group label{ text-align: left; }
        div.col-sm-8.col-sm-offset-2 div.form-inline div.form-group label{ text-align: left; }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>               
                <%--<li><a runat="server" href="~/UI/Reports/TimeTable/ClassScheduleHome.aspx">Class Routine</a></li>--%>
               
                <li class="active">Class Routine</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>

    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
    <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Width="100%" TabIndex="1">
        <ajax:TabPanel ID="TabPanel5" TabIndex="1" runat="server" HeaderText="Class Routine">
            <ContentTemplate>
    <div  class="routine">
<div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">
  
    <asp:UpdatePanel runat="server" ID="upPrint">
        <ContentTemplate> 
             <div class="row tbl-controlPanel"> 
		        <div class="col-xs-12 col-sm-10 col-sm-offset-1">
			        <div class="form-inline">
				         <div class="form-group">
					         <label for="exampleInputName2">Shift</label>
						<asp:DropDownList runat="server" ID="ddlShift" ClientIDMode="Static"  CssClass="input form-control Box"  AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
						                                      
					        </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Batch</label>
					    <asp:DropDownList runat="server" ID="ddlBatch" ClientIDMode="Static" CssClass="input form-control Box"  AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Group</label>
					 <asp:DropDownList runat="server" ID="ddlGroup" ClientIDMode="Static" CssClass="input form-control Box"  AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Section</label>
					    <asp:DropDownList runat="server" ID="ddlSection" ClientIDMode="Static"  CssClass="input form-control Box"  AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2"></label>
						<asp:Button runat="server" ID="btnPrint" Text="Print Preview"  CssClass="btn btn-success" OnClick="btnPrint_Click" />
				         </div>
			        </div>
	          </div>
         </div>  
        </ContentTemplate>
    </asp:UpdatePanel>

</div><br />

<asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlShift" />
        <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
        <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
        <asp:AsyncPostBackTrigger ControlID="ddlSection" />
    </Triggers>
    <ContentTemplate>
        <div id="divRoutineInfo" style="width:100%" runat="server" >
        </div>
    </ContentTemplate>
</asp:UpdatePanel>



</div>
                </ContentTemplate>
            </ajax:TabPanel>
        <ajax:TabPanel ID="TabPanel1" TabIndex="2" runat="server" HeaderText="Teacher Class Routine">
        <ContentTemplate>        
        <div  class="routine">
<div style="text-align:center; border-bottom:1px solid #D2D2D2; padding:10px;">
  
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">        
        <ContentTemplate>
            <div class="row tbl-controlPanel">
                <div class="col-xs-12 col-sm-4 col-sm-offset-4 list">
                    <asp:RadioButtonList ID="rblReportType" runat="server" CssClass="radiobuttonlist" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="rblReportType_SelectedIndexChanged">
                        <asp:ListItem class="radio-inline" Selected="True" style="margin-left:10px" Value="0">Routine</asp:ListItem>
                        <asp:ListItem class="radio-inline" style="margin-left:10px" Value="1">Load Report</asp:ListItem>            
                    </asp:RadioButtonList> 
                </div>
            </div>
              <div class="row tbl-controlPanel"> 
		            <div class="col-xs-12 col-sm-8 col-sm-offset-2 boX">
			            <div class="form-inline">
				             <div class="form-group">
					             <label for="exampleInputName2"> Department</label>
						            <asp:DropDownList runat="server" ID="ddlDepartment" AutoPostBack="true" CssClass="input form-control box" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList>
				             </div>
				            <div class="form-group">
					             <label for="exampleInputName2">Teacher List</label>
					             <asp:DropDownList runat="server" ID="ddlTeacher" AutoPostBack="true" CssClass="input form-control box" OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged"></asp:DropDownList> 
				             </div>
				            <div class="form-group">
					             <label for="exampleInputName2"></label>
					             &nbsp;<asp:Button runat="server" ID="btnPrint_D" Text="Print Preview" Width="120px"
                                        CssClass="btn btn-success " OnClick="btnPrint_D_Click" /> 
				             </div>
			            </div>
	              </div>
             </div> 
          
  
                       
            <%-- <asp:Button runat="server" OnClick="btnPrint_Click" ID="btnPrint" Text="Print Preview" Width="120px" style=" height: 30px; margin-top: -8px; width: 120px; padding: 0 !important;" CssClass="greenBtn" />--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</div><br />
<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddlTeacher" />
         <asp:AsyncPostBackTrigger ControlID="ddlDepartment" />
        <asp:AsyncPostBackTrigger ControlID="rblReportType" />
    </Triggers>
    <ContentTemplate>
        <div id="divTeacherRoutine" style="width:100%" runat="server" >         
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
            </ContentTemplate>
            </ajax:TabPanel>
        </ajax:TabContainer>
        
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
