using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace gigapede.Resources
{
	class PersistanceManager
	{
		private String fileName;

		public PersistanceManager(String fileName)
		{
			this.fileName = fileName;
		}



		public void Save(int highScore)
		{
			using (XmlWriter writer = XmlWriter.Create(fileName))
			{
				writer.WriteStartDocument();

				writer.WriteStartElement("highScores");
				writer.WriteElementString("score", "" + highScore);
				writer.WriteEndElement();

				writer.WriteEndDocument();
			}
		}



		public String Load()
		{
			String value = "";

			using (XmlReader reader = XmlReader.Create(fileName))
			{
				while (reader.Read())
					if (reader.IsStartElement() && reader.Name.Equals("score") && reader.Read())
						value = reader.Value;
			}

			return value;
		}
	}
}
