using Calc;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IParser _parser;

        public HomeController(IParser parser)
        {
            _parser = parser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new MathResponseModel
            {
                Expression = "6*2-4",
                Result = 0
            });
        }

        [HttpPost]
        public IActionResult Index(InputModel model)
        {
            try
            {
                var value = _parser.Evaluate(model.Expression);
                return View(new MathResponseModel
                {
                    Expression = model.Expression,
                    Result = value
                });
            }
            catch (Exception e)
            {
                return View(new MathResponseModel
                {
                    Expression = model.Expression,
                    DescriptionMessage = e.Message,
                    Result = 0
                });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}