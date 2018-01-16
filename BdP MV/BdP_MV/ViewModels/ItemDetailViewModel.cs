using System;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace BdP_MV.ViewModel
{
    public class AcquaintanceDetailViewModel : BaseNavigationViewModel
    {
        public AcquaintanceDetailViewModel(Mitglied mitglied)
        {
           
        }

        public Acquaintance Acquaintance { private set; get; }

        public bool HasEmailAddress => !string.IsNullOrWhiteSpace(Acquaintance?.Email);

        public bool HasPhoneNumber => !string.IsNullOrWhiteSpace(Acquaintance?.Phone);

        public bool HasAddress => !string.IsNullOrWhiteSpace(Acquaintance?.AddressString);

        // this is just a utility service that we're using in this demo app to mitigate some limitations of the iOS simulator
        readonly ICapabilityService _CapabilityService;

        readonly Geocoder _Geocoder;

        Command _EditAcquaintanceCommand;

        public Command EditAcquaintanceCommand
        {
            get
            {
                return _EditAcquaintanceCommand ??
                    (_EditAcquaintanceCommand = new Command(async () => await ExecuteEditAcquaintanceCommand()));
            }
        }

        async Task ExecuteEditAcquaintanceCommand()
        {
            await PushAsync(new AcquaintanceEditPage() { BindingContext = new AcquaintanceEditViewModel(Acquaintance) });
        }

        Command _DeleteAcquaintanceCommand;

        public Command DeleteAcquaintanceCommand => _DeleteAcquaintanceCommand ?? (_DeleteAcquaintanceCommand = new Command(ExecuteDeleteAcquaintanceCommand));

        void ExecuteDeleteAcquaintanceCommand()
        {
            
        }

        Command _DialNumberCommand;

        public Command DialNumberCommand => _DialNumberCommand ??
                                            (_DialNumberCommand = new Command(ExecuteDialNumberCommand));

        void ExecuteDialNumberCommand()
        {
            if (string.IsNullOrWhiteSpace(Acquaintance.Phone))
                return;

           
        }

        Command _MessageNumberCommand;

        public Command MessageNumberCommand => _MessageNumberCommand ??
                                               (_MessageNumberCommand = new Command(ExecuteMessageNumberCommand));

        void ExecuteMessageNumberCommand()
        {
            if (string.IsNullOrWhiteSpace(Acquaintance.Phone))
                return;

        
        }

        Command _EmailCommand;

        public Command EmailCommand => _EmailCommand ??
                                       (_EmailCommand = new Command(ExecuteEmailCommandCommand));

        void ExecuteEmailCommandCommand()
        {
            if (string.IsNullOrWhiteSpace(Acquaintance.Email))
                return;

         
        }

        Command _GetDirectionsCommand;

        public Command GetDirectionsCommand
        {
           // get
           // {
               // return _GetDirectionsCommand ??
                //(_GetDirectionsCommand = new Command(async () =>
                  //      await ExecuteGetDirectionsCommand()));
            //}
        }

       
       

        public void DisplayGeocodingError()
        {
            //MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
            //    {
            //        Title = "Geocoding Error", 
            //        Message = "Please make sure the address is valid, or that you have a network connection.",
            //        Cancel = "OK"
            //    });

            IsBusy = false;
        }

        


        static bool AddressBeginsWithNumber(string address)
        {
            return !string.IsNullOrWhiteSpace(address) && char.IsDigit(address.ToCharArray().First());
        }

       

        static int GetEndingIndexOfNumericPortionOfAddress(string address)
        {
            int endingIndex = 0;

            for (int i = 0; i < address.Length; i++)
            {
                if (char.IsDigit(address[i]))
                    endingIndex = i;
                else
                    break;
            }

            return endingIndex;
        }
    }
}
