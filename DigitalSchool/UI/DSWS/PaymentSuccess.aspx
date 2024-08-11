<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentSuccess.aspx.cs" Inherits="DS.UI.DSWS.PaymentSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" />
     <link href="../../websitedesign/assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
</head>
<body style="background: #ededed">
   <main style="width: 800px; margin: 0 auto; padding: 10px;background-color: #fff;" id="printtable">
        <div class="payment-info-section">
            <header class="payment-header" style="margin: 5px 0;padding: 10px;">             
              
            </header>

            <section>
               <%--Failed Alert--%>
                                        <div class="row">
                                            <br />
                                            <br />
                                            <div class="col-md-12">
                                                <div class="panel panel-default">
                                                  <div class="panel-body">
                                                    <div class="payment-error" style="text-align:center">
                                                        <div style="text-align:center">
                                                            <p>
                                                            <span style="background: #00b703; width: 50px;height:50px;margin: auto;display: block;text-align:center;line-height: 50px;font-size: 28px;color: #fff;border-radius: 50%;"><i class="fa fa-check"></i></span>
                                                            </p>
                                                         </div>
                                                        <div>
                                                            <h2 style="font-size: 32px; color: #000;font-weight:600">Thank You!</h2>
                                                            <h4 style="margin-bottom:30px">Your transaction has succeed.</h4>
                                                            <form runat="server">
                                                                            <asp:Button runat="server" ClientIDMode="Static" ID="btnPreview" class="btn-lg btn-primary" Text="Invoice Preview" OnClick="btnPreview_Click" />
                                                            </form>
                                                
                                                           
                                                            


                                                           
                                                     </div> 
                                                    </div>
                                                  </div>
                                                </div>
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                        <%--Failed Alert--%>
            </section>
            <section>
                
                
               
            </section>
        </div>
    </main>
 
  
    
 
  <!-- BRMS Report Print Kamrul Hasan Rijon-->
</body>
</html>

