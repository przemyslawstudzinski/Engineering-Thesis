using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationToSupportAndControlDiet.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            WelcomeMessage = "Hi, it's main page";
            string databaseFilePath = Path.Combine(Windows.Storage.ApplicationData.
                    Current.LocalFolder.Path, "db.sqlite");
            SQLite.Net.SQLiteConnection connetionToDatabase = new SQLite.Net.SQLiteConnection(
                new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), databaseFilePath);

        }
        private string _welcomeMessage;
        public string WelcomeMessage
        {
            get
            {
                return _welcomeMessage;
            }
            set
            {
                Set(ref _welcomeMessage, value);
            }
        }
    }
}
