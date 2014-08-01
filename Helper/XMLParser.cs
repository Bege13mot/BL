using System.IO;
using System.Xml.Linq;

namespace BookLibrary.Helper
{
    static class XmlParser
    {
        /// <summary>
        /// Загружает XML Data File из выбранной директории
        /// </summary>
        /// <param name="fileName">File Source Path</param>
        /// <returns>Queryable XML doc</returns>
        public static XDocument GetXmlDataFromFileName(string fileName)
        {
            // Validate File Path
            if (fileName == null || !File.Exists(fileName))
            {
                throw new FileNotFoundException("Не найден путь к XML");
            }

            var xmlStream = new FileStream(fileName, FileMode.Open);

            return XDocument.Load(xmlStream);
        }
    }
}
