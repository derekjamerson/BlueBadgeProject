using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class UserProfileItemTwo
    {
        public string UserProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<GroupUpdate> ListOfGroups { get; set; }
    }
}
