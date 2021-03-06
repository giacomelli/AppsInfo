﻿using System.IO;
using System.Web;
using NUnit.Framework;

namespace AppsInfo.HttpHandlers.UnitTests
{
	[TestFixture]
	public class AppsInfoHttpHandlerTest
	{
		[Test]
		public void IsReusable_Always_True()
		{
			var target = new AppsInfoHttpHandler();
			Assert.IsTrue(target.IsReusable);
		}

		[Test]
		public void GetHttpHandler_HttpContext_Current()
		{
			var target = new AppsInfoHttpHandler();
			Assert.AreEqual(target, target.GetHttpHandler(null));
		}

		[Test]
		public void ProcessRequest_HtmlExtension_HtmlResponse()
		{
			var target = new AppsInfoHttpHandler();
			var context = CreateHttpContext("HTML");
			target.ProcessRequest(context);

			var content = context.Response.Output.ToString();
			StringAssert.StartsWith("<!DOCTYPE html>", content);
			StringAssert.EndsWith("</html>", content);
			StringAssert.Contains("Versão", content);
			StringAssert.Contains("Publicação", content);
		}

		private HttpContext CreateHttpContext(string output)
		{			
			var httpRequest = new HttpRequest("", "http://localhost/AppsInfo/Version/" + output, "");
			var stringWriter = new StringWriter();
			var httpResponce = new HttpResponse(stringWriter);
			
			return new HttpContext(httpRequest, httpResponce);
		}
	}
}
