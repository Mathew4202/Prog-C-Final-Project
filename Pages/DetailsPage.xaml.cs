using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Final_Project.Services;
using Final_Project.ViewModels;

namespace Final_Project.Pages
{
    public partial class DetailsPage : UserControl
    {
        private readonly Action _navigateToMovies;
        private readonly Action _navigateToHome;
        private readonly DetailsViewModel _viewModel;

        public DetailsPage(string titleId, MovieService movieService, ReviewService reviewService, Action navigateToMovies, Action navigateToHome)
        {
            InitializeComponent();

            _navigateToMovies = navigateToMovies;
            _navigateToHome = navigateToHome;
            _viewModel = new DetailsViewModel(titleId, movieService, reviewService);

            DataContext = _viewModel;
            Loaded += DetailsPage_Loaded;
        }

        private async void DetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();
            UpdateStarButtons();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToMovies?.Invoke();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToHome?.Invoke();
        }

        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag?.ToString(), out int score))
            {
                _viewModel.ReviewScore = score;
                UpdateStarButtons();
            }
        }

        private async void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _viewModel.SubmitReviewAsync();
                UpdateStarButtons();
                MessageBox.Show("Review submitted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Review Error");
            }
        }

        private void StarButton_MouseEnter(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Tag?.ToString(), out int score))
            {
                PreviewStars(score);
            }
        }

        private void StarButton_MouseLeave(object sender, RoutedEventArgs e)
        {
            UpdateStarButtons();
        }

        private void UpdateStarButtons()
        {
            Brush filled = Brushes.Gold;
            Brush empty = Brushes.LightGray;

            Star1.Foreground = _viewModel.ReviewScore >= 1 ? filled : empty;
            Star2.Foreground = _viewModel.ReviewScore >= 2 ? filled : empty;
            Star3.Foreground = _viewModel.ReviewScore >= 3 ? filled : empty;
            Star4.Foreground = _viewModel.ReviewScore >= 4 ? filled : empty;
            Star5.Foreground = _viewModel.ReviewScore >= 5 ? filled : empty;
        }

        private void PreviewStars(int score)
        {
            Brush filled = Brushes.Gold;
            Brush empty = Brushes.LightGray;

            Star1.Foreground = score >= 1 ? filled : empty;
            Star2.Foreground = score >= 2 ? filled : empty;
            Star3.Foreground = score >= 3 ? filled : empty;
            Star4.Foreground = score >= 4 ? filled : empty;
            Star5.Foreground = score >= 5 ? filled : empty;
        }
    }
}