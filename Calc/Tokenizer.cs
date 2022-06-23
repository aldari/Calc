using System.Globalization;
using System.Text;

namespace Calc
{
    public class Tokenizer: ITokenizer
    {
        public IEnumerable<Token> GetTokens(string expr)
        {
            if (expr is null)
                yield break;
            int i = 0;
            double currValue = 0;
            string currName = null;
            TokenType currToken = TokenType.NUMBER;
            while (i < expr.Length)
            {
                if (i >= expr.Length)
                {
                    yield break;
                }
                char ch = expr[i++];
                while (i < expr.Length && char.IsWhiteSpace(ch))
                {
                    ch = expr[i++];
                }
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
                    case '.':
                    case ',':
                        StringBuilder sb = new();
                        sb.Append(ch);
                        while (i < expr.Length && (char.IsDigit(expr[i]) || expr[i] == ',' || expr[i] =='.'))
                        {
                            if (expr[i] == ',')
                                sb.Append('.');
                            else
                                sb.Append(expr[i]);
                            i++;
                        }
                        currValue = Double.Parse(sb.ToString(), CultureInfo.InvariantCulture);
                        currToken = TokenType.NUMBER;
                        break;
                    default:
                        if (char.IsLetter(ch))
                        {
                            StringBuilder s = new();
                            s.Append(ch);
                            while (i < expr.Length && char.IsLetter(expr[i]))
                            {
                                s.Append(expr[i++]);
                            }
                            currName = s.ToString();
                            currToken = TokenType.NAME;
                        }
                        else 
                            yield break;
                        break;
                }
                yield return new Token(currToken, currToken == TokenType.NUMBER ? currValue : 0, currToken == TokenType.NAME ? currName: null);
            }
        }
    }
}
