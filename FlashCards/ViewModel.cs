﻿using System;
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
        private string DatabaseName = Properties.Settings.Default.DatabaseFile;

        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand ShowRandomCard => new RelayCommand(DoShowRandomCard);
        public RelayCommand FlipCard => new RelayCommand(DoFlipCard);
        public RelayCommand OpenDatabaseSelectionWindow => new RelayCommand(DoOpenDatabaseSelectionWindow);

        public string DisplayedCardText
        {
            get; set;
        }

        private string activeCardFront;
        private string activeCardBack;
        private bool isShowingBack;

        public ViewModel()
        {
            DatabaseManager.ConnectToDatabase();

            isShowingBack = false;
        }

        private void DoShowRandomCard(object? obj)
        {
            var command = DatabaseManager.DatabaseConnection.CreateCommand();

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

                isShowingBack = false;
                UpdateDisplayedCard();                
            }    
        }

        private void DoFlipCard(object? obj)
        {
            isShowingBack = !isShowingBack;
            UpdateDisplayedCard();
        }

        private void DoOpenDatabaseSelectionWindow(object? obj)
        {
            DatabaseFileSelectWindow window = new DatabaseFileSelectWindow();

            window.ShowDialog();
        }

        private void Notify(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void UpdateDisplayedCard()
        {
            if (isShowingBack)
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
