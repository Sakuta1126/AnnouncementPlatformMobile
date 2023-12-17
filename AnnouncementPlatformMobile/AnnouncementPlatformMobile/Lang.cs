using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnouncementPlatformMobile
{ 
    public class Lang
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string Language { get; set; }
        public string LanguageLevel { get; set; }
        public int user_id { get; set; }
    }
}
