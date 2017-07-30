using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Models
{
    public class UserDictionaryMapping
    {
        [PrimaryKey, AutoIncrement]
        public int MappingIndex { get; set; }
        
        public int UserId { get; set; }
        
        public int DictionaryId { set; get; }
    }
}
