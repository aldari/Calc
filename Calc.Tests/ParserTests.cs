using FluentAssertions;

namespace Calc.Tests
{
    public class ParserTests
    {
        [Fact]
        public void SingleNumber()
        {
            var expr = "2";
            var tokenizer = new Tokenizer();
            var sut = new Parser(tokenizer);


            var result = sut.Expr(expr);


            result.Should().Be(2);
        }

        [Fact]
        public void SimplePlusExprReturns4()
        {
            var expr = "2+2";
            var tokenizer = new Tokenizer();
            var sut = new Parser(tokenizer);


            var result = sut.Expr(expr);


            result.Should().Be(4);
        }

        [Fact]
        public void DoublePlusExprReturns6()
        {
            var expr = "2+2+2";
            var tokenizer = new Tokenizer();
            var sut = new Parser(tokenizer);


            var result = sut.Expr(expr);


            result.Should().Be(4);
        }

        [Fact]
        public void MixedMultiplyExprReturns14()
        {
            var expr = "2+3*4";
            var tokenizer = new Tokenizer();
            var sut = new Parser(tokenizer);


            var result = sut.Expr(expr);


            result.Should().Be(14);
        }

        [Fact]
        public void MixedMultiplyExprReturns6()
        {
            var expr = "2*3+4*7";
            var tokenizer = new Tokenizer();
            var sut = new Parser(tokenizer);


            var result = sut.Expr(expr);


            result.Should().Be(34);
        }
    }
}
