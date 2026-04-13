namespace Final_Project.Dtos;

public class MovieDetailsDto
{
    public string TitleId { get; set; } = "";
    public string? PrimaryTitle { get; set; }
    public short? StartYear { get; set; }
    public string? TitleType { get; set; }
    public short? RuntimeMinutes { get; set; }
    public decimal? AverageRating { get; set; }
    public int? NumVotes { get; set; }
}