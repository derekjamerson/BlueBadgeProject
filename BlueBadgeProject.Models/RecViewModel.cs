using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class RecViewModel
    {
        public int RecommendationId { get; set; }
        public string SongTitle { get; set; }
        public string SongArtist { get; set; }
        public string RecommendedBy { get; set;}

    }
}
