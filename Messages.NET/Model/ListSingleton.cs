using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.NET
{
    class ListSingleton
    {
        private static ListSingleton _instance { get; set; }

        private ObservableCollection<Message> _messages;

        public static ListSingleton instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListSingleton();
                }
                return _instance;
            }
        }

        public ObservableCollection<Message> messages
        {
            get => _messages;
            set => _messages = value;
        }

        private ListSingleton()
        {
            _messages = new ObservableCollection<Message>();
        }



    }
}
