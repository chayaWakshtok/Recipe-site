using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Ingredient
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Count { get; set; }

    public int? TypeCount { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
