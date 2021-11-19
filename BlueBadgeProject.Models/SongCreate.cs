using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class SongCreate
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Artist { get; set; }
    }
}
