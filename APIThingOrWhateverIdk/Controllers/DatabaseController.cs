using APIThingOrWhateverIdk.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace APIThingOrWhateverIdk.Controllers
{
    public class DatabaseController : Controller 
    {
        private const string TABLE_PERSONS = "persons";
        private const string TABLE_ITEMS = "items";

        private const string DATABASE_FILE = "database.db";

        private const string CONNECTION_STRING = "Data Source=" + DATABASE_FILE;

        public DatabaseController() { }

        public static int ExecuteWrite(string query, Dictionary<string, object>? args = null)
        {
            int numRowsAffected = -1;

            using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    if (args != null && args.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> entry in args)
                        {
                            command.Parameters.AddWithValue(entry.Key, entry.Value);
                        }
                    }
                    numRowsAffected = command.ExecuteNonQuery();
                }
            }

            return numRowsAffected;
        }

        public static SqliteDataReader ExecuteRead(string query, Dictionary<string, object>? args = null)
        {
            using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    if (args != null && args.Count > 0)
                    {
                        foreach (KeyValuePair<string, object> entry in args)
                        {
                            command.Parameters.AddWithValue(entry.Key, entry.Value);
                        }
                    }
                    
                    return command.ExecuteReader();
                }
            }
        }

        public static bool InitializeDatabaseTables()
        {
            try
            {
                const string createTablesQuery = "CREATE TABLE IF NOT EXISTS " + TABLE_PERSONS + " (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, age INTEGER, insert_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL);" +
                    "CREATE TABLE IF NOT EXISTS " + TABLE_ITEMS + " (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, type TEXT, price DOUBLE(4, 2), quantity INTEGER, insert_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL);";
                ExecuteWrite(createTablesQuery);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static bool ResetDatabase()
        {
            try
            {
                const string resetQuery = "DROP TABLE " + TABLE_ITEMS + "; DROP TABLE " + TABLE_PERSONS + ";";
                ExecuteWrite(resetQuery);

                InitializeDatabaseTables();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static List<PersonModel> GetAllPersons()
        {
            List<PersonModel> persons = null;
            const string getPersonsQuery = "SELECT * FROM " + TABLE_PERSONS + ";";

            using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(getPersonsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            persons = new List<PersonModel>();

                            while (reader.Read())
                            {
                                Int64 id = (Int64)reader["id"];
                                string name = (string)reader["name"];
                                Int64 age = (Int64)reader["age"];
                                DateTime insertDate = new DateTime();
                                DateTime.TryParse((string)reader["insert_date"], out insertDate);

                                PersonModel person = new PersonModel
                                {
                                    Id = id,
                                    Name = name,
                                    Age = age,
                                    InsertDate = insertDate
                                };

                                persons.Add(person);
                            }
                        }
                    }
                }
            }

            return persons;
        }

        public static List<ItemModel> GetAllItems()
        {
            List<ItemModel> items = null;
            const string getItemsQuery = "SELECT * FROM " + TABLE_ITEMS + ";";

            using (SqliteConnection connection = new SqliteConnection(CONNECTION_STRING))
            {
                connection.Open();
                using (SqliteCommand command = new SqliteCommand(getItemsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            items = new List<ItemModel>();

                            while (reader.Read())
                            {
                                Int64 id = (Int64)reader["id"];
                                string name = (string)reader["name"];
                                string type = (string)reader["type"];
                                Double price = (Double)reader["price"];
                                Int64 quantity = (Int64)reader["quantity"];
                                DateTime insertDate = new DateTime();
                                DateTime.TryParse((string)reader["insert_date"], out insertDate);

                                ItemModel item = new ItemModel
                                {
                                    Id = id,
                                    Name = name,
                                    Type = type,
                                    Price = price,
                                    Quantity = quantity,
                                    InsertDate = insertDate
                                };

                                items.Add(item);
                            }
                        }
                    }
                }
            }

            return items;
        }

        public static bool InsertPerson(List<PersonModel> persons)
        {
            try
            {
                const string insertPersonsQuery = "INSERT INTO " + TABLE_PERSONS + "(name, age) VALUES (@name, @age);";
                foreach (PersonModel person in persons) 
                {
                    Dictionary<string, object> args = new Dictionary<string, object>
                    {
                        { "@name", person.Name },
                        { "@age", person.Age }
                    };
                    ExecuteWrite(insertPersonsQuery, args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public static bool InsertItem(List<ItemModel> items)
        {
            try
            {
                const string insertItemsQuery = "INSERT INTO " + TABLE_ITEMS + "(name, type, price, quantity) VALUES (@name, @type, @price, @quantity);";
                foreach (ItemModel item in items)
                {
                    Dictionary<string, object> args = new Dictionary<string, object>
                    {
                        { "@name", item.Name },
                        { "@type", item.Type },
                        { "@price", item.Price },
                        { "@quantity", item.Quantity }
                    };
                    ExecuteWrite(insertItemsQuery, args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }
    }
}
