using CmdPeopleManager.Commands;
using CmdPeopleManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CmdPeopleManager.ViewModels
{
    public class PersonListViewModel : ViewModelBase
    {
        private readonly PeopleDbContext _context;
        private BindingList<Person> _personList;
        public BindingList<Person> PersonList
        {
            get { return _personList; }
            set { SetProperty(ref _personList, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { SetProperty(ref _currentPerson, value); }
        }

        public PersonListViewModel(PeopleDbContext context)
        {
            _context = context;
            PersonList = new BindingList<Person>(_context.People.ToList());

            AddPersonCommand = new RelayCommand(AddPerson, CanAddPerson);
            RemovePersonCommand = new RelayCommand(RemovePerson, CanRemovePerson);
            UpdatePersonCommand = new RelayCommand(UpdatePerson, CanUpdatePerson);
        }

        public RelayCommand AddPersonCommand { get; private set; }
        public ICommand RemovePersonCommand { get; private set; }
        public ICommand UpdatePersonCommand { get; private set; }

        private void AddPerson()
        {
            Person person = new Person { FirstName = _firstName, LastName = _lastName };

            _context.People.Add(person);
            _context.SaveChanges();
            PersonList.Add(person);

            FirstName = string.Empty;
            LastName = string.Empty;
        }

        private bool CanAddPerson()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
        }

        private void RemovePerson()
        {

            if (CurrentPerson != null)
            {
                _context.People.Remove(CurrentPerson);
                _context.SaveChanges();
                PersonList.Remove(CurrentPerson);

                CurrentPerson = null;

                FirstName = string.Empty;
                LastName = string.Empty;
            }
        }

        private bool CanRemovePerson()
        {
            return CurrentPerson != null;
        }

        private void UpdatePerson()
        {
            if (CurrentPerson != null)
            {
                CurrentPerson.FirstName = FirstName;
                CurrentPerson.LastName = LastName;

                _context.Update(CurrentPerson);
                _context.SaveChanges();

                int index = PersonList.IndexOf(CurrentPerson);
                if (index >= 0)
                {
                    PersonList.ResetItem(index);
                }
            }
        }

        private bool CanUpdatePerson()
        {
            return (CurrentPerson != null) && (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName));
        }
    }
}
