using System;
using System.Collections.Generic;
using RestLearning.Dtos;
using Xamarin.Forms;

namespace RestLearning
{
	public partial class UpdateUser : ContentPage
	{
		public UserDto user;

		public UpdateUser (UserDto user)
		{
			InitializeComponent ();
			this.user = user;
			this.FindByName<Entry>("txt_updateName").Text = user.Name;
			this.FindByName<Entry>("txt_updateAge").Text = user.Age.ToString();
		}
	}
}

