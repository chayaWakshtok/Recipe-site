using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Recipe
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
    public string? ImageUrl { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Difficulty? Difficulty { get; set; }
    public virtual ICollection<Likes> Likes { get; set; } = new List<Likes>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public virtual ICollection<Instruction> Instructions { get; set; } = new List<Instruction>();

    public virtual User? User { get; set; }
}
