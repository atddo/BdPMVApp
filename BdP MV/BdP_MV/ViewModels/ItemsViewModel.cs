using System.Linq;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;

namespace BdP_MV.ViewModel
{
    public class ItemsViewModel : BaseNavigationViewModel
    {
        public ItemsViewModel()
        {

            SubscribeToAddAcquaintanceMessages();

            SubscribeToUpdateAcquaintanceMessages();

            SubscribeToDeleteAcquaintanceMessages();

			SetDataSource();
        }

        // this is just a utility service that we're using in this demo app to mitigate some limitations of the iOS simulator



        Command _LoadAcquaintancesCommand;

        Command _RefreshAcquaintancesCommand;

        Command _NewAcquaintanceCommand;

        Command _ShowSettingsCommand;

		void SetDataSource()
		{
		}

     
        /// <summary>
        /// Command to load acquaintances
        /// </summary>
        public Command LoadAcquaintancesCommand
        {
            get { return _LoadAcquaintancesCommand ?? (_LoadAcquaintancesCommand = new Command(async () => await ExecuteLoadAcquaintancesCommand())); }
        }

        public async Task ExecuteLoadAcquaintancesCommand()
        {
            LoadAcquaintancesCommand.ChangeCanExecute();

			// set the data source on each load, because we don't know if the data source may have been updated between page loads
			SetDataSource();

           
            LoadAcquaintancesCommand.ChangeCanExecute();
        }

        public Command RefreshAcquaintancesCommand
        {
            get
            {
                return _RefreshAcquaintancesCommand ?? (_RefreshAcquaintancesCommand = new Command(async () => await ExecuteRefreshAcquaintancesCommandCommand()));
            }
        }

        async Task ExecuteRefreshAcquaintancesCommandCommand()
        {
            RefreshAcquaintancesCommand.ChangeCanExecute();

            await FetchAcquaintances();

            RefreshAcquaintancesCommand.ChangeCanExecute();
        }

        async Task FetchAcquaintances()
        {
            IsBusy = true;

          //  Acquaintances = new ObservableRangeCollection<Acquaintance>(await _DataSource.GetItems());

            // ensuring that this flag is reset
            //Settings.ClearImageCacheIsRequested = false;

            IsBusy = false;
        }

        /// <summary>
        /// Command to create new acquaintance
        /// </summary>
        public Command NewAcquaintanceCommand
        {
            get
            {
                return _NewAcquaintanceCommand ??
                    (_NewAcquaintanceCommand = new Command(async () => await ExecuteNewAcquaintanceCommand()));
            }
        }

        async Task ExecuteNewAcquaintanceCommand()
        {
            //await PushAsync(new AcquaintanceEditPage() { BindingContext = new AcquaintanceEditViewModel() });
        }

        /// <summary>
        /// Command to show settings
        /// </summary>
        public Command ShowSettingsCommand
        {
            get
            {
                return _ShowSettingsCommand ??
                    (_ShowSettingsCommand = new Command(async () => await ExecuteShowSettingsCommand()));
            }
        }

        async Task ExecuteShowSettingsCommand()
        {
            //var navPage = new NavigationPage(
                //new SettingsPage() { BindingContext = new SettingsViewModel() })
            //{
            //    BarBackgroundColor = Color.FromHex("547799")
           // };
            
            //avPage.BarTextColor = Color.White;

//            await PushModalAsync(navPage);
        }

        Command _DialNumberCommand;

        /// <summary>
        /// Command to dial acquaintance phone number
        /// </summary>
        public Command DialNumberCommand
        {
            get
            {
                return _DialNumberCommand ??
                (_DialNumberCommand = new Command((parameter) =>
                        ExecuteDialNumberCommand((string)parameter)));
            }
        }

        void ExecuteDialNumberCommand(string acquaintanceId)
        {
       
        }

        Command _MessageNumberCommand;

        /// <summary>
        /// Command to message acquaintance phone number
        /// </summary>
        public Command MessageNumberCommand
        {
            get
            {
                return _MessageNumberCommand ??
                (_MessageNumberCommand = new Command((parameter) =>
                        ExecuteMessageNumberCommand((string)parameter)));
            }
        }

        void ExecuteMessageNumberCommand(string acquaintanceId)
        {
        }

        Command _EmailCommand;

        /// <summary>
        /// Command to email acquaintance
        /// </summary>
        public Command EmailCommand
        {
            get
            {
                return _EmailCommand ??
                (_EmailCommand = new Command((parameter) =>
                        ExecuteEmailCommand((string)parameter)));
            }
        }

        void ExecuteEmailCommand(string acquaintanceId)
        {
          
        }

        /// <summary>
        /// Subscribes to "AddAcquaintance" messages
        /// </summary>
        void SubscribeToAddAcquaintanceMessages()
        {
         
        }

        /// <summary>
        /// Subscribes to "UpdateAcquaintance" messages
        /// </summary>
        void SubscribeToUpdateAcquaintanceMessages()
        {
        }

        /// <summary>
        /// Subscribes to "DeleteAcquaintance" messages
        /// </summary>
        void SubscribeToDeleteAcquaintanceMessages()
        {
         //   MessagingService.Current.Subscribe<Acquaintance>(MessageKeys.DeleteAcquaintance, async (service, acquaintance) =>
            {
                IsBusy = true;
                

                IsBusy = false;
            };
        }
    }
}
