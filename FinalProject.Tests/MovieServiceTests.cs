using System.Linq;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalProject.Tests
{
    [TestClass]
    public class MovieServiceTests
    {
        private MovieService _movieService = null!;

        [TestInitialize]
        public void Setup()
        {
            var context = new ImdbContext();
            _movieService = new MovieService(context);
        }

        [TestMethod]
        public async Task GetAllMoviesAsync_ReturnsMovies()
        {
            var result = await _movieService.GetAllMoviesAsync();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetAllMoviesAsync_SearchFiltersResults()
        {
            var result = await _movieService.GetAllMoviesAsync("Love");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);

            Assert.IsTrue(result.All(m =>
                string.IsNullOrWhiteSpace(m.PrimaryTitle) ||
                m.PrimaryTitle.Contains("Love", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public async Task GetTopRatedMoviesAsync_ReturnsSortedByRatingDescending()
        {
            var result = await _movieService.GetTopRatedMoviesAsync();

            Assert.IsTrue(result.Count > 1);

            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.IsTrue((result[i].AverageRating ?? 0) >= (result[i + 1].AverageRating ?? 0));
            }
        }

        [TestMethod]
        public async Task GetNewestMoviesAsync_ReturnsSortedByYearDescending()
        {
            var result = await _movieService.GetNewestMoviesAsync();

            Assert.IsTrue(result.Count > 1);

            for (int i = 0; i < result.Count - 1; i++)
            {
                Assert.IsTrue((result[i].StartYear ?? 0) >= (result[i + 1].StartYear ?? 0));
            }
        }

        [TestMethod]
        public async Task GetFullMovieDetailsAsync_ReturnsMovie()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            var firstMovie = movies.First();

            var details = await _movieService.GetFullMovieDetailsAsync(firstMovie.TitleId);

            Assert.IsNotNull(details);
            Assert.AreEqual(firstMovie.TitleId, details.TitleId);
        }
    }
}