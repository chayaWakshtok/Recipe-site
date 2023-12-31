using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class InstructionDTO
    {
        public int? Id { get; set; }

        public int? Step { get; set; }

        public string? Description { get; set; }

        public int? RecipeId { get; set; }
    }
}
