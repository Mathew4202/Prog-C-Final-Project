using System;
using System.Windows;
using System.Windows.Controls;
using Final_Project.ViewModels;

namespace Final_Project.Pages
{
    public partial class DirectorsPage : UserControl
    {
        private readonly DirectorsViewModel _viewModel;
        private readonly Action _navigateToMovies;
        private readonly Action _navigateToHome;

        public DirectorsPage() : this(() => { }, () => { })
        {
        }

        public DirectorsPage(Action navigateToMovies, Action navigateToHome)
        {
            InitializeComponent();

            _navigateToMovies = navigateToMovies;
            _navigateToHome = navigateToHome;
            _viewModel = new DirectorsViewModel();
            DataContext = _viewModel;

            Loaded += DirectorsPage_Loaded;
        }

        private async void DirectorsPage_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadDirectorsAsync();
        }

        private void BackToMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToMovies?.Invoke();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToHome?.Invoke();
        }
    }
}