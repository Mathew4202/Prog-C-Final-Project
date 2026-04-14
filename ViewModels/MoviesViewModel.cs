using System.Collections.ObjectModel;
using System.Windows.Input;
using Final_Project.Dtos;
using Final_Project.Helpers;
using Final_Project.Services;

namespace Final_Project.ViewModels;

public class MoviesViewModel : BaseViewModel
{
    private readonly MovieService _movieService = new(new Data.ImdbContext());

    public ObservableCollection<MovieListItemDto> Movies { get; set; } = new();

    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoadMoviesCommand { get; }
    public ICommand LoadTopRatedCommand { get; }
    public ICommand LoadNewestCommand { get; }

    public MoviesViewModel()
    {
        LoadMoviesCommand = new RelayCommand(async () => await LoadMoviesAsync());
        LoadTopRatedCommand = new RelayCommand(async () => await LoadTopRatedMoviesAsync());
        LoadNewestCommand = new RelayCommand(async () => await LoadNewestMoviesAsync());
    }

    public async Task LoadMoviesAsync()
    {
        Movies.Clear();
        var movies = await _movieService.GetAllMoviesAsync(SearchText);

        foreach (var movie in movies)
            Movies.Add(movie);
    }

    public async Task LoadTopRatedMoviesAsync()
    {
        Movies.Clear();
        var movies = await _movieService.GetTopRatedMoviesAsync();

        foreach (var movie in movies)
            Movies.Add(movie);
    }

    public async Task LoadNewestMoviesAsync()
    {
        Movies.Clear();
        var movies = await _movieService.GetNewestMoviesAsync();

        foreach (var movie in movies)
            Movies.Add(movie);
    }
}