using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class RecCreate
    {
        [Required]
        public int SongId { get; set; }
        [Required]
        public int GroupId { get; set; }
    }
}
