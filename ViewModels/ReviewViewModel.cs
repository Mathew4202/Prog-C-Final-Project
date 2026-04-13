using System.Collections.ObjectModel;
using System.Windows.Input;
using Final_Project.Dtos;
using Final_Project.Helpers;
using Final_Project.Services;

namespace Final_Project.ViewModels;

public class ReviewViewModel : BaseViewModel
{
    private readonly ReviewService _reviewService = new(new Data.ImdbContext());

    public ObservableCollection<ReviewItemDto> Reviews { get; set; } = new();

    public ICommand LoadReviewsCommand { get; }
    public ICommand SubmitReviewCommand { get; }

    private string _titleId = "";
    public string TitleId
    {
        get => _titleId;
        set
        {
            _titleId = value;
            OnPropertyChanged();
        }
    }

    private string _reviewerName = "";
    public string ReviewerName
    {
        get => _reviewerName;
        set
        {
            _reviewerName = value;
            OnPropertyChanged();
        }
    }

    private int _reviewScore;
    public int ReviewScore
    {
        get => _reviewScore;
        set
        {
            _reviewScore = value;
            OnPropertyChanged();
        }
    }

    private string _reviewText = "";
    public string ReviewText
    {
        get => _reviewText;
        set
        {
            _reviewText = value;
            OnPropertyChanged();
        }
    }

    public ReviewViewModel()
    {
        LoadReviewsCommand = new RelayCommand(async () => await LoadReviewsAsync());
        SubmitReviewCommand = new RelayCommand(async () => await SubmitReviewAsync());
    }

    public async Task LoadReviewsAsync()
    {
        Reviews.Clear();

        if (string.IsNullOrWhiteSpace(TitleId))
            return;

        var reviews = await _reviewService.GetReviewsForMovieAsync(TitleId);
        foreach (var review in reviews)
            Reviews.Add(review);
    }

    public async Task SubmitReviewAsync()
    {
        await _reviewService.AddReviewAsync(TitleId, ReviewerName, ReviewScore, ReviewText);
        await LoadReviewsAsync();
    }
}