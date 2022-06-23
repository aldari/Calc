using Calc;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;
using Web.Models;

namespace Web.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void GetIndexReturnsDefaultStateTest()
        {
            var controller = new HomeController(null);

            // Act
            var result = controller.Index();

            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeEquivalentTo(new MathResponseModel
            {
                Expression = "6*2-4",
                Result = 0
            });
        }

        [Fact]
        public void PosttIndexReturnsModelWithEquationResultTest()
        {
            var parser = new Mock<IParser>();
            parser.Setup(x => x.Evaluate(It.IsAny<string>()))
                .Returns(11);
            var controller = new HomeController(parser.Object);

            // Act
            var result = controller.Index(new InputModel { Expression = "4*3-1" });


            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeEquivalentTo(new MathResponseModel
            {
                Expression = "4*3-1",
                Result = 11
            });
        }

        [Fact]
        public void PosttIndexReturnsResultForNullStringTest()
        {
            var parser = new Mock<IParser>();
            parser.Setup(x => x.Evaluate(It.IsAny<string>()))
                .Returns(0);
            var controller = new HomeController(parser.Object);

            // Act
            var result = controller.Index(new InputModel { Expression = null });


            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeEquivalentTo(new MathResponseModel
            {
                Expression = null,
                Result = 0
            });
        }

        [Fact]
        public void PosttIndexCatchesCalculatorExceptionTest()
        {
            var parser = new Mock<IParser>();
            parser.Setup(x => x.Evaluate(It.IsAny<string>()))
                .Throws(new CalcCustomException("message"));
            var controller = new HomeController(parser.Object);

            // Act
            var result = controller.Index(new InputModel { Expression = "1/0" });


            result.Should().BeOfType<ViewResult>();
            (result as ViewResult).Model.Should().BeEquivalentTo(new MathResponseModel
            {
                Expression = "1/0",
                Result = 0,
                DescriptionMessage = "message"
            });
        }
    }
}