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

            result.Should().BeEquivalentTo(new Token[] { 
                new Token(TokenType.NUMBER, 2),
                new Token(TokenType.PLUS),
                new Token(TokenType.NUMBER, 3),
                new Token(TokenType.MUL),
                new Token(TokenType.NUMBER, 4)
            });
        }

        [Fact]
        public void Test2()
        {
            var result = sut.GetTokens("(2+3)*(4-9)");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.LP),
                new Token(TokenType.NUMBER, 2),
                new Token(TokenType.PLUS),
                new Token(TokenType.NUMBER, 3),
                new Token(TokenType.RP),
                new Token(TokenType.MUL),
                new Token(TokenType.LP),
                new Token(TokenType.NUMBER, 4),
                new Token(TokenType.MINUS),
                new Token(TokenType.NUMBER, 9),
                new Token(TokenType.RP)
            });
        }

        [Fact]
        public void Test3()
        {
            var result = sut.GetTokens("524/3285");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.NUMBER, 524),
                new Token(TokenType.DIV),
                new Token(TokenType.NUMBER, 3285)
            });
        }

        [Fact]
        public void Test4()
        {
            var result = sut.GetTokens(" ( 2 +  3 ) * ( 4 - 9 ) ");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.LP),
                new Token(TokenType.NUMBER, 2),
                new Token(TokenType.PLUS),
                new Token(TokenType.NUMBER, 3),
                new Token(TokenType.RP),
                new Token(TokenType.MUL),
                new Token(TokenType.LP),
                new Token(TokenType.NUMBER, 4),
                new Token(TokenType.MINUS),
                new Token(TokenType.NUMBER, 9),
                new Token(TokenType.RP)
            });
        }

        [Fact]
        public void ParseNumberWithCommaSeparatorSuccesful()
        {
            var result = sut.GetTokens("654,684");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.NUMBER, 654.684)
            });
        }

        [Fact]
        public void ParseNumberWithPeriodSeparatorSuccesful()
        {
            var result = sut.GetTokens("654.684");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.NUMBER, 654.684)
            });
        }

        [Fact]
        public void ParseNameSuccesful()
        {
            var result = sut.GetTokens("2 + sin(1.5708)");

            result.Should().BeEquivalentTo(new Token[] {
                new Token(TokenType.NUMBER, 2),
                new Token(TokenType.PLUS),
                new Token(TokenType.NAME, 0, "sin"),
                new Token(TokenType.LP),
                new Token(TokenType.NUMBER, 1.5708),
                new Token(TokenType.RP)
            });
        }
    }
}
