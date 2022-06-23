namespace Calc
{
    public class Parser : IParser
    {
        private readonly ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        delegate double F(double f);
        static readonly Dictionary<string, F> functions = new();
        static Parser()
        {
            functions.Add("sin", (double i) => Math.Sin(i));
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
                        double d = SimpleExpr(enumerator);
                        if (d == 0)
                            throw new CalcCustomException("деление на ноль");
                        left /= d;
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
                    var name = enumerator.Current.Name;
                    if (!functions.ContainsKey(name))
                        throw new CalcCustomException("неверное имя функции");
                    enumerator.MoveNext();
                    if (enumerator.Current.Type != TokenType.LP)
                        throw new CalcCustomException("пропущена левая скобка");
                    v = Expr(enumerator);
                    if (enumerator.Current.Type != TokenType.RP)
                        throw new CalcCustomException("пропущена правая скобка");
                    enumerator.MoveNext();
                    var func = functions[name];
                    return func(v);
                case TokenType.LP:
                    v = Expr(enumerator);
                    if (enumerator.Current.Type != TokenType.RP)
                        throw new CalcCustomException("пропущена правая скобка");
                    enumerator.MoveNext();
                    return v;
                case TokenType.MINUS:
                    return -SimpleExpr(enumerator);
                default:
                    // it is not used, pass compile verification
                    // не используется
                    // подавить ошибку компилятора, выявить тесты проходящие эту ветку
                    throw new CalcCustomException("пропущено выражение");
            }
        }
    }
}
