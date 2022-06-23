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
            var result = _sut.Evaluate("2");


            result.Should().Be(2);
        }

        [Fact]
        public void SimplePlusExprReturns4()
        {
            var result = _sut.Evaluate("2+2");


            result.Should().Be(4);
        }

        [Fact]
        public void DoublePlusExprReturns6()
        {
            var result = _sut.Evaluate("2+2+2");


            result.Should().Be(6);
        }

        [Fact]
        public void MixedMultiplyExprReturns14()
        {
            var result = _sut.Evaluate("2+3*4");


            result.Should().Be(14);
        }

        [Fact]
        public void MixedMultiplyExprReturns6()
        {
            var result = _sut.Evaluate("2*3+4*7");


            result.Should().Be(34);
        }

        [Fact]
        public void ParenthesExprReturns12780300()
        {
            var result = _sut.Evaluate("(2+48)*((871-494)*678)");


            result.Should().Be(12780300);
        }

        [Fact]
        public void UnarMinusExprReturnsMinusThree()
        {
            var result = _sut.Evaluate("4+-7");


            result.Should().Be(-3);
        }

        [Fact]
        public void UnarMinusExpr2ReturnsReturnsMinusThree()
        {
            var result = _sut.Evaluate("4+(-7)");


            result.Should().Be(-3);
        }

        [Fact]
        public void UnarMinusExpr3ReturnsReturnsMinusThree()
        {
            var result = _sut.Evaluate("4+(-(-7))");


            result.Should().Be(11);
        }

        [Fact]
        public void ExprWithFloatNumbersEvaluatesSuccesfull()
        {
            var result = _sut.Evaluate("4681.8468/8649.5466");


            result.Should().Be(0.5412823372730312);
        }

        [Fact]
        public void FunctionExtensionReturns6()
        {
            var result = _sut.Evaluate("2+sin(1.5707963267)+3");


            result.Should().Be(6);
        }

        [Fact]
        public void FunctionExtensionWithUpperCaseTextReturns6()
        {
            var result = _sut.Evaluate("2+SIN(1.5707963267)+3");


            result.Should().Be(6);
        }

        [Fact]
        public void NullInputProcessedWithoutExceptionTest()
        {
            var result = _sut.Evaluate(null);


            result.Should().Be(0);
        }
    }
}
