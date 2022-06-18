namespace Calc
{
    public interface ITokenizer
    {
        IEnumerable<Token> GetTokens(string expr);
    }
}
