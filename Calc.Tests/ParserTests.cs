using FluentAssertions;

namespace Calc.Tests
{
    public class ParserTests
    {
        private Parser _sut;

        public ParserTests()
        {
            var tokenizer = new Tokenizer();
            _sut = new Parser(tokenizer);
        }

        [Fact]
        public void SingleNumber()
        {
            var result = _sut.Expr("2");


            result.Should().Be(2);
        }

        [Fact]
        public void SimplePlusExprReturns4()
        {
            var result = _sut.Expr("2+2");


            result.Should().Be(4);
        }

        [Fact]
        public void DoublePlusExprReturns6()
        {
            var result = _sut.Expr("2+2+2");


            result.Should().Be(6);
        }

        [Fact]
        public void MixedMultiplyExprReturns14()
        {
            var result = _sut.Expr("2+3*4");


            result.Should().Be(14);
        }

        [Fact]
        public void MixedMultiplyExprReturns6()
        {
            var result = _sut.Expr("2*3+4*7");


            result.Should().Be(34);
        }
    }
}
