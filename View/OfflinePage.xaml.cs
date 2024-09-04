using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace Module02Exercise01.View
{
    public partial class OfflinePage : ContentPage
    {
        public OfflinePage()
        {
            InitializeComponent();
        }

        private async void OnRetryButtonClicked(object sender, EventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                bool isWebsiteReachable = await IsWebsiteReachable(App.TestUrl);

                if (isWebsiteReachable)
                {
                    await Shell.Current.GoToAsync("//EmployeePage");
                }
                else
                {
                    await DisplayAlert("Unable to Connect", "Unable to reach the server. Please check your internet connection and try again.", "OK");
                }
            }
            else
            {
                await DisplayAlert("No Internet", "Please check your internet connection and try again.", "OK");
            }
        }

        private async Task<bool> IsWebsiteReachable(string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
                    var response = await client.GetAsync(url);
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }

        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }
}
