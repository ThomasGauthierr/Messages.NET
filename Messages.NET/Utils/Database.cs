using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.NET.Utils
{
    class Database
    {
        private static Database _instance;
        private SQLiteAsyncConnection db;
        private const string _dbName = "Database.db3";
        private const string _dbRepository = "Data";

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }

                return _instance;
            }
        }

        private Database()
        {
            string databasePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName , _dbRepository, _dbName);
            Console.WriteLine(databasePath);

            db = new SQLiteAsyncConnection(databasePath);
        }

        public async void createTablePerson()
        {
            await db.CreateTableAsync<Person>();

            Console.WriteLine("Table Person created");
        }

        public async void createPerson(Person person)
        {
            try {
                await db.InsertAsync(person);

                Console.WriteLine("Person with id {0} created", person.Id);
            } catch 
            {
                Console.WriteLine("Nickname {0} already used", person.Nickname);
            }

        }

        public async void createTableMessage()
        {
            await db.CreateTableAsync<Message>();

            Console.WriteLine("Table Message created");
        }

        public async void createMessage(Message message)
        {
            await db.InsertAsync(message);

            Console.WriteLine("Person with id {0} created", message.Id);

        }
    }


}
