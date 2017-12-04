using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace LibraryDB
{
    public static class DbListExtension
    {
        public static void ToXMLFile<T>(this List<T> libraryEntity, string connectionString) where T : class
        {
            XmlSerializer _formatter = new XmlSerializer(typeof(List<T>));
             
            using (FileStream fs = new FileStream(connectionString, FileMode.OpenOrCreate))
            {
                _formatter.Serialize(fs, libraryEntity);
            }
        }
    }
}