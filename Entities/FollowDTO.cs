using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class FollowDTO
    {
        public int Id { get; set; }

        public int? ToUser { get; set; }

        public int? FromUser { get; set; }

        public UserDTO? FromUserNavigation { get; set; }

        public UserDTO? ToUserNavigation { get; set; }
    }
}
