﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CategoryDTO : BaseEntity
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public int? Status { get; set; }
        public int CountRecipe { get; set; } = 0;


    }
}
