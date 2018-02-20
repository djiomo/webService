using System;
using System.Windows;
using System.Web.Services;
using System.Web.Services.Description;
using System.CodeDom;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using System.Net;

namespace SmartWCFClient.WpfApp
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentDirectory = System.IO.Directory.GetCurrentDirectory();
                Console.WriteLine(currentDirectory);
                Uri webServiceUrl = new Uri(ComboBoxURL.Text.ToString());
                ComboBoxURL.Items.Add(webServiceUrl);
                Console.WriteLine("URL valide");
                WebClient wc = new WebClient();
                String path = "ws2.xml";
                wc.DownloadFile(webServiceUrl,path);
                Console.WriteLine("Téléchargement valide");
                ServiceDescription wsdlDescription = ServiceDescription.Read(path);
                Console.WriteLine("1");
                ServiceDescriptionImporter wsdlImporter = new ServiceDescriptionImporter();
                Console.WriteLine("2");
                //Vérifier si le protocole d'accès au webservice est Soap12
                wsdlImporter.ProtocolName = "Soap12";
                Console.WriteLine("3");
                wsdlImporter.AddServiceDescription(wsdlDescription, null, null);
                Console.WriteLine("4");
                wsdlImporter.Style = ServiceDescriptionImportStyle.Server;
                Console.WriteLine("5");
                wsdlImporter.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;
                Console.WriteLine("6");
                CodeNamespace codeNamespace = new CodeNamespace();
                Console.WriteLine("7");
                CodeCompileUnit codeUnit = new CodeCompileUnit();
                Console.WriteLine("8");
                codeUnit.Namespaces.Add(codeNamespace);
                Console.WriteLine("9");
                ServiceDescriptionImportWarnings importWarnings = wsdlImporter.Import(codeNamespace, codeUnit);
                Console.WriteLine("10");

                if (importWarnings ==0)
                {
                    Console.WriteLine("11");
                    StringBuilder stringBuilder = new StringBuilder();
                    Console.WriteLine("12");
                    StringWriter stringWriter = new StringWriter(stringBuilder);
                    Console.WriteLine("13");
                    CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                    Console.WriteLine("14");
                    provider.GenerateCodeFromCompileUnit(codeUnit, stringWriter, new CodeGeneratorOptions());
                    Console.WriteLine("15");
                    stringWriter.Close();
                    Console.WriteLine("16");
                    //Indiquer le chemin de sortie du ws genere en local
                    File.WriteAllText("Myrandomservice.cs", stringBuilder.ToString(), Encoding.UTF8);
                    Console.WriteLine("17");
                }
                else
                {
                    Console.WriteLine(importWarnings);
                }
            }

            catch (UriFormatException exception)
            { System.Windows.Forms.MessageBox.Show("URL invalide. Veuillez entrer une adresse valide"); }
        }
    }
}
