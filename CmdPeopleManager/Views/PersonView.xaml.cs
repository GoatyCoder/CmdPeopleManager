using CmdPeopleManager.Models;
using CmdPeopleManager.ViewModels;
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
using System.Windows.Shapes;

namespace CmdPeopleManager.Views
{
    /// <summary>
    /// Logica di interazione per PersonView.xaml
    /// </summary>
    public partial class PersonView : Window
    {
        public PersonView()
        {
            InitializeComponent();
            this.DataContext = new PersonListViewModel();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedPerson = e.AddedItems[0] as Person;
                firstNameTextBox.Text = selectedPerson.FirstName;
                lastNameTextBox.Text = selectedPerson.LastName;
            }
        }
    }
}
