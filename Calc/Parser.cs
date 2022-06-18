namespace Calc
{
    public class Parser
    {
        private readonly ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public double Evaluate(string input)
        {
            var enumerator = _tokenizer.GetTokens(input).GetEnumerator();
            return Expr(enumerator);
        }

        private double Expr(IEnumerator<Token> enumerator)
        {
            
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
            double v;
            enumerator.MoveNext();
            switch (enumerator.Current.Type)
            {
                case TokenType.NUMBER:
                    v = enumerator.Current.Value;
                    enumerator.MoveNext();
                    return v;
                case TokenType.LP:
                    v = Expr(enumerator);
                    enumerator.MoveNext();
                    return v;
                default:
                    return 0;
            }
        }
    }
}
