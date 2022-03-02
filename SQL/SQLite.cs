using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using System.IO;

namespace LWarehouse.SQL
{
    public class SQLite
    {
        private const string dataSource = @"Data Source=.\EDB.db";

        public void SelectElement()
        {
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
                    Stream str = new MemoryStream();
                    _ = readStream.CopyToAsync(str);
                }
            }
        }

        public void UpdateElement() //TODO update rest
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

        public string InsertElement(bool isImage = true)
        {
            TabElement element = new();
            element.Symbol = "1234565";
            element.Warehouse = "Technolog";
            element.Komponent = "Rezystor";
            element.Info = "alibaba D:";

            if (isImage)
            {
                FileStream inputStream = new(@"Images\x.PNG", FileMode.Open);

                element.Image = new byte[inputStream.Length];

                inputStream.Read(element.Image, 0, element.Image.Length);
                inputStream.Close();
            }

            using var connection = new SqliteConnection(dataSource);
            connection.Open();
            var command = connection.CreateCommand();

            string cmd = $"INSERT INTO element ({ReturnTableNameIfNotNull(element.Symbol, "symbol")}" +
                                                $"{ReturnTableNameIfNotNull(element.Warehouse, "warehouse")}" +
                                                $"{ReturnTableNameIfNotNull(element.Komponent, "komponent")}" +
                                                $"{ReturnTableNameIfNotNull(element.Image, "image")}" +
                                                $"{ReturnTableNameIfNotNull(element.Info, "info", false)})\n";

            cmd += $"VALUES ({ReturnValueIfNotNull(element.Symbol)}" +
                            $"{ReturnValueIfNotNull(element.Warehouse)}" +
                            $"{ReturnValueIfNotNull(element.Komponent)}" +
                            $"{ReturnValueIfNotNull(element.Image)}" +
                            $"{ReturnValueIfNotNull(element.Info, false)})";

            command.CommandText = cmd;

            if (element.Image != null)
                command.Parameters.Add("@image", SqliteType.Blob, element.Image.Length).Value = element.Image;

            command.ExecuteNonQuery();

            return "OK";
        }

        private string ReturnValueIfNotNull<T>(T value, bool adComma = true)
        {
            string newValue = "";

            if (value is byte[])
            {
                byte[] image = value as byte[];
                newValue = image.Length == 0 ? "" : "@image";
            }
            else if (value is string)
                newValue = value == null ? "" : $"'{value}'";

            if (adComma && (value is byte[] || value is string))
                newValue += ",";

            return newValue;
        }

        private string ReturnTableNameIfNotNull<T>(T value, string tab, bool adComma = true)
        {
            string newValue = "";

            if (value is byte[])
            {
                byte[] image = value as byte[];
                newValue = image.Length == 0 ? "" : tab;
            }
            else if (value is string)
                newValue = value == null ? "" : tab;

            if (adComma && (value is byte[] || value is string))
                newValue += ",";

            return newValue;
        }

        //public void Select()
        //{
        //    using var connection = new SqliteConnection(@"Data Source=.\EDB.db;");
        //    connection.Open();

        //    var command = connection.CreateCommand();
        //    command.CommandText =
        //                @"
        //            SELECT *
        //            FROM Location
        //        ";
        //    //  command.Parameters.AddWithValue("$id", id);

        //    using var reader = command.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        var name = reader.GetString(0);

        //        Console.WriteLine($"Hello, {name}!");
        //        //return name;
        //    }
        //    //return null;
        //}

    }
}
