using Microsoft.EntityFrameworkCore;
using Final_Project.Data;
using Final_Project.Data.Models;
using Final_Project.Dtos;

namespace Final_Project.Services;

public class ReviewService
{
    private readonly ImdbContext _context;

    public ReviewService(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<ReviewItemDto>> GetReviewsForMovieAsync(string titleId)
    {
        return await _context.Reviews
            .Where(r => r.TitleId == titleId)
            .OrderByDescending(r => r.ReviewDate)
            .Select(r => new ReviewItemDto
            {
                ReviewId = r.ReviewId,
                TitleId = r.TitleId,
                ReviewerName = r.ReviewerName,
                ReviewScore = r.ReviewScore,
                ReviewText = r.ReviewText,
                ReviewDate = r.ReviewDate
            })
            .ToListAsync();
    }

    public async Task AddReviewAsync(string titleId, string reviewerName, int reviewScore, string reviewText)
    {
        if (string.IsNullOrWhiteSpace(titleId))
            throw new ArgumentException("TitleId is required.");

        if (string.IsNullOrWhiteSpace(reviewerName))
            throw new ArgumentException("Reviewer name is required.");

        if (reviewScore < 1 || reviewScore > 5)
            throw new ArgumentException("Review score must be between 1 and 5.");

        if (string.IsNullOrWhiteSpace(reviewText))
            throw new ArgumentException("Review text is required.");

        var review = new Review
        {
            TitleId = titleId,
            ReviewerName = reviewerName,
            ReviewScore = reviewScore,
            ReviewText = reviewText,
            ReviewDate = DateTime.Now
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
    }
}