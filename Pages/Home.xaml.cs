using System;
using System.Windows;
using System.Windows.Controls;

namespace Final_Project.Pages
{
    public partial class Home : UserControl
    {
        private readonly Action _navigateToMovies;

        public Home() : this(() => { })
        {
        }

        public Home(Action navigateToMovies)
        {
            InitializeComponent();
            _navigateToMovies = navigateToMovies;
        }

        private void BrowseMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            _navigateToMovies?.Invoke();
        }
    }
}