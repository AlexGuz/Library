using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Library.BLL.ExtensionMethods
{
    public static class DbListExtension
    {
        public static void ToXMLFile<T>(this List<T> libraryEntity, string connectionString) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<T>));
             
            using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, libraryEntity);
            }
        }
    }
}