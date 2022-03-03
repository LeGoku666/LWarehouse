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
        public static string InsertElement(string dataSource, TabElement tab)
        {
            using var connection = new SqliteConnection(dataSource);
            connection.Open();
            var command = connection.CreateCommand();

            string cmd = $"INSERT INTO element ({ReturnTableNameIfNotNull(tab.Symbol, "symbol")}" +
                                                $"{ReturnTableNameIfNotNull(tab.Warehouse, "warehouse")}" +
                                                $"{ReturnTableNameIfNotNull(tab.Komponent, "komponent")}" +
                                                $"{ReturnTableNameIfNotNull(tab.GetImage, "image")}" +
                                                $"{ReturnTableNameIfNotNull(tab.Info, "info", false)})\n";

            cmd += $"VALUES ({ReturnValueIfNotNull(tab.Symbol)}" +
                            $"{ReturnValueIfNotNull(tab.Warehouse)}" +
                            $"{ReturnValueIfNotNull(tab.Komponent)}" +
                            $"{ReturnValueIfNotNull(tab.GetImage)}" +
                            $"{ReturnValueIfNotNull(tab.Info, false)})";

            command.CommandText = cmd;

            if (tab.GetImage != null)
                command.Parameters.Add("@image", SqliteType.Blob, tab.GetImage.Length).Value = tab.GetImage;

            int count = command.ExecuteNonQuery();

            return $"OK, {count} inserted to db.";
        }

        public static string InsertLocation(string dataSource, TabLocation tab)
        {
            return "";
        }

        public static string InsertWarehouse(string dataSource, TabWarehouse tab)
        {
            return "";
        }

        private static string ReturnValueIfNotNull<T>(T value, bool adComma = true)
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

        private static string ReturnTableNameIfNotNull<T>(T value, string tab, bool adComma = true)
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
