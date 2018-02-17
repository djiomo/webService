using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartWcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        private List<Human> humans = new List<Human>();

        public Service1()
        {
            humans.Add(new Human() { FirstName = "Simon", LastName = "QUÉMÉNEUR", BirthDate = new DateTime(1981, 3, 23), Id = "1" });
            humans.Add(new Human() { FirstName = "Loik", LastName = "DJIOMO", BirthDate = new DateTime(1995, 3, 3), Id = "2" });
            humans.Add(new Human() { FirstName = "Thibault", LastName = "SALAUN", BirthDate = new DateTime(1994, 12, 10), Id = "3" });
        }

        public Human AddHuman(Human human)
        {
           return human;
        }

        public Human GetHuman(string id)
        {
            return humans.Where(h => h.Id == id).SingleOrDefault();
        }

        public List<Human> GetHumans(DateTime fromDate, DateTime toDate)
        {
            return humans.Where(h => fromDate != null ? h.BirthDate >= fromDate : false && toDate != null ? h.BirthDate <= toDate : false).ToList();
        }

    }
}
