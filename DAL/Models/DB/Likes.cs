using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DB
{
    public partial class Likes
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RecipeId { get; set; }

        public virtual User? User { get; set; }
        public virtual Recipe? Recipe { get; set; }


    }
}
