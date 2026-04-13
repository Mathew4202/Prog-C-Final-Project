using System;
using System.Windows.Controls;
using Final_Project.ViewModels;

namespace Final_Project.Pages
{
    public partial class MoviesPage : UserControl
    {
        private readonly Action _navigateToDirectors;
        private readonly MoviesViewModel _viewModel;

        public MoviesPage() : this(() => { })
        {
        }

        public MoviesPage(Action navigateToDirectors)
        {
            InitializeComponent();

            _navigateToDirectors = navigateToDirectors;

            _viewModel = new MoviesViewModel();
            DataContext = _viewModel;

            Loaded += MoviesPage_Loaded;
        }

        private async void MoviesPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _viewModel.LoadMoviesAsync();
        }
    }
}