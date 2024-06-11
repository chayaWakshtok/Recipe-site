using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Feedback
{
    public int Id { get; set; }

    public string? Mark { get; set; }

    public int? UserId { get; set; }

    public int? Type { get; set; }

    public int? RecipeId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }
    public int? FeedbackId { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
