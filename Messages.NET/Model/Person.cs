﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using SQLite;

namespace Messages.NET
{
    [Table("Person")]
    public class Person
    {
        #region Attributes

        private String _id;
        private String _nickname;
        private Nullable<DateTime> _lastSeen;
        private String _pubKey;
        private String _privKey;

        #endregion

        #region Properties

        [PrimaryKey]
        public String Id
        {
            get => _id;
        }

        [MaxLength(50), Unique]
        public String Nickname
        {
            get => _nickname;
            set => _nickname = value;
        }

        public Nullable<DateTime> LastSeen
        {
            get => _lastSeen;
            set => _lastSeen = value;
        }

        public String PubKey
        {
            get => _pubKey;
            set => _pubKey = value;
        }

        public String PrivKey
        {
            get => _privKey;
            set => _privKey = value;
        }

        public ObservableCollection<Message> Messages(Person person)
        {
            IEnumerable<Message> authorQuery =
                   from message in ListSingleton.Instance.Messages
                   where message.Author == this && message.Receiver == person || message.Receiver == this && message.Author == person
                   select message;
            return new ObservableCollection<Message>(authorQuery.ToList());
        }

        #endregion

        public Person()
        {
            _id = System.Guid.NewGuid().ToString();
            _nickname = "";
            _lastSeen = null;
            
        }

        public Person(String nickname, Nullable<DateTime> lastSeen)
        {
            _id = System.Guid.NewGuid().ToString();
            _nickname = nickname;
            _lastSeen = lastSeen;
            _pubKey = generatePubKey();
            _privKey = generatePrivKey(); ;
        }

        public override string ToString()
        {
            string message;
            message = "ID : " + _id;
            message += "\nNickname : " + _nickname;
            message += "\nLast seen : " + _lastSeen;

            return message;
        }

        private string generatePubKey()
        {
            return "pubKey";
        }

        private string generatePrivKey()
        {
            return "privKey";
        }

    }
}
