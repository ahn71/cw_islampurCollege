
<%@ Page Title="Fee Collection Details" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="FeeCollectionDetails.aspx.cs" Inherits="DS.UI.Administration.Finance.FeeManaged.CollectionDetails" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel{
            width:100%;
            min-height:500px;
        }
        input[type="checkbox"]
        {
            margin: 5px;
        }
        .controlLength{
            min-width:140px!important;
        }
          .table tr th{
            background-color: #23282C;
            color: white;
        }

        .box2 {
        min-width:130px;
        }
       .tb{
            float:right!important;
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
         /*.dataTables_length, .dataTables_filter{
            display: none;
            padding: 15px;
        }*/
        /*#MainContent_TabContainer_TabPanel5_gvcollectiondtl_info {
             display: none;
            padding: 15px;
        }
         #MainContent_TabContainer_TabPanel5_gvcollectiondtl_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }*/

        .controlLength {
            min-width:115px;
            
        }
        .form-group {
            margin-top:10px;
        }  
        @media (min-width: 320) and (max-width: 480) {
            .boxs {
                width:none;
            }
            #CheckBox1 {
                margin-left:0px!important;
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FeeManaged/FeeHome.aspx">Fee Management</a></li>
                <li class="active">Fee Collection Details</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <ajax:TabContainer ID="TabContainer" runat="server" ActiveTabIndex="0" Width="100%" TabIndex="1">
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
                                    <div class="row tbl-controlPanel"> 
                                        <div class="col-sm-8 col-sm-offset-2">
                                            <table>
                                            <div class="form-inline row">
                                                 <div class="form-group col-md-3">
                                                     <label for="exampleInputName2">Shift</label>
                                                        <asp:DropDownList ID="ddlCShift" runat="server" ClientIDMode="Static"
                                                            CssClass="input controlLength form-control"> 
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>                                                   
                                                        </asp:DropDownList>
                                                 </div>
                                                <div class="form-group col-md-3">
                                                     <label for="exampleInputName2">Batch</label>
                                                        <asp:DropDownList ID="ddlCBatch" AutoPostBack="true" ClientIDMode="Static"
                                                            OnSelectedIndexChanged="ddlCBatch_SelectedIndexChanged" 
                                                            runat="server" CssClass="input controlLength form-control">
                                                            <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                                                 </div>
                                                <div class="form-group col-md-3">
                                                     <label for="exampleInputName2">Group</label>
                                                     <asp:DropDownList ID="ddlGroup" AutoPostBack="true" ClientIDMode="Static"
                                                          OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"
                                                        runat="server" CssClass="input controlLength form-control">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                 </div>
                                                <div class="form-group col-md-3">
                                                     <label for="exampleInputName2">Section </label>
                                                      <asp:DropDownList ID="ddlCSection" runat="server" ClientIDMode="Static"
                                                             CssClass="input controlLength form-control">
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                                                 </div>
                                               
                                            </div>
                                                </table>
                                        </div>
                                    </div>

                               

                                    <div class="row tbl-controlPanel"> 
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <div class="form-inline row">
                                            <div class="form-group col-md-3">
                                                <label for="exampleInputName2">From</label>
                                                <asp:TextBox runat="server" ID="txtCFrom" ClientIDMode="Static" CssClass="input controlLength form-control boxs"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender5" Format="dd-MM-yyyy" runat="server" 
                                                    TargetControlID="txtCFrom"></ajax:CalendarExtender>
                                            </div>
                                            <div class="form-group col-md-3">
                                               <label for="exampleInputName2">To</label>
                                                <asp:TextBox runat="server" ID="txtCTo" ClientIDMode="Static" CssClass="input controlLength form-control"></asp:TextBox>
                                                <ajax:CalendarExtender ID="CalendarExtender6" Format="dd-MM-yyyy" runat="server" 
                                                    TargetControlID="txtCTo"></ajax:CalendarExtender>
                                            </div>
                                            <div class="form-group col-md-3">
                                               <label for="exampleInputName2">Category</label>
                                                <asp:DropDownList ID="ddlCFeeCat" runat="server" ClientIDMode="Static"
                                                     CssClass="input controlLength form-control">
                                                    <asp:ListItem Value="0">...Select...</asp:ListItem>      
                                                </asp:DropDownList>
                                            </div>
                                            
                                           
                                        </div>                               
                                       <div class="form-inline row">
                                                <div class="form-group col-md-12">
                                               <label for="exampleInputName2"></label>
                                                <asp:CheckBox ID="chkCTodayCollect"  ClientIDMode="Static" Text="Is Today Collection" runat="server" />
                                            </div>
                                            </div>
                                           
                                       
                                    </div>
                                </div>

                                         <div class="row tbl-controlPanel">
                                        <div class="col-sm-8 col-sm-offset-2">
                                            <div class="form-inline row">
                                                <div class="form-group col-md-12 style="margin-left:10px">
                                                    <asp:RadioButtonList runat="server" ID="rblReport" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="Summary" Selected="True">&nbsp; Fee Collection Summary Report </asp:ListItem>
                                                        <asp:ListItem Value="Details">&nbsp; Fee Collection Details Report </asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                               
                                <div class="row tbl-controlPanel"> 
                                    <div class="col-sm-4 col-sm-offset-2">
                                        <div class="col-sm-4">
                                        <div class="form-inline">
                                            <div class="form-group">
                                                    <label for="exampleInputName2"></label>
                                                    <asp:Button ID="btnsearch" OnClick="btnsearch_Click" Text="Search" 
                                                    OnClientClick="return btnCSearch_Validation();" 
                                                    ClientIDMode="Static" runat="server" CssClass="btn btn-success tb"/>
                                                </div>
                                           </div>
                                        </div>
                                         <div class="form-inline">
                                            <div class="form-group">
                                                    <label for="exampleInputName2"></label>
                                                    <asp:Button ID="btnCSearch" OnClick="btnCSearch_Click" Text="Preview & Print" 
                                                    OnClientClick="return btnCSearch_Validation();" 
                                                    ClientIDMode="Static" runat="server" CssClass="btn btn-success tb"/>
                                                </div>
                                           </div>
                                       
                                    </div>
                                </div>
                                      <div class="">
                    <div class="row">
                        <div class="col-md-12">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">                               

                            <asp:GridView ID="gvcollectiondtl" runat="server" Width="100%" DataKeyNames="StudentId"
                                 CssClass="table table-striped table-bordered dt-responsive nowrap"  CellSpacing="0"  AutoGenerateColumns="False"                                 >

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
                                            <asp:HiddenField ID="hideStdTypeId" runat="server"
                                                Value='<%# DataBinder.Eval(Container.DataItem, "StdTypeId")%>' />
                                            <asp:Label ID="lblName" Style="float: left" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FullName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Class">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClassName" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "ClassName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Roll">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoll" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "RollNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FeeCatName">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hideFeeCatId" runat="server"
                                                Value='<%# DataBinder.Eval(Container.DataItem, "FeeCatId")%>' />
                                            <asp:Label ID="lblFeeCatName" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FeeCatName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FeeAmount" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblFeeAmount" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "FeeAmount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "GrandTotal")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DiscountTK">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscountTK" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "DiscountTK")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AmountPaid">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmountPaid" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "AmountPaid")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DueAmount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDueAmount" Style="width: 50px; text-align: start;" runat="server"
                                                Text='<%# DataBinder.Eval(Container.DataItem, "DueAmount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reprint">
                                        <ItemTemplate>
                                            <asp:Button ID="btnprintmoneyreceipt" Style="width: 50px; text-align: start;" CssClass="btn-default" runat="server"
                                               OnClick="btnprintmoneyreceipt_Click"
                                                  Text="Reprint"></asp:Button>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <%--  </div>--%>
                        </asp:Panel>
                            </div>
                    </div>
                        </div>                                                               
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
           
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
                                    <div class="row tbl-controlPanel"> 
                                        <div class="col-sm-8 col-sm-offset-2">
                                            <div class="form-inline">
                                                 <div class="form-group">
                                                     <label for="exampleInputName2">Shift</label>                                                    
                                                         <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                            CssClass="form-control box2">
                                                            <asp:ListItem>Morning</asp:ListItem>
                                                            <asp:ListItem>Day</asp:ListItem>
                                                        </asp:DropDownList>
                                                 </div>
                                                <div class="form-group">
                                                     <label for="exampleInputName2">Batch</label>                                                    
                                                          <asp:DropDownList ID="dlBatchDueList" AutoPostBack="true" runat="server" ClientIDMode="Static"
                                                        OnSelectedIndexChanged="dlBatchDueList_SelectedIndexChanged" CssClass="form-control box2">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                     </div>
                
                                                 <div class="form-group">
                                                    <label for="exampleInputName2">Group</label>                                                     
                                                         <asp:DropDownList ID="dlGroupDueList" runat="server" CssClass="form-control box2"
                                                             OnSelectedIndexChanged="dlGroupDueList_SelectedIndexChanged" ClientIDMode="Static">
                                                            <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                        </asp:DropDownList>
                                                     </div>
                
                                                 
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row tbl-controlPanel"> 
                                    <div class="col-sm-8 col-sm-offset-2">
                                        <div class="form-inline">
                                             <div class="form-group">
                                                <label for="exampleInputName2">Section</label>                                                
                                                    <asp:DropDownList ID="dlSectionDueList" runat="server" CssClass="form-control box2" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                    
                                           <div class="form-group">
                                                 <label for="exampleInputName2">Fee Category</label>                                                  
                                                      <asp:DropDownList ID="dlFeesCategoryDueList" runat="server" CssClass="form-control box2" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                  </div>
                                             <div class="form-group">
                                                <label for="exampleInputName2"></label>                                               
                                                    <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Preview & Print" CssClass="btn btn-success" ClientIDMode="Static"
                                                       OnClick="btnPrintPreviewDueList_Click"  />

                                                    <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                                </div>
                                             
                                           
                                        </div>
                                    </div>
                                </div>

                                    <div class="">
                                        <%--<table class="tbl-controlPanel" style="width: 800px;">
                                            <tr>
                                                <td>Shift
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlShiftDueList" runat="server" ClientIDMode="Static" AutoPostBack="false"
                                                        CssClass="input controlLength">
                                                        <asp:ListItem>Morning</asp:ListItem>
                                                        <asp:ListItem>Day</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Batch
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlBatchDueList" AutoPostBack="true" runat="server" ClientIDMode="Static"
                                                        OnSelectedIndexChanged="dlBatchDueList_SelectedIndexChanged" CssClass="input controlLength">
                                                        <asp:ListItem Selected="True">...Select...</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Group
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlGroupDueList" runat="server" CssClass="input controlLength"
                                                         OnSelectedIndexChanged="dlGroupDueList_SelectedIndexChanged" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                
                                            </tr>
                                            <tr>
                                                <td>Section
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlSectionDueList" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>Fee Category
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dlFeesCategoryDueList" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">...Select...</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                                <td>                                                   
                                                    <asp:Button runat="server" ID="btnPrintPreviewDueList" Text="Preview & Print" CssClass="btn btn-success" ClientIDMode="Static"
                                                       OnClick="btnPrintPreviewDueList_Click"  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td colspan="3">
                                                    <asp:Label ID="Label2" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>--%>
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
            if(fromDate == "" && toDate == "" && chk == false)
            {
                showMessage("Plese Select Date Rang", 'error');
                return false;
            }
            if (fromDate != "" && toDate != "" && chk != false) {
                showMessage("Plese Select From And To Date Or Is Today Collection Collection At a Time", 'error');
                return false;
            }
            if ((fromDate == "" && toDate != "") || (fromDate != "" && toDate == "") && chk == false)
            {
                showMessage("Plese Select Another Date Rang", 'error');
                return false;
            }
            return true;
        }
        $(document).ready(function () {
            //$(document).on("keyup", '.Search_New', function () {
               // searchTable($(this).val(), 'MainContent_TabContainer_TabPanel5_gvcollectiondtl', '');
                $('#MainContent_TabContainer_TabPanel5_gvcollectiondtl').dataTable({
                    "iDisplayLength": 10,
                    "lengthMenu": [10, 20, 30, 40, 50, 100]
                });
            //});
        });
        function loadStudentInfo() {
            $('#MainContent_TabContainer_TabPanel5_gvcollectiondtl').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
    </script>
</asp:Content>

