using System.Windows;
using Final_Project.Data;
using Final_Project.Pages;
using Final_Project.Services;

namespace Final_Project
{
    public partial class MainWindow : Window
    {
        private readonly MovieService _movieService;
        private readonly ReviewService _reviewService;

        public MainWindow()
        {
            InitializeComponent();

            _movieService = new MovieService(new ImdbContext());
            _reviewService = new ReviewService(new ImdbContext());

            ShowHome();
        }

        private void ShowHome()
        {
            MainContent.Content = new Home(ShowMovies);
        }

        private void ShowMovies()
        {
            MainContent.Content = new MoviesPage(ShowDirectors, ShowHome, ShowDetails);
        }

        private void ShowDirectors()
        {
            MainContent.Content = new DirectorsPage(ShowMovies, ShowHome);
        }

        private void ShowDetails(string titleId)
        {
            MainContent.Content = new DetailsPage(titleId, _movieService, _reviewService, ShowMovies, ShowHome);
        }
    }
}