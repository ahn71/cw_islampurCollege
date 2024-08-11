<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Slider.aspx.cs" Inherits="DS.UI.Administration.DSWS.Slider" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }

        .tbl-controlPanel td:first-child {
            text-align: right;
            padding-right: 5px;
        }

        .table-bordered tr th {
            background-color: #23282C;
            color: white;
        }

        .padingtable {
            margin-top: 0px;
        }
        .padingtable th:nth-child(3),th:nth-child(4),th:nth-child(5),th:nth-child(6), td:nth-child(3), td:nth-child(4), td:nth-child(5), td:nth-child(6) {
            text-align:center;
            width:50px;
        }
        .chkbox label {
            margin-left: 7px;
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
               <%--<li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/DSWS/DSWSHome.aspx">Website Settings Module</a></li>--%>
                <li>
                     <a runat="server" id="aDashboard" >
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li>
                <li><a runat="server" id="aAdministration">Administration Module</a></li>
                <li><a runat="server" id="aWebsite">Website Settings Module</a></li>
                <li class="active">Slider</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
     <div class="row">
            <div class="col-md-6">
                 <div class="dataTables_filter_New" style="float: right;margin-right:-223px;">
                     <label>
                         Search:
                         <input type="text" class="Search_New"  placeholder="type here" />
                     </label>
            </div>
                <h4 class="text-right">Slider Image List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">
            <%--<div class="col-md-2"></div>--%>
            <div class="col-md-7">
                <div class="tgPanel">
                <asp:GridView ID="gvSlider" CssClass="table table-bordered padingtable" DataKeyNames="SL,Location,Chosen" AutoGenerateColumns="False" runat="server" AllowPaging="true" PageSize="5" OnRowCommand="gvSlider_RowCommand" OnRowDeleting="gvSlider_RowDeleting" OnPageIndexChanging="gvSlider_PageIndexChanging">
                    <PagerStyle CssClass="gridview" />
                    <Columns>
                         <asp:TemplateField>
                            <HeaderTemplate>
                                SL
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField  HeaderText="Image">
                  <ItemTemplate>
                      <asp:Image ID="imgSlider" Height="100px" ImageUrl='<%# Bind("Location")%>' runat="server" />
                  </ItemTemplate>
                    </asp:TemplateField>                      
                        <asp:BoundField HeaderText="Ordering" DataField="Ordering" />  
                         <asp:TemplateField HeaderText="Chosen">                                      
                                     <ItemTemplate  >
                                        <asp:CheckBox ID="chkChosen"  Enabled="false"  runat="server" Checked='<%#bool.Parse(Eval("Chosen").ToString())%>' />
                                     </ItemTemplate>
                                 </asp:TemplateField>                               
                        <asp:ButtonField CommandName="change" HeaderText="Edit" ButtonType="Button" Text="Edit" ControlStyle-CssClass="btn btn-success" />
                        <%-- <asp:TemplateField HeaderText="Edit">                                    
                                     <ItemTemplate >
                                         <asp:Button ID="btnEdit" runat="server"  Text="Edit" CommandName="Edit" CssClass="btn btn-success" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"/>
                                     </ItemTemplate>
                                 </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="Delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Are you sure you want to delete');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                    </div>
            </div>
            <div class="col-md-5">
                <div class="tgPanelHead">Add Slider Image</div>
                <div class="tgPanel">
                     <%-- <div class="row tbl-controlPanel">
                        <div class="col-sm-10 col-sm-offset-1">
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:Image ID="imgProfile" class="profileImage" ClientIDMode="Static" runat="server" ImageUrl="~/Images/dsimages/blank-frame.png" />  <br />
                                    <asp:FileUpload ID="FileUpload1" runat="server" onclick="" onchange="previewFile()" Width="122px" ClientIDMode="Static" />    
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4">Ordering</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtOrdering" runat="server" ClientIDMode="Static" class="input controlLength form-control"></asp:TextBox>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <label class="col-sm-4"></label>
                                <div class="col-sm-8">
                                    <asp:CheckBox ID="chkChosen" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Chosen"/>
                                </div>
                              </div>
                              <div class="row tbl-controlPanel">
                                <div class="col-sm-offset-4 col-sm-8">
                                    <asp:Button CssClass="btn btn-primary" ID="btnSaveAD" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSaveAD_Click"/>
                                    &nbsp;<asp:Button runat="server" Text="Clear" ID="btnClearAD" ClientIDMode="Static" CssClass="btn btn-default"/>
                                </div>
                              </div>
                        </div>
                    </div>--%>
                      <div class="row">
                    <div style="width:246px; margin:0px auto">
                        <asp:Image ID="imgProfile" class="profileImage" Width="250px" ClientIDMode="Static" runat="server" ImageUrl="~/Images/dsimages/blank-frame.png" />  <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" onclick="" onchange="previewFile()" Width="122px" ClientIDMode="Static" />                                
                    </div>                                                     
                </div>
                    <div class="row" style="margin-left:10px">                         
                    </div>
                    <table class="tbl-controlPanel">
                        <tr>                         
                            <td>Ordering
                            </td>
                            <td>
                                <asp:TextBox ID="txtOrdering" runat="server" Width="246px" ClientIDMode="Static" class="input controlLength"></asp:TextBox>
                            </td>
                        </tr>
                      <tr><td></td><td>
                                        <asp:CheckBox ID="chkChosen" CssClass="chkbox" runat="server" ClientIDMode="Static" Text="Chosen"/></td>
                                </tr>
                        <tr>
                            <td>
                            </td>
                            <td><asp:Button CssClass="btn btn-primary" ID="btnSaveAD" runat="server" Text="Save" ClientIDMode="Static" OnClick="btnSaveAD_Click"/>
                                &nbsp;<asp:Button runat="server" Text="Clear" ID="btnClearAD" ClientIDMode="Static" CssClass="btn btn-default"/>
                            </td>
                        </tr>                       
                    </table>                 
                </div>                   
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'MainContent_gvSlider', '');
            });
        });
          function previewFile() {
              var preview = document.querySelector('#<%=imgProfile.ClientID %>');
             var file = document.querySelector('#<%=FileUpload1.ClientID %>').files[0];
             var reader = new FileReader();

             reader.onloadend = function () {
                 preview.src = reader.result;
             }

             if (file) {
                 reader.readAsDataURL(file);
             } else {
                 preview.src = "";
             }
         }
         function validateInputs() {
             if (validateText('txtAlbumName', 1, 50, 'Enter Valid Album Name') == false) return false;            
             return true;
         }
      </script>
</asp:Content>
