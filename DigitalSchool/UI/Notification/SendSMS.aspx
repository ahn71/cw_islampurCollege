 <%@ Page Title="Send SMS" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SendSMS.aspx.cs" Inherits="DS.UI.Notification.SendSMS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .tgPanel {
            width: 100%;
        }      
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
            width:23%;
        }
        .tbl-controlPanel td:nth-child(2){           
            width: 50%;
            padding-right:5px;
        }
        .charCount {
            padding: 0px;
            list-style: outside none none;
            margin: 0px;
        }
        .charCount li{
            display: inline;
        }
        table.tbl-controlPanel1 {
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 621px;
        }
        table.tbl-controlPanel1 tr{
            margin-bottom: 5px;
        }
        table.tbl-controlPanel1 td{
            width: 25%;
           padding-bottom: 5px;
        }
        table.tbl-controlPanel1 td:first-child,
        table.tbl-controlPanel1 td:nth-child(3){
            width: 15%;
            text-align: right;
            padding-right: 5px;
        }
        .litleMarging{
            margin-left: 5px;
        }
        .extraMargin {
            margin-right : 2px;
        }        
        .tahoma {
            text-align:center;
        }        
        #MainContent_CalendarExtender1_daysTable td,
        #MainContent_CalendarExtender1_daysTable td:first-child,
        #MainContent_CalendarExtender1_daysTable td:nth-child(3),
        #MainContent_CalendarExtender2_daysTable td,
        #MainContent_CalendarExtender2_daysTable td:first-child,
        #MainContent_CalendarExtender2_daysTable td:nth-child(3),
        #MainContent_CalendarExtender3_daysTable td,
        #MainContent_CalendarExtender3_daysTable td:first-child,
        #MainContent_CalendarExtender3_daysTable td:nth-child(3){
            width: auto;
            margin: 0;
            padding: 0;
        }
        .ajax__calendar_footer {
            height: auto !important;
        }
        .btnRadio {
            padding: 3px;
        }
        .table tr th{
            background-color: #23282C;
            color: white;
        }
        table.tbl-controlPanel2 {
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 100%;
        }
        table.tbl-controlPanel2 tr{
            margin-bottom: 5px;
        }
        table.tbl-controlPanel2 td{
            width: 25%;
           padding-bottom: 5px;
        }
        table.tbl-controlPanel2 td:first-child,
        table.tbl-controlPanel2 td:nth-child(3){
            width: 5%;
            text-align: right;
            padding-right: 5px;
        }      
        table.tbl-controlPanel3 {
            font-family: Calibri;
            font-size: 15px;
            margin: 10px auto;
            padding: 5px;
            width: 100%;
        }
        table.tbl-controlPanel3 tr{
            margin-bottom: 5px;
        }
        table.tbl-controlPanel3 td{
            width: 25%;
           padding-bottom: 5px;
        }  
        table.tbl-controlPanel3 td:first-child,
        table.tbl-controlPanel3 td:nth-child(3)
        {
            width:17%;
            text-align: right;
            padding-right: 5px;
        }         
        legend{
            font-size:15px;
            margin-bottom:2px;
        }
        hr{
            margin-bottom: 5px;
            margin-top: 5px;
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
        .nav.nav-tabs{
		border: none;
	}
	.nav.nav-tabs.custom_tab li a{
		background: #AEC785 none repeat scroll 0 0;
		margin-right: 20px;
		border-radius: 0;
		color: #fff;
		border: none;
		padding: 10px 20px;
	}
	.nav.nav-tabs.custom_tab li a:hover{
		background: #AEC785 none repeat scroll 0 0;
		color:#444;
	}
    
	.nav-tabs.custom_tab > li.active > a, .nav-tabs.custom_tab > li.active > a:focus, .nav-tabs.custom_tab > li.active > a:hover {
	  border: none;
	  background: #fff none repeat scroll 0 0;
      color:#444;
	}
	.nav.nav-tabs.custom_tab li a::after {
		  background: #AEC785 none repeat scroll 0 0;
		  bottom: 0;
		  content: "";
		  height: 100%;
		  position: absolute;
		  right: -7px;
		  transform: skewX(30deg);
		  width: 20px;
		}
        .dat {
        min-width:100px;
        }
        .heading-border-bottom-5 {
        font-size:18px;
        }
        .det {
        width:100!important;
        }
         @media (min-width: 320px) and (max-width: 480px) {
            .nav-tabs li {
            
          width:100%;;
          margin-top:1px;
        
          border-left:none;
          border-right:none;
            }
            .MainContent_alitab1 {
                margin-top:2px
            }
            .det {
        width:none;
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
    <asp:HiddenField ID="lblSmsBodyTitle" ClientIDMode="Static" runat="server"/>
    <asp:HiddenField ID="lblHidetabIndex" ClientIDMode="Static" runat="server" Value="0"/>
    <asp:HiddenField ID="hdnfldTabSequence" ClientIDMode="Static" runat="server" Value="0"/>   
   
        
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
                <li class="active">Send SMS</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>


                    <div class="row">
                        <div class="col-md-8">
                            <div class="tgPanel">
                                <div class="tgPanelHead">SMS For

                                    <asp:TextBox runat="server" ID="txtTestMobileNo" ClientIDMode="Static"   BackColor="#23282c" BorderStyle="None" Text=""></asp:TextBox>
                                </div>
        
                                    <div class="row tbl-controlPanel">
                                        <div class="col-md-12">
                                            <!-- Nav tabs -->
	                                      <ul class="nav nav-tabs custom_tab" role="tablist">
	                                        <li role="presentation" class="active" id="alitab1" runat="server"><a href="#MainContent_home" aria-controls="home"  role="tab" data-toggle="tab">ABSENT STUDENT</a></li>
	                                        <li role="presentation"  id="alitab2" runat="server"><a href="#MainContent_profile" aria-controls="profile" role="tab" data-toggle="tab">FAIL STUDENT LIST</a></li>
	                                        <li role="presentation"  id="alitab3" runat="server" ><a href="#MainContent_messages" aria-controls="messages" role="tab" data-toggle="tab">NOTICE</a></li>
	                                        <li role="presentation" id="alitab4" runat="server"><a href="#MainContent_settings" aria-controls="settings" role="tab"  data-toggle="tab">GREETING</a></li>
	                                      </ul>

	                                      <!-- Tab panes -->
	                                      <div class="tab-content solid-tabs">
	                                        <div role="tabpanel" class="tab-pane active" id="home" runat="server">
	    	                                    <div class="l-container animation-slideUp">
	                                                <div class="l-row">
	                                                  <div class="l-col-12">
	                                                    <h2 class="heading-border-bottom-5">ABSENT STUDENT</h2>
                                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"> 
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                                                                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                    
                                                                <div class="row tbl-controlPanel"> 
                                                                    <div class="col-sm-10 col-sm-offset-1">
                                                                        <div class="form-inline">
                                                                             <div class="form-group">
                                                                                 <label for="exampleInputName2">Shift</label>
                                                                                     <asp:DropDownList ID="dlShift" runat="server" CssClass="input form-control" ClientIDMode="Static">
                                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>                                                    
                                                                            </asp:DropDownList>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2">Bacth</label>
                                                                                     <asp:DropDownList ID="dlBatch" runat="server" CssClass="input form-control" ClientIDMode="Static" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                             </div>
                                                                            <div class="form-group" runat="server" visible="false" id="tdgrplbl">
                                                                                 <label for="exampleInputName2" runat="server" visible="false" id="tdgrp">Group</label>
                                                                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="input form-control" ClientIDMode="Static" AutoPostBack="true"
                                                                                 OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2">Section</label>
                                                                                    <asp:DropDownList ID="dlSection" runat="server" CssClass="input form-control" 
                                                                                        ClientIDMode="Static">
                                                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                             </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row tbl-controlPanel"> 
                                                                    <div class="col-sm-10 col-sm-offset-1">
                                                                        <div class="form-inline">
                                                                             <div class="form-group">
                                                                                 <label for="exampleInputName2">From Date</label>
                                                                                   <asp:TextBox ID="txtFromDate" runat="server" CssClass="input form-control dat" ClientIDMode="Static"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MM-yyyy"
                                                                                TargetControlID="txtFromDate">
                                                                            </asp:CalendarExtender>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2">To Date</label>
                                                                                 <asp:TextBox ID="txtDate" runat="server" CssClass="input form-control dat" ClientIDMode="Static"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                                                TargetControlID="txtDate">
                                                                            </asp:CalendarExtender>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2"></label>
                                                                                  <asp:Button ID="btnSearch" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                                                OnClientClick="return btnSearch_validation();" OnClick="btnSearch_Click" />
                                                                             </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                               
                                                                <hr />
                                                                <asp:Panel ID="absentgridPanel" runat="server" Height="263px" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                                                                    <asp:GridView ID="adsentStdView" runat="server" DataKeyNames="AbsentStdID" CssClass="table table-bordered table-strip"
                                                                        ClientIDMode="Static" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkall" runat="server" 
                                                                                        OnClick="javascript:CheckAll(this,adsentStdView.id);" Checked="true"/>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkIndivisual" runat="server" 
                                                                                        OnClick="javascript:singleChk(this,adsentStdView.id);" Checked="true"/>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="btnRadio" />
                                                                                <ItemStyle CssClass="btnRadio" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                                                            <asp:BoundField DataField="Roll" HeaderText="Roll" />
                                                                            <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                                                            <asp:BoundField DataField="Section" HeaderText="Section" />
                                                                            <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                            <asp:BoundField DataField="GuardiantMobile" HeaderText="Guardiant Mobile" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
	                                                  </div>
	                                                </div>
	                                              </div>
	                                        </div>
	                                        <div role="tabpanel" class="tab-pane" id="profile" runat="server">
	    	                                    <div class="l-container animation-slideUp">
                                                    <div class="l-row">
                                                      <div class="l-col-12">
                                                        <h2 class="heading-border-bottom-5">FAIL STUDENT LIST</h2>
                                                          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnSearchExmID" />
                                                                <asp:AsyncPostBackTrigger ControlID="btnSearchFailStd" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <div class="row tbl-controlPanel"> 
                                                                    <div class="col-sm-12">
                                                                        <div class="form-inline">
                                                                             <div class="form-group">
                                                                                 <label for="exampleInputName2">From Date</label>
                                                                                        <asp:TextBox ID="txtFDate" runat="server" CssClass="input form-control det" ClientIDMode="Static"></asp:TextBox>
                                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy"
                                                                                TargetControlID="txtFDate">
                                                                            </asp:CalendarExtender>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2">To Date</label>
                                                                                    <asp:TextBox ID="txtTDate" runat="server" CssClass="input form-control det" 
                                                                                            ClientIDMode="Static"></asp:TextBox>
                                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd-MM-yyyy"
                                                                                            TargetControlID="txtTDate">
                                                                                        </asp:CalendarExtender>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2"></label>
                                                                                    <asp:Button ID="btnSearchExmID" runat="server" Text="Search Exam" ClientIDMode="Static"
                                                                                CssClass="btn btn-warning litleMarging" 
                                                                                OnClick="btnSearchExmID_Click"
                                                                                OnClientClick="return btnSearchExmID_validation();"/>
                                                                             </div>
                                                                            <div class="form-group">
                                                                                 <label for="exampleInputName2"></label>
                                                                                     <asp:DropDownList ID="dlExmID" runat="server" 
                                                                                CssClass="input form-control" ClientIDMode="Static">
                                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                             </div>
                                                                            
                                                                        </div>
                                                                    </div>
                                                                </div> 
                                                                <div class="row tbl-controlPanel"> 
                                                                    <div class="col-sm-offset-10 col-sm-2 ">
                                                                        <div class="form-inline">
                                                                             <div class="form-group">
                                                                                 <label for="exampleInputName2"></label>
                                                                                    <asp:Button ID="btnSearchFailStd" runat="server" Text="Search" ClientIDMode="Static"
                                                                                CssClass="btn btn-primary litleMarging" 
                                                                                OnClientClick="return btnSearchFailStd_validation();"
                                                                                OnClick="btnSearchFailStd_Click"/>
                                                                             </div>
                                                                            
                                                                        </div>
                                                                    </div>
                                                                </div> 

                                                      
                                                                <hr />
                                                                <asp:Panel ID="FailgridPanel" runat="server" Height="263px" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                                                                    <asp:GridView ID="failStdView" runat="server" DataKeyNames="ID" 
                                                                        CssClass="table table-bordered table-strip"
                                                                        ClientIDMode="Static" AutoGenerateColumns="false">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkall" runat="server" 
                                                                                        OnClick="javascript:CheckAll(this,failStdView.id);" Checked="true"/>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkIndivisual" runat="server" 
                                                                                        OnClick="javascript:singleChk(this,failStdView.id);" Checked="true"/>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="btnRadio" />
                                                                                <ItemStyle CssClass="btnRadio" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <%# Container.DataItemIndex+1%>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Student Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStdName" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.StudentName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Roll">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStdRoll" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.Roll")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Class">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStdClsName" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.ClassName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>  
                                                                            <asp:TemplateField HeaderText="Section">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStdSection" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.Section")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                               
                                                                            <asp:TemplateField HeaderText="Shift">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblStdShift" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.Shift")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Guardiant Mobile">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGuardiantMobile" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Student.GuardiantMobile")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Subject Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSubName" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "SubjectName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Marks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMarks" runat="server" 
                                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "GetMark")%>'></asp:Label>
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
	                                        <div role="tabpanel" class="tab-pane" id="messages" runat="server">
	    	                                    <div class="l-container animation-slideUp">
                                                    <div class="l-row">
                                                      <div class="l-col-12">
                                                        <h2 class="heading-border-bottom-5">NOTICE</h2>
                                                          <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="dlBatchN" />
                                                                <asp:AsyncPostBackTrigger ControlID="ddlGroupN" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <div class="col-md-12">
                                                                    <div class="col-md-6">
                                                                        <fieldset class="scheduler-border">
                                                                            <legend class="scheduler-border">Student And Guardiant</legend>
                                                                             <div class="row tbl-controlPanel">
                                                                                <div class="col-sm-12">
                                                                                      <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Shift</label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="dlShiftN" runat="server" Width="100%" CssClass="input form-control" ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                                                
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                      </div>
                                                                                      <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Batch</label>
                                                                                        <div class="col-sm-8">
                                                                                             <asp:DropDownList ID="dlBatchN" runat="server" Width="100%" CssClass="input form-control" ClientIDMode="Static" AutoPostBack="true"
                                                                                            OnSelectedIndexChanged="dlBatchN_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                      </div>
                                                                                      <div class="row tbl-controlPanel" id="trGroupN" runat="server" visible="false">
                                                                                        <label  class="col-sm-4">Group</label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="ddlGroupN" OnSelectedIndexChanged="ddlGroupN_SelectedIndexChanged"
                                                                                             AutoPostBack="true" runat="server" Width="100%" CssClass="input form-control" 
                                                                                            ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                      </div>
                                                                                       <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Section</label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="dlSectionN" runat="server" Width="100%" CssClass="input form-control"
                                                                                            ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                      </div>
                                                                                    <div class="row tbl-controlPanel">
                                                                                    <label class="col-sm-4">Sending To</label>
                                                                                    <div class="col-sm-8">
                                                                                    <asp:RadioButtonList runat="server" ID="rblSendToN" ClientIDMode="Static" RepeatDirection="Horizontal">                                                           
                                                                                    <asp:ListItem Value="student" Selected="True">Student</asp:ListItem>
                                                                                    <asp:ListItem Value="guardian" >Guardian</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                                    </div>
                                                                                      </div>


                                                                                      <div class="row tbl-controlPanel">
                                                                                        <div class="col-sm-offset-4 col-sm-8">
                                                                                            <asp:Button ID="btnSTDSearchN" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                                                            OnClientClick="return btnSTDSearchN_validation();" OnClick="btnSTDSearchN_Click" />
                                                                                        </div>
                                                                                      </div>
                                                                                </div>
                                                                            </div>
                                                                                                                            
                                                                        </fieldse>
                                                                    </div>                                        
                                                                    <div class="col-md-6">
                                                                        <fieldset class="scheduler-border">
                                                                            <legend class="scheduler-border">Teacher/Staff</legend>
                                                                             <div class="row tbl-controlPanel">
                                                                                <div class="col-sm-12">
                                                                                        <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Department</label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="dlDeptN" runat="server" Width="100%" CssClass="input form-control" ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                        </div>
                                                                                        <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Designation</label>
                                                                                        <div class="col-sm-8">
                                                                                            <asp:DropDownList ID="dlDesignationN" runat="server" Width="100%" CssClass="input form-control" ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                        </div>
                                                                                        <div class="row tbl-controlPanel">
                                                                                        <label class="col-sm-4">Employee Shift</label>
                                                                                        <div class="col-sm-8">
                                                                                           <asp:DropDownList ID="dlEShiftN" runat="server" Width="100%" CssClass="input form-control" ClientIDMode="Static">
                                                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                                               
                                                                                        </asp:DropDownList>
                                                                                        </div>
                                                                                        </div>
                                                                                        <div class="row tbl-controlPanel">
                                                                                        <div class="col-sm-offset-4 col-sm-8">
                                                                                            <asp:Button ID="btnESearchN" runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMarging"
                                                                                            OnClientClick="return btnESearchN_validation();" OnClick="btnESearchN_Click" />
                                                                                        </div>
                                                                                        </div>
                                                                                </div>
                                                                            </div>
                                                                                                                          
                                                                        </fieldset>
                                                                    </div>                                        
                                                                </div>                                   
                                                                <div class="col-md-12">
                                                                    <hr />
                                                                </div>                                                                   
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="noticePanel" runat="server" Height="157px" CssClass="datatables_wrapper" 
                                                                        Width="100%" ScrollBars="Auto" Visible="true"></asp:Panel>
                                                                    <asp:Panel ID="noticeSTDGridPanel" runat="server" Height="238px" CssClass="datatables_wrapper" 
                                                                        Width="100%" ScrollBars="Auto" Visible="false">
                                                                        <asp:GridView ID="noticeSTDView" runat="server" DataKeyNames="StudentID" CssClass="table table-bordered table-strip"
                                                                            ClientIDMode="Static" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkall" runat="server" 
                                                                                            OnClick="javascript:CheckAll(this,noticeSTDView.id);" Checked="true"/>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkIndivisual" runat="server" 
                                                                                            OnClick="javascript:singleChk(this,noticeSTDView.id);" Checked="true"/>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="btnRadio" />
                                                                                    <ItemStyle CssClass="btnRadio" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex+1%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="StudentName" HeaderText="Student Name" />
                                                                                <asp:BoundField DataField="Roll" HeaderText="Roll" />
                                                                                <asp:BoundField DataField="ClassName" HeaderText="Class" />
                                                                                <asp:BoundField DataField="Section" HeaderText="Section" />
                                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                                <asp:BoundField DataField="Mobile" HeaderText="Student Mobile" />
                                                                                <asp:BoundField DataField="GuardiantMobile" HeaderText="Guardiant Mobile" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </div>
                                                                <div class="col-md-12">
                                                                    <asp:Panel ID="noticeEmpGridPanel" runat="server" Height="238px" CssClass="datatables_wrapper" 
                                                                        Width="100%" ScrollBars="Auto" Visible="false">
                                                                        <asp:GridView ID="noticeEmpView" runat="server" DataKeyNames="EmployeeId" CssClass="table table-bordered table-strip"
                                                                            ClientIDMode="Static" AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkall" runat="server" 
                                                                                            OnClick="javascript:CheckAll(this,noticeEmpView.id);" Checked="true"/>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkIndivisual" runat="server" 
                                                                                            OnClick="javascript:singleChk(this,noticeEmpView.id);" Checked="true"/>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="btnRadio" />
                                                                                    <ItemStyle CssClass="btnRadio" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex+1%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="EmpName" HeaderText="Teacher/Employee Name" />
                                                                                <asp:BoundField DataField="DeptName" HeaderText="Department" />
                                                                                <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                                <asp:BoundField DataField="Shift" HeaderText="Shift" />
                                                                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                                                                                <asp:BoundField DataField="IsTeacher" HeaderText="Is Teacher" />
                                                                                <asp:BoundField DataField="IsExaminer" HeaderText="Is Examiner" />
                                                                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>                              
                                                      </div>
                                                    </div>
                                                  </div>
	                                        </div>
	                                        <div role="tabpanel" class="tab-pane" id="settings" runat="server">
	    	                                    <div class="l-container animation-slideUp">
                                                    <div class="l-row">
                                                        <div class="l-col-12">
                                                        <h2 class="heading-border-bottom-5">GREETING</h2>
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="dlPhnGrp" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddGrp" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddNewNumber" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddGrpName" />
                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton3" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddNumber" />
                                                                    <asp:AsyncPostBackTrigger ControlID="LinkButton4" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <table class="tbl-controlPanel">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:DropDownList ID="dlPhnGrp" runat="server" Width="200px" CssClass="input" 
                                                                                    ClientIDMode="Static"
                                                                                    OnSelectedIndexChanged="dlPhnGrp_SelectedIndexChanged" AutoPostBack="true">
                                                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAddGrp" runat="server" Text="New Group" CssClass="btn btn-danger" ClientIDMode="Static"
                                                                                    OnClick="btnAddGrp_Click" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnAddNewNumber" runat="server" 
                                                                                    Text="Add Number Belongs To Selected Group" ClientIDMode="Static"
                                                                                    CssClass="btn btn-success" OnClientClick="return btnAddNewNumber_validation();" 
                                                                                    OnClick="AddNumber_Click"/>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <hr />
                                                                    <asp:Panel ID="GrpNumPanel" runat="server" Height="263px" CssClass="datatables_wrapper" Width="100%" ScrollBars="Auto">
                                                                        <asp:GridView ID="GrpNumView" runat="server" DataKeyNames="NumID" 
                                                                            CssClass="table table-bordered table-strip"
                                                                            ClientIDMode="Static" AutoGenerateColumns="false">
                                                                            <Columns>                                                
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkall" runat="server" 
                                                                                            OnClick="javascript:CheckAll(this,GrpNumView.id);" Checked="true"/>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>                                                        
                                                                                        <asp:CheckBox ID="chkIndivisual" runat="server" 
                                                                                            OnClick="javascript:singleChk(this,GrpNumView.id);" Checked="true"/>
                                                                                        <asp:HiddenField ID="hideGrpId" runat="server" ClientIDMode="Static"
                                                                                            Value='<%# DataBinder.Eval(Container.DataItem, "Group.GrpID")%>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="btnRadio" />
                                                                                    <ItemStyle CssClass="btnRadio" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <%# Container.DataItemIndex+1%>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                                                <asp:BoundField DataField="Number" HeaderText="Number" />
                                                                                <asp:BoundField DataField="Details" HeaderText="Details" />                                                
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                              </div>
	                                    </div>
	                                </div>
                                </div>
                            </div>










                <%--<header class="panel-heading tab-bg-dark-navy-blue ">
                    <ul class="nav nav-tabs">
                        <li id="litab1" runat="server" class="active">
                            <a data-toggle="tab" id="alitab1" runat="server" href="#MainContent_tabs1">Absent Students</a>
                        </li>
                        <li id="litab2" runat="server" class="">
                            <a data-toggle="tab" id="alitab2" runat="server" href="#MainContent_tabs2">Fail Students List</a>
                        </li>
                        <li id="litab3" runat="server" class="">
                            <a data-toggle="tab" id="alitab3" runat="server" href="#MainContent_tabs3">Notice</a>
                        </li>
                        <li id="litab4" runat="server" class="">
                            <a data-toggle="tab" id="alitab4" runat="server" href="#MainContent_tabs4">Greetings</a>
                        </li>
                        <%--<li class="">
                            <a data-toggle="tab" href="#tabs-5">Others</a>
                        </li>--%>
                    <%--</ul>
                </header>--%>
              
                  
            </div>
        </div>
        <div class="col-md-4">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSend" />
                    <asp:AsyncPostBackTrigger ControlID="dlSMSTemplate" />
                    <asp:AsyncPostBackTrigger ControlID="btnSMSReport" />
                    <asp:AsyncPostBackTrigger ControlID="btnAddNewNumber" />                    
                </Triggers>
                <ContentTemplate>                                      
                    <div class="tgPanel">
                        <div class="tgPanelHead">SMS Template And Send</div>
                        <table class="tbl-controlPanel">
                            <tr>
                                <td>Template
                                </td>
                                <td>
                                    <asp:DropDownList ID="dlSMSTemplate" runat="server" Width="100%" CssClass="input" AutoPostBack="true"
                                        OnSelectedIndexChanged="dlSMSTemplate_SelectedIndexChanged" ClientIDMode="Static">
                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddNewTemplate" runat="server" Text="Add New" OnClick="btnAddNewTemplate_Click"
                                        ClientIDMode="Static" CssClass="btn btn-success" />
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top">Message
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtMsgBody" runat="server" TextMode="MultiLine" Width="98%" MaxLength="2000" Rows="15"
                                        CssClass="input" ClientIDMode="Static"></asp:TextBox>                                 
                                    <ul class="charCount">
                                        <li>Characters : </li>
                                        <li>
                                            <asp:Label ID="lblCharCount" runat="server" Text="0" Width="30px" ClientIDMode="Static" CssClass="tahoma"></asp:Label></li>
                                        <%--<li>SMS Count  : </li>
                                             <li>
                                                <asp:Label ID="Label2" runat="server" Text="1" Width="30px" CssClass="tahoma"></asp:Label>
                                            </li>--%>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                    <asp:Button ID="btnSend" runat="server" Text="Send" ClientIDMode="Static"
                                         OnClick="btnSend_Click" 
                                        CssClass="btn btn-primary" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" ClientIDMode="Static"
                                        OnClientClick="return btnClear_Clear();"
                                        CssClass="btn btn-default" />
                                    <asp:Button ID="btnSMSReport" runat="server" Text="Report" ClientIDMode="Static"  CssClass="btn btn-success"
                                        OnClick="btnSMSReport_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- add message body modal -->
                    <asp:ModalPopupExtender ID="showAddMsgBody" runat="server" BehaviorID="modalpopup1" CancelControlID="Button4"
                        OkControlID="LinkButton1"
                        TargetControlID="button5" PopupControlID="showAddMsg" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showAddMsg" runat="server" style="display: none;" class="confirmationModal400">
                        <div class="modal-header">
                            <button id="Button4" type="button" class="close white"></button>
                            <div class="tgPanelHead">Add SMS Template</div>
                        </div>
                        <div class="modal-body">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td style="width: 15%">Title
                                    </td>
                                    <td style="width: 78%;">
                                        <asp:TextBox ID="txtTitle" runat="server" ClientIDMode="Static"
                                            Width="100%" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%; vertical-align: top">Message
                                    </td>
                                    <td style="width: 78%;">
                                        <asp:TextBox ID="txtMsg" runat="server" TextMode="MultiLine" MaxLength="2000" Rows="10" ClientIDMode="Static"
                                            Width="100%" CssClass="input"></asp:TextBox>
                                        <ul class="charCount">
                                            <li>Characters : </li>
                                            <li>
                                                <asp:Label ID="lblBWordCount" runat="server" ClientIDMode="Static"
                                                    Text="0" Width="30px" CssClass="tahoma"></asp:Label></li>
                                            <%--<li>SMS Count  : </li><li><asp:Label ID="lblBSMSCount" runat="server" Text="1" Width="30px" CssClass="tahoma"></asp:Label></li>--%>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="button5" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton1" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                <i class="icon-remove"></i>                                    
                                Close
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnAddMsg" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false"
                                OnClientClick="return btnAddMsg_validation();" OnClick="btnAddMsg_Click">
                                <i class="icon-ok"></i>
                                Save
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- END add message body modal -->
                    <!-- add SMS Report modal -->
                    <asp:ModalPopupExtender ID="smsReportModal" runat="server" CancelControlID="Button12"
                        OkControlID="LinkButton2"
                        TargetControlID="button7" PopupControlID="showSMSReport" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showSMSReport" runat="server" style="display: none;" class="confirmationModal900">
                        <div class="modal-header">
                            <button id="Button12" type="button" class="close white"></button>
                            <div class="tgPanelHead">SMS Sending Report</div>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="smsReportView" runat="server" CssClass="table table-bordered table-strip"
                                ClientIDMode="Static" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SMSID" HeaderText="SMS ID" />
                                    <asp:BoundField DataField="MobileNo" HeaderText="Mobile" />
                                    <asp:BoundField DataField="MessageBody" HeaderText="Message Body" />
                                    <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
                                    <asp:BoundField DataField="SentTime" HeaderText="Sent Time" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button id="button7" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton2" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                <i class="icon-remove"></i>                                    
                                Close
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- END SMS Report modal modal -->
                    <!-- Phone Group Modal-->
                    <asp:ModalPopupExtender ID="phnGrpModal" runat="server" CancelControlID="Button13"
                        OkControlID="LinkButton3"
                        TargetControlID="button1" PopupControlID="showPhnGrp" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showPhnGrp" runat="server" style="display: none;" class="confirmationModal400">
                        <div class="modal-header">
                            <button id="Button13" type="button" class="close white"></button>
                            <div class="tgPanelHead">Add New Phone Group</div>
                        </div>
                        <div class="modal-body">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Group Name<span class="required">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtGrpName" runat="server" Width="100%" CssClass="input" 
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Details
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrpDetails" runat="server" Width="100%" ClientIDMode="Static"
                                            CssClass="input" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="button1" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton3" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                <i class="icon-remove"></i>                                    
                                Close
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnAddGrpName" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false" 
                                ClientIDMode="Static"
                                OnClientClick="return btnAddGrpName_validation();" OnClick="btnAddGrpName_Click">
                                <i class="icon-ok"></i>
                                Save
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- END Phone Group Modal-->
                    <!-- Phone Number Modal-->
                    <asp:ModalPopupExtender ID="addNumModal" runat="server" CancelControlID="Button14"
                        OkControlID="LinkButton4"
                        TargetControlID="button2" PopupControlID="showAddNum" BackgroundCssClass="ModalPopupBG">
                    </asp:ModalPopupExtender>
                    <div id="showAddNum" runat="server" style="display: none;" class="confirmationModal400">
                        <div class="modal-header">
                            <button id="Button14" type="button" class="close white"></button>
                            <div class="tgPanelHead">Add New Phone Number</div>
                        </div>
                        <div class="modal-body">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Group Name<span class="required">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="dlGrpName" runat="server" CssClass="input" Width="100%" Enabled="false"
                                            ClientIDMode="Static">
                                            <asp:ListItem Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Name<span class="required">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="input" 
                                            ClientIDMode="Static"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Mobile Number<span class="required">*</span></td>
                                    <td>
                                        <asp:TextBox ID="lblMobile" runat="server" Width="18%" Text="+88" 
                                            CssClass="input text-danger text-center" 
                                            ReadOnly="true" ClientIDMode="Static"></asp:TextBox>
                                        <asp:TextBox ID="txtNumber" runat="server" Width="80%" MaxLength="11" CssClass="input" 
                                            ClientIDMode="Static"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="F1" runat="server" FilterType="Numbers" 
                                            TargetControlID="txtNumber" ValidChars=""></asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Details</td>
                                    <td>
                                        <asp:TextBox ID="txtNumDetails" runat="server" Width="100%" ClientIDMode="Static"
                                            CssClass="input" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button id="button2" type="button" runat="server" style="display: none;" />
                            <asp:LinkButton ID="LinkButton4" runat="server" ClientIDMode="Static" CssClass="btn btn-default">
                                <i class="icon-remove"></i>                                    
                                Close
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnAddNumber" runat="server" CssClass="btn btn-primary" UseSubmitBehavior="false" 
                                ClientIDMode="Static"
                                OnClientClick="return btnAddNumber_validation();" OnClick="btnAddNumber_Click">
                                <i class="icon-ok"></i>
                                Save
                            </asp:LinkButton>
                        </div>
                    </div>
                    <!-- END Phone Number Modal-->
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>   
    
 
    
    
       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        $(document).ready(function () {
           
            $('#GrpNumView').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            $('#tblClassList4').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#tblClassList').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
            $('#tblClassList4').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function btnAddMsg_validation() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;
            if (validateText('txtMsg', 1, 2000, 'Enter a Message Body') == false) return false;
            return true;
        }
        function btnSend_validation() {
            if (validateCombo('dlSMSTemplate', "0", 'Select a SMS Template') == false) return false;
            if (validateText('txtMsgBody', 1, 2000, 'Enter a Message Body') == false) return false;
            return true;
        }
        function btnClear_Clear() {
            $('#dlSMSTemplate').val('0');
            $('#txtMsgBody').val('');
        }
        function pageLoad() {
            $('#txtMsgBody').bind("keyup change", function () {
                $('#lblCharCount').text($(this).val().length);
            });
            $('#txtMsg').bind("keyup change", function () {
                $('#lblBWordCount').text($(this).val().length);
            });            
        }
        $('.nav-tabs a').click(function (e) {
            e.preventDefault();
            $('#lblHidetabIndex').val($($(this).attr('href')).index());            
        });
        function updateSuccess() {
            showMessage('Update successfully', 'success');            
        }
        function SavedSuccess() {
            showMessage('Save successfully', 'success');           
        }
        function btnSearch_validation() {            ;
            if (validateCombo('dlBatch', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlShift', "0", 'Select a Shift') == false) return false;
            var dlBatch = $('#dlBatch option:selected').text();
            if(dlBatch != 'All')
            {     
                if (validateCombo('dlSection', "0", 'Select a Section') == false) return false;
            }
            return true;
        }
        function btnSTDSearchN_validation() {
            if (validateCombo('dlBatchN', "0", 'Select a Batch Name') == false) return false;
            if (validateCombo('dlShiftN', "0", 'Select a Shift') == false) return false;
            var dlBatch = $('#dlBatchN option:selected').text();
            if (dlBatch != 'All') {
                if (validateCombo('dlSectionN', "0", 'Select a Section') == false) return false;
            }
            return true;
        }
        function btnESearchN_validation(){
            if (validateCombo('dlDeptN', "0", 'Select a Department Name') == false) return false;
            if (validateCombo('dlDesignationN', "0", 'Select a Designation') == false) return false;
            if (validateCombo('dlEshiftN', "0", 'Select a Shift') == false) return false;
            return true;
        }
        function CheckAll(oCheckbox, gridID) {
            var GridView = document.getElementById(gridID);
            $('#contactId').css({ border: '2px solid #1A4C1A' });
            for (i = 1; i < GridView.rows.length; i++) {
                GridView.rows[i].cells[0].getElementsByTagName("input")[0].checked = oCheckbox.checked;
            }
        }
        function singleChk(oCheckbox, gridID) {
            var GridView = document.getElementById(gridID);
            var inputList = GridView.getElementsByTagName("input");
            $('#contactId').css({ border: '2px solid #1A4C1A' });
            if (oCheckbox.checked == false) {
                inputList[0].checked = false;
            }
            else {
                for (var i = 0; i < inputList.length; i++) {
                    var headerCheckBox = inputList[0];
                    var checked = true;
                    if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                        if (!inputList[i].checked) {
                            checked = false;
                            break;
                        }
                    }
                }
                headerCheckBox.checked = checked;
            }
        }
        function btnSearchExmID_validation() {
            if (validateText('txtFDate', 1, 100, 'Enter a From Date') == false) return false;
            if (validateText('txtTDate', 1, 100, 'Enter a To Date') == false) return false;
            return true;
        }
        function btnSearchFailStd_validation() {
            if (validateText('txtFDate', 1, 100, 'Enter a From Date') == false) return false;
            if (validateText('txtTDate', 1, 100, 'Enter a To Date') == false) return false;
            if (validateCombo('dlExmID', "0", 'Select a ExamID') == false) return false;
            return true;
        }
        function btnAddNewNumber_validation(){
            if (validateCombo('dlPhnGrp', "0", 'Select a Group Name') == false) return false;
            return true;
        }
        function btnAddGrpName_validation() {
            if (validateText('txtGrpName', 1, 100, 'Enter a Group Name') == false) return false;            
            return true;
        }
        function btnAddNumber_validation() {
            if (validateCombo('dlGrpName', "0", 'Select a Group Name') == false) return false;
            if (validateText('txtName', 1, 100, 'Enter a Number Owner Name') == false) return false;
            if (validateText('txtNumber', 1, 100, 'Enter a Mobile Number') == false) return false;            
            return true;
        }       
       
    </script>
</asp:Content>
