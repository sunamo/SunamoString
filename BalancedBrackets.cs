namespace SunamoString;

// C# program for checking
// balanced Brackets


public class BalancedBrackets
{
    public class stack
    {
        public int top = -1;
        public char[] items = new char[100];

        public void push(char x)
        {
            if (top == 99)
            {
                //ThisApp.Info("Stack full");
            }
            else
            {
                items[++top] = x;
            }
        }

        char pop()
        {
            if (top == -1)
            {
                //ThisApp.Error("Underflow error");
                return '�';
            }
            else
            {
                char element = items[top];
                top--;
                return element;
            }
        }

        Boolean isEmpty()
        {
            return top == -1 ? true : false;
        }
    }

    // Returns true if character1 and character2
    // are matching left and right brackets */
    static Boolean isMatchingPair(char character1,
    char character2)
    {
        if (character1 == '(' && character2 == ')')
            return true;
        else if (character1 == '{' && character2 == '}')
            return true;
        else if (character1 == '[' && character2 == ']')
            return true;
        else
            return false;
    }

    // Return true if expression has balanced
    // Brackets
    public static Boolean areBracketsBalanced(List<char> exp)
    {
        // Declare an empty character stack */
        Stack<char> st = new Stack<char>();

        // Traverse the given expression to
        //   check matching brackets
        for (int i = 0; i < exp.Count; i++)
        {
            // If the exp[i] is a starting
            // bracket then push it
            if (exp[i] == '{' || exp[i] == '('
            || exp[i] == '[')
                st.Push(exp[i]);

            //  If exp[i] is an ending bracket
            //  then pop from stack and check if the
            //   popped bracket is a matching pair
            if (exp[i] == '}' || exp[i] == ')'
            || exp[i] == ']')
            {

                // If we see an ending bracket without
                //   a pair then return false
                if (st.Count == 0)
                {
                    return false;
                }

                // Pop the top element from stack, if
                // it is not a pair brackets of
                // character then there is a mismatch. This
                // happens for expressions like {(})
                else if (!isMatchingPair(st.Pop(),
                exp[i]))
                {
                    return false;
                }
            }
        }

        // If there is something left in expression
        // then there is a starting bracket without
        // a closing bracket

        if (st.Count == 0)
            return true; // balanced
        else
        {
            // not balanced
            return false;
        }
    }

    public static void Test(List<char> s)
    {

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
