using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Entities
{
    public class RecipeDTO
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? VideoUrl { get; set; }

        public double? Servings { get; set; }

        public double? PrepTime { get; set; }

        public double? Calories { get; set; }

        public double? Fat { get; set; }

        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public int? UserId { get; set; }

        public int? DifficultyId { get; set; }

        public int? Status { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public int? CategoryId { get; set; }

        public CategoryDTO? Category { get; set; }

        public DifficultyDTO? Difficulty { get; set; }

        public ICollection<FeedbackDTO> Feedbacks { get; set; } = new List<FeedbackDTO>();

        public string? ImageUrl { get; set; }

        public ICollection<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
        public ICollection<InstructionDTO> Instructions { get; set; } = new List<InstructionDTO>();
        public ICollection<LikesDTO> Likes { get; set; } = new List<LikesDTO>();


        public UserDTO? User { get; set; }
        public int CountLikes { get; set; } 
    }
}
