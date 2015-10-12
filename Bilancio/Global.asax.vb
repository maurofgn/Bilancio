' Nota: per istruzioni su come abilitare la modalità classica di IIS6 o IIS7, 
' visitare il sito Web all'indirizzo http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization

Imports Bilancio.DAL
Imports System.Data.Entity.Infrastructure.Interception

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        AuthConfig.RegisterAuth()

        DbInterception.Add(New BilancioInterceptorLogging())    'interceptor per il log sul db

    End Sub
End Class
