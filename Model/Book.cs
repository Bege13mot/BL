using System.ComponentModel;

namespace BookLibrary.Model
{
    //     Уведомляет клиентов об изменении значения свойства.
    public interface INotifyPropertyChanged
    {
        // Сводка:
        //     Происходит при изменении значения свойства.
        event PropertyChangedEventHandler PropertyChanged;
    }

    class Book : INotifyPropertyChanged
    {
        #region Properties

        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _author;

        public string Author
        {
            get
            {
                return _author;
            }
            set
            {
                _author = value;
                OnPropertyChanged("Author");
            }
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private int _year;

        public int Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged("Year");
            }
        }

        private string _cover;

        public string Cover
        {
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                OnPropertyChanged("Cover");
            }
        }

        private string _url;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                OnPropertyChanged("Url");
            }
        }

        private string _shelf;

        public string Shelf
        {
            get
            {
                return _shelf;
            }
            set
            {
                _shelf = value;
                OnPropertyChanged("Count");
            }
        }              

        #endregion
    
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                // Notify UI about the property change
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
