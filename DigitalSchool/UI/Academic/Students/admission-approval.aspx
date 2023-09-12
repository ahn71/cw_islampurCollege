<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="admission-approval.aspx.cs" Inherits="DS.UI.Academic.Students.admission_approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .kk {
            width: 140px!important;
        }
        @media only screen and (max-width: 600px) {
          .kk {
                 width: 100%!important;
            }
          .Search_New{
              width:100%!important;
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
                    <%--<a runat="server" href="~/Dashboard.aspx">--%>
                    <a runat="server" id="aDashboard">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <%--<li><a runat="server" href="~/UI/Academic/AcademicHome.aspx">Academic Module</a></li>
                <li><a runat="server" href="~/UI/Academic/Students/StdHome.aspx">Student Module</a></li> --%>
                <li><a runat="server" id="aAcademicHome">Academic Module</a></li>
                <li><a runat="server" id="aStudentHome">Student Module</a></li> 
                <li class="active">Admission Approval</li>                              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="gride-view-list">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>  
                          
                <asp:AsyncPostBackTrigger ControlID="ddlClass" />              
                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />              
            </Triggers>
            <ContentTemplate>                
            <div class="row tbl-controlPanel"> 
		        <div class="col-xs-12 col-sm-10 col-sm-offset-1 boX">
			        <div class="form-inline">
                         <div class="form-group">
					         <label for="exampleInputName2">Year</label>
					            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control kk" ClientIDMode="Static" >                                                            
                            </asp:DropDownList>
				         </div>
				         <div class="form-group">
					         <label for="exampleInputName2">Shift</label>
					            <asp:DropDownList ID="ddlShift" runat="server" CssClass="form-control kk" ClientIDMode="Static" >
                                            <asp:ListItem Value="00">All</asp:ListItem>                  
                            </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Class</label>
                             <asp:DropDownList ID="ddlClass" runat="server" CssClass="form-control kk"
                                  ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged" >                                <asp:ListItem Value="00">All</asp:ListItem>
                            </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Group</label>
                             <asp:DropDownList ID="ddlGroup" runat="server" CssClass="form-control kk" 
                               AutoPostBack="true"  ClientIDMode="Static" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">                             <asp:ListItem Value="00">All</asp:ListItem>
                            </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2">Section</label>
                            <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control kk" ClientIDMode="Static" > 
                                <asp:ListItem Value="00">All</asp:ListItem>
                            </asp:DropDownList>
				         </div>
				        <div class="form-group">
					         <label for="exampleInputName2"></label>
					            <asp:Button ID="btnSearch"  runat="server" Text="Search" ClientIDMode="Static" CssClass="btn btn-primary litleMargin" 
                                OnClientClick="return btnSearch_validation();" OnClick="btnSearch_Click" />
				         </div>
                        
				        
			        </div>
	          </div>
         </div>
             
             <div class="row tbl-controlPanel"> 
		        <div class="col-md-12 boX">
			        <div class="form-inline pull-right">
                        <input type="text"  class="Search_New form-control" style="color:black"  placeholder="type here to search" />
                        </div>
                    </div>
                 </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
     <asp:UpdatePanel ID="up2" runat="server" UpdateMode="Conditional">
         <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />             
                
            </Triggers>
            <ContentTemplate>
    <div class="gride-view-list">
                 <asp:GridView ID="gvStudentList" runat="server" AutoGenerateColumns="false" DataKeyNames="SL" 
                     CssClass="table table-bordered" BackColor="White" HeaderStyle-BackColor="Black" HeaderStyle-ForeColor="White" OnRowCommand="gvStudentList_RowCommand">
                     <%--<PagerStyle CssClass="gridview" />--%>
            <Columns>
                  <asp:TemplateField HeaderText="SL"> 
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Photo
                    </HeaderTemplate>
                    <ItemTemplate>                                       
                     <asp:Image runat="server" ID="imgStudentPhoto" ClientIDMode="Static" Height="60px" Width="60px" ImageUrl='<%# Eval("ImageName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
               <asp:BoundField DataField="AdmissionFormNo" HeaderText="Admission Form No" />
               <asp:BoundField DataField="FullName" HeaderText="Full Name" />
                 <asp:BoundField DataField="Gender" HeaderText="Gender" />
                 <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
                 <%--<asp:BoundField DataField="FathersName" HeaderText="Father's Name" />
                 <asp:BoundField DataField="MothersName" HeaderText="Mother's Name" />
                 <asp:BoundField DataField="GuardianName" HeaderText="Guardian Name" />
                <asp:BoundField DataField="GuardianMobileNo" HeaderText="Guardian Mobile" />--%>
                <asp:BoundField DataField="ClassName" HeaderText="Class" />               
                <asp:BoundField DataField="GroupName" HeaderText="Group" />                
                <asp:BoundField DataField="SectionName" HeaderText="Section" />
                <asp:BoundField DataField="ShiftName" HeaderText="Shift" />               
                <asp:BoundField DataField="BatchName" HeaderText="Batch" />               
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Batch
                    </HeaderTemplate>
                    <ItemTemplate>    
                        <asp:Label runat="server" ClientIDMode="Static" ID="lblBatchName" Text='<%# Bind("BatchName") %>'></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:BoundField DataField="CreateOn" HeaderText="Create On" />
                <asp:BoundField DataField="PaymentStatus" HeaderText="Payment Status" ItemStyle-Font-Size="X-Large" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" />
                <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Roll
                    </HeaderTemplate>
                    <ItemTemplate>                                       
                        <asp:TextBox ID="txtRollNo"  runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Roll"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Note
                    </HeaderTemplate>
                    <ItemTemplate>                                       
                        <asp:TextBox ID="txtNote"  runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Note"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center">
                    <HeaderTemplate >
                       Approve
                    </HeaderTemplate>
                    <ItemTemplate>                                       
                       <asp:Button runat="server" ID="btnApprove" Text="Approve" CssClass="btn btn-success" CommandName="Approve"  OnClientClick="return confirm('Are you sure, you want to apporve the application?')"  CommandArgument='<%#((GridViewRow)Container).RowIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate >
                       Reject
                    </HeaderTemplate>
                    <ItemTemplate>
                       <asp:Button runat="server" ID="btnReject" Text="Reject" CssClass="btn btn-danger" CommandName="Reject" OnClientClick="return confirm('Are you sure, you want to reject the application?')" CommandArgument='<%#((GridViewRow)Container).RowIndex %>' />
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>
                    </div>
     </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvStudentList', '');
            });
        });
     </script>
</asp:Content>
