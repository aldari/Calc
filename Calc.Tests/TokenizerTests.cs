using FluentAssertions;

namespace Calc.Tests
{
    public class TokenizerTests
    {
        private Tokenizer sut;

        public TokenizerTests()
        {
            sut = new Tokenizer();
        }
        [Fact]
        public void Test()
        {
            var result = sut.GetTokens("2+3*4");

            result.Should().BeEquivalentTo(new TokenType[] { TokenType.NUMBER, TokenType.PLUS, TokenType.NUMBER, TokenType.MUL, TokenType.NUMBER });
        }

        [Fact]
        public void Test2()
        {
            var result = sut.GetTokens("(2+3)*(4-9)");

            result.Should().BeEquivalentTo(new TokenType[] { TokenType.LP, TokenType.NUMBER, TokenType.PLUS, TokenType.NUMBER, TokenType.RP, TokenType.MUL, TokenType.LP, TokenType.NUMBER, TokenType.MINUS, TokenType.NUMBER, TokenType.RP });
        }

        [Fact]
        public void Test3()
        {
            var result = sut.GetTokens("524/3285");

            result.Should().BeEquivalentTo(new TokenType[] { TokenType.NUMBER, TokenType.DIV, TokenType.NUMBER });
        }
    }
}
