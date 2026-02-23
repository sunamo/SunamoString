namespace SunamoString;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class BalancedBrackets
{
    // Returns true if openingChar and closingChar
    // are matching left and right brackets */
    private static bool IsMatchingPair(char openingChar,
        char closingChar)
    {
        if (openingChar == '(' && closingChar == ')')
            return true;
        if (openingChar == '{' && closingChar == '}')
            return true;
        if (openingChar == '[' && closingChar == ']')
            return true;
        return false;
    }

    // Return true if expression has balanced
    // Brackets
    /// <summary>
    /// Are Brackets Balanced operation on the string.
    /// </summary>
    public static bool AreBracketsBalanced(List<char> brackets)
    {
        // Declare an empty character stack */
        var stack = new Stack<char>();

        // Traverse the given expression to
        //   check matching brackets
        for (var i = 0; i < brackets.Count; i++)
        {
            // If the expression[i] is a starting
            // bracket then push it
            if (brackets[i] == '{' || brackets[i] == '('
                              || brackets[i] == '[')
                stack.Push(brackets[i]);

            //  If expression[i] is an ending bracket
            //  then pop from stack and check if the
            //   popped bracket is a matching pair
            if (brackets[i] == '}' || brackets[i] == ')'
                              || brackets[i] == ']')
            {
                // If we see an ending bracket without
                //   a pair then return false
                if (stack.Count == 0)
                    return false;

                // Pop the top element from stack, if
                // it is not a pair brackets of
                // character then there is a mismatch. This
                // happens for expressions like {(})
                if (!IsMatchingPair(stack.Pop(),
                        brackets[i]))
                    return false;
            }
        }

        // If there is something left in expression
        // then there is a starting bracket without
        // a closing bracket

        if (stack.Count == 0)
            return true; // balanced
        // not balanced
        return false;
    }
}

// This code is contributed by 29AjayKumar
