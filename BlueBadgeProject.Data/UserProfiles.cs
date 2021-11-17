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


        //private List<Groups> _groups = new List<Groups>();
        //private List<Recommendations> _recommendations = new List<Recommendations>();

        //public List<Groups> Groups
        //{
        //    get
        //    { return _groups; }
        //}

        //public List<Recommendations> Recommendations
        //{
        //    get 
        //    { return _recommendations; }
        //}
    }
}
