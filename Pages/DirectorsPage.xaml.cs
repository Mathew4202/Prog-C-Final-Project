using System.Windows.Controls;
using Final_Project.ViewModels;

namespace Final_Project.Pages
{
    public partial class DirectorsPage : UserControl
    {
        private readonly DirectorsViewModel _viewModel;

        public DirectorsPage()
        {
            InitializeComponent();
            _viewModel = new DirectorsViewModel();
            DataContext = _viewModel;
            Loaded += DirectorsPage_Loaded;
        }

        private async void DirectorsPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await _viewModel.LoadDirectorsAsync();
        }
    }
}