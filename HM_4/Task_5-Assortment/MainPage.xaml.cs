using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Collections.ObjectModel;


namespace Task_5_Assortment
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Goods> GoodsList { get; set; }
        public MainPage()
        {
            InitializeComponent();
            GoodsList = new ObservableCollection<Goods> ();
            BindingContext = this;
        }

        private bool addProduct = true;
        private void OnAddClicked(object sender, EventArgs e)
        {
            if (addProduct) 
            {
                GoodsList.Add(new Products
                {
                    Name = "Продукт",
                    Description = "Опис",
                    Price = 0,
                    Country = "Країна",
                    Date = DateTime.Now,
                    ExpDate = DateTime.Now.AddDays(30),
                    Count = 1,
                    UnitOfMeasurement = "шт."
                });
            }
            else
            {
                GoodsList.Add(new Books
                {
                    Name = "Книга",
                    Description = "Опис",
                    Price = 0,
                    Country = "Країна",
                    Date = DateTime.Now,
                    Pages = 0,
                    PublishHouse = "Видавництво",
                    Authors = "Автор 1, Автор 2"
                });
            }
            addProduct = !addProduct;
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (GoodsList.Count > 0)
            {
                GoodsList.RemoveAt(GoodsList.Count - 1);
            }
        }
    }
}
