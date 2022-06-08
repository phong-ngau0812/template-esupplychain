<%@ page language="C#" autoeventwireup="true" inherits="Admin_ProductPackage_ProductPackage_Diagram, App_Web_quwo0bpb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <%--<title>CodePen - CSS Layout &amp; SVG Practice</title>--%>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="stylesheet" href="/chart/style.css">
</head>
<body>
    <!-- partial:index.partial.html -->
    <!-- in a wrapping section include different containers for each step of the flow: data sources, build, deploy -->
    <section class="container">
        <h2>SƠ ĐỒ ĐỐI TÁC MẠNG TRUY XUẤT</h2>
        <br />
        <!-- in the sources container show three cards, side by side, or one atop the other on smaller viewports -->
        <div class="container__sources">

            <div class="sources--cms">
                <h3>CMSs</h3>
                <p>Contentful, Drupal, WordPress, etc.</p>
            </div>
            <div class="sources--markdown">
                <h3>Markdown</h3>
                <p>Documentation, Posts, etc.</p>
            </div>
            <div class="sources--data">
                <h3>Data</h3>
                <p>APIs, Databases, YAML, JSON, CSV, etc.</p>
            </div>
        </div>

        <!-- include a simple line to divide the container, and animate it to show a connection between the different containers  -->
        <svg viewbox="0 0 10 100">
            <line x1="5" x2="5" y1="0" y2="100" />
        </svg>

        <!-- in the build container show two cards, atop of one another and the first of one showing an SVG icon -->
        <div class="container__build">
            <asp:Repeater runat="server" ID="rptData">
                <ItemTemplate>
                    <div class="build--powered" style="text-align: left;">
                        <p>
                            <b>&diams; Thông tin D/N (Who):</b><br />
                            - <%#Eval("ProductBrandName") %>
                            <%#Eval("Type").ToString()=="2"?"<br />- Hộ sản xuất: "+Eval("HoSX"):"" %>
                            <br />
                            <b>&diams; Đối tượng truy xuất (What)</b>
                            <br />
                            - Tên sản phẩm: <a href="javascript:void(0);" onclick='GetProductInfo(<%#Eval("Product_ID")%>)' data-toggle="modal" data-target=".bd-example-modal-xl" class="text-success"><b><%#Eval("ProductName") %> (<%#Eval("ItemCount") %> cá thể)</b></a>
                            <%--<a href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>' class="breadcrumb-item active"><b><%#Eval("Name").ToString().ToUpper()%></b></a>--%>
                            <br />
                            - Lô/mẻ: <a class="text-success" href='ProductPackage_Edit?ProductPackage_ID=<%#Eval("ProductPackage_ID") %>'><b><%#Eval("CODE")%> | <%#Eval("SGTIN") %></b></a>
                            <br />
                            - Tiêu chuẩn: <%# string.IsNullOrEmpty( Eval("QualityName").ToString())?"Tự công bố":Eval("QualityName") %>
                            <br />
                            <b>&diams; Nơi sản xuất (Where): </b>
                            <br />
                            <%#Eval("ProductBrandAddress") %>
                            <br />
                            <b>&diams; Thời gian sản xuất (When): </b>
                            <br />
                            Từ <%#  string.IsNullOrEmpty( Eval("StartDate").ToString())?"": DateTime.Parse( Eval("StartDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("StartDate").ToString()).Minute.ToString())%> đến <%#  string.IsNullOrEmpty( Eval("EndDate").ToString())?"": DateTime.Parse( Eval("EndDate").ToString()).ToString("dd/MM/yyyy") + " "+ (DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Hour.ToString())+":"+(DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString().Length<2?("0"+DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString()):DateTime.Parse( Eval("EndDate").ToString()).Minute.ToString())%>
                            <br />
                            <b>&diams; Lý do (Why): </b><%# string.IsNullOrEmpty(Eval("TenLenh").ToString())?"Lệnh sản xuất": "<span class='badge badge-danger'>"+ Eval("TenLenh").ToString()+"</span>"%>
                            <br />
                        </p>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- repeat the svg line to connect the second and third containers as well -->
        <svg viewbox="0 0 10 100">
            <line x1="5" x2="5" y1="0" y2="100" />
        </svg>
        <div class="container__sources">
            <div>
                <h3>CMSs</h3>
                <p>Contentful, Drupal, WordPress, etc.</p>
            </div>
            <div>
                <h3>CMSs</h3>
                <p>Contentful, Drupal, WordPress, etc.</p>
            </div>
            <div>
                <h3>CMSs</h3>
                <p>Contentful, Drupal, WordPress, etc.</p>
            </div>
            <div>
                <h3>CMSs</h3>
                <p>Contentful, Drupal, WordPress, etc.</p>
            </div>
            <div>
                <h3>Markdown</h3>
                <p>Documentation, Posts, etc.</p>
            </div>

            <div>
                <h3>Data</h3>
                <p>APIs, Databases, YAML, JSON, CSV, etc.</p>
            </div>
        </div>
    </section>
    <!-- partial -->
    <script src="/chart/script.js"></script>
</body>
</html>
