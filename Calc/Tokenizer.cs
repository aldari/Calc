using System.Text;

namespace Calc
{
    public class Tokenizer
    {
        public IEnumerable<Token> GetTokens(string expr)
        {
            int i = 0;
            Token currToken;
            while (i < expr.Length)
            {
                if (i >= expr.Length)
                {
                    currToken = Token.END;
                    yield return currToken;
                }
                char ch = expr[i++];
                switch (ch)
                {
                    case '*':
                        currToken = Token.MUL;
                        break;
                    case '/':
                        currToken = Token.DIV;
                        break;
                    case '+':
                        currToken = Token.PLUS;
                        break;
                    case '-':
                        currToken = Token.MINUS;
                        break;
                    case '(':
                        currToken = Token.LP;
                        break;
                    case ')':
                        currToken = Token.RP;
                        break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        StringBuilder sb = new();
                        sb.Append(ch);
                        while (i < expr.Length && char.IsDigit(expr[i]))
                        {
                            sb.Append(expr[i++]);
                        }
                        double currValue = Int32.Parse(sb.ToString());
                        currToken = Token.NUMBER;
                        break;
                    default:
                        currToken = Token.END;
                        break;
                }
                yield return currToken;
            }
        }
    }
}
