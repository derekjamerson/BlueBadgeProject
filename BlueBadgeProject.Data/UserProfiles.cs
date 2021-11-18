using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Data
{
    public class UserProfile
    {
        [Key]
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Group> ListOfGroups { get; set; }
        public virtual ICollection<Recommendation> ListOfRecommendations { get; set; }
        public UserProfile()
        {
            ListOfGroups = new HashSet<Group>();
            ListOfRecommendations = new HashSet<Recommendation>();
        }

    }
}
