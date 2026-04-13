using System;
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
    }
}