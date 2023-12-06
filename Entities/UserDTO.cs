using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserDTO : BaseEntity
    {
        public string Username { get; set; } = null!;

        public int? Status { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Picture { get; set; }

        public int? RoleId { get; set; }
        public RoleDTO? Role { get; set; }
        public string? FirstName { get; set; }
        public string? Token { get; set; }

    }
}
