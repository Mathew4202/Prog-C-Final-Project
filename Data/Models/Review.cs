using System;
using System.Collections.Generic;

namespace Final_Project.Data.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public string TitleId { get; set; } = null!;

    public string ReviewerName { get; set; } = null!;

    public int ReviewScore { get; set; }

    public string ReviewText { get; set; } = null!;

    public DateTime ReviewDate { get; set; }

    public virtual Title Title { get; set; } = null!;
}
