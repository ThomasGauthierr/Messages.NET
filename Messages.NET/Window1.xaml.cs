using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Messages.NET
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {

        //TODO: Remove later, for test purpose ONLY
        private static Person connectedUserTest = null;

        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> persons
        {
            get => _persons;
            set
            {
                if (_persons != value)
                {
                    _persons = value;
                    this.NotifyPropertyChanged("persons");
                }
            }
        }

        private ObservableCollection<Message> _messages
        {
            get => ListSingleton.instance.messages;
            set
            {
                if (ListSingleton.instance.messages != value)
                {
                    ListSingleton.instance.messages = value;
                    this.NotifyPropertyChanged("messages");
                }
            }
        }

        private ObservableCollection<Message> _selectedMessages
        {
            get
            {
                if (_selectedContact == null) return null;
                return _selectedContact.messages(connectedUserTest);
            }
            set
            {
                var messages = _selectedContact.messages(connectedUserTest);
                if (messages != value)
                {
                    messages = value;
                    this.NotifyPropertyChanged("selectedMessages");
                }
            }
        }

        public ObservableCollection<Message> selectedMessages
        {
            get
            {
                if (_selectedContact == null) return null;
                return _selectedMessages;
            }

            set
            {
                var messages = _selectedContact.messages(connectedUserTest);
                if (messages != value)
                {
                    messages = value;
                    this.NotifyPropertyChanged("selectedMessages");
                }
            }
        }

        private Person _selectedContact;

        public Window1()
        {

            _persons = new ObservableCollection<Person>();
            _selectedContact = null;

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

            _messages.Add(new Message("bjr", p1, p2));
            _messages.Add(new Message("slt", p2, p1));
            _messages.Add(new Message("bonsoir", p1, p3));
            _messages.Add(new Message("bjr", p3, p2));
            _messages.Add(new Message("ok", p3, p1));
            _messages.Add(new Message("t ki ?", p1, p2));
            _messages.Add(new Message("ca va ?", p1, p2));
            _messages.Add(new Message("xd", p1, p2));
            _messages.Add(new Message("mdr", p1, p2));
            _messages.Add(new Message("yoyoyo", p1, p3));

            InitializeComponent();

            this.DataContext = this;
        }

        public void ClickDelete(object sender, RoutedEventArgs e)
        {
            int indexSelectedContact = ContactList.SelectedIndex;

            if (indexSelectedContact == -1) return;

            persons.RemoveAt(indexSelectedContact);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedContact = ContactList.SelectedItem as Person;
            if (_selectedContact == null) return;

            Console.WriteLine(_selectedMessages.Count);
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            Person receiver = ContactList.SelectedItem as Person;
            String content = MessageBox.Text;
            

            _messages.Add(new Message(content, connectedUserTest, receiver));
            MessageBox.Text = "";

            foreach (Message m in selectedMessages)
            {
                Console.WriteLine(m.content);
            }

        }
    }
}
