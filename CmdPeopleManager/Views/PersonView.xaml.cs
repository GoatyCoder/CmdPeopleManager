using CmdPeopleManager.Models;
using CmdPeopleManager.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            var dbContext = new PeopleDbContext();
            this.DataContext = new PersonListViewModel(dbContext);
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
