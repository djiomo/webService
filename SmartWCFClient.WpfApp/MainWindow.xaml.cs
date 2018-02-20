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
            
            ServiceReference1.Service1Client test = new ServiceReference1.Service1Client();
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            Uri WebServiceUrl = test.Endpoint.Address.Uri;
            TreeViewItem wsurl = new TreeViewItem();
            wsurl.Header = WebServiceUrl;
            TreeViewWebService.Items.Add(wsurl);
            /*foreach (var m in methods)
            {
                MethodesListBox.Items.Add(m.Name);        
                ParameterInfo[] listParam = m.GetParameters();
                wsurl.Items.Add(m.Name);
            }*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LabelResult.Visibility = Visibility.Visible;
            //LabelMethodResult.Visibility = Visibility.Visible;
            ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
            var method = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString());
            ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(MethodesListBox.SelectedItem.ToString()).GetParameters();
            object[] givenParamInit = null;
            try
            {
                givenParamInit = new object[] { };
                int compteur=0;
                /*foreach (TextBox p in StackPanelMiddleCenter.Children)
                {
                    object param1 = Convert.ChangeType(p.Text, param[compteur].ParameterType);
                    object[] givenParam = new object[givenParamInit.Length + 1];
                    givenParamInit.CopyTo(givenParam, 0);
                    givenParam[givenParam.Length-1] = param1;
                    givenParamInit = givenParam;
                    compteur++;
                }*/
                object result = method.Invoke(test, givenParamInit);
                //LabelMethodResult.Content = result.ToString();
            }
            catch (Exception exc)
            {
                //LabelResult.Content = "Error";
                //LabelMethodResult.Content = exc.ToString();
            }
        }

        private void OnSelected(object sender, SelectionChangedEventArgs e)
        {
            //StackPanelMiddleLeft.Children.Clear();
            //StackPanelMiddleCenter.Children.Clear();
            ListBox lb = e.Source as ListBox;
            if(lb.SelectedItem != null)
            {
                ServiceReference1.IService1 test = new ServiceReference1.Service1Client();
                ParameterInfo[] param = typeof(ServiceReference1.IService1).GetMethod(lb.SelectedItem.ToString()).GetParameters();
                if (param.Length>0)
                {
                    foreach (var p in param)
                    {
                        Label labelParamType = new Label();
                        labelParamType.HorizontalAlignment = HorizontalAlignment.Center;
                        TextBox textBoxValue = new TextBox();
                        textBoxValue.Height = 25;
                        labelParamType.Content = p.ToString();
                        //StackPanelMiddleLeft.Children.Add(labelParamType);
                        //StackPanelMiddleCenter.Children.Add(textBoxValue);
                    }
                }
                else
                {
                    Label noParam = new Label();
                    noParam.Content = "Pas de paramètres pour cette fonction";
                    //StackPanelMiddleLeft.Children.Add(noParam);
                }
            }
            else
            {
                Console.WriteLine("item nul");
            }
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var tViewItem = sender as TreeViewItem;
            TabItem tItem = new TabItem();
            tItem.Header = tViewItem.Header;
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var methodeSelectionnee = methods.Where(m => m.Name.Equals(tViewItem.Header));
            var premiereMethode = methodeSelectionnee.First();
            ParameterInfo[] parametres = premiereMethode.GetParameters();
            List<MyParameterInfo> listMpi = parametres.Select(p => new MyParameterInfo(p)).ToList();
            UserControl user = new UserControl1(listMpi);

            tItem.Content = user;
            TabControlDemarrage.Items.Add(tItem);
            TabControlDemarrage.Items.Refresh();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window WindowURL = new Window1();
            WindowURL.ShowDialog();
        }

        private void URLOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TreeViewItemProjets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            Window WindowURL = new Window1();
            WindowURL.ShowDialog();
            GridTabControlDemarrage.Children.Clear();
           
        }
    }
}
