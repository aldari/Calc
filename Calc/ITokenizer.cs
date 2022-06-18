namespace Calc
{
    public interface ITokenizer
    {
        TokenType GetCurrToken();
        double SingleValue();
        IEnumerable<Token> GetTokens(string expr);
    }
}
