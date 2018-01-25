﻿using BdP_MV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BdP_MV.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        LoginViewModel ViewModel;

        public Login ()
		{
           

            InitializeComponent ();
            ViewModel = new LoginViewModel();
            BindingContext = ViewModel;
            Debug();

        }
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
         //   await Navigation.PushAsync(new SignUpPage());
        }
        private void Debug()
        {
            usernameEntry.Text = "API_User.WeisseRose_Tool";
            passwordEntry.Text = "apiapi123";
        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            IsBusy = true;
            Boolean isValid = false;
            String response = await Task.Run(async () => await ViewModel.CheckLogin(usernameEntry.Text,passwordEntry.Text)); 
            if (String.IsNullOrEmpty(response))
            {
                isValid = true;
            }
            if (isValid)
            {
                Navigation.InsertPageBefore(new ItemsPage(ViewModel.mainc), this);
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = response;
                passwordEntry.Text = string.Empty;
            }
            IsBusy = false;
        }

     
    }
}