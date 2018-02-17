using System;
using System.Windows;
using System.Web.Services;
using System.Web.Services.Description;
using System.CodeDom;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;

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
                Uri webServiceUrl = new Uri(ComboBoxURL.Text.ToString());
                ComboBoxURL.Items.Add(webServiceUrl);
                ServiceDescription wsdlDescription = ServiceDescription.Read(webServiceUrl.ToString());
                ServiceDescriptionImporter wsdlImporter = new ServiceDescriptionImporter();
                //Vérifier si le protocole d'accès au webservice est Soap12
                wsdlImporter.ProtocolName = "Soap12";
                wsdlImporter.AddServiceDescription(wsdlDescription, null, null);
                wsdlImporter.Style = ServiceDescriptionImportStyle.Server;
                wsdlImporter.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

                CodeNamespace codeNamespace = new CodeNamespace();
                CodeCompileUnit codeUnit = new CodeCompileUnit();
                codeUnit.Namespaces.Add(codeNamespace);
                ServiceDescriptionImportWarnings importWarnings = wsdlImporter.Import(codeNamespace, codeUnit);

                if (importWarnings ==0)
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    StringWriter stringWriter = new StringWriter();
                    CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                    provider.GenerateCodeFromCompileUnit(codeUnit, stringWriter, new CodeGeneratorOptions());
                    stringWriter.Close();
                    //Indiquer le chemin de sortie du ws genere en local
                    File.WriteAllText("generatedWSPath", stringBuilder.ToString(), Encoding.UTF8);
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
