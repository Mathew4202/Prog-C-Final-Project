using System;
using System.Collections.Generic;

namespace Final_Project.Data.Models;

public partial class Title
{
    public string TitleId { get; set; } = null!;

    public string? TitleType { get; set; }

    public string? PrimaryTitle { get; set; }

    public string? OriginalTitle { get; set; }

    public bool? IsAdult { get; set; }

    public short? StartYear { get; set; }

    public short? EndYear { get; set; }

    public short? RuntimeMinutes { get; set; }

    public virtual Rating? Rating { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Name> Names { get; set; } = new List<Name>();
}
