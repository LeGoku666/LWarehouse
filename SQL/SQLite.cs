using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Collections;

namespace LWarehouse.SQL
{
    public class SQLite
    {
        private const string dataSource = @"Data Source=.\EDB.db";

        public static void Insert<T>(T tab)
        {
            if(tab is TabElement)
            {
                SQLInsert.InsertElement(dataSource, tab as TabElement);
            }
            else if (tab is TabLocation)
            {
                SQLInsert.InsertLocation(dataSource, tab as TabLocation);
            }
            else if (tab is TabWarehouse)
            {
                SQLInsert.InsertWarehouse(dataSource, tab as TabWarehouse);
            }
        }

        public static IList Select()
        {
           // SQLSelect select = new();
            IList<TabElement> list = SQLSelect.SelectElement(dataSource);

            return (IList)list;
        }

        //public void SelectElement(string dataSource)
        //{
        //    using var connection = new SqliteConnection(dataSource);
        //    connection.Open();
        //    var command = connection.CreateCommand();

        //    command.CommandText =
        //    @"
        //            SELECT *
        //            FROM element
        //        ";

        //    using var reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        TabElement element = new();
        //        element.Symbol = reader.GetString("symbol");
        //        element.Warehouse = reader.GetString("warehouse");
        //        element.Komponent = reader.GetString("komponent");
        //        element.Info = reader.GetString("info");

        //        if (!reader.IsDBNull("image"))
        //        {
        //            using Stream readStream = reader.GetStream("image");
        //            Stream str = new MemoryStream();
        //            _ = readStream.CopyToAsync(str);
        //        }
        //    }
        //}

        public static void UpdateElement() //TODO update rest
        {
            using var connection = new SqliteConnection(dataSource);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
            @"
                    UPDATE element
                    SET image = @image
                    WHERE symbol = '1540025-000'
                ";

            FileStream inputStream = new(@"Images\x.PNG", FileMode.Open);
            byte[] imageNew = new byte[inputStream.Length];
            inputStream.Read(imageNew, 0, imageNew.Length);
            inputStream.Close();

            command.Parameters.Add("@image", SqliteType.Blob, imageNew.Length).Value = imageNew;
            command.ExecuteNonQuery();
        }
    }
}
