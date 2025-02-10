using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Reflection.PortableExecutable;
using System.ComponentModel;

namespace FlashCards
{
    public class ViewModel : INotifyPropertyChanged
    {
        private SQLiteConnection sqliteConnection;

        private const string DatabaseName = "Cards.db";

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand ShowRandomCard => new RelayCommand(DoShowRandomCard);
        public RelayCommand FlipCard => new RelayCommand(DoFlipCard);

        public string DisplayedCardText
        {
            get; set;
        }

        private string activeCardFront;
        private string activeCardBack;
        private bool isShowingBack;

        public ViewModel()
        {
            // Connect to database
            sqliteConnection = new SQLiteConnection("Data Source=" + DatabaseName);
            sqliteConnection.Open();

            isShowingBack = false;
        }

        private void DoShowRandomCard(object? obj)
        {
            var command = sqliteConnection.CreateCommand();

            command.CommandText =
            @"
                SELECT front,back
                FROM card
                ORDER BY RANDOM()
                LIMIT 1
            ";

            using (var reader = command.ExecuteReader())
            {
                reader.Read();
                activeCardFront = reader.GetString(0);
                activeCardBack = reader.GetString(1);

                UpdateDisplayedCard(false);                
            }    
        }

        private void DoFlipCard(object? obj)
        {
            isShowingBack = !isShowingBack;
            UpdateDisplayedCard(isShowingBack);
        }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateDisplayedCard(bool isBack)
        {
            if (isBack)
            {
                DisplayedCardText = activeCardBack;
            }
            else
            {
                DisplayedCardText = activeCardFront;
            }

            Notify(nameof(DisplayedCardText));
        }
    }
}
