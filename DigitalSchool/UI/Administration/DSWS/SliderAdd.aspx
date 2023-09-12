<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SliderAdd.aspx.cs" Inherits="DS.UI.Administration.DSWS.SliderAdd" %>
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
                <li><a runat="server" id="aSlider">Slider</a></li>               
                <li class="active" runat="server" id="liAddEdit"></li>
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
            <div class="col-md-2"></div>           
            <div class="col-md-5">
                <div class="tgPanelHead">Add Slider Image</div>
                <div class="tgPanel">                     
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
