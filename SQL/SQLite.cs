using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;

namespace LWarehouse.SQL
{
    public class SQLite
    {

        public void Select()
        {
            using var connection = new SqliteConnection(@"Data Source=.\EDB.db;");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                        @"
                    SELECT *
                    FROM Location
                ";
            //  command.Parameters.AddWithValue("$id", id);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var name = reader.GetString(0);

                Console.WriteLine($"Hello, {name}!");
                //return name;
            }
            //return null;
        }
    }
}
