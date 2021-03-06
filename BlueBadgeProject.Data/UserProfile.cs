using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueBadgeProject.Data
{
    public class UserProfile
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserProfileId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Group> ListOfGroups { get; set; }
        
        public UserProfile()
        {
            ListOfGroups = new HashSet<Group>();
        }

    }
}
