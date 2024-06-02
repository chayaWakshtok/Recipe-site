using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.DB
{
    public partial class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    }
}
