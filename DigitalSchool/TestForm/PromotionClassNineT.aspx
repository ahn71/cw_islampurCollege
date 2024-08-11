<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PromotionClassNineT.aspx.cs" Inherits="DigitalSchool.TestForm.PromotionClassNine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMessage" runat="server">
    <ContentTemplate><p class="message"  id="lblMessage" clientidmode="Static" runat="server"></p></ContentTemplate>
</asp:UpdatePanel>

<asp:HiddenField ID="lblDepartmentId" ClientIDMode="Static" runat="server"/>

<div class="main-div">

        <div class="leftSide-div-promotion">

              <div class="tgPanel">
                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                      <ContentTemplate>
                <div class="tgPanelHead-promotion">All Student List</div>
               </ContentTemplate>

                  </asp:UpdatePanel>
            </div>
            <div style="margin-top:18px">
            <asp:CheckBoxList ID="chkbl" runat="server"></asp:CheckBoxList>
            </div>
            
        </div>
      <div class="leftRight-div-promotion">

              <div class="tgPanel">
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                      <ContentTemplate>
                <div class="tgPanelHead-promotion">Add Group</div>
               </ContentTemplate>

                  </asp:UpdatePanel>
            </div>
          <div class="depertment_button">
          <asp:Button ID="btnScience" runat="server" Text="Science" CssClass="promotion_button" OnClientClick="return validateInputs();" OnClick="btnScience_Click"/>
           </div>
          <div class="depertment_button">
          <asp:Button ID="btnCommerce" runat="server" Text="Commerce" CssClass="promotion_button" OnClientClick="return validateInputs();"/>
          </div>
          <div class="depertment_button">
          <asp:Button ID="btnAtrs" runat="server" Text="Arts" CssClass="promotion_button" OnClientClick="return validateInputs();"/>
          </div>
          <div class="depertment_button">
          <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="promotion_button" OnClientClick="return validateInputs();"/>
          </div>
        </div>
     <div class="RightLeft-div-promotion">

              <div class="tgPanel-promotion">
                  <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                      <ContentTemplate>
                <div class="tgPanelHead-promotion">Science</div>
               </ContentTemplate>

                  </asp:UpdatePanel>
            </div>
        <div id="div3" style="height:200px;max-height:200px; margin-top:18px;overflow:auto;overflow-x:hidden;">
                      <asp:GridView ID="gvScience" runat="server" Width="232px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" ClientIDMode="Static" Font-Size="X-Small">
                                      <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true"/></ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                          <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                          <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Left" />
                          <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                          <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                          <SortedAscendingHeaderStyle BackColor="#594B9C" />
                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                          <SortedDescendingHeaderStyle BackColor="#33276A" />
                      </asp:GridView>
                  </div>
            
        </div>

<div class="rightSide-div-promotion">
    <div class="tgPanel">
                  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>
                <div class="tgPanelHead-promotion">Commerce</div>
               </ContentTemplate>

                  </asp:UpdatePanel>
            </div>
     <div id="div1" style="height:200px;max-height:200px;margin-top:18px;overflow:auto;overflow-x:hidden;">
                      <asp:GridView ID="gvCommerce" runat="server" Width="232px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" ClientIDMode="Static" Font-Size="X-Small">
                                      <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true"/></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                          <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                          <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Left" />
                          <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                          <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                          <SortedAscendingHeaderStyle BackColor="#594B9C" />
                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                          <SortedDescendingHeaderStyle BackColor="#33276A" />
                          
                      </asp:GridView>
                  </div>
        </div>

    <div class="right-div-promotion">
    <div class="tgPanel">
                  <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                      <ContentTemplate>
                <div class="tgPanelHead-promotion"> Arts</div>
               </ContentTemplate>

                  </asp:UpdatePanel>
            </div>
        <div id="div2" style="height:200px;margin-top:18px;max-height:200px;overflow:auto;overflow-x:hidden;">
                      <asp:GridView ID="gvArts" runat="server" Width="232px" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" ClientIDMode="Static" Font-Size="X-Small">
                                      <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkRow" runat="server" AutoPostBack="true"/></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                          <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                          <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                          <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Left" />
                          <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                          <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                          <SortedAscendingCellStyle BackColor="#F1F1F1" />
                          <SortedAscendingHeaderStyle BackColor="#594B9C" />
                          <SortedDescendingCellStyle BackColor="#CAC9C9" />
                          <SortedDescendingHeaderStyle BackColor="#33276A" />
                          
                      </asp:GridView>
                  </div>
        </div>


    </div>



<script src="/Scripts/adviitJS.js" type="text/javascript"></script>
<script src="/Scripts/master.js"></script>


    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtDepartment', 1, 50, 'Enter Department Name') == false) return false;
            return true;
        }
        function editDepartment(Did) {
            $('#lblDepartmentId').val(Did);

            var strDepartment = $('#r_' + Did + ' td:first-child').html();
            $('#txtDepartment').val(strDepartment);
            var strS = $('#r_' + Did + ' td:nth-child(2)').html();
            if (strS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            $("#btnSave").val('Update');

        }
    </script>
</asp:Content>
