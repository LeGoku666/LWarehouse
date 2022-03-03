using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LWarehouse.SQL
{
    class SQLSelect
    {
        public static List<TabElement> SelectElement(string dataSource)
        {
            List<TabElement> list = new();

            using var connection = new SqliteConnection(dataSource);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText =
            @"
                    SELECT *
                    FROM element
                ";

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                TabElement element = new();
                element.Symbol = reader.GetString("symbol");
                element.Warehouse = reader.GetString("warehouse");
                element.Komponent = reader.GetString("komponent");
                element.Info = reader.GetString("info");

                if (!reader.IsDBNull("image"))
                {
                    using Stream readStream = reader.GetStream("image");
                    //Stream str = new MemoryStream();
                    //_ = readStream.CopyToAsync(str);

                    using MemoryStream ms = new();
                    _ = readStream.CopyToAsync(ms);
                    byte[] image = ms.ToArray();
                    element.SetImageFromBytes = image;
                }

                list.Add(element);
            }

            return list;
        }
    }
}
