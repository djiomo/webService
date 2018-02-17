using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartWcfService1
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "/Humans/?from={from}&to={to}")]
        List<Human> GetHumans(DateTime from, DateTime to);

        [OperationContract]
        [WebGet(UriTemplate = "/Humans/{id}")]
        Human GetHuman(string id);
        [OperationContract]
        // [WebGet(UriTemplate = "/Humans/AddHumain/?human={human}")]

        [WebInvoke(Method = "POST", UriTemplate = "/Humans/addHumain")]
        Human AddHuman(Human human);
        




        ////[OperationContract]
        ////[WebGet(UriTemplate = "/Humans/{id}")]
        //Human AddHuman(string FisrtName, string lastName);
        //Human DeleteHuman(string FisrtName, string lastName);
    }


    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    [DataContract]
    public class Human
    {

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime BirthDate { get; set; }

       // public IEnumerable<Human> Children { get; set; }


        public Human()
        {
           // Children = new List<Human>() { new Human() { FirstName = "enfant 1", LastName = "toto" }, new Human() { FirstName = "enfant 2", LastName = "titi" } };
        }

       
    }
}
