    Public Function LoadDLL(ByVal PluginBytes As Byte(), ByVal className As String, ByVal methodName As String, Optional ByVal parameters As Dictionary(Of Object, Object) = Nothing) As Object
        Dim p = Reflection.Assembly.Load(PluginBytes)

        Dim rootTypes() As Type = p.GetTypes

        Dim ci = p.CreateInstance(Split(rootTypes(0).Namespace, ".")(0) & "." & className)

        If parameters IsNot Nothing Then
            For Each parameter In parameters
                CallByName(ci, parameter.Key, CallType.Set, parameter.Value)
            Next
        End If

        Dim obj As Object = CallByName(ci, methodName, CallType.Method)

        Return obj
    End Function
