
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace MauiApp_Archive
{
    public partial class MainPage : ContentPage
    {
        private readonly XMLSearchContext _context = new();
        private string _xmlPath = "";
        private string _xslPath = "";
        private string _xmlText = "";
        private List<SelectableItem> _availableMatTypes = new();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                _xmlPath = Path.Combine(FileSystem.AppDataDirectory, "archive.xml");
                _xslPath = Path.Combine(FileSystem.AppDataDirectory, "archive.xsl");

                using (var stream = await FileSystem.OpenAppPackageFileAsync("archive.xml"))
                using (var reader = new StreamReader(stream))
                {
                    _xmlText = await reader.ReadToEndAsync();
                    await File.WriteAllTextAsync(_xmlPath, _xmlText);
                }
                using (var stream = await FileSystem.OpenAppPackageFileAsync("archive.xsl"))
                using (var reader = new StreamReader(stream))
                {
                    await File.WriteAllTextAsync(_xslPath, await reader.ReadToEndAsync());
                }

                var doc = XDocument.Parse(_xmlText);
                AuthorPicker.ItemsSource = doc.Descendants("author")
                    .Select(x => (string?)x.Attribute("AU_NAME"))
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Distinct()
                    .ToList();

                DepartmentPicker.ItemsSource = doc.Descendants("book")
                    .Select(x => (string?)x.Attribute("BK_DEPARTMENT"))
                    .Where(x => !string.IsNullOrEmpty(x))
                    .Distinct()
                    .ToList();

                _availableMatTypes = doc.Descendants("book")
                    .Select(x => (string?)x.Attribute("BK_TYPE"))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Distinct()
                    .Select(x => new SelectableItem
                    {
                        Name = x!,
                        IsSelected = false
                    })
                    .ToList();

                MaterialTypesCollection.ItemsSource = _availableMatTypes;
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка завантаження", ex.Message, "OK");
            }
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            if (RbLinq.IsChecked)
                _context.SetStrategy(new LINQ_XML());
            else if (RbDom.IsChecked)
                _context.SetStrategy(new DOM_API());
            else if (RbSax.IsChecked)
                _context.SetStrategy(new SAX_API());

            var selectedMatTypes = _availableMatTypes
                .Where(item => item.IsSelected)
                .Select(item => item.Name)
                .ToList();

            var filter = new SearchFilter
            {
                Author = AuthorPicker.SelectedItem as string,
                Department = DepartmentPicker.SelectedItem as string,
                MaterialTypes = selectedMatTypes,
                Keyword = KeywordEntry.Text?.Trim()
            };

            var results = _context.ExecuteSearch(_xmlText, filter);
            ResultsCollection.ItemsSource = results;

            if (results.Count == 0)
            {
                DisplayAlertAsync("Результат", "За вказаними критеріями нічого не знайдено.", "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            AuthorPicker.SelectedItem = null;
            DepartmentPicker.SelectedItem = null;
            KeywordEntry.Text = string.Empty;

            foreach (var item in _availableMatTypes)
            {
                item.IsSelected = false;
            }

            MaterialTypesCollection.ItemsSource = null;
            MaterialTypesCollection.ItemsSource = _availableMatTypes;
            ResultsCollection.ItemsSource = null;
        }

        private async void OnTransformClicked(object sender, EventArgs e)
        {
            try
            {
                string htmlPath = Path.Combine(FileSystem.AppDataDirectory, "archive.html");
                XsltHelper.Transform(_xmlPath, _xslPath, htmlPath);
                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(htmlPath)
                });
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Помилка XSLT", ex.Message, "OK");
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlertAsync("Підтвердження", "Чи дійсно ви хочете завершити роботу з програмою?", "Так", "Ні");
            if (confirm)
            {
                Application.Current?.Quit();
            }
        }
    }
}

