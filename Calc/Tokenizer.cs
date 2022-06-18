using System.Text;

namespace Calc
{
    public class Tokenizer: ITokenizer
    {
        public IEnumerable<Token> GetTokens(string expr)
        {
            int i = 0;
            double currValue = 0;
            TokenType currToken;
            while (i < expr.Length)
            {
                if (i >= expr.Length)
                {
                    yield return new Token(TokenType.END);
                }
                char ch = expr[i++];
                switch (ch)
                {
                    case '*':
                    case '/':
                    case '+':
                    case '-':
                    case '(':
                    case ')':
                        currToken = (TokenType) ch;
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
                        currValue = Int32.Parse(sb.ToString());
                        currToken = TokenType.NUMBER;
                        break;

                    default:
                        currToken = TokenType.END;
                        break;
                }
                yield return new Token(currToken, currToken ==TokenType.NUMBER ? currValue : 0);
            }
        }
    }
}
