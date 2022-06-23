namespace Calc
{
    public class Parser : IParser
    {
        private readonly ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public double Evaluate(string input)
        {
            var enumerator = _tokenizer.GetTokens(input?.ToLower()).GetEnumerator();
            return Expr(enumerator);
        }

        private double Expr(IEnumerator<Token> enumerator)
        {
            double left = MultiplExpr(enumerator);
            while (true)
            {
                switch (enumerator.Current.Type)
                {
                    case TokenType.PLUS:
                        left += MultiplExpr(enumerator);
                        break;
                    case TokenType.MINUS:
                        left -= MultiplExpr(enumerator);
                        break;
                    default:
                        return left;
                }
            }
        }

        private double MultiplExpr(IEnumerator<Token> enumerator)
        {
            double left = SimpleExpr(enumerator);
            while (true)
            {
                switch (enumerator.Current.Type)
                {
                    case TokenType.MUL:
                        left *= SimpleExpr(enumerator);
                        break;
                    case TokenType.DIV:
                        left /= SimpleExpr(enumerator);
                        break;
                    default:
                        return left;
                }
            }
        }

        private double SimpleExpr(IEnumerator<Token> enumerator)
        {
            double v;
            enumerator.MoveNext();
            switch (enumerator.Current.Type)
            {
                case TokenType.NUMBER:
                    v = enumerator.Current.Value;
                    enumerator.MoveNext();
                    return v;
                case TokenType.NAME:
                    if (enumerator.Current.Name != "sin")
                        throw new ArgumentException();
                    enumerator.MoveNext();
                    v = Expr(enumerator);
                    enumerator.MoveNext();
                    return Math.Sin(v);
                case TokenType.LP:
                    v = Expr(enumerator);
                    enumerator.MoveNext();
                    return v;
                case TokenType.MINUS:
                    return -SimpleExpr(enumerator);
                default:
                    // it is not used, pass compile verification
                    // не используется
                    // подавить ошибку компилятора, выявить тесты проходящие эту ветку
                    throw new ArgumentException();
            }
        }
    }
}
