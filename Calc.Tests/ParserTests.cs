using FluentAssertions;
using Moq;

namespace Calc.Tests
{
    public class ParserTests
    {
        [Fact]
        public void SingleNumber()
        {
            var expr = "2";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(TokenType.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr(expr);


            result.Should().Be(2);
        }

        [Fact]
        public void SimplePlusExprReturns4()
        {
            var expr = "2+2";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2)
                .Returns(2);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.PLUS)
                .Returns(TokenType.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr(expr);


            result.Should().Be(4);
        }

        [Fact]
        public void DoublePlusExprReturns6()
        {
            var expr = "2+2+2";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2)
                .Returns(2)
                .Returns(2);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.PLUS)
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.PLUS)
                .Returns(TokenType.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr(expr);


            result.Should().Be(4);
        }

        [Fact]
        public void MixedMultiplyExprReturns14()
        {
            var expr = "2+3*4";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2)
                .Returns(3)
                .Returns(4);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.PLUS)
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.MUL)
                .Returns(TokenType.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr(expr);


            result.Should().Be(14);
        }

        [Fact]
        public void MixedMultiplyExprReturns6()
        {
            var expr = "2*3+4*7";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2)
                .Returns(3)
                .Returns(4)
                .Returns(7);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.MUL)
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.PLUS)
                .Returns(TokenType.NUMBER)
                .Returns(TokenType.MUL)
                .Returns(TokenType.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr(expr);


            result.Should().Be(34);
        }
    }
}
