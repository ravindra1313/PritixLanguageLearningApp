using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PritixDictionary.Models
{
    public class UserProgress
    {
        [ForeignKey(typeof(User)), Indexed(Name = "ProgressID", Order = 1, Unique = true)]
        public int UserId { get; set; }
        [ForeignKey(typeof(Dictionary)), Indexed(Name = "ProgressID", Order = 1, Unique = true)]
        public int DictionaryId { get; set; }
        public int UserMarks { get; set; }
        public int TotalMarks { get; set; }
        public DateTime ScoreTime { get; set; }
    }
}
