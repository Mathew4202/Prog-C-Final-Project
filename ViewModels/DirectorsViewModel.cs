using System.Collections.ObjectModel;
using System.Windows.Input;
using Final_Project.Dtos;
using Final_Project.Helpers;
using Final_Project.Services;

namespace Final_Project.ViewModels;

public class DirectorsViewModel : BaseViewModel
{
    private readonly DirectorService _directorService = new(new Data.ImdbContext());

    public ObservableCollection<DirectorListItemDto> Directors { get; set; } = new();
    public ObservableCollection<SimpleMovieDto> DirectorMovies { get; set; } = new();

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

    private DirectorListItemDto? _selectedDirector;
    public DirectorListItemDto? SelectedDirector
    {
        get => _selectedDirector;
        set
        {
            _selectedDirector = value;
            DirectorMovies.Clear();
            OnPropertyChanged();
        }
    }

    public ICommand LoadDirectorsCommand { get; }
    public ICommand LoadDirectorMoviesCommand { get; }

    public DirectorsViewModel()
    {
        LoadDirectorsCommand = new RelayCommand(async () => await LoadDirectorsAsync());
        LoadDirectorMoviesCommand = new RelayCommand(async () => await LoadMoviesForSelectedDirectorAsync());
    }

    public async Task LoadDirectorsAsync()
    {
        Directors.Clear();
        var directors = await _directorService.GetDirectorsAsync(SearchText);

        foreach (var director in directors)
            Directors.Add(director);
    }

    public async Task LoadMoviesForSelectedDirectorAsync()
    {
        DirectorMovies.Clear();

        if (SelectedDirector == null)
            return;

        var movies = await _directorService.GetMoviesByDirectorAsync(SelectedDirector.NameId);

        foreach (var movie in movies)
            DirectorMovies.Add(movie);
    }
}