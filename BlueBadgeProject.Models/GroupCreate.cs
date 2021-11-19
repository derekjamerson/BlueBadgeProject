using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class GroupCreate
    {
        [Required]
        public int GroupId { get; set; }
        [Required]
        public int UserProfileId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

