using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace Messages.NET
{
    class ViewModel : INotifyPropertyChanged
    {
        #region Attributes & events

        private static Person connectedUserTest;
        private ObservableCollection<Person> _persons;
        private String _messageText;
        private Person _selectedContact;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                if (_persons != value)
                {
                    _persons = value;
                    this.NotifyPropertyChanged("Persons");
                }
            }
        }

        public ObservableCollection<Message> SelectedMessages
        {
            get {
                Console.WriteLine("roiehothirehtoirehtoiehteriothertio");
                if (_selectedContact == null) return null;
                return connectedUserTest.Messages(_selectedContact);
            }
        }

        public ObservableCollection<Message> Messages
        {
            get => ListSingleton.Instance.Messages;
            set
            {
                if (ListSingleton.Instance.Messages != value)
                {
                    ListSingleton.Instance.Messages = value;
                    this.NotifyPropertyChanged("Messages");
                }
            }
        }
        
        public String MessageText
        {
            get => _messageText;
            set
            {

            if (_messageText != value)
                {
                    _messageText = value;
                    this.NotifyPropertyChanged("MessageText");
                }
            }
        }

        public Person SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    this.NotifyPropertyChanged("SelectedContact");
                    this.NotifyPropertyChanged("SelectedMessages");
                }
            }
        }

        #endregion


        public ViewModel()
        {
            #region Objects initialization

            _persons = new ObservableCollection<Person>();

            Person p1 = new Person("Bob", DateTime.Now);
            Person p2 = new Person("Alice", DateTime.Now);
            Person p3 = new Person("Tom", DateTime.Now);

            //TODO: Remove later, for test purpose ONLY
            connectedUserTest = p1;

            _persons.Add(p1);
            _persons.Add(p2);
            _persons.Add(p3);
            _persons.Add(new Person("Sarah", DateTime.Now));
            _persons.Add(new Person("Fred", DateTime.Now));
            _persons.Add(new Person("Ashley", DateTime.Now));
            _persons.Add(new Person("Ted", DateTime.Now));
            _persons.Add(new Person("Sarah", DateTime.Now));

            Messages.Add(new Message("bjr", p1, p2));
            Messages.Add(new Message("slt", p2, p1));
            Messages.Add(new Message("bonsoir", p1, p3));
            Messages.Add(new Message("bjr", p3, p2));
            Messages.Add(new Message("ok", p3, p1));
            Messages.Add(new Message("t ki ?", p1, p2));
            Messages.Add(new Message("ca va ?", p1, p2));
            Messages.Add(new Message("xd", p1, p2));
            Messages.Add(new Message("mdr", p1, p2));
            Messages.Add(new Message("yoyoyo", p1, p3));

            #endregion

            #region Commands initialization

            SendMessageCommand = new HandleCommands(SendMessage);

            #endregion
        }

        

        private void contactListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int indexSelectedContact = ContactList.SelectedIndex;
            //selectedMessages = _persons[indexSelectedContact].messages(connectedUserTest);
        }

        public void ClickDelete(object sender, RoutedEventArgs e)
        {
            /*
            int indexSelectedContact = ContactList.SelectedIndex;

            if (indexSelectedContact == -1) return;

            persons.RemoveAt(indexSelectedContact);*/
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public bool NotifyPropertyChanged<T>(ref T variable, T valeur, string nomPropriete = null)
        {
            if (object.Equals(variable, valeur)) return false;

            variable = valeur;
            NotifyPropertyChanged(nomPropriete);
            return true;
        }


        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          /*  Person selectedContact = ContactList.SelectedItem as Person;
            if (selectedContact == null) return;
            selectedMessages = selectedContact.messages(connectedUserTest);*/
        }



        #region Commands
        public ICommand SendMessageCommand { get; set; }

        private void SendMessage()
        {
            MessageBox.Show(_messageText);

            Messages.Add(new Message(_messageText, connectedUserTest, null));

            _messageText = "";
            /*Person receiver = ContactList.SelectedItem as Person;
            String content = MessageBox.Text;

            Console.WriteLine(this.selectedMessages.Count);

            _messages.Add(new Message(content, connectedUserTest, receiver));
            MessageBox.Text = "";*/

        }

        #endregion

    }
}
