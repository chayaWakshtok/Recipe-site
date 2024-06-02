using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Difficulty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
