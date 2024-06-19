using MauiAppmovilRebe.Models;
using Newtonsoft.Json;


namespace MauiAppmovilRebe
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await LoadBurgersAsync();
        }

        private async Task LoadBurgersAsync()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:7123/api/");
                var response = await client.GetAsync("burger");
                if (response.IsSuccessStatusCode)
                {
                    var burgers = await response.Content.ReadAsStringAsync();
                    var burgersList = JsonConvert.DeserializeObject<List<BurgerRB>>(burgers);
                    listView.ItemsSource = burgersList;
                }
                else
                {
                    await DisplayAlert("Error", "No se pudieron cargar las hamburguesas.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
