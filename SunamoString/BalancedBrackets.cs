namespace SunamoString;

public class BalancedBrackets
{
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

    public static bool AreBracketsBalanced(List<char> list)
    {
        var stack = new Stack<char>();

        for (var i = 0; i < list.Count; i++)
        {
            if (list[i] == '{' || list[i] == '('
                              || list[i] == '[')
                stack.Push(list[i]);

            if (list[i] == '}' || list[i] == ')'
                              || list[i] == ']')
            {
                if (stack.Count == 0)
                    return false;

                if (!IsMatchingPair(stack.Pop(),
                        list[i]))
                    return false;
            }
        }

        if (stack.Count == 0)
            return true;
        return false;
    }
}
