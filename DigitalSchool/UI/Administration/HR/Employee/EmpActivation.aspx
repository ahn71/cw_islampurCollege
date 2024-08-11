<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="EmpActivation.aspx.cs" Inherits="DS.UI.Administration.HR.Employee.EmpActivation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
        .tgPanel
        {
            width:100%;
        }
        input[type="checkbox"]
        {
            margin: 10px 5px 5px 0;
        }
        .dataTables_length, .dataTables_filter {
          display: none;
          padding: 15px;
        }
        .numeric img {
        height:20px;
        width:20px;
        }
        .mid {
            text-align:center;
        }
        @media (min-width: 320px) and (max-width: 480px) {
        .numeric img {
        height:20px;
        width:20px;
        }
            .pagination {
                width:100%;
                float:left;
            }
        }
        #MainContent_rblEmpType label {
            margin-left:3px;
            margin-right:3px;
        }
        #MainContent_rblEmpActivation label {
            margin-left:3px;
            margin-right:3px;
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
                <li><a runat="server" href="~/UI/Administration/HR/hrHome.aspx">Human Resource Module</a></li>  
                <li><a runat="server" href="~/UI/Administration/HR/Employee/EmpHome.aspx">Employee Management</a></li> 
                <li class="active">Employee Activation</li>              
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="tgPanel">
        <asp:UpdatePanel runat="server">
            <Triggers> 
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
            </Triggers>
            <ContentTemplate>
                 <div class="tgPanelHead">
            <asp:Label ID="lblTitle" runat="server" Text="" ClientIDMode="Static"></asp:Label>
            Information
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
        <div class="widget">
            <div class="head">
                <img src="/Images/action/refresh.png" class="refresh" onclick="$('#btnSearch').click();" />
                <div class="head_title">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="rblEmpType" />--%>
                        </Triggers>
                        <ContentTemplate>
                            <div>
                                Employee Type : &nbsp
                                 <asp:RadioButtonList ID="rblEmpType"  runat="server" RepeatLayout="Flow"  RepeatDirection="Horizontal" CssClass="radiobuttonlist" AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged">
                                     
                                </asp:RadioButtonList>
                                 &nbsp &nbsp &nbsp &nbsp Status : &nbsp
                                <asp:RadioButtonList ID="rblEmpActivation"  runat="server" RepeatLayout="Flow"  RepeatDirection="Horizontal" CssClass="radiobuttonlist" AutoPostBack="true" OnSelectedIndexChanged="rblEmpActivation_SelectedIndexChanged" >
                                     <asp:ListItem  Selected="True" Value="1">Active</asp:ListItem>
                                     <asp:ListItem  Value="0">Inactive</asp:ListItem>
                                     
                                </asp:RadioButtonList>
                                <%--<asp:CheckBox runat="server" ID="chkIsTeacher" Text="Is Teacher" ClientIDMode="Static" AutoPostBack="true" Checked="true" 
                                    OnCheckedChanged="chkIsTeacher_CheckedChanged" />  --%>                   
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="dataTables_filter_New" style="float: right;">
                    <label>
                        Search:
                    <input type="text" class="Search_New" placeholder="type here..." />
                    </label>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
                    <asp:AsyncPostBackTrigger ControlID="rblEmpActivation" />
                </Triggers>
                <ContentTemplate>
                   <%--<div id="divTeacherInfo" class="datatables_wrapper" runat="server" style="width: 100%; max-height:680px; overflow-y: scroll;overflow-x: hidden; "></div>--%>
                      <asp:GridView HeaderStyle-BackColor="#23282C" ID="gvEmployeeList" runat="server" AutoGenerateColumns="false" DataKeyNames="EID"  HeaderStyle-ForeColor="White" HeaderStyle-Font-Bold="true" HeaderStyle-Height="25px" HeaderStyle-Font-Size="14px" Width="100%" OnRowCommand="gvEmployeeList_RowCommand"  >
                        <%-- <PagerStyle CssClass="gridview" />--%>
                          <Columns>  
                              <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                     <%# Container.DataItemIndex + 1 %>                                  
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>                         
                          
                                 <asp:BoundField DataField="EName" HeaderText="Full Name" />
                                 <asp:BoundField DataField="EGender" HeaderText="Gender" />
                                 <asp:BoundField DataField="EFathersName" HeaderText="Father's Name"  />
                                 <asp:BoundField DataField="EMobile" HeaderText="Mobile"  />
                                 <asp:BoundField DataField="IsFaculty" HeaderText="Is Faculty"  />
                                
                                 <%--<asp:BoundField DataField="EmpStatusName" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />--%>                                
                              <asp:TemplateField HeaderText="Note" HeaderStyle-Width="30px" >
                                  <ItemTemplate>
                                     <asp:TextBox runat="server" ID="txtNote" CssClass="dropdown-wrapper"  ></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField> 
                               <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                  <ItemTemplate>
                                      <asp:Button ID="btnInActive" runat="server" CommandName="InActive" Font-Bold="true" ForeColor="red" Text="In Active" Width="55px" Height="30px" OnClientClick="return confirm('Do you want to inactive this student ?')" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                  </ItemTemplate>
                              </asp:TemplateField>                               
                              <asp:TemplateField HeaderText="Action" HeaderStyle-Width="30px">
                                  <ItemTemplate>
                                      <asp:Button ID="btnActive" runat="server" CommandName="Active" Font-Bold="true" ForeColor="Green" Text="Active" Width="55px" Height="30px" OnClientClick="return confirm('Do you want to inactive this student ?')" CommandArgument='<%#((GridViewRow)Container).RowIndex%>' />
                                  </ItemTemplate>
                              </asp:TemplateField>  
                          </Columns>
                     </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvEmployeeList', '');
            });
           
        });
       
      
    </script>
</asp:Content>