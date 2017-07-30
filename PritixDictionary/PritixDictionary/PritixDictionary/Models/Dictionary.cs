using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Models
{
    public class Dictionary
    {
        [PrimaryKey, AutoIncrement]
        public int DictionaryId { get; set; }
        [Indexed(Name = "ListingID", Order = 1, Unique = true)]
        public string FromLanguage { get; set; }
        [Indexed(Name = "ListingID", Order = 2, Unique = true)]
        public string ToLanguage { get; set; }
    }
}
