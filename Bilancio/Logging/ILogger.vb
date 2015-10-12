Namespace Logging

    Public Interface ILogger
        Sub Information(message As String)
        Sub Information(fmt As String, ParamArray vars As Object())
        Sub Information(exception As Exception, fmt As String, ParamArray vars As Object())

        Sub Warning(message As String)
        Sub Warning(fmt As String, ParamArray vars As Object())
        Sub Warning(exception As Exception, fmt As String, ParamArray vars As Object())

        Sub Err(message As String)
        Sub Err(fmt As String, ParamArray vars As Object())
        Sub Err(exception As Exception, fmt As String, ParamArray vars As Object())

        Sub TraceApi(componentName As String, method As String, timespan As TimeSpan)
        Sub TraceApi(componentName As String, method As String, timespan As TimeSpan, properties As String)
        Sub TraceApi(componentName As String, method As String, timespan As TimeSpan, fmt As String, ParamArray vars As Object())

    End Interface

End Namespace