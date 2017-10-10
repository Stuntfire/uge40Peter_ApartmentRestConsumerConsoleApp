using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Net.Http.Headers;

namespace uge40Peter_ApartmentRestConsumerConsoleApp
{
    class Program
    {
        private static string ApartmentServiceUri = "http://localhost:55216/ApartmentService.svc/apartment/";

        #region Henter alle Apartments.
        private static async Task<IList<Apartment>> GetApertmentsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(ApartmentServiceUri);

                IList<Apartment> apartments = JsonConvert.DeserializeObject<IList<Apartment>>(content);
                return apartments;
            }
        }
        #endregion

        #region Henter en Apartment via Postnummer.
        private static async Task<IList<Apartment>> GetApartmentByPostalcode(int postalcode)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(ApartmentServiceUri + "postal/" + postalcode);

                IList<Apartment> apartmentByPostalcode = JsonConvert.DeserializeObject<IList<Apartment>>(content);
                return apartmentByPostalcode;
            }
        }
        #endregion

        #region Henter en Apartment via By.
        private static async Task<IList<Apartment>> GetApartmentByLocation(string location)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(ApartmentServiceUri + "location/" + location);

                IList<Apartment> apartmentByPostalcode = JsonConvert.DeserializeObject<IList<Apartment>>(content);
                return apartmentByPostalcode;
            }
        }
        #endregion

        #region Opretter en ny Apartment.
        private static async void PostApartment(Apartment apartment)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonApartment = JsonConvert.SerializeObject(apartment);

                client.DefaultRequestHeaders.Clear();

                var response = await client.PostAsync(ApartmentServiceUri, new StringContent(jsonApartment, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Din nye lejlighed er oprette...");
                }
                else
                {
                    Console.WriteLine(response.StatusCode + "Fejl, en lejlighed med samme id findes allerede...");
                }
            }
        }
        #endregion

        #region Opdatere en Apartment.
        private static async void PutApartment(Apartment apartment)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync<Apartment>(ApartmentServiceUri, apartment);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Lejligheden er opdateret...");
                }
                else
                {
                    Console.WriteLine(response.StatusCode + ". Fejl, lejligheden blev ikke opdateret...");
                }
            }
        }
        #endregion

        #region Slette en Apartment.
        private static async void DeleteApartment(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(ApartmentServiceUri + id);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.StatusCode + ". Lejligheden er slettet...");
                }
                else
                {
                    Console.WriteLine(response.StatusCode + ". Fejl, lejligheden blev ikke slettet...");
                }
            }
        }
        #endregion

        static void Main(string[] args)
        {
            //her opretter vi en ny Apartment: (Price, Location, Postalcode, Size, NoRoom, WashingMachine, Dishwasher)
            //Apartment newApartment = new Apartment() { Price = 10500, Location = "Paris", Postalcode = 4500, Size = 80, NoRoom = 3, WashingMachine = true, Dishwasher = true };

            //Task.Run(() => PostApartment(newApartment));

            //her opdaterer vi en eksisterende Apartment: (Price, Location, Postalcode, Size, NoRoom, WashingMachine, Dishwasher)
            //Apartment updateApartment = new Apartment() { Id = 13, Price = 10500, Location = "Hamburg", Postalcode = 4500, Size = 80, NoRoom = 3, WashingMachine = true, Dishwasher = true };

            //Task.Run(() => PutApartment(updateApartment));

            //Apartment deleteApartment = new Apartment() { Id = 13, Price = 10500, Location = "Hamburg", Postalcode = 4500, Size = 80, NoRoom = 3, WashingMachine = true, Dishwasher = true };

            Task.Run(() => DeleteApartment(12));

            //her henter vi alle Apartments på listen:
            var list = Task.Run(async () => await GetApertmentsAsync());

            foreach (var apartment in list.Result)
            {
                Console.WriteLine(apartment);
            }

            //her henter vi Apartments ud fra Postnummer:
            var taskPostal = Task.Run(async () => await GetApartmentByPostalcode(4200));
            foreach (var postal in taskPostal.Result)
            {
                Console.WriteLine($"\nResultatet ud fra postnummer: \n\t{postal}");
            }

            //her henter vi Apartments ud fra By:
            var taskLocation = Task.Run(async () => await GetApartmentByLocation("Aarhus"));
            foreach (var location in taskLocation.Result)
            {
                Console.WriteLine($"\nResultatet ud fra by: \n\t{location}");
            }


        }
    }
}
