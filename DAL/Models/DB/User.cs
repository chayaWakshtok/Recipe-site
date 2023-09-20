using System;
using System.Collections.Generic;

namespace DAL.Models.DB;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public int? Status { get; set; }

    public DateTime CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Picture { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Follow> FollowFromUserNavigations { get; set; } = new List<Follow>();

    public virtual ICollection<Follow> FollowToUserNavigations { get; set; } = new List<Follow>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role? Role { get; set; }
}
