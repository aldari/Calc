﻿namespace Calc
{
    public enum Token { NUMBER, END, PLUS = '+', MINUS = '-', MUL = '*', DIV = '/', LP='(', RP = ')' };
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

        private double Mult()
        {
            double left = _tokenizer.SingleValue();
            while (true)
            {
                switch (_tokenizer.GetCurrToken())
                {
                    case Token.MUL:
                        left *= _tokenizer.SingleValue();
                        break;
                    case Token.DIV:
                        left /= _tokenizer.SingleValue();
                        break;
                    default:
                        return left;
                }
            }
        }
    }
}
