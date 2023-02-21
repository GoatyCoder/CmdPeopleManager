using CmdPeopleManager.Commands;
using CmdPeopleManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CmdPeopleManager.ViewModels
{
    public class PersonListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _personList;
        public ObservableCollection<Person> PersonList
        {
            get { return _personList; }
            set
            {
                _personList = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        private Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                _currentPerson = value;
                OnPropertyChanged();
            }
        }

        public PersonListViewModel()
        {
            PersonList = new ObservableCollection<Person>();
            AddPersonCommand = new RelayCommand(AddPerson, CanAddPerson);
            RemovePersonCommand = new RelayCommand(RemovePerson, CanRemovePerson);
            UpdatePersonCommand = new RelayCommand(UpdatePerson);
        }

        public RelayCommand AddPersonCommand { get; private set; }
        public ICommand RemovePersonCommand { get; private set; }
        public ICommand UpdatePersonCommand { get; private set; }

        private void AddPerson()
        {
            Person person = new Person { FirstName = _firstName, LastName = _lastName };
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
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
