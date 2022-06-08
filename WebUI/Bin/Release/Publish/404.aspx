<%@ page language="C#" autoeventwireup="true" inherits="_404, App_Web_mnfemkl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="Shortcut Icon" href="/images/vietnamstay.ico" type="image/x-icon"/>
    <title>Nothing found for 404</title>
    <style>
        body {
            background: #f9fee8;
            margin: 0;
            padding: 20px;
            text-align: center;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            color: #666666;
        }

        .error_page {
            width: 600px;
            padding: 50px;
            margin: auto;
        }

            .error_page h1 {
                margin: 20px 0 0;
            }

            .error_page p {
                margin: 10px 0;
                padding: 0;
            }

        a {
            color: #9caa6d;
            text-decoration: none;
        }

            a:hover {
                color: #9caa6d;
                text-decoration: underline;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="error_page">
            <img alt="Error 404" src="/images/error_404.gif">
            <h1>We're sorry...</h1>
            <p>
                The page or journal you are looking for cannot be found.
            </p>
            <p>
                <a href="/">Return to the homepage</a>
            </p>
        </div>
    </form>
</body>
</html>
