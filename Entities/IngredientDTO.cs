﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class IngredientDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? Count { get; set; }

        public int? TypeCount { get; set; }

        public int? RecipeId { get; set; }

    }
}
