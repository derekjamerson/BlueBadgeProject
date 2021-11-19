using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBadgeProject.Data
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserProfile> ListOfUsers { get; set; }
        public Group()
        {
            ListOfUsers = new HashSet<UserProfile>();
        }
    }
}
