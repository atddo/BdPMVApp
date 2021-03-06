﻿using BdP_MV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BdP_MV.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BdP_MV.View.LoginForms
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



        }
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
         //   await Navigation.PushAsync(new SignUpPage());
        }

        async void OnPWLostButtonClicked(object sender, EventArgs e)
        {
            
            await Navigation.PushAsync(new ForgotPW(ViewModel.mainc));

        }
        async void OnLoginButtonClicked(object sender, EventArgs e)
        {

            IsBusy = true;
            
            Boolean isValid = false;
            lbl_Akt_Aktion.Text = "Einloggen";
            String response = await Task.Run(async () => await ViewModel.CheckLogin(usernameEntry.Text,passwordEntry.Text)); 
            if (String.IsNullOrEmpty(response))
            {
                isValid = true;
            }
            if (isValid)
            {
                lbl_Akt_Aktion.Text = "Gruppen laden";
                await ViewModel.LoadGroups();
                App.Current.MainPage = new MasterDetail.MasterDetail_Main (ViewModel.mainc);
            }
            else
            {
                await DisplayAlert("Fehler bei der Anmeldung", response, "OK");
                if (!string.Equals(response, "Es ist ein Fehler mit deiner Internetverbindung aufgetreten."))
{
                    passwordEntry.Text = string.Empty;
                }
            }
            IsBusy = false;
        }

     
    }
}