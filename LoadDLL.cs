public object LoadDLL(byte[] PluginBytes, string className, string methodName, Dictionary<object, object> parameters = null)
{
    var p = System.Reflection.Assembly.Load(PluginBytes);

    Type[] rootTypes = p.GetTypes();

    var ci = p.CreateInstance(Strings.Split(rootTypes[0].Namespace, ".")(0) + "." + className);

    if (parameters != null)
    {
        foreach (var parameter in parameters)
            Interaction.CallByName(ci, parameter.Key, CallType.Set, parameter.Value);
    }

    object obj = Interaction.CallByName(ci, methodName, CallType.Method);

    return obj;
}
