namespace Calc
{
    public class Parser
    {
        int i = 1;
        enum Token { NUMBER, PLUS = '+', MINUS = '-', END };
        Token currToken; 

        public double Expr()
        {
            double left = SingleValue();
            while (true)
            {
                switch (currToken)
                {
                    case Token.PLUS:
                        left += SingleValue();
                        break;
                    case Token.MINUS:
                        left -= SingleValue();
                        break;
                    default:
                        return left;
                }
            }
        }

        double SingleValue()
        {
            currToken = GetCurrToken();
            return 2;
        }

        private Token GetCurrToken()
        {
            if (i > 7)
                return Token.END;
            return i++ % 2 == 1 ? Token.NUMBER : Token.PLUS;
        }
    }
}
