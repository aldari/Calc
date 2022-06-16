namespace Calc.Tests
{
    public class ParserTests
    {
        private Parser _sut;

        public ParserTests()
        {
            _sut = new Parser();
        }

        [Fact]
        public void SingleNumber()
        {
            var expr = "2";


            var result = _sut.Expr(1);


            result.Equals(2);
        }

        [Fact]
        public void SimplePlusExprReturns4()
        {
            var expr = "2+2";


            var result = _sut.Expr(3);


            result.Equals(4);
        }

        [Fact]
        public void DoublePlusExprReturns6()
        {
            var expr = "2+2+2";


            var result = _sut.Expr(5);


            result.Equals(4);
        }
    }
}
