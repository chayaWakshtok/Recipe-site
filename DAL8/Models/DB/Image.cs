using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Image
{
    public int Id { get; set; }

    public string? Url { get; set; }

    public string? Image1 { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
