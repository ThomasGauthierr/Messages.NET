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
        #region Attributes

        private static ListSingleton _instance { get; set; }
        private ObservableCollection<Message> _messages;

        #endregion

        #region Properties
        public static ListSingleton Instance
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

        public ObservableCollection<Message> Messages
        {
            get => _messages;
            set => _messages = value;
        }

        private ListSingleton()
        {
            _messages = new ObservableCollection<Message>();
        }

        #endregion



    }
}
