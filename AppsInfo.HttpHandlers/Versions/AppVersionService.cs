using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using AppsInfo.HttpHandlers.Versions.Resources;

namespace AppsInfo.HttpHandlers
{
	/// <summary>
	/// Serviço que disponibiliza as informações referentes a versão do aplicativo.
	/// </summary>
	public static class AppVersionService
	{
		#region Public methods
		/// <summary>
		/// Obtém as informações do aplicativo em formato Html.
		/// </summary>
		/// <returns>O conteúdo html.</returns>
		public static string GetVersionHtml()
		{
			var version = GetVersion();

			return AppVersionServiceResource.Html
				.Replace("{App.Name}", version.Name)
				.Replace("{App.Version.Number}", version.Number)
				.Replace("{App.Version.Date}", version.Date.ToString("dd/MM/yyyy HH:mm:ss"));
		}

		/// <summary>
		/// Obtém as informações do aplicativo em formato Json.
		/// </summary>
		/// <returns>O conteúdo Json.</returns>
		public static string GetVersionJson()
		{
			var version = GetVersion();
			var serializer = new DataContractJsonSerializer(version.GetType());
			using (var ms = new MemoryStream())
			{
				serializer.WriteObject(ms, version);
				var result = Encoding.Default.GetString(ms.ToArray());

				return result;
			}
		}

		/// <summary>
		/// Obtém as informações do aplicativo em formato Png.
		/// </summary>
		/// <returns>O conteúdo Png.</returns>
		public static byte[] GetVersionImage()
		{
			var version = GetVersion();
			var text = String.Format(CultureInfo.InvariantCulture, "{0}: {1} - {2:dd/MM/yy HH:mm:ss}", 
				version.Name,
				version.Number, 
				version.Date);

			using (var bitmap = new Bitmap(800, 20))
			{
				var g = Graphics.FromImage((Image)bitmap);

				using (var font = new Font(FontFamily.GenericMonospace, 12))
				{
					g.DrawString(text, font, Brushes.Black, 0, 0);
				}

				using (var mem = new MemoryStream())
				{
					bitmap.Save(mem, ImageFormat.Png);

					return mem.ToArray();
				}
			}
		}
		#endregion

		#region Private methods
		/// <summary>
		/// Obtém a versão do aplicativo.
		/// </summary>
		/// <returns>A versão.</returns>
		private static AppVersion GetVersion()
		{
			var version = new AppVersion();
			Assembly assembly = null;
			AssemblyName assemblyName = null;
			var appSettingAssemblyName = ConfigurationManager.AppSettings["AppsInfo.AssemblyName"];

			if (!String.IsNullOrEmpty(appSettingAssemblyName))
			{
				assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName.StartsWith(appSettingAssemblyName));

				if (assembly == null)
				{
					assembly = Assembly.Load(appSettingAssemblyName);
				}
			}

			if (assembly == null && HttpContext.Current != null)
			{
				assembly = HttpContext.Current.ApplicationInstance.GetType().BaseType.Assembly;
			}			

			if (assembly == null)
			{
				assembly = GetFallbackAssembly();			
			}

			assemblyName = assembly.GetName();

			version.Name = assemblyName.Name;
			version.Number = assemblyName.Version.ToString();
			version.Date = File.GetLastWriteTime(assembly.Location);
			return version;
		}

		/// <summary>
		/// Obtém um assembly de reserva caso não seja possível obter o esperado.
		/// </summary>
		/// <returns>O assembly.</returns>
		private static Assembly GetFallbackAssembly()
		{
			var query = from assembly in AppDomain.CurrentDomain.GetAssemblies()
						where
							!assembly.GlobalAssemblyCache
						select assembly;

			var result = query.FirstOrDefault();

			if (result == null)
			{
				result = typeof(AppVersionService).Assembly;
			}

			return result;
		}
		#endregion
	}
}
