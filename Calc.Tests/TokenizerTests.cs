using FluentAssertions;

namespace Calc.Tests
{
    public class TokenizerTests
    {
        List<Token> GetTokens(string input)
        {
            var sut = new Tokenizer(input);
            List<Token> list = new ();
            while (sut.MoveNext())
            {
                list.Add(sut.GetCurrToken());
            }
            return list;
        }

        [Fact]
        public void Test()
        {
            var result = GetTokens("2+3*4");

            result.Should().BeEquivalentTo(new Token[] { Token.NUMBER, Token.PLUS, Token.NUMBER, Token.MUL, Token.NUMBER });
        }

        [Fact]
        public void Test2()
        {
            var result = GetTokens("(2+3)*(4-9)");

            result.Should().BeEquivalentTo(new Token[] { Token.LP, Token.NUMBER, Token.PLUS, Token.NUMBER, Token.RP, Token.MUL, Token.LP, Token.NUMBER, Token.MINUS, Token.NUMBER, Token.RP });
        }

        [Fact]
        public void Test3()
        {
            var result = GetTokens("524/3285");

            result.Should().BeEquivalentTo(new Token[] { Token.NUMBER, Token.DIV, Token.NUMBER });
        }
    }
}
