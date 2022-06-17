namespace Calc
{
    public interface ITokenizer
    {
        TokenType GetCurrToken();
        double SingleValue();
    }
}
