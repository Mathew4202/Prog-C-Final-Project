using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using Final_Project.Dtos;

namespace Final_Project.Services;

public class DirectorService
{
    private readonly ImdbContext _context;

    public DirectorService(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<DirectorListItemDto>> GetDirectorsAsync(string? searchText = null)
    {
        var query = _context.Names
            .Where(n => n.Titles.Any(t => t.TitleType == "movie"))
            .Select(n => new DirectorListItemDto
            {
                NameId = n.NameId,
                DirectorName = n.PrimaryName,
                MovieCount = n.Titles.Count(t => t.TitleType == "movie")
            });

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            query = query.Where(d => d.DirectorName != null && d.DirectorName.Contains(searchText));
        }

        return await query
            .OrderBy(d => d.DirectorName)
            .Take(200)
            .ToListAsync();
    }

    public async Task<List<SimpleMovieDto>> GetMoviesByDirectorAsync(string nameId)
    {
        return await _context.Names
            .Where(n => n.NameId == nameId)
            .SelectMany(n => n.Titles
                .Where(t => t.TitleType == "movie")
                .Select(t => new SimpleMovieDto
                {
                    TitleId = t.TitleId,
                    PrimaryTitle = t.PrimaryTitle,
                    StartYear = t.StartYear
                }))
            .OrderByDescending(t => t.StartYear)
            .ThenBy(t => t.PrimaryTitle)
            .ToListAsync();
    }
}