using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Models
{
    public class DictionaryDetail
    {
        [ForeignKey(typeof(UserDictionaryMapping))]
        public int MappingKey { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
