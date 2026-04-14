using System;
using System.Windows;
using System.Windows.Controls;
using Final_Project.Dtos;
using Final_Project.ViewModels;

namespace Final_Project.Pages
{
    public partial class MoviesPage : UserControl
    {
        private readonly MoviesViewModel _viewModel;
        private readonly Action _navigateToDirectors;
        private readonly Action _navigateToHome;
        private readonly Action<string> _navigateToDetails;

        public MoviesPage() : this(() => { }, () => { }, _ => { })
        {
        }

        public MoviesPage(Action navigateToDirectors, Action navigateToHome, Action<string> navigateToDetails)
        {
            InitializeComponent();

            _navigateToDirectors = navigateToDirectors;
            _navigateToHome = navigateToHome;
            _navigateToDetails = navigateToDetails;

            _viewModel = new MoviesViewModel();
            DataContext = _viewModel;

            Loaded += MoviesPage_Loaded;
        }

        private async void MoviesPage_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadMoviesAsync();
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadMoviesAsync();
        }

        private async void TopRatedButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadTopRatedMoviesAsync();
        }

        private async void NewestButton_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadNewestMoviesAsync();
        }

        private void DirectorsButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToDirectors?.Invoke();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToHome?.Invoke();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is MovieListItemDto movie)
            {
                _navigateToDetails?.Invoke(movie.TitleId);
            }
        }
    }
}