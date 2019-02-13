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

        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> messages
        {
            get => _messages;
            set
            {
                if (_messages != value)
                {
                    _messages = value;
                    this.NotifyPropertyChanged("messages");
                }
            }
        }

        private ObservableCollection<Message> _selectedMessages;
        
        public ObservableCollection<Message> selectedMessages
        {
            get => _selectedMessages;
            set
            {
                if (_selectedMessages != value)
                {
                    _selectedMessages = value;
                    this.NotifyPropertyChanged("selectedMessages");
                }
            }
        }

        public Window1()
        {

            _persons = new ObservableCollection<Person>();
            _messages = ListSingleton.instance.messages;
            _selectedMessages = new ObservableCollection<Message>();

            Person p1 = new Person("Bob", DateTime.Now);
            Person p2 = new Person("Alice", DateTime.Now);

            _persons.Add(p1);
            _persons.Add(p2);
            _persons.Add(new Person("Tom", DateTime.Now));
            _persons.Add(new Person("Sarah", DateTime.Now));
            _persons.Add(new Person("Fred", DateTime.Now));
            _persons.Add(new Person("Ashley", DateTime.Now));
            _persons.Add(new Person("Ted", DateTime.Now));
            _persons.Add(new Person("Sarah", DateTime.Now));

            messages.Add(new Message("bjr", p1,p2));

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
            int indexSelectedContact = ContactList.SelectedIndex;
            selectedMessages = _persons[indexSelectedContact].messages;
        }

        private void ListMessages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
