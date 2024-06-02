using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class Follow
{
    public int Id { get; set; }

    public int? ToUser { get; set; }

    public int? FromUser { get; set; }

    public virtual User? FromUserNavigation { get; set; }

    public virtual User? ToUserNavigation { get; set; }
}
