using Microsoft.AspNetCore.Http;
using System;
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

        public IFormFile ImageFile { get; set; }

        public int? Status { get; set; }


    }
}
