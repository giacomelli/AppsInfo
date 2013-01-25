using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppsInfo.HttpHandlers.UnitTests
{
	[TestClass]
	public class AppsInfoHttpHandlerTest
	{
		[TestMethod]
		public void IsReusable_Always_True()
		{
			var target = new AppsInfoHttpHandler();
			Assert.IsTrue(target.IsReusable);
		}

		[TestMethod]
		public void GetHttpHandler_HttpContext_Current()
		{
			var target = new AppsInfoHttpHandler();
			Assert.AreEqual(target, target.GetHttpHandler(null));
		}

		[TestMethod]
		public void ProcessRequest_JsonExtension_JsonResponse()
		{
			var target = new AppsInfoHttpHandler();
			var context = CreateHttpContext("json");
			target.ProcessRequest(context);

			var content = context.Response.Output.ToString();
			StringAssert.StartsWith(content, "{");
			StringAssert.EndsWith(content, "}");
			StringAssert.Contains(content, "\"Date\":");
			StringAssert.Contains(content, "\"Number\":");
		}

		[TestMethod]
		public void ProcessRequest_NoExtension_HtmlResponse()
		{
			var target = new AppsInfoHttpHandler();
			var context = CreateHttpContext("");
			target.ProcessRequest(context);

			var content = context.Response.Output.ToString();
			StringAssert.StartsWith(content, "<!DOCTYPE html>");
			StringAssert.EndsWith(content, "</html>");
			StringAssert.Contains(content, "Versão");
			StringAssert.Contains(content, "Publicação");
		}

		private HttpContext CreateHttpContext(string extension)
		{			
			var httpRequest = new HttpRequest("", "http://localhost/AppsInfo." + extension, "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			
			return new HttpContext(httpRequest, httpResponce);
		}
	}
}
