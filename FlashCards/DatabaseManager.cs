using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace FlashCards
{
    public static class DatabaseManager
    {
        public static SQLiteConnection DatabaseConnection { get; private set; } = new SQLiteConnection();

        public static void ConnectToDatabase()
        {
            DatabaseConnection = new SQLiteConnection("Data Source=" + Properties.Settings.Default.DatabaseFile);
            DatabaseConnection.Open();
        }

        public static void DisconnectFromDatabase()
        {
            DatabaseConnection.Close();
        }
    }
}
