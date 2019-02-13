using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;

namespace Messages.NET
{
    public class Person
    {
        private String _id;

        private String _nickName;

        private Nullable<DateTime> _lastSeen;

        private String _pubKey;

        private String _privKey;

        public String id
        {
            get => _id;
        }

        public String nickName
        {
            get => _nickName;
            set => _nickName = value;
        }

        public Nullable<DateTime> lastSeen
        {
            get => _lastSeen;
            set => _lastSeen = value;
        }

        public String pubKey
        {
            get => _pubKey;
            set => _pubKey = value;
        }

        public String privKey
        {
            get => _privKey;
            set => _privKey = value;
        }

        public ObservableCollection<Message> messages
        {
            get
            {
                //TODO : Modify request so it retrieves the message where the connected user and the selected user are the author/receiver
                IEnumerable<Message> authorQuery =
                    from message in ListSingleton.instance.messages
                    where message.author == this
                    select message;
                return new ObservableCollection<Message>(authorQuery.ToList());
            }
        }

        public Person(String nickname, Nullable<DateTime> lastSeen)
        {
            _id = System.Guid.NewGuid().ToString();
            _nickName = nickname;
            _lastSeen = lastSeen;
            _pubKey = "pubKey";
            _privKey = "privKey";
        }

        public override string ToString()
        {
            string message;
            message = "ID : " + _id;
            message += "\nNickname : " + _nickName;
            message += "\nLast seen : " + _lastSeen;

            return message;
        }

    }
}
