using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Ingredient
{
    public int Id { get; set; }

    public decimal? Count { get; set; }

    public int? TypeCount { get; set; }

    public int? RecipeId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
