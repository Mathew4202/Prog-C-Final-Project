using System.Collections.ObjectModel;
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