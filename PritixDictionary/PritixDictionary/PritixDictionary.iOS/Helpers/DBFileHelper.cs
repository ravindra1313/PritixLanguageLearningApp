using System;
using PritixDictionary.Interfaces.ServiceInterface;
using System.IO;
using PritixDictionary.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(DBFileHelper))]
namespace PritixDictionary.iOS.Helpers
{
    public class DBFileHelper : IDBFileHelper
    {
        public string GetLocalDBFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}