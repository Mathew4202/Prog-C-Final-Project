using System;
using System.Collections.Generic;

namespace Final_Project.Data.Models;

public partial class Name
{
    public string NameId { get; set; } = null!;

    public string? PrimaryName { get; set; }

    public int? BirthYear { get; set; }

    public int? DeathYear { get; set; }

    public string? PrimaryProfession { get; set; }

    public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
}
