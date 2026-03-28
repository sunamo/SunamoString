namespace SunamoString._sunamo.SunamoExceptions;

/// <summary>
/// Provides methods for generating formatted exception messages and inspecting the call stack.
/// </summary>
internal sealed partial class Exceptions
{
    /// <summary>
    /// Prepends a context prefix to an exception message if provided.
    /// </summary>
    /// <param name="before">The context prefix, or null/whitespace to skip.</param>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Extracts the type name, method name, and full stack trace from the current call stack.
    /// </summary>
    /// <param name="isFillAlsoFirstTwo">Whether to also extract type and method name from the first non-ThrowEx frame.</param>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var text = stackTrace.ToString();
        var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var lineIndex = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; lineIndex < lines.Count; lineIndex++)
        {
            var line = lines[lineIndex];
            if (isFillAlsoFirstTwo)
                if (!line.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(line, out type, out methodName);
                    isFillAlsoFirstTwo = false;
                }
            if (line.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts the type name and method name from a stack trace line.
    /// </summary>
    /// <param name="line">A single stack trace line.</param>
    /// <param name="type">Output: the extracted type name.</param>
    /// <param name="methodName">Output: the extracted method name.</param>
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var methodCallInfo = line.Split("at ")[1].Trim();
        var text = methodCallInfo.Split("(")[0];
        var parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified stack depth.
    /// </summary>
    /// <param name="depth">The stack frame depth (1 = direct caller).</param>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }

    /// <summary>
    /// Returns a formatted message indicating that something is not allowed.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="what">The item that is not allowed.</param>
    internal static string? IsNotAllowed(string before, string what)
    {
        return CheckBefore(before) + what + " is not allowed.";
    }

    /// <summary>
    /// Returns a formatted custom exception message.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="message">The custom message.</param>
    internal static string? Custom(string before, string message)
    {
        return CheckBefore(before) + message;
    }

    /// <summary>
    /// Returns a formatted message indicating a method is not implemented.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    internal static string? NotImplementedMethod(string before)
    {
        return CheckBefore(before) + "Not implemented method.";
    }

    /// <summary>
    /// Returns a formatted message if start is higher than end, or null if valid.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    internal static string? StartIsHigherThanEnd(string before, int start, int end)
    {
        return start > end ? CheckBefore(before) + $"Start {start} is higher than end {end}" : null;
    }

    /// <summary>
    /// Returns a formatted message for a not-implemented case.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="notImplementedName">The name or type of the not-implemented case.</param>
    internal static string? NotImplementedCase(string before, object notImplementedName)
    {
        var forClause = string.Empty;
        if (notImplementedName != null)
        {
            forClause = " for ";
            if (notImplementedName.GetType() == typeof(Type))
                forClause += ((Type)notImplementedName).FullName;
            else
                forClause += notImplementedName.ToString();
        }
        return CheckBefore(before) + "Not implemented case" + forClause + " . internal program error. Please contact developer" +
        ".";
    }

    /// <summary>
    /// Returns a formatted message listing which expected values are not contained in the original text, or null if all are found.
    /// </summary>
    /// <param name="before">The context prefix.</param>
    /// <param name="originalText">The text to search in.</param>
    /// <param name="expectedValues">The values that should be contained in the text.</param>
    internal static string? NotContains(string before, string originalText, params string[] expectedValues)
    {
        List<string> notContained = [];
        foreach (var expectedValue in expectedValues)
            if (!originalText.Contains(expectedValue))
                notContained.Add(expectedValue);
        return notContained.Count == 0
        ? null
        : CheckBefore(before) + "Original text dont contains: " + string.Join(",", notContained) + ". Original text: " + originalText;
    }
}
