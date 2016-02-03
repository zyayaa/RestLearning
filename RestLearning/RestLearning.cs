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
using Plugin.Toasts;
using System.Text;

namespace RestLearning {
    public class App : Application {

		ListView UsersList;
		Button Add;
		Entry Name;
		Entry Age;
		IToastNotificator notificator;
		List<UserDto> users;
        public App() {
            // The root page of your application
			MainPage = new Layouts.Main();
            UsersList = MainPage.FindByName<ListView> ("UserList");
			Add = MainPage.FindByName<Button> ("btn_add");
			Name = MainPage.FindByName<Entry> ("txt_name");
			Age = MainPage.FindByName<Entry> ("txt_age");
			users = new List<UserDto> ();

			PopulateGrid ();

			Add.Clicked += OnAddClick;	
		}

		void OnAddClick(object sender, EventArgs e){
			int age;
			notificator = DependencyService.Get<IToastNotificator> ();
			if(string.IsNullOrEmpty(Name.Text))
			{					
				notificator.Notify(ToastNotificationType.Error, "Name must be provided", null, new TimeSpan(0,0,3));
			}
			else if(string.IsNullOrEmpty(Age.Text))
			{					
				notificator.Notify(ToastNotificationType.Error, "Age must be provided", null, new TimeSpan(0,0,3));
			}
			else if(!int.TryParse(Age.Text, out age))
			{
				notificator.Notify(ToastNotificationType.Error, "Age must be integer", null, new TimeSpan(0,0,3));
			}
			else
			{
				var user = new UserDto{
					UserId = Guid.NewGuid(),
					Name=Name.Text,
					Age = age,
					AddedOn = DateTime.Now
				};
				AddUser(user);
			}
		}
			
		public async Task AddUser(UserDto user)
		{
			using(var httpClient = CreateClient()){

			var json = JsonConvert.SerializeObject (user);

			var content = new StringContent (json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = null;
				response = await httpClient.PostAsync (string.Format ("values/new"), content);
				PopulateGrid ();
				notificator = DependencyService.Get<IToastNotificator> ();
				notificator.Notify(ToastNotificationType.Info, "User added", null, new TimeSpan(0,0,3));
			}
		}

		void PopulateGrid ()
		{
			var response = GetUsers ();
			users = response.Result;
			UsersList.ItemsSource = from u in users
									select u.Name;
			UsersList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>  {
				string name = (string)e.SelectedItem;
				if (!string.IsNullOrEmpty (name)) {
					
					int age = (from u in users
					where u.Name == name
					select u.Age).FirstOrDefault ();
					notificator = DependencyService.Get<IToastNotificator> ();
					notificator.Notify (ToastNotificationType.Info, string.Format ("{0}: {1}", name, age.ToString ()), null, new TimeSpan (0, 0, 3));
					UsersList.SelectedItem = null;
				}
			};
		}

		public async Task<List<UserDto>> GetUsers ()
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

