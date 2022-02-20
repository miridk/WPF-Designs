using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
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

namespace Design_4
{
    /// <summary>
    /// Interaction logic for NameProject.xaml
    /// </summary>
    public partial class NameProject : Page
    {
        public NameProject()
        {
            InitializeComponent();
        }

        private void nextBtnNameProject(object sender, MouseButtonEventArgs e)
        {
           
            string templ = Functionality.templateOfChoice;
            string projectName = propertyNameTextBox.Text;
            projectName = projectName.Replace(" ", "-");
            string script = @"dotnet new --install c:\temp\webapi.template.dotnet6.01.1.0.1.nupkg"; //Works
            RunScript(script);
            script = @$"dotnet new {templ} -o c:\temp\Template\{projectName}"; //Works
            RunScript(script);
        }


        private static void RunScript(string script)
        {
            using (var powershell = PowerShell.Create())
            {
                powershell.AddScript(script);
                Collection<PSObject> results = powershell.Invoke();

                foreach (PSObject result in results)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
