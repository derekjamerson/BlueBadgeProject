using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Data
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
