namespace Final_Project.Dtos;

public class ReviewItemDto
{
    public int ReviewId { get; set; }
    public string TitleId { get; set; } = "";
    public string ReviewerName { get; set; } = "";
    public int ReviewScore { get; set; }
    public string ReviewText { get; set; } = "";
    public DateTime ReviewDate { get; set; }
}