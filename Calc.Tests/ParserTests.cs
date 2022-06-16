using Moq;

namespace Calc.Tests
{
    public class ParserTests
    {
        private Parser _sut;

        public ParserTests()
        {
            _sut = new Parser(null);
        }

        [Fact]
        public void SingleNumber()
        {
            var expr = "2";
            var tokenizer = new Mock<ITokenizer>();
            tokenizer.SetupSequence(x => x.SingleValue())
                .Returns(2);
            tokenizer.SetupSequence(x => x.GetCurrToken())
                .Returns(Token.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr();


            result.Equals(2);
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
                .Returns(Token.NUMBER)
                .Returns(Token.PLUS)
                .Returns(Token.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr();


            result.Equals(4);
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
                .Returns(Token.NUMBER)
                .Returns(Token.PLUS)
                .Returns(Token.NUMBER)
                .Returns(Token.PLUS)
                .Returns(Token.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr();


            result.Equals(4);
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
                .Returns(Token.NUMBER)
                .Returns(Token.PLUS)
                .Returns(Token.NUMBER)
                .Returns(Token.MUL)
                .Returns(Token.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr();


            result.Equals(14);
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
                .Returns(Token.NUMBER)
                .Returns(Token.MUL)
                .Returns(Token.NUMBER)
                .Returns(Token.PLUS)
                .Returns(Token.NUMBER)
                .Returns(Token.MUL)
                .Returns(Token.NUMBER);
            var sut = new Parser(tokenizer.Object);


            var result = sut.Expr();


            result.Equals(34);
        }
    }
}
