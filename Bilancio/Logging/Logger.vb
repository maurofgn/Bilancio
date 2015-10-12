Imports System.Diagnostics
Imports System.Text

Namespace Logging
    Public Class Logger
        Implements ILogger

        Public Sub Information(message As String) Implements ILogger.Information
            Trace.TraceInformation(message)
        End Sub

        Public Sub Information(fmt As String, ParamArray vars As Object()) Implements ILogger.Information
            Trace.TraceInformation(fmt, vars)
        End Sub

        Public Sub Information(exception As Exception, fmt As String, ParamArray vars As Object()) Implements ILogger.Information
            Trace.TraceInformation(FormatExceptionMessage(exception, fmt, vars))
        End Sub

        Public Sub Warning(message As String) Implements ILogger.Warning
            Trace.TraceWarning(message)
        End Sub

        Public Sub Warning(fmt As String, ParamArray vars As Object()) Implements ILogger.Warning
            Trace.TraceWarning(fmt, vars)
        End Sub

        Public Sub Warning(exception As Exception, fmt As String, ParamArray vars As Object()) Implements ILogger.Warning
            Trace.TraceWarning(FormatExceptionMessage(exception, fmt, vars))
        End Sub

        Public Sub Err(message As String) Implements ILogger.Err
            Trace.TraceError(message)
        End Sub

        Public Sub Err(fmt As String, ParamArray vars As Object()) Implements ILogger.Err
            Trace.TraceError(fmt, vars)
        End Sub

        Public Sub Err(exception As Exception, fmt As String, ParamArray vars As Object()) Implements ILogger.Err
            Trace.TraceError(FormatExceptionMessage(exception, fmt, vars))
        End Sub

        Public Sub TraceApi(componentName As String, method As String, timespan As TimeSpan) Implements ILogger.TraceApi
            TraceApi(componentName, method, timespan, "")
        End Sub

        Public Sub TraceApi(componentName As String, method As String, timespan As TimeSpan, fmt As String, ParamArray vars As Object()) Implements ILogger.TraceApi
            TraceApi(componentName, method, timespan, String.Format(fmt, vars))
        End Sub

        Public Sub TraceApi(componentName As String, method As String, timespan As TimeSpan, properties As String) Implements ILogger.TraceApi
            Dim message As String = [String].Concat("Component:", componentName, ";Method:", method, ";Timespan:", timespan.ToString(), _
                ";Properties:", properties)
            Trace.TraceInformation(message)
        End Sub

        Private Shared Function FormatExceptionMessage(exception As Exception, fmt As String, vars As Object()) As String
            ' Simple exception formatting: for a more comprehensive version see 
            ' http://code.msdn.microsoft.com/windowsazure/Fix-It-app-for-Building-cdd80df4
            Dim sb = New StringBuilder()
            sb.Append(String.Format(fmt, vars))
            sb.Append(" Exception: ")
            sb.Append(exception.ToString())
            Return sb.ToString()
        End Function
    End Class
End Namespace
