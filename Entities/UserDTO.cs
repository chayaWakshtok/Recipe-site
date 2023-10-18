using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserDTO : BaseEntity
    {
        public string Username { get; set; } = null!;

        public int? Status { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Picture { get; set; }
        public string? FirstName { get; set; }


        public int? RoleId { get; set; }

        //public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        //public virtual ICollection<Follow> FollowFromUserNavigations { get; set; } = new List<Follow>();

        ///public virtual ICollection<Follow> FollowToUserNavigations { get; set; } = new List<Follow>();

        //public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

        public RoleDTO? Role { get; set; }
    }
}
