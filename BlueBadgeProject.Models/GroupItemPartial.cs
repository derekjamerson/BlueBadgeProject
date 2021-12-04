using BlueBadgeProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Models
{
    public class GroupItemPartial
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<UserProfileViewModel> ListOfUsers { get; set; }
    }
}
