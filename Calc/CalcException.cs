namespace Calc
{
    public class CalcCustomException : Exception
    {
        public CalcCustomException()
        {
        }

        public CalcCustomException(string message)
            : base(message)
        {
        }
    }
}
