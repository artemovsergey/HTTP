using System.Net.Http.Json;

namespace MAUIClient
{
    public partial class MainPage : ContentPage
    {
        static HttpClient? httpClient;

        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Подключение к API
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnConnectAPI(object sender, EventArgs e)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            httpClient = new HttpClient(socketsHandler);

            try
            {
                object? data = await httpClient.GetFromJsonAsync("https://localhost:7009/1", typeof(Person));
                if (data is Person person)
                {
                    DisplayAlert("Подключение", $"Name: {person.Name} ", "OK");
                    helloLabel.Text = $"Hello, {person.Name}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                DisplayAlert("Подключение", $"Ошибка: {ex.Message} ", "OK");
            }
        }

        private async void OnConnectAPIWifi(object sender, EventArgs e)
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(2)
            };
            httpClient = new HttpClient(socketsHandler);

            try
            {
                object? data = await httpClient.GetFromJsonAsync("http://192.168.1.5:7009/1", typeof(Person));
                if (data is Person person)
                {
                    DisplayAlert("Подключение", $"Name: {person.Name} ", "OK");
                    helloLabel.Text = $"Hello, {person.Name}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
                Console.WriteLine(ex.Message.ToString());
                DisplayAlert("Подключение", $"Ошибка: {ex.InnerException} ", "OK");
            }
        }
    }

    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}