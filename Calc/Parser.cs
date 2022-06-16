namespace Calc
{
    public enum Token { NUMBER, PLUS = '+', MINUS = '-', MUL = '*', DIV = '/', END };
    public class Parser
    {
        private readonly ITokenizer _tokenizer;

        public Parser(ITokenizer tokenizer)
        {
            _tokenizer = tokenizer;
        }

        public double Expr()
        {
            double left = Mult();
            while (true)
            {
                switch (_tokenizer.GetCurrToken())
                {
                    case Token.PLUS:
                        left += Mult();
                        break;
                    case Token.MINUS:
                        left -= Mult();
                        break;
                    default:
                        return left;
                }
            }
        }

        public double Mult()
        {
            double left = _tokenizer.SingleValue();
            while (true)
            {
                switch (_tokenizer.GetCurrToken())
                {
                    case Token.MUL:
                        left += _tokenizer.SingleValue();
                        break;
                    case Token.DIV:
                        left -= _tokenizer.SingleValue();
                        break;
                    default:
                        return left;
                }
            }
        }
    }
}
