﻿using NUnit.Framework;

namespace AppsInfo.HttpHandlers.UnitTests.Versions
{
	[TestFixture]
	public class VersionServiceTest
	{
		[Test]
		public void GetVersionJson_AnyAssembly_HasDataAndNumberProperties()
		{
			var content = AppVersionService.GetVersionJson();
			StringAssert.StartsWith("{", content);
			StringAssert.EndsWith("}", content);
			StringAssert.Contains("\"Date\":", content);
			StringAssert.Contains("\"Number\":", content);
		}

		[Test]
		public void GetVersionHtml_AnyAssembly_HasDataAndNumberProperties()
		{
			var content = AppVersionService.GetVersionHtml();
			StringAssert.StartsWith("<!DOCTYPE html>", content);
			StringAssert.EndsWith("</html>", content);
			StringAssert.Contains("Versão", content);
			StringAssert.Contains("Publicação", content);
		}

		[Test]
		public void GetVersionImage_AnyAssembly_ImageBytes()
		{
			var content = AppVersionService.GetVersionImage();
			Assert.IsNotNull(content);
			Assert.AreNotEqual(0, content.Length);
		}
	}
}
