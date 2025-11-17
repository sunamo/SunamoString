// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString;

// C# program for checking
// balanced Brackets

public class BalancedBrackets
{
    // Returns true if openingChar and closingChar
    // are matching left and right brackets */
    private static bool isMatchingPair(char openingChar,
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
    public static bool areBracketsBalanced(List<char> expression)
    {
        // Declare an empty character stack */
        var stack = new Stack<char>();

        // Traverse the given expression to
        //   check matching brackets
        for (var i = 0; i < expression.Count; i++)
        {
            // If the expression[i] is a starting
            // bracket then push it
            if (expression[i] == '{' || expression[i] == '('
                              || expression[i] == '[')
                stack.Push(expression[i]);

            //  If expression[i] is an ending bracket
            //  then pop from stack and check if the
            //   popped bracket is a matching pair
            if (expression[i] == '}' || expression[i] == ')'
                              || expression[i] == ']')
            {
                // If we see an ending bracket without
                //   a pair then return false
                if (stack.Count == 0)
                    return false;

                // Pop the top element from stack, if
                // it is not a pair brackets of
                // character then there is a mismatch. This
                // happens for expressions like {(})
                if (!isMatchingPair(stack.Pop(),
                        expression[i]))
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

    public class Stack
    {
        public char[] Items { get; set; } = new char[100];
        public int Top { get; set; } = -1;

        public void Push(char value)
        {
            if (Top == 99)
            {
                //ThisApp.Info("Stack full");
            }
            else
            {
                Items[++Top] = value;
            }
        }

        private char Pop()
        {
            if (Top == -1)
                //ThisApp.Error("Underflow error");
                return '�';

            var element = Items[Top];
            Top--;
            return element;
        }

        private bool IsEmpty()
        {
            return Top == -1 ? true : false;
        }
    }


    // Driver code
    //public static void Main(String[] args)
    //{
    //    char[] exp = { '{', '(', ')', '}', '[', ']' };

    //    // Function call
    //    if (areBracketsBalanced(exp))
    //        CL.WriteLine("Balanced ");
    //    else
    //        CL.WriteLine("Not Balanced ");
    //}
}

// This code is contributed by 29AjayKumar