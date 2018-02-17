using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Reflection;
using System.Windows.Controls.Primitives;
using SmartWCFClient_2.Classes;
using System.Collections;

namespace SmartWCFClient_2
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        private String NomMethod { get; set; }
        ServiceReference1.Service1Client test = new ServiceReference1.Service1Client();
        public UserControl1(List<MyParameterInfo> list1,String methodName)
        {
          
            InitializeComponent();
            dataGrid1.IsReadOnly = false;
            dataGrid1.ItemsSource = list1;
            var a= dataGrid1.Columns[1].GetCellContent(dataGrid1.Items[0]);
            NomMethod = methodName;

        }


        
       
        static public DataGridCell GetCell(DataGrid dg, int row, int column)
        {
            DataGridRow rowContainer = GetRow(dg, row);

            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);

                // try to get the cell but it may possibly be virtualized
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                if (cell == null)
                {
                    // now try to bring into view and retreive the cell
                    dg.ScrollIntoView(rowContainer, dg.Columns[column]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                }
                return cell;
            }
            return null;
        }

        static public DataGridRow GetRow(DataGrid dg, int index)
        {
            DataGridRow row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // may be virtualized, bring into view and try again
                dg.ScrollIntoView(dg.Items[index]);
                row = (DataGridRow)dg.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }

        static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            List<Final> listFinal = new List<Final>();
            
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var method_final = methods.Where(m => m.Name.Equals(NomMethod));
            var method = method_final.First();
            //Je détermine le nombre de ligne de ma datagrid,ainsi je détermine le nombre de paramètre de ma méthod
            int numLigne = dataGrid1.Items.Count;
            //je recupère le nombre paramètre de ma méthode
            ParameterInfo[] parameteres = method.GetParameters();
            var parametre = new object[numLigne];
            for (int i = 0; i < numLigne; i++)
            {
                //je recupére le type de ce paramètre
                Type T = parameteres[0].ParameterType;
                //je récupére la valeur de chaque cellule
                DataGridCell cell = GetCell(dataGrid1, i, 1);
                var cellcontent = (TextBlock)cell.Content;
                string cellValue = cellcontent.Text;
                //je convertis cellValue en T
                var t = Convert.ChangeType(cellValue, T);
                parametre[i] = t;
            }       
            object result = method.Invoke(test, parametre);
            try
            {
                if (result != null)
                {
                    if (result.GetType().IsArray)
                    {
                        
                        IList collection = (IList)result;
                     
                        foreach ( var v in collection)
                        {
                            List<Response> listResponse = new List<Response>();

                            foreach (var prop in v.GetType().GetProperties())
                            {
                                Response r = new Response();
                                r.name = prop.Name;
                                r.valeur = prop.GetValue(v, null).ToString();
                                r.type = prop.PropertyType.ToString();
                                listResponse.Add(r);
                            }
                            listResponse.RemoveAt(0);
                            listFinal.Add(new Final()
                            {
                                result = "return",
                                responseList = listResponse
                            });
                      

                         }

                    }
                    else
                    {
                        List<Response> listResponse = new List<Response>();
                        foreach (var prop in result.GetType().GetProperties())
                        {
                            //Console.WriteLine("{0}={1}-{2}", prop.Name, prop.GetValue(result, null),prop.GetType().GetType().GetType());

                            Response r = new Response();
                            r.name = prop.Name;
                            r.valeur = prop.GetValue(result, null).ToString();
                            r.type = prop.PropertyType.ToString();
                            listResponse.Add(r);
                        }
                        listResponse.RemoveAt(0);
                        listFinal.Add(new Final()
                        {
                            result = "return",
                            responseList = listResponse
                        });

                        foreach (var item in listResponse)
                        {
                            Console.WriteLine("{0}={1}-{2}", item.name, item.valeur, item.type);
                        }
                    }

                }
                treeviewList.ItemsSource = listFinal;
               

            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }



        }
        
    }

    public class Final
    {
        public String result { get; set; }
        public List<Response> responseList { get; set; }
    }


    public class Response
    {
        public String name { get; set; }
        public String valeur { get; set; }
        public String type { get; set; }
    }



    public class CourseValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            MyParameterInfo course = (value as BindingGroup).Items[0] as MyParameterInfo;
            Type t2 = course.parameterInfo.ParameterType;
            object val;
            bool check = TryChangeType(course.ValueTest, t2, out val);    
            if (check)
            {
                return ValidationResult.ValidResult;
               
            }
            else
            {
                return new ValidationResult(false,
                   "valeur pas convertible,elle n'est du type "+t2.ToString());
            }
        }
        public bool TryChangeType(string value, Type conversionType, out object val)
        {
            try
            {
                val = Convert.ChangeType(value, conversionType);
                return true;
            }
            catch (Exception)
            {
                val = null;
                return false;
            }
        }

        /*
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MethodInfo[] methods = typeof(ServiceReference1.IService1).GetMethods();
            var method_final = methods.Where(m => m.Name.Equals(NomMethod));
            var method = method_final.First();
            //Je détermine le nombre de ligne de ma datagrid,ainsi je détermine le nombre de paramètre de ma méthod
            int numLigne = dataGrid1.Items.Count;
            //je recupère le nombre paramètre de ma méthode
            ParameterInfo[] parameteres = method.GetParameters();
            var parametre = new object[numLigne];
            for (int i = 0; i < numLigne; i++)
            {
                //je recupére le type de ce paramètre
                Type T = parameteres[0].ParameterType;
                //je récupére la valeur de chaque cellule
                DataGridCell cell = GetCell(dataGrid1, i, 1);
                var cellcontent = (TextBlock)cell.Content;
                string cellValue = cellcontent.Text;
                //je convertis cellValue en T
                var t = Convert.ChangeType(cellValue, T);
                parametre[i] = t;
            }
            object result = method.Invoke(test, parametre);
            List<Request> listDatagrid_2 = new List<Request>();
            try
            {
                if (result != null)
                {
                    foreach (var prop in result.GetType().GetProperties())
                    {
                        //Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(result, null));
                        Request r = new Request();
                        r.sortie = prop.GetValue(result, null);
                        listDatagrid_2.Add(r);
                    }
                    listDatagrid_2.RemoveAt(0);
                }
                foreach (var itemList in listDatagrid_2)
                {
                    Console.WriteLine(itemList.sortie);
                }
                dataGrid2.ItemsSource = listDatagrid_2;

            }
            catch (Exception)
            {
                Console.WriteLine("error");
            }
        }
    }*/





}



}
