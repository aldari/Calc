namespace Calc
{
    public interface ITokenizer
    {
        Token GetCurrToken();
        double SingleValue();
    }
}
