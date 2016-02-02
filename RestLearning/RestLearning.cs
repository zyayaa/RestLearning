using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestLearning.Dtos;
using System.Linq;

namespace RestLearning {
    public class App : Application {

		ListView UsersList;
        public App() {
            // The root page of your application
            MainPage = new Layouts.Main();
            UsersList = MainPage.FindByName<ListView> ("UserList");

			var response = GetValues ();
            
            List<UserDto> users = response.Result;
            UsersList.ItemsSource = from u in users
                                    select u.Name;
        }

		public async Task<List<UserDto>> GetValues ()
		{
			using (var httpClient = CreateClient ()) {
				var response = await httpClient.GetAsync ("values").ConfigureAwait(false);
				if (response.IsSuccessStatusCode) {
					var json = await response.Content.ReadAsStringAsync ().ConfigureAwait(false);
					return JsonConvert.DeserializeObject<List<UserDto>>(json);
				}
			}
			return null;
		}

		private const string ApiBaseAddress = "http://192.168.1.102:60374/";
		private HttpClient CreateClient ()
		{
			var httpClient = new HttpClient 
			{ 
				BaseAddress = new Uri(ApiBaseAddress)
			};

			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return httpClient;
		}
        

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}

