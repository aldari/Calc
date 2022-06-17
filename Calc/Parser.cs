namespace Calc
{
    public enum TokenType { NUMBER, END, PLUS = '+', MINUS = '-', MUL = '*', DIV = '/', LP='(', RP = ')' };
    readonly public struct Token
    {
        public Token(TokenType type, double value = 0)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type {get;}
        public double Value { get; }
    }

    public class Parser
    {
        private readonly ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public double Expr(string input)
        {
            var enumerator = new Tokenizer().GetTokens(input).GetEnumerator();
            double left = Mult();
            while (true)
            {
                switch (_tokenizer.GetCurrToken())
                {
                    case TokenType.PLUS:
                        left += Mult();
                        break;
                    case TokenType.MINUS:
                        left -= Mult();
                        break;
                    default:
                        return left;
                }
            }
        }

        private double Mult()
        {
            double left = _tokenizer.SingleValue();
            while (true)
            {
                switch (_tokenizer.GetCurrToken())
                {
                    case TokenType.MUL:
                        left *= _tokenizer.SingleValue();
                        break;
                    case TokenType.DIV:
                        left /= _tokenizer.SingleValue();
                        break;
                    default:
                        return left;
                }
            }
        }
    }
}
