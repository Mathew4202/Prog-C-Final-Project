using System;
using System.Linq;
using System.Windows;
using Final_Project.Data;
using Final_Project.Services;

namespace Final_Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using var context = new ImdbContext();

                var movieService = new MovieService(context);
                var directorService = new DirectorService(context);
                var reviewService = new ReviewService(context);

                var movies = await movieService.GetAllMoviesAsync();
                var topRated = await movieService.GetTopRatedMoviesAsync();
                var newest = await movieService.GetNewestMoviesAsync();
                var directors = await directorService.GetDirectorsAsync();

                string message =
                    $"Movies loaded: {movies.Count}\n" +
                    $"Top rated loaded: {topRated.Count}\n" +
                    $"Newest loaded: {newest.Count}\n" +
                    $"Directors loaded: {directors.Count}";

                MessageBox.Show(message, "Backend Test Success");

                if (movies.Count > 0)
                {
                    var firstMovie = movies.First();

                    await reviewService.AddReviewAsync(
                        firstMovie.TitleId,
                        "Mathew",
                        5,
                        "Backend insert test"
                    );

                    var reviews = await reviewService.GetReviewsForMovieAsync(firstMovie.TitleId);

                    MessageBox.Show(
                        $"Inserted review for: {firstMovie.PrimaryTitle}\nTotal reviews now: {reviews.Count}",
                        "Review Test Success"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Backend Test Error");
            }
        }
    }
}