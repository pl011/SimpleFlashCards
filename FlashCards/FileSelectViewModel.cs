using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlashCards
{
    public class FileSelectViewModel
    {
        public string FileName { get; set; }

        public FileSelectViewModel()
        {
            FileName = Properties.Settings.Default.DatabaseFile;
        }

        public RelayCommand Cancel => new RelayCommand(DoCancel);

        private void DoCancel(object? obj)
        {
            if (obj != null)
            {
                Window window = (Window)obj;
                window?.Close();
            }
        }

        public RelayCommand Update => new RelayCommand(DoUpdate);

        private void DoUpdate(object? obj)
        {
            Properties.Settings.Default.DatabaseFile = FileName;
            Properties.Settings.Default.Save();

            // Connect to the new database
            DatabaseManager.DisconnectFromDatabase();
            DatabaseManager.ConnectToDatabase();

            if (obj != null)
            {
                Window window = (Window)obj;
                window?.Close();
            }
        }
    }
}
