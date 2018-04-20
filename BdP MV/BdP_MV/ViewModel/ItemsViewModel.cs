using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BdP_MV.Exceptions;
using BdP_MV.Model.Mitglied;
using BdP_MV.Model;
using BdP_MV.Services;
using MvvmHelpers;
using Xamarin.Forms;

namespace BdP_MV.ViewModel
{
    public class ItemsViewModel : BaseNavigationViewModel
    {
        public MainController mainC;
        public List<Gruppe> alleGruppen {get; set; }
        public Gruppe aktGruppe { get; set; }
        public List<Mitglied> ausgewaehlteMitglieder { get; set; }
        public ItemsViewModel(MainController mainCo)
        {
            mainC = mainCo;


          
            alleGruppen = mainC.groupControl.alleGruppen;
            ausgewaehlteMitglieder = new List<Mitglied>();
            Mitglied test = new Mitglied();
            test.ansprechname = "test";
            ausgewaehlteMitglieder.Add(test);
            


        }
        public async Task GruppenLaden()
        {
            
                IsBusy = true;
                await mainC.groupControl.AlleGruppenAbrufen(0);
                alleGruppen = mainC.groupControl.alleGruppen;
                IsBusy = false;
           
          
        }
        public async Task MitgliederAusGruppeLaden()
        {
            
            mainC.einsteillungen.aktuelleGruppe = aktGruppe.id;
            await Task.Run(async () => await mainC.mitgliederController.MitgliederAktualisierenByGroup());

            ausgewaehlteMitglieder = mainC.mitgliederController.AktiveMitglieder;
            
        }
        public async Task<ItemDetailViewModel> mitgliedDetailsVorladen(int idMitglied)
        {
            int idGruppe = aktGruppe.id;
            Task<List<Taetigkeit>> taetigkeitenLaden = mainC.mitgliederController.TaetigkeitenAbrufen(idMitglied);
            taetigkeitenLaden.Start();
            Task<List<Ausbildung>> ausbildungLaden = mainC.mitgliederController.AusbildungenAbrufen(idMitglied);
            ausbildungLaden.Start();
            Task<List<SGB8>> fuehrungszeugnisseLaden = mainC.mitgliederController.Sgb8Abrufen(idMitglied);
            fuehrungszeugnisseLaden.Start();
            Task<MitgliedDetails> mitgliederLaden = mainC.mitgliederController.MitgliedDetailsAbrufen(idMitglied, aktGruppe.id);
            mitgliederLaden.Start();
            MitgliedDetails mitgliedDetails = await mitgliederLaden;
            ItemDetailViewModel viewModelMitgliedDetails = new ItemDetailViewModel(mitgliedDetails, mainC);
            viewModelMitgliedDetails.sgb8 = await fuehrungszeugnisseLaden;
            viewModelMitgliedDetails.ausbildung = await ausbildungLaden;
            viewModelMitgliedDetails.taetigkeiten = await taetigkeitenLaden;
            return viewModelMitgliedDetails;




        }

    }
}

