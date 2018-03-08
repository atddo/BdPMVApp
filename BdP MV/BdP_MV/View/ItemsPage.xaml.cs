using System;
using Xamarin.Forms;
using BdP_MV.ViewModel;
using BdP_MV.Services;
using System.Collections.Generic;
using BdP_MV.Model;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using BdP_MV.Exceptions;
using System.Net;
using Xamarin.Forms.Internals;

namespace BdP_MV.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
	{
        protected ItemsViewModel viewModel;


        public ItemsPage(MainController mainCo)
        {


            InitializeComponent();

            viewModel = new ItemsViewModel(mainCo);

            BindingContext = viewModel;
        }


         protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Other code etc.
            try
            {
                await Task.Run(async () => await viewModel.GruppenLaden());
                Console.WriteLine(viewModel.alleGruppen.ToString());
                testpicker.ItemsSource = viewModel.alleGruppen;

            }

             catch (NewLoginException e)
            {
                await DisplayAlert("Fehler", "Deine Sitzung ist abgelaufen. Bitte logge dich neu in die App ein.", "OK");
                Navigation.InsertPageBefore(new Login(), this);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                await Navigation.PopAsync();
                
            }
            catch (WebException e)
            {
                await DisplayAlert("Fehler", "Fehler beim Herstellen der Internetverbinugn", "OK");


            }


            //etc, etc,
        }





    
        public async void thePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            //await DisplayAlert("Ausgew�hlte Gruppe", viewModel.aktGruppe.id.ToString(), "OK");//Method call every time when picker selection changed
            await Task.Run(async () => await this.viewModel.MitgliederAusGruppeLaden());
            MitgliedView.ItemsSource = viewModel.ausgewaehlteMitglieder;
            
            
        }

        /// <summary>
        /// The action to take when a list item is tapped.
        /// </summary>
        /// <param name="sender"> The sender.</param>
        /// <param name="e">The ItemTappedEventArgs</param>
        async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            IsBusy = true;
            Mitglied selected = (Mitglied)MitgliedView.SelectedItem;
            int selectedId = selected.id;
            MitgliedDetails mitDetails = await viewModel.mainC.mitgliederController.MitgliedDetailsAbrufen(selectedId);
            await Navigation.PushAsync(new ItemDetailPage( new ItemDetailViewModel(mitDetails, viewModel.mainC)));

            // prevents the list from displaying the navigated item as selected when navigating back to the list
            ((ListView)sender).SelectedItem = null;
            IsBusy=false;
        }

        /// <summary>
        /// The action to take when the + ToolbarItem is clicked on Android.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The EventArgs</param>
        void AndroidAddButtonClicked(object sender, EventArgs e)
		{
			
		}

		
    }
}
