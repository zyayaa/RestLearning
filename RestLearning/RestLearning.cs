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
using System.Collections.ObjectModel;
using RestLearning.Layouts;

namespace RestLearning {
    public class App : Application {

		ListView UsersList;
		Button Add;
		Entry Name;
		Entry Age;
		IToastNotificator notificator;
		ObservableCollection<UserDto> users;

        public App() {
            // The root page of your application
			InitializeMainPage ();
		}

		void InitializeMainPage ()
		{
			MainPage = new Main ();
			UsersList = MainPage.FindByName<ListView> ("UserList");
			Add = MainPage.FindByName<Button> ("btn_add");
			Name = MainPage.FindByName<Entry> ("txt_name");
			Age = MainPage.FindByName<Entry> ("txt_age");
			users = new ObservableCollection<UserDto> ();
			PopulateGrid ();
			Add.Clicked += OnAddClick;
		}

		void OnAddClick(object sender, EventArgs e){
			UserDto user = new UserDto();
			int age;

			bool valid = ValidateUser (Name.Text, Age.Text, out age);
			if (valid) {
				user.UserId = Guid.NewGuid();
				user.Name = Name.Text;
				user.Age = age;
				AddUser (user);
			}
		}

		private bool ValidateUser (string name, string ageString, out int age)
		{
			age = 0;
			notificator = DependencyService.Get<IToastNotificator> ();
			if (string.IsNullOrEmpty (name)) {
				return false;
				notificator.Notify (ToastNotificationType.Error, "Name must be provided", null, new TimeSpan (0, 0, 3));
			}
			else if (string.IsNullOrEmpty (ageString)) {
				notificator.Notify (ToastNotificationType.Error, "Age must be provided", null, new TimeSpan (0, 0, 3));
				return false;
			}
			else if (!int.TryParse (ageString, out age)) {
				notificator.Notify (ToastNotificationType.Error, "Age must be integer", null, new TimeSpan (0, 0, 3));
				return false;
			}
			else {
				return true;
			}
		}
			
		public async Task AddUser(UserDto user)
		{
			using(var httpClient = CreateClient()){

			var json = JsonConvert.SerializeObject (user);

			var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				response = await httpClient.PostAsync (string.Format("values/{0}/new", user.UserId), content);
				if (response != null && response.IsSuccessStatusCode) {
					PopulateGrid ();
					notificator = DependencyService.Get<IToastNotificator> ();
					await notificator.Notify (ToastNotificationType.Info, "User added", null, new TimeSpan (0, 0, 3));
				}
			}
		}

		void PopulateGrid ()
		{
			var response = GetUsers ();
			users = response.Result;
			UsersList.ItemsSource = users;
			
			UsersList.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>  {
				UserDto user = (UserDto)e.SelectedItem;
				if(user != null){						
					MainPage = new UpdateUser(user);
					Button update = MainPage.FindByName<Button>("btn_update");
					update.Clicked += OnUpdateClick;
				}
			};
		}

		private void OnUpdateClick (object sender, EventArgs e)
		{
			UserDto user = ((UpdateUser)MainPage).user;
			string Name = MainPage.FindByName<Entry>("txt_updateName").Text;
			string Age =MainPage.FindByName<Entry>("txt_updateAge").Text;
			int age = 0;
			bool valid = ValidateUser (Name, Age, out age);

			if (valid) {
				user.Name = Name;
				user.Age = age;
				UpdateUserRequest (user);
			}
		}

		public async Task UpdateUserRequest(UserDto user)
		{
			using (var httpClient = CreateClient ()) {
				var json = JsonConvert.SerializeObject (user);

				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				response = await httpClient.PutAsync (string.Format("values/{0}", user.UserId), content);
				if (response != null && response.IsSuccessStatusCode) {
					notificator = DependencyService.Get<IToastNotificator> ();
					notificator.Notify(ToastNotificationType.Info, "User updated", null, new TimeSpan(0,0,3));
					InitializeMainPage ();
				}
			}
		}

		public async Task<ObservableCollection<UserDto>> GetUsers ()
		{
			using (var httpClient = CreateClient ()) {
				var response = await httpClient.GetAsync ("values").ConfigureAwait(false);
				if (response.IsSuccessStatusCode) {
					var json = await response.Content.ReadAsStringAsync ().ConfigureAwait(false);
					return JsonConvert.DeserializeObject<ObservableCollection<UserDto>>(json);
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

