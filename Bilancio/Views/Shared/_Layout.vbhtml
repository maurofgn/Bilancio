<!DOCTYPE html>
<html lang="it">
    <head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <meta charset="utf-8" />
        <title>@ViewData("Title") - Bilancio</title>
        <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
        <meta name="viewport" content="width=device-width" />
        @Styles.Render("~/Content/css")
        @Scripts.Render("~/bundles/modernizr")
    </head>

    <body>
        <header>
            <div class="content-wrapper">
                <div class="float-left">

                    @*<p class="site-title">@Html.ActionLink("Inserire qui il logo", "Index", "Home")</p>*@
                    <p class="site-title">
                        <a href="@Url.Action("Index", "Home")" title="Home" class="links">
                            <img alt="Home" src="@Url.Content("~/Images/site_logo.png")">
                        </a>
                    </p>

                </div>
                <div class="float-right">
                    <section id="login">
                        @Html.Partial("_LoginPartial")
                    </section>

            <div class="navbar navbar-inverse navbar-fixed-top">
                <div class="container">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        @Html.ActionLink("Bilancio", "Index", "Home", Nothing, New With {.class = "navbar-brand"})
                    </div>
                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li>@Html.ActionLink("Home", "Index", "Home")</li>
                            <li>@Html.ActionLink("About", "About", "Home")</li>

                            <li>@Html.ActionLink("PC CEE", "Index", "AccountCee")</li>
                            <li>@Html.ActionLink("PC interno", "Index", "AccountChart")</li>
                            <li>@Html.ActionLink("Documenti", "Index", "Document")</li>
                            <li>@Html.ActionLink("Tipo doc", "Index", "DocumentType")</li>
                            <li>@Html.ActionLink("Reports", "Index", "Report")</li>
                            <li>@Html.ActionLink("Avis", "Index", "Avis")</li>

                        </ul>
                    </div>
                </div>
            </div>


                    <nav>
                        <ul id="menu">
                            <li>@Html.ActionLink("Home", "Index", "Home")</li>
                            @*<li>@Html.ActionLink("Informazioni", "About", "Home")</li>
                            <li>@Html.ActionLink("Contatto", "Contact", "Home")</li>*@

                            <li>@Html.ActionLink("PC CEE", "Index", "AccountCee")</li>
                            <li>@Html.ActionLink("PC interno", "Index", "AccountChart")</li>
                            <li>@Html.ActionLink("Documenti", "Index", "Document")</li>
                            <li>@Html.ActionLink("Tipo doc", "Index", "DocumentType")</li>
                            <li>@Html.ActionLink("Reports", "Index", "Report")</li>
                            <li>@Html.ActionLink("Avis", "Index", "Avis")</li>
                        </ul>
                    </nav>

                </div>
            </div>
        </header>
        <div id="body">
            @RenderSection("featured", required:=false)
            <section class="content-wrapper main-content clear-fix">
                @RenderBody()
            </section>
        </div>
        <footer>
            <div class="content-wrapper">
                <div class="float-left">
                    <p>&copy; @DateTime.Now.Year - MeSIS srl</p>
                </div>
            </div>
        </footer>

        @Scripts.Render("~/bundles/jquery")
        @RenderSection("scripts", required:=False)
    </body>
</html>
