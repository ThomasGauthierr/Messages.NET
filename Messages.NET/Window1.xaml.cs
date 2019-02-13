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

        private ObservableCollection<Message> _messages;

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

        public Window1()
        {
            ListSingleton singleton = ListSingleton.instance;

            _persons = singleton.persons;
            _messages = singleton.messages;

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

            singleton.messages.Add(new Message("bjr", p1,p2));

            InitializeComponent();

            this.DataContext = this;
        }

        public void ClickDelete(object sender, RoutedEventArgs e)
        {
            int indexSelectedContact = ContactList.SelectedIndex;

            if (indexSelectedContact == -1) return;

            //var binding = CollectionViewSource.GetDefaultView(ContactList.ItemsSource);

            persons.RemoveAt(indexSelectedContact);
            //ContactList.ItemsSource = persons;

            //binding.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }
}
