using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Interfaces.ServiceInterface
{
    public interface IDBFileHelper
    {
        string GetLocalDBFilePath(string filename);
    }
}
