using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AppsInfo.HttpHandlers.UnitTests.Versions
{
	[TestClass]
	public class VersionServiceTest
	{
		[TestMethod]
		public void GetVersionJson_AnyAssembly_HasDataAndNumberProperties()
		{
			var content = AppVersionService.GetVersionJson();
			StringAssert.StartsWith(content, "{");
			StringAssert.EndsWith(content, "}");
			StringAssert.Contains(content, "\"Date\":");
			StringAssert.Contains(content, "\"Number\":");
		}

		[TestMethod]
		public void GetVersionHtml_AnyAssembly_HasDataAndNumberProperties()
		{
			var content = AppVersionService.GetVersionHtml();
			StringAssert.StartsWith(content, "<!DOCTYPE html>");
			StringAssert.EndsWith(content, "</html>");
			StringAssert.Contains(content, "Versão");
			StringAssert.Contains(content, "Publicação");
		}

		[TestMethod]
		public void GetVersionImage_AnyAssembly_ImageBytes()
		{
			var content = AppVersionService.GetVersionImage();
			Assert.IsNotNull(content);
			Assert.AreNotEqual(0, content.Length);
		}
	}
}
