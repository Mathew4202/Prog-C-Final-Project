using System.Collections.ObjectModel;
using Final_Project.Dtos;
using Final_Project.Helpers;
using Final_Project.Services;

namespace Final_Project.ViewModels
{
    public class DetailsViewModel : BaseViewModel
    {
        private readonly MovieService _movieService;
        private readonly ReviewService _reviewService;

        public string TitleId { get; }

        private FullMovieDetailsDto? _movie;
        public FullMovieDetailsDto? Movie
        {
            get => _movie;
            set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ReviewItemDto> Reviews { get; set; } = new();

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

        public DetailsViewModel(string titleId, MovieService movieService, ReviewService reviewService)
        {
            TitleId = titleId;
            _movieService = movieService;
            _reviewService = reviewService;
        }

        public async Task LoadAsync()
        {
            Movie = await _movieService.GetFullMovieDetailsAsync(TitleId);

            var reviews = await _reviewService.GetReviewsForMovieAsync(TitleId);
            Reviews.Clear();

            foreach (var review in reviews)
                Reviews.Add(review);
        }

        public async Task SubmitReviewAsync()
        {
            await _reviewService.AddReviewAsync(TitleId, ReviewerName, ReviewScore, ReviewText);

            ReviewerName = "";
            ReviewScore = 0;
            ReviewText = "";

            OnPropertyChanged(nameof(ReviewerName));
            OnPropertyChanged(nameof(ReviewScore));
            OnPropertyChanged(nameof(ReviewText));

            await LoadAsync();
        }
    }
}