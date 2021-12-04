using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class GroupItemFull
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<UserProfileViewModel> ListOfUsers { get; set; }
        public ICollection<RecViewModel> ListOfRecs { get; set; }
    }
}
