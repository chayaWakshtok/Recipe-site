using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Instruction
{
    public int? Id { get; set; }

    public int? Step { get; set; }

    public string? Description { get; set; }

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
