﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DifficultyDTO:BaseEntity
    {
        public string Name { get; set; } = null!;

        public int? Status { get; set; }
    }
}
