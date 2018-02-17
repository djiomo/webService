using SmartWCFClient_2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartWCFClient_2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBlock1.Text="Pour ajouter un service :"+
                "\r\n"+".Sélectionnez \"Ajouter un service\" dans le menu Fichier ou le menu contextuel de \"Mes projets de service\"." +
                 "\r\n" + ".Entrez l'adresse des métadonnées du service dans la zone de saisie, puis cliquez sur \"OK\"." + "\r\n" + "\r\n" +
                "Pour tester une opération de service :" +
                 "\r\n" + "Double - cliquez sur l'opération à tester à partir de l'arborescence du volet gauche" +
                  "\r\n" + ".Une nouvelle page d'onglets s'affiche dans le volet gauche." +
                 "\r\n" + "Entrez la valeur des paramètres dans la zone Requête du volet droit." +
                  "\r\n" + ".Cliquez sur le bouton \"Appeler\"";
            ServiceReference1.Service1Client test = new ServiceReference1.Service1Client();         
            Uri uri = test.Endpoint.Address.Uri;
            TreeViewItem child = new TreeViewItem();
            child.Header = uri;
            WsTreeView.Items.Add(child);
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            foreach(var m in methods)
            {
                TreeViewItem it = new TreeViewItem();
                it.Header = m.Name;
                child.Items.Add(it);


                it.MouseDoubleClick += It_MouseDoubleClick;
            }
            Console.WriteLine(uri);
      
        }

        private void It_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            var tvi = sender as TreeViewItem;
            TabItem t = new TabItem();
            t.Header = tvi.Header;
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var method_final = methods.Where(m => m.Name.Equals(tvi.Header));
            var method = method_final.First();
            ParameterInfo[] parameters = method.GetParameters();
            List<MyParameterInfo> listMyParameterInfo = parameters.Select(p => new MyParameterInfo(p)).ToList();
            UserControl1 user = new UserControl1(listMyParameterInfo,tvi.Header.ToString());
            t.Content = user;
            tabControl1.Items.Add(t);
            tabControl1.Items.Refresh();

        }

        private void menuItemQuitter(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

      

    }
}
