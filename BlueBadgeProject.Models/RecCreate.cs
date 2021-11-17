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
        public int SongID { get; set; }
        [Required]
        public int UserProfileId { get; set; }
        [Required]
        public int GroupID { get; set; }
    }
}
