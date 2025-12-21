namespace SunamoString._sunamo.SunamoExceptions;

// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
internal partial class ThrowEx
{


    internal static bool Custom(string message, bool reallyThrow = true, string secondMessage = "")
    {
        string joined = string.Join(" ", message, secondMessage);
        string? str = Exceptions.Custom(FullNameOfExecutedCode(), joined);
        return ThrowIsNotNull(str, reallyThrow);
    }

    internal static bool IsNotAllowed(string what)
    { return ThrowIsNotNull(Exceptions.IsNotAllowed(FullNameOfExecutedCode(), what)); }

    internal static bool NotContains(string text, params string[] shouldContains)
    { return ThrowIsNotNull(Exceptions.NotContains(FullNameOfExecutedCode(), text, shouldContains)); }

    internal static bool NotImplementedCase(object notImplementedName)
    { return ThrowIsNotNull(Exceptions.NotImplementedCase, notImplementedName); }
    internal static bool NotImplementedMethod() { return ThrowIsNotNull(Exceptions.NotImplementedMethod); }

    internal static bool StartIsHigherThanEnd(int start, int end)
    { return ThrowIsNotNull(Exceptions.StartIsHigherThanEnd(FullNameOfExecutedCode(), start, end)); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfExc = Exceptions.PlaceOfException();
        string fullMethodName = FullNameOfExecutedCode(placeOfExc.Item1, placeOfExc.Item2, true);
        return fullMethodName;
    }

    static string FullNameOfExecutedCode(object type, string methodName, bool fromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (fromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type type2)
        {
            typeFullName = type2.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type actualType = type.GetType();
            typeFullName = actualType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    internal static bool ThrowIsNotNull(string? exception, bool reallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (reallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }

    #region For avoid FullNameOfExecutedCode


    internal static bool ThrowIsNotNull<A>(Func<string, A, string?> exceptionFunc, A exceptionParameter)
    {
        string? exceptionMessage = exceptionFunc(FullNameOfExecutedCode(), exceptionParameter);
        return ThrowIsNotNull(exceptionMessage);
    }

    internal static bool ThrowIsNotNull(Func<string, string?> exceptionFunc)
    {
        string? exceptionMessage = exceptionFunc(FullNameOfExecutedCode());
        return ThrowIsNotNull(exceptionMessage);
    }
    #endregion
    #endregion
}