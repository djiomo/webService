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
            MethodInfo[]methods=typeof(ServiceReference1.IService1).GetMethods();
            var methods_final=methods.Where(m=>!m.Name.EndsWith("Async"));
            foreach(var m in methods_final)
            {
                listBox.Items.Add(m.Name);
            }
           
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textblock1.Text = "";
            string str = (string)listBox.SelectedItem;
            ServiceReference1.Service1Client test = new ServiceReference1.Service1Client(); 
            
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var method_final = methods.Where(m => m.Name.Equals(str));
            var method = method_final.First();
            ParameterInfo[] parameteres = method.GetParameters();
            int nbrPara = parameteres.Length;
            
            if (nbrPara == 1)
            {
                
                Type T = parameteres[0].ParameterType;
                var t = Convert.ChangeType(textbox1.Text, T);
                var parametre = new object[] { t };
                var result = method.Invoke(test, parametre);
                textblock1.Text = (string)result;
            }

            if (nbrPara == 2)
            {
                Type T1 = parameteres[0].ParameterType;
                Type T2 = parameteres[1].ParameterType;
                var t1 = Convert.ChangeType(textbox1.Text, T1);
                var t2 = Convert.ChangeType(textbox2.Text, T2);
                var parametre = new object[] { t1,t2 };
                var result = method.Invoke(test, parametre);
                textblock1.Text =  result.ToString();

            }
           
           
            
            

            
     
        }

       

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textblock1.Text = " ";
            int i = 0;
            string str = (string)listBox.SelectedItem;
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var method_final = methods.Where(m => m.Name.Contains(str));
            var method = method_final.First();
            ParameterInfo[] parametres = method.GetParameters();

            foreach (var p in parametres)
            {
                textblock1.Text = "param " + ": " + p.ToString() + "\r\n" + textblock1.Text + "\r\n";
                i = i + 1;
            }
        }
    }
}
