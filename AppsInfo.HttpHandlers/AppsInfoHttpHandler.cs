using System.IO;
using System.Web;
using System.Web.Routing;

namespace AppsInfo.HttpHandlers
{
	/// <summary>
	/// Manipulador HTTP para disponibilizar as informações do aplicativo no qual foi configurado.
	/// <remarks>
	/// Atualmente apresenta apenas informações da versão do aplicativo e pode retornar essas informações em 3 formatos, são eles:
	/// * Json: http://site/AppsInfo.Version.Json
	/// * Png: http://site/AppsInfo.Version.Png
	/// * Html: http://site/AppsInfo.Version.Html
	/// </remarks>
	/// </summary>
	public class AppsInfoHttpHandler : IHttpHandler, IRouteHandler
	{	
		#region IHttpHandler Members
		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
		///   </returns>
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			var response = context.Response;
			var extension = Path.GetExtension(context.Request.CurrentExecutionFilePath).ToLowerInvariant();

			switch (extension)
			{
				case ".json":
					response.Write(AppVersionService.GetVersionJson());
					break;

				case ".png":
					response.ContentType = "image/png";
					response.BinaryWrite(AppVersionService.GetVersionImage());
					break;

				default:
					response.Write(AppVersionService.GetVersionHtml());
					break;
			}
		}

		#endregion

		#region IRouteHandler Members
		/// <summary>
		/// Provides the object that processes the request.
		/// </summary>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <returns>
		/// An object that processes the request.
		/// </returns>
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return this;
		}
		#endregion
	}
}