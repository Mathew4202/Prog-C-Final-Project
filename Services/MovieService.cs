using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using Final_Project.Data.Models;
using Final_Project.Dtos;

namespace Final_Project.Services;

public class MovieService
{
    private readonly ImdbContext _context;

    public MovieService(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<MovieListItemDto>> GetAllMoviesAsync(string? searchText = null)
    {
        var query =
            from t in _context.Titles
            join r in _context.Ratings on t.TitleId equals r.TitleId into ratingJoin
            from r in ratingJoin.DefaultIfEmpty()
            where t.TitleType == "movie"
            select new MovieListItemDto
            {
                TitleId = t.TitleId,
                PrimaryTitle = t.PrimaryTitle,
                StartYear = t.StartYear,
                TitleType = t.TitleType,
                AverageRating = r != null ? r.AverageRating : null,
                NumVotes = r != null ? r.NumVotes : null
            };

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(m => m.PrimaryTitle != null && m.PrimaryTitle.Contains(searchText));
        }

        return await query
            .OrderBy(m => m.PrimaryTitle)
            .Take(200)
            .ToListAsync();
    }

    public async Task<List<MovieListItemDto>> GetTopRatedMoviesAsync()
    {
        return await (
            from t in _context.Titles
            join r in _context.Ratings on t.TitleId equals r.TitleId
            where t.TitleType == "movie"
            orderby r.AverageRating descending, r.NumVotes descending
            select new MovieListItemDto
            {
                TitleId = t.TitleId,
                PrimaryTitle = t.PrimaryTitle,
                StartYear = t.StartYear,
                TitleType = t.TitleType,
                AverageRating = r.AverageRating,
                NumVotes = r.NumVotes
            })
            .Take(100)
            .ToListAsync();
    }

    public async Task<List<MovieListItemDto>> GetNewestMoviesAsync()
    {
        return await (
            from t in _context.Titles
            join r in _context.Ratings on t.TitleId equals r.TitleId into ratingJoin
            from r in ratingJoin.DefaultIfEmpty()
            where t.TitleType == "movie"
            orderby t.StartYear descending
            select new MovieListItemDto
            {
                TitleId = t.TitleId,
                PrimaryTitle = t.PrimaryTitle,
                StartYear = t.StartYear,
                TitleType = t.TitleType,
                AverageRating = r != null ? r.AverageRating : null,
                NumVotes = r != null ? r.NumVotes : null
            })
            .Take(100)
            .ToListAsync();
    }

    public async Task<Title?> GetMovieWithDirectorsAsync(string titleId)
    {
        return await _context.Titles
            .Include(t => t.Rating)
            .Include(t => t.Names)
            .Include(t => t.Reviews)
            .FirstOrDefaultAsync(t => t.TitleId == titleId);
    }

    public async Task<FullMovieDetailsDto?> GetFullMovieDetailsAsync(string titleId)
    {
        var movie = await _context.Titles
            .Include(t => t.Rating)
            .Include(t => t.Names)
            .FirstOrDefaultAsync(t => t.TitleId == titleId);

        if (movie == null)
            return null;

        return new FullMovieDetailsDto
        {
            TitleId = movie.TitleId,
            PrimaryTitle = movie.PrimaryTitle,
            OriginalTitle = movie.OriginalTitle,
            TitleType = movie.TitleType,
            IsAdult = movie.IsAdult,
            StartYear = movie.StartYear,
            EndYear = movie.EndYear,
            RuntimeMinutes = movie.RuntimeMinutes,
            AverageRating = movie.Rating?.AverageRating,
            NumVotes = movie.Rating?.NumVotes,
            Directors = movie.Names
                .Select(n => n.PrimaryName ?? "")
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList()
        };
    }
}