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
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1(List<MyParameterInfo> listParamInfo)
        {
            InitializeComponent();
            DataGridRequete.IsReadOnly = false;
            DataGridRequete.ItemsSource = listParamInfo;
        }
    }
    public class MyParameterInfo
    {
        public ParameterInfo nomParametre { get; set; }
        public String valeurParametre { get; set; }
        public MyParameterInfo (ParameterInfo param)
        {
            nomParametre = param;
            valeurParametre = "2";
        }
    }
}
