namespace Calc
{
    public class Parser
    {
        int i = 1;
        enum Token { NUMBER, PLUS = '+', MINUS = '-', END };
        Token currToken;

        public double Expr(int size)
        {
            i = size;
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
            currToken = GetNextToken();
            return 2;
        }

        private Token GetNextToken()
        {
            if (i == 0)
                return Token.END;
            return i-- % 2 == 1 ? Token.NUMBER : Token.PLUS;
        }
    }
}
