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
            var enumerator = _tokenizer.GetTokens(input).GetEnumerator();
            double left = Mult(enumerator);
            while (true)
            {
                switch (enumerator.Current.Type)
                {
                    case TokenType.PLUS:
                        left += Mult(enumerator);
                        break;
                    case TokenType.MINUS:
                        left -= Mult(enumerator);
                        break;
                    default:
                        return left;
                }
            }
        }

        private double Mult(IEnumerator<Token> enumerator)
        {
            double left = SingleToken(enumerator);
            while (true)
            {
                switch (enumerator.Current.Type)
                {
                    case TokenType.MUL:
                        left *= SingleToken(enumerator);
                        break;
                    case TokenType.DIV:
                        left /= SingleToken(enumerator);
                        break;
                    default:
                        return left;
                }
            }
        }

        private double SingleToken(IEnumerator<Token> enumerator)
        {
            enumerator.MoveNext();
            if (enumerator.Current.Type == TokenType.NUMBER)
            {
                return enumerator.Current.Value;
            }
            else
                return 0;
        }
    }
}
