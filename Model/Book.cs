using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Test1.Model
{
    // Сводка:
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

        private string title;

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string author;

        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged("Author");
            }
        }

        private string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private int year;

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
                OnPropertyChanged("Year");
            }
        }

        private string cover;

        public string Cover
        {
            get
            {
                return cover;
            }
            set
            {
                cover = value;
                OnPropertyChanged("Cover");
            }
        }

        private string url;

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                OnPropertyChanged("Url");
            }
        }

        private string shelf;

        public string Shelf
        {
            get
            {
                return shelf;
            }
            set
            {
                shelf = value;
                OnPropertyChanged("Count");
            }
        }              

        #endregion

        #region Constructor

        public Book()
        {
            this.Title = string.Empty;
            this.Author = string.Empty;
            this.Description = string.Empty;
            this.Year = 0 ;
            this.Cover = string.Empty;
            this.Url = string.Empty;
            this.Shelf = string.Empty;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
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
