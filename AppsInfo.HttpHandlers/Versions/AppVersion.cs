using System;
using System.Runtime.Serialization;

namespace AppsInfo.HttpHandlers
{
	/// <summary>
	/// Entidade para representação das informações de versão do aplicativo.
	/// </summary>
	[DataContract]
	public class AppVersion
	{
		/// <summary>
		/// Obtém ou define o nome da versão do aplicativo.
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Obtém ou define o número da versão do aplicativo.
		/// </summary>
		[DataMember]
		public string Number { get; set; }

		/// <summary>
		/// Obtém ou define a data da versão do aplicativo.
		/// </summary>
		[DataMember]
		public DateTime Date { get; set; }
	}
}
