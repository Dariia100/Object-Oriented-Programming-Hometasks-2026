
namespace MovieCatalogApp;

public partial class EditPage : ContentPage
{
    public Movie MovieItem { get; private set; }
    public bool IsSaved { get; private set; }
    private readonly TaskCompletionSource<bool> _completion = new TaskCompletionSource<bool>();
    public Task<bool> EditResult => _completion.Task;
    public EditPage(Movie? movie = null)
    {
        InitializeComponent();
        Disappearing += (s, e) => _completion.TrySetResult(IsSaved);
        MovieItem = movie != null ? 
            new Movie
            {
                Id = movie.Id,
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseYear = movie.ReleaseYear,
                MainActors = new List<string>(movie.MainActors)
            }
            : new Movie();

        if (movie != null)
        {
            TitleEntry.Text = MovieItem.Title;
            GenreEntry.Text = MovieItem.Genre;
            YearEntry.Text = MovieItem.ReleaseYear.ToString();

            if (MovieItem.MainActors.Count > 0) Actor1Entry.Text = MovieItem.MainActors[0];
            if (MovieItem.MainActors.Count > 1) Actor2Entry.Text = MovieItem.MainActors[1];
            if (MovieItem.MainActors.Count > 2) Actor3Entry.Text = MovieItem.MainActors[2];
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        int currentYear = DateTime.Now.Year;
        if (string.IsNullOrWhiteSpace(TitleEntry.Text)
            ||string.IsNullOrWhiteSpace(GenreEntry.Text)
            || !int.TryParse(YearEntry.Text, out int year)
            || year < 1800
            || year > currentYear)
        {
            await DisplayAlertAsync("Помилка", $"Введіть назву, жанр та рік від 1800 до {currentYear}.", "Oк");
            return;
        }

        MovieItem.Title = TitleEntry.Text.Trim();
        MovieItem.Genre = GenreEntry.Text.Trim();
        MovieItem.ReleaseYear = year;
        MovieItem.MainActors = new List<string>();

        if (!string.IsNullOrWhiteSpace(Actor1Entry.Text)) MovieItem.MainActors.Add(Actor1Entry.Text.Trim());
        if (!string.IsNullOrWhiteSpace(Actor2Entry.Text)) MovieItem.MainActors.Add(Actor2Entry.Text.Trim());
        if (!string.IsNullOrWhiteSpace(Actor3Entry.Text)) MovieItem.MainActors.Add(Actor3Entry.Text.Trim());

        IsSaved = true;
        await Navigation.PopModalAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        IsSaved = false;
        await Navigation.PopModalAsync();
    }
}
