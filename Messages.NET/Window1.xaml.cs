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
    public partial class Window1 : Window
    {

        public Window1()
        {

            InitializeComponent();

           // this.DataContext = this;
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
            /*
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }*/
        }

        private void ContactList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*  Person selectedContact = ContactList.SelectedItem as Person;
              if (selectedContact == null) return;
              selectedMessages = selectedContact.messages(connectedUserTest);*/
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            /*Person receiver = ContactList.SelectedItem as Person;
            String content = MessageBox.Text;

            Console.WriteLine(this.selectedMessages.Count);

            _messages.Add(new Message(content, connectedUserTest, receiver));
            MessageBox.Text = "";*/

        }

    }
}
