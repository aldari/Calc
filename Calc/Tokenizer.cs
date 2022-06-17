using System.Text;

namespace Calc
{
    public class Tokenizer : ITokenizer
    {
        public Tokenizer(string expr)
        {
            if (expr == null)
                throw new ArgumentNullException("Empty Expression");
            _expr = expr;
        }
        private Token currToken;
        private double currValue;
        private string _expr;
        int i = 0;

        public Token GetCurrToken()
        {
            return currToken;
        }

        public double SingleValue()
        {
            return currValue;
        }

        public bool MoveNext()
        {
            GetToken();
            return currToken != Token.END;
        }

        

        Token GetToken()
        {
            char ch;
            if (i >= _expr.Length)
            {
                currToken = Token.END;
                return currToken;
            }
            ch = _expr[i++];
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
                    while (i < _expr.Length && char.IsDigit(_expr[i]))
                    {
                        sb.Append(_expr[i++]);
                    }
                    currValue = Int32.Parse(sb.ToString());
                    currToken = Token.NUMBER;
                    break;
                default:
                    currToken = Token.END;
                    break;
            }
            return currToken;
        }
    }
}
