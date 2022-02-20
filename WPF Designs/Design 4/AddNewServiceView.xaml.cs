using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Design_4
{
    /// <summary>
    /// Interaction logic for AddNewServiceView.xaml
    /// </summary>
    public partial class AddNewServiceView : Page
    {
        

        public AddNewServiceView()
        {
            InitializeComponent();

            Functionality.SeedingListOfTemplates();

            foreach (string template in Functionality.templates)
            {
                listBoxOfTemplates.Items.Add(template);
            }

            listBoxOfTemplates.SelectedIndex = 0;
        }

        private void nextBtnAddNewService(object sender, MouseButtonEventArgs e)
        {
            if (listBoxOfTemplates.SelectedItem != null)
            {
            Functionality.templateOfChoice = listBoxOfTemplates.SelectedItem.ToString();
            }
            else
            {
                Functionality.templateOfChoice = Functionality.templates[0].ToString();
            }
        }
    }
}
