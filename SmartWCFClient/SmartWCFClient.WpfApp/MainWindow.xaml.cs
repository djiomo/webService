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
            //object objet = maMethode.Invoke(test, ob);



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
            ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
            var method = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString());
            ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString()).GetParameters();
            object[] givenParam = null;
            try
            {
                if (param.Length == 1)
                {
                    object param1 = Convert.ChangeType(TextBoxValeurParam1.Text, param[0].ParameterType);
                    givenParam = new object[] { param1 };
                }
                if (param.Length == 2)
                {
                    object param1 = Convert.ChangeType(TextBoxValeurParam1.Text, param[0].ParameterType);
                    object param2 = Convert.ChangeType(TextBoxValeurParam2.Text, param[1].ParameterType);
                    givenParam = new object[] { param1, param2 };
                }
                if (param.Length == 3)
                {
                    object param1 = Convert.ChangeType(TextBoxValeurParam1.Text, param[0].ParameterType);
                    object param2 = Convert.ChangeType(TextBoxValeurParam2.Text, param[1].ParameterType);
                    object param3 = Convert.ChangeType(TextBoxValeurParam3.Text, param[2].ParameterType);
                    givenParam = new object[] { param1, param2, param3 };
                }
                object result = method.Invoke(test, givenParam);
                LabelResult.Content = result.ToString();
            }
            catch (Exception exc)
            {
                LabelResult.Content = exc.ToString();
            }
        }

        private void OnSelected(object sender, SelectionChangedEventArgs e)
        {
            LabelParam1.Visibility = Visibility.Hidden;
            LabelParam2.Visibility = Visibility.Hidden;
            LabelParam3.Visibility = Visibility.Hidden;
            TextBoxValeurParam1.Visibility = Visibility.Hidden;
            TextBoxValeurParam2.Visibility = Visibility.Hidden;
            TextBoxValeurParam3.Visibility = Visibility.Hidden;

            LabelTestParam.Content = null;
            ListBox lb = e.Source as ListBox;
            if(lb.SelectedItem != null)
            //if(MethodesListBox.SelectedItem != null)
            {
                ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
                //ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString()).GetParameters();
                ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(lb.SelectedItem.ToString()).GetParameters();
                if (param.Length >= 1)
                {
                    LabelParam1.Visibility = Visibility.Visible;
                    LabelParam1.Content = param[0].ParameterType.ToString();
                    TextBoxValeurParam1.Visibility = Visibility.Visible;
                }
                if (param.Length >= 2)
                {
                    LabelParam2.Visibility = Visibility.Visible;
                    LabelParam2.Content = param[1].ParameterType.ToString();
                    TextBoxValeurParam2.Visibility = Visibility.Visible;
                }
                if (param.Length >= 3)
                {
                    LabelParam3.Visibility = Visibility.Visible;
                    LabelParam3.Content = param[2].ParameterType.ToString();
                    TextBoxValeurParam3.Visibility = Visibility.Visible;
                }
                
                /*foreach (var p in param)
                {
                    //Dynamique pour créer Label et TextBox
                    LabelTestParam.Content = LabelTestParam.Content+p.ToString();
                    dynamic labelX = new Label();
                    labelX.Content = p.ToString();
                    labelX.Height = 40;
                    labelX.Width = 60;
                    //this.AddChild(labelX);
                }*/
                //Console.WriteLine(LabelTestParam.Content.ToString());
            }
            else
            {
                Console.WriteLine("item nul");
            }
        }
    }
}
