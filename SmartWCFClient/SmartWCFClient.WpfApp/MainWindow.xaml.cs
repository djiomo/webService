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

namespace SmartWCFClient.WpfApp
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();

            foreach (var m in methods)
            {
                //Console.WriteLine(m.Name);
                MethodesListBox.Items.Add(m.Name);
                
                ParameterInfo[] listParam = m.GetParameters();
                foreach (var p in listParam)
                {
                    //Dynamique pour créer Label et TextBox
                    //LabelTestParam.Content = LabelTestParam.Content.ToString() + p;
                }
                //GetData
                //GetDataAsync
                //GetDataUsingDataContract
                //GetDataUsingDataContractAsync
            }

            var result = methods.Where(m => !m.Name.EndsWith("Async"));
            var maMethode = result.First();

            ParameterInfo[] param = maMethode.GetParameters();
            foreach (var p in param)
            {
                //Console.WriteLine(p);
            }
            object[] ob = new object[] { 5 };
            //pi.
            //String paramTest = "test";
            object objet = maMethode.Invoke(test, ob);



            //MethodInfo[] methodsOK = { };
            //foreach (var m in methods)
            //{
            //    string nom = m.Name;
            //    if(nom.Contains("Async"))
            //    {
            //        //oui
            //    } else
            //    {
            //        //non
            //        methodsOK[] = m;
            //    }
            //}




            using (var svc = new ServiceReference1.Service1Client())
            {
                LabelResult.Content = svc.GetData(2);
                //svc.GetDataUsingDataContract();
                //svc.GetDataUsingDataContractAsync(null);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    

        /*private void OnSelected(object sender, RoutedEventArgs e)
        {
            ListBoxItem lbi = e.Source as ListBoxItem;
            if(lbi != null)
            {

            }
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //code
        }

        private void OnSelected(object sender, SelectionChangedEventArgs e)
        {
            LabelTestParam.Content = null;
            ListBox lb = e.Source as ListBox;
            if(lb.SelectedItem != null)
            //if(MethodesListBox.SelectedItem != null)
            {
                ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
                //ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString()).GetParameters();
                ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(lb.SelectedItem.ToString()).GetParameters();
                foreach (var p in param)
                {
                    //Dynamique pour créer Label et TextBox
                    LabelTestParam.Content = LabelTestParam.Content+p.ToString();

                }
                Console.WriteLine(LabelTestParam.Content.ToString());
            }
            else
            {
                Console.WriteLine("item nul");
            }
        }
    }
}
