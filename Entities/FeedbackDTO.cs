using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FeedbackDTO:BaseEntity
    {
        public string? Mark { get; set; }

        public int? UserId { get; set; }

        public int? Type { get; set; }

        public int? RecipeId { get; set; }

        public UserDTO? User { get; set; }
    }
}
