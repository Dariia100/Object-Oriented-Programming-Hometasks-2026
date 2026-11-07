using System.Collections.ObjectModel;
using System.Text.Json;

namespace MovieCatalogApp
{
    public partial class MainPage : ContentPage
    {
        private List<Movie> _movies = new();
        public ObservableCollection<Movie> DisplayedMovies { get; set; } = new();
        private string _filePath = string.Empty;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true 
        };

        public MainPage()
        {
            InitializeComponent();
            MoviesView.ItemsSource = DisplayedMovies;
            _movies = new List<Movie>();
            UpdateList(_movies);
        }

        private void UpdateList(IEnumerable<Movie> list)
        {
            DisplayedMovies.Clear();
            foreach (var item in list)
            {
                DisplayedMovies.Add(item);
            }
            UpdateGenrePicker();
        }

        private void UpdateGenrePicker()
        {
            var currSelected = GenrePicker.SelectedItem as string;
            var genres = _movies
                .Select(m => m.Genre)
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Distinct()
                .OrderBy(g => g)
                .ToList();
            GenrePicker.ItemsSource = genres;
            if (!string.IsNullOrEmpty(currSelected) && genres.Contains(currSelected))
            {
                 GenrePicker.SelectedItem = currSelected;
            }
        }

        private async void OnOpenJsonClicked(object sender, EventArgs e)
        {
            var file = await FilePicker.Default.PickAsync(new PickOptions { PickerTitle = "Оберіть JSON файл" });
            if (file != null)
            {
                _filePath = file.FullPath;
                using FileStream fs = new(_filePath, FileMode.Open, FileAccess.Read);
                _movies = await JsonSerializer.DeserializeAsync<List<Movie>>(fs, _jsonOptions) ?? new();
                UpdateList(_movies);
                StatusLabel.Text = $"Завантажено з {_filePath}";
            }
        }


        private async void OnSaveJsonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_filePath))
            {
                await DisplayAlertAsync("Увага", "Відкрийте JSON-файл.", "Oк");
                return;
            }
            try
            {
                using FileStream fs = new(_filePath, FileMode.Create, FileAccess.Write);
                await JsonSerializer.SerializeAsync(fs, _movies, _jsonOptions);
                StatusLabel.Text = $"Збережено: {_filePath}";
                await DisplayAlertAsync("Успіх", "Файл успішно збережено.", "Oк");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка", ex.Message, "Oк");
            }
        }


        private async void OnAddMovieClicked(object sender, EventArgs e)
        {
            var editPage = new EditPage();
            await Navigation.PushModalAsync(editPage);
            bool saved = await editPage.EditResult;
            if (saved)
            {
                if (_movies.Any())
                {
                    editPage.MovieItem.Id = _movies.Max(m => m.Id) + 1;
                }
                else
                {
                    editPage.MovieItem.Id = 1;
                }
                _movies.Add(editPage.MovieItem);
                UpdateList(_movies);
                StatusLabel.Text = "Фільм додано";
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is Movie selected)
            {
                var editPage = new EditPage(selected);
                await Navigation.PushModalAsync(editPage);
                bool saved = await editPage.EditResult;
                if (saved)
                {
                    int index = _movies.FindIndex(m => m.Id == selected.Id);
                    if (index >= 0)
                    {
                        _movies[index] = editPage.MovieItem;
                        UpdateList(_movies);
                        StatusLabel.Text = "Фільм відредаговано";
                    }
                }
            }
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {

            if (sender is Button btn && btn.CommandParameter is Movie movie)
            {
                _movies.Remove(movie);
                UpdateList(_movies);
            }
        }
        private void OnFilterClicked(object sender, EventArgs e)
        {
            var query = _movies.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(TitleFilterEntry.Text))
                query = query.Where(m => m.Title.Contains(TitleFilterEntry.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (GenrePicker.SelectedItem is string selectedGenre && !string.IsNullOrWhiteSpace(selectedGenre))
                query = query.Where(m => string.Equals(m.Genre, selectedGenre, StringComparison.OrdinalIgnoreCase));

            if (int.TryParse(MaxAgeFilterEntry.Text, out int maxAge))
                query = query.Where(m => m.Age <= maxAge);
            
            DisplayedMovies.Clear();
            foreach (var item  in query.ToList())
            {
                DisplayedMovies.Add(item);
            }
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            TitleFilterEntry.Text = string.Empty;
            GenrePicker.SelectedItem = null;
            MaxAgeFilterEntry.Text = string.Empty;
            UpdateList(_movies);
        }

        private async void OnAboutClicked(object sender, EventArgs e) => await Navigation.PushModalAsync(new AboutPage());
    }
}
