<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FeeCollectionDetailsReport.aspx.cs" Inherits="DS.UI.Reports.Finance.FeeCollectionDetailsReport" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
		.hide-view{
			display:none;
		}
        .tgPanel{
            width:100%;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
        .controlLength{
            min-width:150px;
        }
        /*.tbl-controlPanel
        {
            width: 835px;
        }*/
        .tbl-controlPanel td:first-child,
        .tbl-controlPanel td:nth-child(3),
        .tbl-controlPanel td:nth-child(5)
        {
            text-align: right;
            padding-right: 5px;
        }
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
        .particular {
             width:662px;
        }
        .littlemargin {
            margin-left:10px;
        }
        #MainContent_TabContainer_TabPanel5_UpdatePanel6 {
        min-height:300px;
        }
        .last {
            margin-bottom:100px;
        }

        .bt {
            margin-left:30px;
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
                <li><a id="A2" runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>    
                <li class="active">Fee Collection Details Report</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Width="100%" TabIndex="1">
             <ajax:TabPanel ID="TabPanel6" TabIndex="6" runat="server" HeaderText="Daily Transactions">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel" style="min-height:400px">
                            <div class="tgPanelHead">Daily Transactions</div>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger  ControlID="ddlClass"/>
                                    <asp:AsyncPostBackTrigger  ControlID="btnPreviewP"/>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row tbl-controlPanel"> 
                                            <div class="col-sm-11">
                                                <div class="form-inline">
                                                     <div class="form-group" runat="server" visible="false">
                                                         <label for="exampleInputName2">Class</label>
                                                            <asp:DropDownList ID="ddlClass" AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                                  OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                     </div>
                                                   
                                                    <div class="form-group" runat="server" visible="false">
                                                         <label for="exampleInputName2">Fee Category</label>
                                                        <asp:DropDownList ID="ddlCategoryA" runat="server" ClientIDMode="Static"
                                                            CssClass="input controlLength form-control">
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                
                                                     </div>
                                                     <div class="form-group">
                                                         <label for="exampleInputName2">From Date</label>                                                        <asp:TextBox runat="server" ID="txtTransactionDate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtTransactionDate"></ajax:CalendarExtender>              
                                                     </div>
                                                    <div class="form-group">
                                                         <label for="exampleInputName2">To Date</label>                                                        <asp:TextBox runat="server" ID="txtTransactionToDate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtTransactionToDate"></ajax:CalendarExtender>              
                                                     </div>
                                                    <div class="form-group">
                                                         <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                          OnClick="btnSearch_Click"  />
                
                                                     </div>

                                                    
                                                   
                                                </div>
                                            </div>
											<div class="col-sm-1">
												<div class="form-group" runat="server" visible="false">
                                                         <asp:Button ID="btnPreviewP" Text="Preview & Print" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                         OnClick="btnPreviewP_Click"  />
                
                                                     </div>
												<div class="form-group" >
													<a class="btn btn-info" onclick="portraitPrintHTML('PrintDailyTransaction')" >Print</a>
                
                                                     </div>
											</div>
                                        </div> 
                                        <br />
                                      <div id="PrintDailyTransaction">
                                          <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="True" 
                     CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"> </asp:GridView>
                                      </div>
                                    </div>                                  
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>

            <ajax:TabPanel ID="TabPanel8" TabIndex="20" runat="server" HeaderText="Fee Collections">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel" style="min-height:400px">
                            <div class="tgPanelHead">Fee Collections</div>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger  ControlID="ddlYearFeeCollection"/>
                                   <asp:AsyncPostBackTrigger  ControlID="ddlClassFeeCollection"/>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row tbl-controlPanel"> 
                                            <div class="col-sm-10">
                                                <div class="form-inline"> 
                                                    <div class="form-group" style="margin-bottom:10px">
                                            <label class="">Payment For</label>
                                         
                                                <asp:DropDownList ID="ddlPaymentFor" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentFor_SelectedIndexChanged">
                                                        <%--<asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        <asp:ListItem Value="admission">Admission</asp:ListItem>
                                                        <asp:ListItem Value="regular">Regular Fee</asp:ListItem>
                                                        <asp:ListItem Value="openPayment">Open Payment</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            
                                        </div>
                                                    <asp:Panel runat="server" ClientIDMode="Static" id="pnlAcademicInfo" class="form-group" >
                                                   
                                                    <div class="form-group">
                                            <label class="col-md-3 control-label">Year</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlYearFeeCollection" runat="server" CssClass="form-control"
                                                    ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlYearFeeCollection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputName2">Class</label>
                                            
                                                <asp:DropDownList ID="ddlClassFeeCollection" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlClassFeeCollection_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            
                                        </div>
                                                    </asp:Panel>
                                                    <div class="form-group" >
                                                         <label for="exampleInputName2">Category</label>                                                        <asp:DropDownList ID="ddlCategoriesFeeCollection" runat="server" ClientIDMode="Static"
                                                                CssClass="form-control" >
                                                            </asp:DropDownList>          
                                                     </div>
                                                     <div class="form-group">
                                                         <label for="exampleInputName2">From Date</label>                                                        <asp:TextBox runat="server" ID="txtFeeCollectionDate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender2" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtFeeCollectionDate"></ajax:CalendarExtender>              
                                                     </div>
                                                    <div class="form-group">
                                                         <label for="exampleInputName2">To Date</label>                                                        <asp:TextBox runat="server" ID="txtFeeCollectionToDate" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender7" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtFeeCollectionToDate"></ajax:CalendarExtender>              
                                                     </div>

                                                    

                                                   
                                                   
                                                </div>
                                            </div>
											<div class="col-sm-2">
												<div class="form-inline"> 
												<div class="form-group">
                                                         <asp:Button  ID="btnSearchFeeCollections" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                           OnClick="btnSearchFeeCollections_Click"  />
                
                                                     </div>
												 <div class="form-group "  style="text-align:right">
                                                         <a class="btn btn-info" onclick="portraitPrintHTML('PrintFeeCollection')" >Print</a>
                										</div>
                                                     </div>
											</div>
                                        </div> 
                                      <div id="PrintFeeCollection">
										  <div class="hide-view text-center">
											  <p><img style="height:60px" src="http://islampurcollege.edu.bd/websitedesign/assets/images/logo.png"></p>
											  <h3>Govt. Islampur College</h3>
											  <h5>Fee Collection Report</h5>
										  </div>
                                          <asp:GridView ID="gvFeeCollections" runat="server" AutoGenerateColumns="False" 
                     CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White"> 
                                               <Columns>
                  <asp:TemplateField HeaderText="SL"> 
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>

               <asp:BoundField DataField="AdmissionNo" HeaderText="Admission No/Reg No" />
               <asp:BoundField DataField="FullName" HeaderText="Full Name" />
               <asp:BoundField DataField="BatchName" HeaderText="Batch" />             
                <asp:BoundField DataField="GroupName" HeaderText="Group" />               
                <asp:BoundField DataField="RollNo" HeaderText="Class Roll" /> 
                <asp:BoundField DataField="FeeCatName" HeaderText="Category" /> 
                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                   <asp:TemplateField>
                                                       <HeaderTemplate>
                                                           Amount <br />( 
                                                           <strong>Total=</strong> <asp:Label runat="server" ClientIDMode="Static" ID="lblTotalAmount"></asp:Label>)
                                                       </HeaderTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label runat="server" ClientIDMode="Static" ID="lblAmount" Text='<%# Eval("PaidAmount").ToString() %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                <%--<asp:BoundField DataField="PaidAmount" HeaderText="Amount" />--%>
                <asp:BoundField DataField="CreatedAt" HeaderText="Order Time" />
                <asp:BoundField DataField="UpdatedAt" HeaderText="Payment Time" Visible="false" />

                
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false">
                    <HeaderTemplate >
                       View
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnShowUIP" runat="server"  ImageUrl="~/Images/gridImages/view.png" Width="30px" CommandName="View" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' />                 
                         
                    </ItemTemplate>
                </asp:TemplateField>
               
                
            </Columns>
                                          </asp:GridView>
                                      </div>
                                    </div>                                  
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>


             <ajax:TabPanel ID="TabPanel7" TabIndex="7" runat="server" HeaderText="Admisssion Collection Reprint">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Admisssion Collection Reprint</div>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <Triggers>                                  
                                    <asp:AsyncPostBackTrigger  ControlID="btnReprint"/>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row tbl-controlPanel"> 
                                            <div class="col-sm-6 col-sm-offset-3">
                                                <div class="form-inline">
                                                     <div class="form-group">
                                                         <label for="exampleInputName2">Admission No/Transaction No</label>
                                                         <asp:TextBox runat="server" ID="txtAdmNoorTransactionNo" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                     </div>
                                                    <div class="form-group">
                                                       <asp:Button ID="btnReprint" Text="Reprint Preview" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                         OnClick="btnReprint_Click" />
                
                                                     </div>
                                                    
                                                </div>
                                            </div>
                                        </div> 
                                       <%-- <table style="width:530px;" class="tbl-controlPanel">
                                            <tr>
                                                <td>Admission No/Transaction No
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtAdmNoorTransactionNo" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                </td>
                                                 <td>
                                                    <asp:Button ID="btnReprint" Text="Reprint Preview" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                         OnClick="btnReprint_Click" />
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </div>                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>          
           

            <ajax:TabPanel ID="TabPanel2" TabIndex="6" runat="server" HeaderText="Particular Reports">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Particular Report</div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger  ControlID="ddlBatchP"/>
                                    <asp:AsyncPostBackTrigger  ControlID="btnPrintParticular"/>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row tbl-controlPanel"> 
                                            <div class="col-sm-6 col-sm-offset-3">
                                                <div class="form-inline">
                                                     <div class="form-group">
                                                         <label for="exampleInputName2">Batch</label>
                                                            <asp:DropDownList ID="ddlBatchP" AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                                 OnSelectedIndexChanged="ddlBatchP_SelectedIndexChanged" runat="server">
                                                                <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
                                                     </div>
                                                    <div class="form-group">
                                                         <label for="exampleInputName2">Fee Category</label>
                                                        <asp:DropDownList ID="ddlCategoryP" runat="server" ClientIDMode="Static"
                                                            CssClass="input controlLength form-control">
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                                                     </div>
                                                    <div class="form-group">
                                                         
                                                        <asp:Button ID="btnPrintParticular" Text="Preview & Print" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                         OnClick="btnPrintParticular_Click" />
                                                     </div>
                                                   
                                                </div>
                                            </div>
                                        </div> 
                                         
                                    </div>
                                    <div id="div2" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <asp:Label ID="Label3" runat="server" ForeColor="Black"></asp:Label>
                                        <asp:Button ID="Button4" OnClick="btnPreviewDetails_Click" Visible="false" Text="Preview" 
                                            CssClass="btn btn-success pull-right" ClientIDMode="Static" runat="server" />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>           
            <ajax:TabPanel ID="TabPanel1" TabIndex="5" runat="server" HeaderText="Student Wise Collection Details">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Student Wise Collection Details</div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                   <asp:AsyncPostBackTrigger  ControlID="ddlBatchS"/>
                                    <asp:AsyncPostBackTrigger  ControlID="ddlGroupS"/>
                                    <asp:AsyncPostBackTrigger  ControlID="ddlSectionS"/>
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row">
                                            <div class="col-sm-8 col-sm-offset-2">
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-4">
                                                        <label class="col-sm-4">Shift</label>
                                                        <div class="col-sm-8">
                                                            <asp:DropDownList ID="ddlShiftS" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control"
                                                                AutoPostBack="false">                                                        
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
            	                                        <label class="col-sm-4">Batch</label>
	                                                    <div class="col-sm-8">
	                                                        <asp:DropDownList ID="ddlBatchS" AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                                 OnSelectedIndexChanged="ddlBatchS_SelectedIndexChanged" runat="server">
                                                       
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-4" id="tdGroupSH" runat="server" visible="false">Group</label>
	                                                    <div class="col-sm-8" id="tdGroupS" runat="server" visible="false">
	                                                        <asp:DropDownList ID="ddlGroupS" AutoPostBack="true" CssClass="input controlLength form-control" ClientIDMode="Static"
                                                         OnSelectedIndexChanged="ddlGroupS_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="0" Selected="True" Text="...Select..."></asp:ListItem>
                                                    </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-4">Section</label>
	                                                    <div class="col-sm-8">
	                                                        <asp:DropDownList ID="ddlSectionS" runat="server" CssClass="input controlLength form-control" AutoPostBack="true"
                                                                 OnSelectedIndexChanged="ddlSectionS_SelectedIndexChanged" ClientIDMode="Static">
                                                        
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                </div>
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-4">roll</label>
	                                                    <div class="col-sm-8">
	                                                        <asp:DropDownList ID="ddlRollS" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">
                                                       
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
            	                                        <label class="col-sm-4">Fee Category</label>
	                                                    <div class="col-sm-8">
	                                                        <asp:DropDownList ID="ddlFeeCatS" runat="server" ClientIDMode="Static"
                                                        CssClass="input controlLength form-control">
                                                       
                                                    </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                   
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-4"></label>
	                                                    <div class="col-sm-8">
	                                                        <asp:Button ID="btnStudentPaymentDetails" Text="Preview" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                         OnClick="btnStudentPaymentDetails_Click" />
	                                                    </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                       
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <ajax:TabPanel ID="TabPanel5" TabIndex="1" runat="server" HeaderText="Collection Details">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fees Collection Details</div>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlCBatch" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-10 col-sm-offset-1">
                                            <div class="row tbl-controlPanel">
                                                <div class="col-sm-3">
                                                    <label class="col-sm-4">Shift</label>
                                                    <div class="col-sm-8">
                                                        <asp:DropDownList ID="ddlCShift" runat="server" ClientIDMode="Static"
                                                            CssClass="form-control"> 
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                                   
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
            	                                    <label class="col-sm-4">Batch</label>
	                                                <div class="col-sm-8">
	                                                    <asp:DropDownList ID="ddlCBatch" AutoPostBack="true" ClientIDMode="Static"
                                                            OnSelectedIndexChanged="ddlCBatch_SelectedIndexChanged" 
                                                            runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
	                                                </div>
                                                </div>
                                                <div class="col-sm-3">
	                                                <label class="col-sm-4">Group</label>
	                                                <div class="col-sm-8">
	                                                    <asp:DropDownList ID="ddlGroup" AutoPostBack="true" ClientIDMode="Static"
                                                              OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"
                                                            runat="server" CssClass="form-control">
                                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
	                                                </div>
                                                </div>
                                                <div class="col-sm-3">
	                                                <label class="col-sm-4">Section</label>
	                                                <div class="col-sm-8">
	                                                    <asp:DropDownList ID="ddlCSection" runat="server" ClientIDMode="Static"
                                                             CssClass="form-control">
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
	                                                </div>
                                                </div>
                                            </div>
                                            <div class="row tbl-controlPanel">
            
                                                <div class="col-sm-3">
	                                                <label class="col-sm-4">Form</label>
	                                                <div class="col-sm-8">
	                                                    <asp:TextBox runat="server" ID="txtCFrom" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                            <ajax:CalendarExtender ID="CalendarExtender5" Format="dd-MM-yyyy" runat="server" 
                                                                TargetControlID="txtCFrom"></ajax:CalendarExtender>
	                                                </div>
                                                </div>
                                                <div class="col-sm-3">
	                                                <label class="col-sm-4">To</label>
	                                                <div class="col-sm-8">
	                                                    <asp:TextBox runat="server" ID="txtCTo" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                                        <ajax:CalendarExtender ID="CalendarExtender6" Format="dd-MM-yyyy" runat="server" 
                                                        TargetControlID="txtCTo"></ajax:CalendarExtender>
	                                                </div>
                                                </div>
                                                <div class="col-sm-3">
	                                                <label class="col-sm-4">Categori</label>
	                                                <div class="col-sm-8">
	                                                    <asp:DropDownList ID="ddlCFeeCat" runat="server" ClientIDMode="Static"
                                                     CssClass="form-control">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>      
                                                </asp:DropDownList>
	                                                </div>
                                                </div>
                                                <div class="col-sm-3">
	                                                
	                                                <div class="col-sm-12">
	                                                    <asp:CheckBox ID="chkCTodayCollect" ClientIDMode="Static" Text="Is Today Collection" runat="server" />
	                                                </div>
                                                </div>
                                            </div>
                                            <div class="row last">
                                                <div class="col-sm-9"></div>
                                                <div class="col-sm-3">
                                                     <asp:Button ID="btnCSearch" OnClick="btnCSearch_Click" Text="Preview & Print" 
                                                    OnClientClick="return btnCSearch_Validation();" 
                                                    ClientIDMode="Static" runat="server" CssClass="btn btn-success bt"/>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                                                    
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <%--<ajax:TabPanel ID="TabPanel1" TabIndex="2" runat="server" HeaderText="View">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Collection View</div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <table class="tbl-controlPanel" style="width: 580px;">
                                        <tr>
                                            <td>Batch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlBatch" AutoPostBack="true" OnSelectedIndexChanged="dlBatch_SelectedIndexChanged"
                                                    runat="server" class="input controlLength">
                                                </asp:DropDownList></td>
                                            <td>Categery
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="dlCategory"
                                                    CssClass="input controlLength">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                    OnClick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="3">
                                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="border: 1px solid gray; padding: 05px;" runat="server" id="divFeeCategory" visible="false">
                                                <asp:Label runat="server" ID="lblCategory"></asp:Label>
                                            </div>
                                            <div id="divCollectionView" class="datatables_wrapper" runat="server"
                                                style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>--%>
            <%--<ajax:TabPanel ID="TabPanel2" TabIndex="3" runat="server" HeaderText="Summary">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fee Collection Summary</div>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatchSummary" />
                                </Triggers>
                                <ContentTemplate>
                                    <table class="tbl-controlPanel">
                                        <tr>
                                            <td>Batch
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlBatchSummary" AutoPostBack="true" OnSelectedIndexChanged="dlBatchSummary_SelectedIndexChanged"
                                                    runat="server" CssClass="input controlLength">
                                                    <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>Section
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlSectionSummary" runat="server" CssClass="input controlLength"></asp:DropDownList>
                                            </td>
                                            <td>Shift                        
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlShift" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                    CssClass="input controlLength">
                                                    <asp:ListItem>Morning</asp:ListItem>
                                                    <asp:ListItem>Day</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>From
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFromDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"></ajax:CalendarExtender>
                                            </td>
                                            <td>To
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtToDate" ClientIDMode="Static" CssClass="input controlLength"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"></ajax:CalendarExtender>
                                            </td>
                                            <td>Fee Category
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dlFeeCategorySummary" runat="server" CssClass="input controlLength">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <asp:CheckBox ID="chkToday" ClientIDMode="Static" Text="Is Today Collection" runat="server" />
                                                <asp:Button ID="btnSearchSummary" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                    OnClick="btnSearchSummary_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divCollectionSummary" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <asp:Label ID="lblClassName" runat="server" ForeColor="Black"></asp:Label>
                                        <asp:Button ID="btnPreview" OnClick="btnPreview_Click" Visible="false" Text="Preview" CssClass="btn btn-success pull-right"
                                            ClientIDMode="Static" runat="server" />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>--%>
            <ajax:TabPanel ID="TabPanel3" TabIndex="4" runat="server" Visible="false" HeaderText="Fees Category Wise Collection Details">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Fees Category Wise Collection Details</div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatchDetails" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <table class="tbl-controlPanel">
                                            <tr>
                                                <td>Batch
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlBatchDetails" AutoPostBack="true" CssClass="input controlLength" ClientIDMode="Static"
                                                        OnSelectedIndexChanged="dlBatchDetails_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Section
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlSectionDetails" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Shift                    
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlShiftForDetails" runat="server" ClientIDMode="Static" CssClass="input controlLength"
                                                        AutoPostBack="false">
                                                        <asp:ListItem>Morning</asp:ListItem>
                                                        <asp:ListItem>Day</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>From
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFromDateDetails" ClientIDMode="Static"
                                                        CssClass="input controlLength"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender3" runat="server"
                                                        TargetControlID="txtFromDateDetails">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td>To
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtToDateDetails" ClientIDMode="Static"
                                                        CssClass="input controlLength"></asp:TextBox>
                                                    <ajax:CalendarExtender ID="CalendarExtender4" runat="server"
                                                        TargetControlID="txtToDateDetails">
                                                    </ajax:CalendarExtender>
                                                </td>
                                                <td>Fee Category
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlFeeCategoryDetails" runat="server" ClientIDMode="Static"
                                                        CssClass="input controlLength">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnSearchDetails" Text="Search" ClientIDMode="Static" runat="server" CssClass="btn btn-success"
                                                        OnClick="btnSearchDetails_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div id="divCollectionDetails" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <asp:Label ID="lblClassDetails" runat="server" ForeColor="Black"></asp:Label>
                                        <asp:Button ID="btnPreviewDetails" OnClick="btnPreviewDetails_Click" Visible="false" Text="Preview" 
                                            CssClass="btn btn-success pull-right" ClientIDMode="Static" runat="server" />
                                        <br />
                                        <br />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
            <ajax:TabPanel ID="TabPanel4" TabIndex="5" runat="server" HeaderText="Due List">
                <ContentTemplate>
                    <div class="">
                        <div class="tgPanel">
                            <div class="tgPanelHead">Due List</div>
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dlBatchDueList" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="">
                                        <div class="row">
                                            <div class="col-sm-10 col-sm-offset-1">
                                                <div class="row tbl-controlPanel">
                                                    <div class="col-sm-4">
                                                        <label class="col-sm-5">Shift</label>
                                                        <div class="col-sm-7">
                                                            <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                                CssClass="input controlLength form-control">
                                                                <asp:ListItem>Morning</asp:ListItem>
                                                                <asp:ListItem>Day</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
            	                                        <label class="col-sm-5">Batch</label>
	                                                    <div class="col-sm-7">
	                                                        <asp:DropDownList ID="dlBatchDueList" AutoPostBack="true" runat="server" ClientIDMode="Static"
                                                                OnSelectedIndexChanged="dlBatchDueList_SelectedIndexChanged" CssClass="input controlLength form-control">
                                                                <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-5">Group</label>
	                                                    <div class="col-sm-7">
	                                                        <asp:DropDownList ID="dlGroupDueList" runat="server" CssClass="input controlLength form-control"
                                                                 OnSelectedIndexChanged="dlGroupDueList_SelectedIndexChanged" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                   
                                                </div>
                                                <div class="row tbl-controlPanel">
            
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-5">Section</label>
	                                                    <div class="col-sm-7">
	                                                        <asp:DropDownList ID="dlSectionDueList" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-5">Fee Category</label>
	                                                    <div class="col-sm-7">
	                                                        <asp:DropDownList ID="dlFeesCategoryDueList" runat="server" CssClass="input controlLength form-control" ClientIDMode="Static">
                                                                <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                            </asp:DropDownList>
	                                                    </div>
                                                    </div>
                                                    <div class="col-sm-4">
	                                                    <label class="col-sm-5"></label>
	                                                    <div class="col-sm-7">
	                                                        <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Preview & Print" CssClass="btn btn-success" ClientIDMode="Static"
                                                       OnClick="btnPrintPreviewDueList_Click"  />
                                                            <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
	                                                    </div>
                                                    </div>
                                                    
                                                </div>
                                            </div>
                                        </div>
                                        
                                    </div>
                                    <div id="divDueList" style="max-height: 360px; overflow: auto" runat="server" class="TabFull ComTabLeft">
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblBatch" class="lblFontStyle"></asp:Label></h4>
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblShift" class="lblFontStyle"></asp:Label></h4>
                                        <h4>
                                            <asp:Label runat="server" Text="" ID="lblSection" class="lblFontStyle"></asp:Label></h4>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>            
        </ajax:TabContainer>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script>
        function btnCSearch_Validation() {
            //if (validateCombo('ddlCBatch', "0", 'Select a Batch Name') == false) return false;
            //if (validateCombo('ddlCSection', "0", 'Select a Section') == false) return false;
            //if (validateCombo('ddlCShift', "0", 'Select a Shift') == false) return false;
            if (validateCombo('ddlCFeeCat', "0", 'Select a Fee Category') == false) return false;
            var fromDate = $('#txtCFrom').val();
            var toDate = $('#txtCTo').val();
            var chk = $('#chkCTodayCollect').is(":checked");
            if (fromDate == "" && toDate == "" && chk == false) {
                showMessage("Plese Select Date Rang", 'error');
                return false;
            }
            if (fromDate != "" && toDate != "" && chk != false) {
                showMessage("Plese Select From And To Date Or Is Today Collection Collection At a Time", 'error');
                return false;
            }
            if ((fromDate == "" && toDate != "") || (fromDate != "" && toDate == "") && chk == false) {
                showMessage("Plese Select Another Date Rang", 'error');
                return false;
            }
            return true;
        }
    </script>
	<script>
	 function portraitPrintHTML(divPrint) {

      //$(".hidePrint").css("display", "none");

      var mywindow = window.open('', 'PRINT', 'height=400,width=600');

      //  mywindow.document.write('<html><head><title>' + document.title  + '</title>');
      //  mywindow.document.write('</head><body >');
      //  mywindow.document.write('<h1>' + document.title  + '</h1>');
      mywindow.document.write('<html><head><title></title>');
      mywindow.document.write('<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css">');
      mywindow.document.write('<link href="./styles.css" rel="stylesheet">');
      //mywindow.document.write('<link href="./assets/fonts/nikosh/stylesheet.css" rel="stylesheet">');
      mywindow.document.write('<style>');
      //mywindow.document.write('@page { size: 210mm 297mm;margin: 1cm 1cm 1cm 1cm; }');
      mywindow.document.write('@media print{');
      mywindow.document.write('.page-break{page-break-after: always;}');
      mywindow.document.write('table {width:100%}');
      mywindow.document.write('.table-bordered th{vertical-align: middle!important;background: #ededed!important;text-align:center!important}');
      mywindow.document.write('.table-bordered th, .table-bordered td {border: 1px solid #000 !important;font-size:16px!important;color:#000!important}');
      mywindow.document.write('.print-report-header table th {font-size:18px!important;font-weight:500!important;}');
      mywindow.document.write('.small-gap-td td {padding: 7px!important}');
      mywindow.document.write('.table-footer td {background: #ededed!important}');
      mywindow.document.write('.page-break{page-break-after: always;}');
      mywindow.document.write('}');
      mywindow.document.write('</style>');
      //mywindow.document.write('@media print{*,:after,:before{color:#000!important;text-shadow:none!important;background:0 0!important;-webkit-box-shadow:none!important;box-shadow:none!important}');
      // mywindow.document.write('<>');
      // mywindow.document.write('.table>tbody>tr>td, .table>tbody>tr>th, .table>tfoot>tr>td, .table>tfoot>tr>th, .table>thead>tr>td, .table>thead>tr>th {');
      // mywindow.document.write('border: solid black !important;border-width: 1px 0 0 1px !important;}</>');
      mywindow.document.write('</head><body style="background-color: #fff !important;" >');
      mywindow.document.write(document.getElementById(divPrint).innerHTML);
      mywindow.document.write('</body></html>');

      mywindow.document.close(); // necessary for IE >= 10
      mywindow.focus(); // necessary for IE >= 10*/

      //mywindow.print();
      //mywindow.close();

      setTimeout(function () {
        mywindow.print();
        var ival = setInterval(function () {
          mywindow.close();
          clearInterval(ival);
        }, 200);
      }, 500);

      //$(".hidePrint").css("display", "block");

      return true;
    }

	</script>
</asp:Content>
