using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Test_ASP_Project.Controllers
{
	[Route("{controller}")]
	public class ErrorController : Controller
	{
		private readonly ILogger<ErrorController> logger;
		public ErrorController(ILogger<ErrorController> logger)
		{
			this.logger = logger;
		}

		[Route("{statusCode}")] // localhost/Error/404
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			// UseStatusCodePagesWithReExecute("error/{0}")
			var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

			switch (statusCode)
			{
				case 404:
					ViewBag.ErrorMessage = "The resource couldnt be found";
					logger.LogWarning($"404 Error. Path {statusCodeResult?.OriginalPath} || " +
							$"QueryString {statusCodeResult?.OriginalQueryString}");
					break;

				default:
                    ViewBag.ErrorMessage = "Placeholder Error";
					break;
            }

			// return custom view "NotFound", because this method's name is not appropriate
			return View("NotFound");
		}

		[Route("")] // localhost/Error
		[AllowAnonymous]
		public IActionResult Error()
		{
			// UseExeceptionHander("/error")
			var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

			logger.LogError($"Path: {exceptionDetails.Path} || " +
                $"Message {exceptionDetails.Error.Message} || " +
                $"StackTrace {exceptionDetails.Error.StackTrace}");

			return View();
		}
	}
}
