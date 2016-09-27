
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
            DatabaseConnection.CreateSqliteDatabase();
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
