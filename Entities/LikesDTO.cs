    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LikesDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public int? RecipeId { get; set; }
        public RecipeLikeDTO Recipe { get; set; }

    }

    public class RecipeLikeDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

    }

}
