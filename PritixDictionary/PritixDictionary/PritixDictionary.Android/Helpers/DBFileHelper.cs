using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PritixDictionary.Droid.Helpers;
using Xamarin.Forms;
using PritixDictionary.Interfaces.ServiceInterface;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace PritixDictionary.Droid.Helpers
{
   public class FileHelper : IDBFileHelper
    {
        public string GetLocalDBFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}