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

        [Required]
        public string UserName { get; set; }


        public virtual Recommendation Recs { get; set; }

        public virtual Group UserGroups { get; set; }

    }
}
