<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="DS.UI.websitedesign.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="assets/plugins/bootstrap/css/bootstrap.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <header class="header-area">
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                </div>
                <div class="col-md-6">
                    <div class="top-menu">
                        <ul class="pull-right">                            
                            <li><a href="">Notice</a></li>
                            <li><a href="">Gallery</a></li>
                            <li><a href="">Contact us</a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    <div class="logo">
                        <img src="assets/images/logo.png" alt="">
                    </div>
                </div>
                <div class="col-md-7">
                    <div class="header-text ">
                        <h1>ড. মাহবুবুর রহমান মোল্লা কলেজ</h1>
                        <h4>ডেমরা রোড, মাতুয়াইল, যাত্রাবাড়ী, ঢাকা-১৩৬১</h4>
                    </div>
                </div>
                <div class="col-md-3">
                    <a class="online-btn pull-right" href="">ONLINE PORTAL</a>
                </div>
            </div>
        </div>
    </header>
    <section class="menu-area">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="menu">

                        <!-- Navbar default -->
                        <nav class="navbar navbar-default wow fadeInDown">
                            <div class="container">
                                <div class="navbar-header">
                                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar1" aria-expanded="false">
				<span class="sr-only">Toggle navigation</span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
				<span class="icon-bar"></span>
			</button>

                                </div>
                                <div id="navbar1" class="navbar-collapse collapse">
                                    <ul class="nav navbar-nav">
                                        <li class=""><a>Home</a></li>
                                        
                                        <li><a href="#contact">Contact</a></li>
                                        <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">About <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Chairman Message</a></li>
                                                <li><a href="#">Principal Message</a></li>
                                                <li><a href="#">Governing Body</a></li>
                                                <li class="dropdown-header">At a Glance</li>
                                                <li><a href="#">Why DMRC</a></li>
                                                <li><a href="#">Teacher`s information</a></li>
                                                <li><a href="#">Officer`s &amp; Staff`s Information</a></li>
                                            </ul>
                                        </li>
                                         <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Facilities <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">D. Tech</a></li>
                                                <li><a href="#">D. Net</a></li>
                                                <li><a href="#">Lab</a></li>
                                                <li class="dropdown-header">At a Glance</li>
                                                <li><a href="#">Library</a></li>
                                                <li><a href="#">Transport</a></li>
                                            </ul>
                                        </li>
                                        
                                          <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Admission <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Help Desk</a></li>
                                                <li><a href="#">EIIN</a></li>
                                                <li><a href="#">Admission Form</a></li>                                              
                                                <li><a href="#">Prospectus</a></li>                                           
                                            </ul>
                                        </li>
                                        <li><a href="#about">Academic Calendar</a></li>
                                        <li><a href="#about">Exam Schedule</a></li>
                                        
                                        <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Class Schedule <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Morning Shift – XI</a></li>
                                                <li><a href="#">Day Shift – XI</a></li>
                                                <li><a href="#">Morning Shift - XII</a></li>                                              
                                                <li><a href="#">Day Shift – XII</a></li>                                           
                                            </ul>
                                        </li>
                                        
                                        <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Extra-Curricular <span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">Scout</a></li>
                                                <li><a href="#">Debate Club</a></li>
                                                <li><a href="#">Science Club</a></li>                                              
                                                <li><a href="#">Day Shift – XII</a></li>                                           
                                            </ul>
                                        </li>
                                        <li class="dropdown">
                                            <a class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Result<span class="caret"></span></a>
                                            <ul class="dropdown-menu">
                                                <li><a href="#">First Year Final</a></li>
                                                <li><a href="#">Test Exam</a></li>
                                                <li><a href="#">Model Test Exam</a></li>                                              
                                                <li><a href="#">Recruitment Exam</a></li>                                           
                                            </ul>
                                        </li>
                                        
                                        
                                    </ul>
                                </div>
                            </div>
                        </nav>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section>
        <div class="slider">
            <div id="first-slider">
                <div id="carousel-example-generic" class="carousel slide carousel-fade">
                    <!-- Indicators -->
                    <ol class="carousel-indicators">
                        <li data-target="#carousel-example-generic" data-slide-to="0" class="active"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="1"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="2"></li>
                        <li data-target="#carousel-example-generic" data-slide-to="3"></li>
                    </ol>
                    <!-- Wrapper for slides -->
                    <div class="carousel-inner" role="listbox">
                        <!-- Item 1 -->
                        <div class="item active slide1">
                            <div class="row">
                                <div class="container">
                                    <div class="col-md-3 text-right">
                                        <!--                                        <img style="max-width: 200px;" data-animation="animated zoomInLeft" src="http://s20.postimg.org/pfmmo6qj1/window_domain.png">-->
                                    </div>
                                    <div class="col-md-9 text-left">
                                        <h3 data-animation="animated bounceInDown">Add images, or even your logo!</h3>
                                        <h4 data-animation="animated bounceInUp">Easily use stunning effects</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Item 2 -->
                        <div class="item slide2">
                            <div class="row">
                                <div class="container">
                                    <div class="col-md-7 text-left">
                                        <h3 data-animation="animated bounceInDown"> 50 animation options A beautiful</h3>
                                        <h4 data-animation="animated bounceInUp">Create beautiful slideshows </h4>
                                    </div>
                                    <div class="col-md-5 text-right">
                                        <!--                                        <img style="max-width: 200px;" data-animation="animated zoomInLeft" src="http://s20.postimg.org/sp11uneml/rack_server_unlock.png">-->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Item 3 -->
                        <div class="item slide3">
                            <div class="row">
                                <div class="container">
                                    <div class="col-md-7 text-left">
                                        <h3 data-animation="animated bounceInDown">Simple Bootstrap Carousel</h3>
                                        <h4 data-animation="animated bounceInUp">Bootstrap Image Carousel Slider with Animate.css</h4>
                                    </div>
                                    <div class="col-md-5 text-right">

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Item 4 -->
                        <div class="item slide4">
                            <div class="row">
                                <div class="container">
                                    <div class="col-md-7 text-left">
                                        <h3 data-animation="animated bounceInDown">We are creative</h3>
                                        <h4 data-animation="animated bounceInUp">Get start your next awesome project</h4>
                                    </div>
                                    <div class="col-md-5 text-right">
                                        <!--                                        <img style="max-width: 200px;" data-animation="animated zoomInLeft" src="http://s20.postimg.org/9vf8xngel/internet_speed.png">-->
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End Item 4 -->

                    </div>
                    <!-- End Wrapper for slides-->
                    <a class="left carousel-control" href="#carousel-example-generic" role="button" data-slide="prev">
            <i class="fa fa-angle-left"></i><span class="sr-only">Previous</span>
        </a>
                    <a class="right carousel-control" href="#carousel-example-generic" role="button" data-slide="next">
            <i class="fa fa-angle-right"></i><span class="sr-only">Next</span>
        </a>
                </div>
            </div>

        </div>
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="welcome">
                        <div class="callage-img">
                            <img src="assets/images/logo.png" alt="">
                        </div>
                        <div class="callage-content">
                            <h2>Welcome to DMRC College</h2>
                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Nemo asperiores, aut necessitatibus sint fugiat. Excepturi odit, maxime, nisi expedita et autem optio odio distinctio obcaecati reiciendis doloremque! Ipsam ab modi ratione, quia rem culpa similique accusamus repellat reprehenderit necessitatibus odit eos aperiam fuga, voluptate deleniti laudantium reiciendis quidem commodi, aliquid iste nostrum, possimus dicta doloribus! Suscipit tempora atque totam, quod dolorem quibusdam molestias, dignissimos quis quas omnis vero nam nihil doloribus harum numquam inventore eaque, maiores voluptatem et unde corrupti, consequatur officiis incidunt voluptatum. Itaque, quisquam! Ut culpa facilis, nulla voluptatibus quam, quibusdam deleniti odit nisi accusantium voluptates fugiat at obcaecati reiciendis sapiente quasi architecto ad quidem nam velit ab officia. Similique odit, nesciunt eveniet commodi eum fuga nobis optio ipsam alias accusantium, mollitia aspernatur velit, iure sed a laudantium laboriosam. Repellendus ipsum eveniet impedit incidunt aperiam vero quia veritatis, voluptas deleniti nam nulla quasi ab totam mollitia nisi adipisci natus minima voluptate doloribus ad architecto libero vel. Accusamus beatae voluptatem adipisci molestiae et voluptas vitae ab laudantium odio dolorum rem, veritatis, eum animi enim quis eos quam incidunt reiciendis delectus consectetur doloribus aliquam impedit hic. Sunt dolores nemo laboriosam, error molestiae in, a eveniet consequuntur excepturi consectetur placeat nobis voluptates ex deserunt quisquam similique quidem voluptatem labore numquam magni tempora cumque atque. Nam sit non eligendi culpa quos. Quam pariatur eligendi fugit asperiores dolorem, iure, voluptas enim, deleniti quasi aspernatur iste, doloribus autem vitae? Porro molestias quia dolores molestiae aliquam exercitationem saepe, ducimus corrupti sit suscipit, magni aperiam dicta, mollitia eligendi quas pariatur deleniti dolor autem esse. Ipsum nostrum magnam nesciunt exercitationem minima architecto voluptate fugit, soluta labore quo porro recusandae accusantium cum dolores fuga ab placeat! Possimus laboriosam quo nam aspernatur accusantium nemo illo ducimus necessitatibus odit, consequuntur corporis, maxime impedit, repellat sed recusandae! Ad aliquid omnis sapiente.</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </section>

    <section class="container">
        <div class="row">
            <div class="col-md-8">
                <div class="row">
                    <div class="col-md-6">
                        <div class="massage-box">
                            <h2>Principal Message</h2>
                            <div class="massage-content">
                                <img src="assets/images/logo.png" alt="">
                                <h4>Md: Obaidullah Nayan</h4>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quis dolores, ipsam nostrum eos facilis velit doloremque quidem ad sed voluptas amet eveniet repudiandae magnam aspernatur, ullam temporibus. Iste maxime dolor quia atque impedit fuga animi culpa, blanditiis, nisi esse numquam. Enim fugiat ratione, quo inventore ad veniam non consequatur error officiis est, alias. Doloremque aut blanditiis, nulla, sint vero possimus! Iusto dicta, eveniet, similique deserunt officiis quos fugiat aperiam veritatis voluptas. Consequuntur repellendus corrupti debitis labore nam ad porro esse eligendi quia. nihil, unde. Perspiciatis atque omnis, officia modi ex ad quisquam deleniti tenetur blanditiis dolores neque et, minus soluta quae. Fuga itaque totam neque laboriosam dolorum sunt nisi adipisci.</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="massage-box">
                            <h2>Chairman Message</h2>
                            <div class="massage-content">
                                <img src="assets/images/logo.png" alt="">
                                <h4>Dr. Mahbubur Rahman Molla</h4>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Quis dolores, ipsam nostrum eos facilis velit doloremque quidem ad sed voluptas amet eveniet repudiandae magnam aspernatur, ullam temporibus. Iste maxime dolor quia atque impedit fuga animi culpa, blanditiis, nisi esse numquam. Enim fugiat ratione, quo inventore ad veniam non consequatur error officiis est, alias. Doloremque aut blanditiis, nulla, sint vero possimus! Iusto dicta, eveniet, similique deserunt officiis quos fugiat aperiam veritatis voluptas. Consequuntur repellendus corrupti debitis labore nam ad porro esse eligendi quia. nihil, unde. Perspiciatis atque omnis, officia modi ex ad quisquam deleniti tenetur blanditiis dolores neque et, minus soluta quae. Fuga itaque totam neque laboriosam dolorum sunt nisi adipisci.</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="gallery-container">
                        <div class="tz-gallery">
                            <div class="gallery-title">
                                <h2>Our gallery</h2>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/dmrc-psd.jpg">
                    <img src="assets/images/gallery/benches.jpg" alt="Park"> </a>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/gallery/bridge.jpg">
                    <img src="assets/images/gallery/bridge.jpg" alt="Bridge">                </a>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/gallery/park.jpg">
                    <img src="assets/images/gallery/park.jpg" alt="Tunnel">                </a>
                            </div>
                            <div class="col-md-3">
                                <a class="lightbox" href="assets/images/gallery/coast.jpg">
                    <img src="assets/images/gallery/coast.jpg" alt="Coast">                </a>
                            </div>
                            <div class="col-md-3">
                                <a class="lightbox" href="assets/images/gallery/sky.jpg">
                    <img src="assets/images/gallery/sky.jpg" alt="Rails">                </a>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/gallery/benches.jpg">
                    <img src="assets/images/gallery/benches.jpg" alt="Park">                </a>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/gallery/benches.jpg">
                    <img src="assets/images/gallery/benches.jpg" alt="Park">                </a>
                            </div>
                            <div class="col-md-3 ">
                                <a class="lightbox" href="assets/images/gallery/benches.jpg">
                    <img src="assets/images/gallery/benches.jpg" alt="Park">                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-4">
                <div class="right-site">
                    <div class="notice">
                        <div class="notice-title">
                            <h2>notice</h2>
                        </div>
                        <ul>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                            <li>
                                <div class="single-notice-box">
                                    <a href="">Lorem ipsum dolor sit amet, consectetur </a> <br/><span>12 Jan 2018 With comments</span>
                                </div>
                            </li>
                        </ul>
                    </div>

                    <div class="notice event">
                        <div class="notice-title">
                            <h2>Event</h2>
                            <ul class="event-list">
                                <li>
                                    <time datetime="2014-07-20">
                                        <span class="day">4</span>
                                        <span class="month">Jul</span>
                                        <span class="year">2014</span>
                                        <span class="time">ALL DAY</span>
                                    </time>

                                    <div class="info">
                                        <h3 class="title">Independence Day</h3>
                                        <p class="desc">United States Holiday</p>
                                    </div>
                                </li>
                                <li>
                                    <time datetime="2014-07-20">
                                        <span class="day">4</span>
                                        <span class="month">Jul</span>
                                        <span class="year">2014</span>
                                        <span class="time">ALL DAY</span>
                                    </time>

                                    <div class="info">
                                        <h3 class="title">Independence Day</h3>
                                        <p class="desc">United States Holiday</p>
                                    </div>
                                </li>
                                <li>
                                    <time datetime="2014-07-20">
                                        <span class="day">4</span>
                                        <span class="month">Jul</span>
                                        <span class="year">2014</span>
                                        <span class="time">ALL DAY</span>
                                    </time>

                                    <div class="info">
                                        <h3 class="title">Independence Day</h3>
                                        <p class="desc">United States Holiday</p>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>



    <footer>
        <div class="container">
            <div class="row">
                <div class="col-md-3 col-sm-6 footerleft ">
                    <div class="logofooter"> DMRC</div>
                    <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley.of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley</p>

                    <!--
                    <p><i class="fa fa-map-pin"></i> 210, Aggarwal Tower, Rohini sec 9, New Delhi -        110085, INDIA</p>
                    <p><i class="fa fa-phone"></i> Phone (India) : +91 9999 878 398</p>
                    <p><i class="fa fa-envelope"></i> E-mail : info@webenlance.com</p>
-->

                </div>
                <div class="col-md-6 col-sm-6 paddingtop-bottom">
                    <h6 class="heading7">GENERAL LINKS</h6>
                    <img src="http://www.telegraph.co.uk/content/dam/news/2016/11/27/dump-tower-twitter_trans_NvBQzQNjv4BqeY8zn44CJx5co60z9sMGTUWjmulo7wva9c-kqRbE-Zc.jpg?imwidth=450" height="200" width="500" alt="">
                </div>
                <div class="col-md-3 col-sm-6 paddingtop-bottom">
                    <h6 class="heading7">LATEST POST</h6>
                    <div class="post">
                        <p> <i class="fa fa-map-pin"></i>
                            <string>Address: </string> <span> August 3,2015August 3,2015 August 3,2015</span></p>
                        <p> <i class="fa fa-map-pin"></i>
                            <string>Address: </string> <span> August 3,2015August 3,2015 August 3,2015</span></p>
                        <p> <i class="fa fa-map-pin"></i>
                            <string>Address: </string> <span> August 3,2015August 3,2015 August 3,2015</span></p>

                    </div>
                </div>
            </div>
        </div>
    </footer>
    <!--footer start from here-->

    <div class="copyright">
        <div class="container">
            <div class="col-md-6">
                <p>© 2016 - All Rights with Webenlance</p>
            </div>
            <div class="col-md-6">
                <ul class="bottom_ul">
                    <li><a href="#">webenlance.com</a></li>
                    <li><a href="#">About us</a></li>
                    <li><a href="#">Blog</a></li>
                    <li><a href="#">Faq's</a></li>
                    <li><a href="#">Contact us</a></li>
                    <li><a href="#">Site Map</a></li>
                </ul>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
