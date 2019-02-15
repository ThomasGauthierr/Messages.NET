using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.NET
{
    [Table("Message")]
    public class Message
    {
        #region Attributes

        private String _id;
        private String _content;
        private Person _author;
        private Person _receiver;
        private DateTime _date;

        #endregion

        #region Properties

        [PrimaryKey]
        public String Id { get => _id; }

        [MaxLength(200)]
        public String Content
        {
            get => _content;
            set => _content = value;
        }
        
        public Person Author
        {
            get => _author;
            set => _author = value;
        }
        
        public Person Receiver
        {
            get => _receiver;
            set => _receiver = value;
        }

        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        #endregion

        public Message()
        {
            _id = System.Guid.NewGuid().ToString();
            _date = DateTime.Now;
        }

        public Message(String content, Person author, Person target)
        {
            _id = System.Guid.NewGuid().ToString();
            _content = content;
            _author = author;
            _receiver = target;
            _date = DateTime.Now;
        }

    }
}
