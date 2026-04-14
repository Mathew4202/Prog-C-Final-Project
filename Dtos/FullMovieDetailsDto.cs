namespace Final_Project.Dtos;

public class FullMovieDetailsDto
{
    public string TitleId { get; set; } = "";
    public string? PrimaryTitle { get; set; }
    public string? OriginalTitle { get; set; }
    public string? TitleType { get; set; }
    public bool? IsAdult { get; set; }
    public short? StartYear { get; set; }
    public short? EndYear { get; set; }
    public short? RuntimeMinutes { get; set; }
    public decimal? AverageRating { get; set; }
    public int? NumVotes { get; set; }
    public List<string> Directors { get; set; } = new();
}