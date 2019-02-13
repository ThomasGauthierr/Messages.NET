using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.NET
{
    public class Message
    {
        private String _id;
        private String _content;
        private Person _author;
        private Person _receiver;
        private DateTime _date;

        public String id { get => _id; }

        public String content
        {
            get => _content;
            set => _content = value;
        }

        public Person author
        {
            get => _author;
            set => _author = value;
        }

        public Person receiver
        {
            get => _receiver;
            set => _receiver = value;
        }

        public DateTime date
        {
            get => _date;
            set => _date = value;
        }

        public Message(String message, Person author, Person target)
        {
            _id = System.Guid.NewGuid().ToString();
            _content = message;
            _author = author;
            _receiver = target;
            _date = DateTime.Now;
        }

    }
}
