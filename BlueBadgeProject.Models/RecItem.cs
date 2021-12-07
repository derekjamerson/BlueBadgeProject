using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class RecItem
    {
        public int RecommendationId { get; set; }
        public int SongId { get; set; }
        public string UserProfileId { get; set; }
        public int GroupId { get; set; }
    }
}
