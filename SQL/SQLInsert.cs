using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LWarehouse.SQL
{
    class SQLInsert
    {
        public string InsertElement(string dataSource, TabElement element)
        {
            //TabElement element = new();
            //element.Symbol = symbol;
            //element.Warehouse = warehouse;
            //element.Komponent = komponent;
            //element.Info = info;

            //if (imagePatch != "")
            //{
            //    try
            //    {
            //        //  FileStream inputStream = new(@"Images\x.PNG", FileMode.Open);
            //        FileStream inputStream = new(imagePatch, FileMode.Open);

            //        element.Image = new byte[inputStream.Length];

            //        inputStream.Read(element.Image, 0, element.Image.Length);
            //        inputStream.Close();
            //    }
            //    catch
            //    {
            //        return "Image file not found!";
            //    }
            //}

            using var connection = new SqliteConnection(dataSource);
            connection.Open();
            var command = connection.CreateCommand();

            string cmd = $"INSERT INTO element ({ReturnTableNameIfNotNull(element.Symbol, "symbol")}" +
                                                $"{ReturnTableNameIfNotNull(element.Warehouse, "warehouse")}" +
                                                $"{ReturnTableNameIfNotNull(element.Komponent, "komponent")}" +
                                                $"{ReturnTableNameIfNotNull(element.GetImage, "image")}" +
                                                $"{ReturnTableNameIfNotNull(element.Info, "info", false)})\n";

            cmd += $"VALUES ({ReturnValueIfNotNull(element.Symbol)}" +
                            $"{ReturnValueIfNotNull(element.Warehouse)}" +
                            $"{ReturnValueIfNotNull(element.Komponent)}" +
                            $"{ReturnValueIfNotNull(element.GetImage)}" +
                            $"{ReturnValueIfNotNull(element.Info, false)})";

            command.CommandText = cmd;

            if (element.GetImage != null)
                command.Parameters.Add("@image", SqliteType.Blob, element.GetImage.Length).Value = element.GetImage;

            int count = command.ExecuteNonQuery();

            return $"OK, {count} inserted to db.";
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
    }
}
