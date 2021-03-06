﻿using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml.Linq;
using BookLibrary.Helper;
using BookLibrary.Model;
using System.Configuration;

namespace BookLibrary.ViewModel
{
    class BookViewModel : BaseViewModel
    {
        #region Properties

        private string _xmlPath;

        /// <summary>
        /// Путь к XML файлу
        /// </summary>
        public string XmlPath
        {
            get
            {
                return _xmlPath;
            }
            set
            {
                _xmlPath = value;
                OnPropertyChanged("XMLPath");
            }
        }

        private string _errorMessage;

        /// <summary>
        /// Сообщение об ошибке на WPF
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        private string _searchQuery;

        public string SearchQuery
        {
            get
            {
                return _searchQuery;
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }
        
        private ObservableCollection<Book> _bookListCollection;

        public ObservableCollection<Book> BookListCollection
        {
            get
            {
                if (_bookListCollection == null)
                {                    
                    _bookListCollection = new ObservableCollection<Book>();                    
                }
                return _bookListCollection;
            }
            set
            {
                _bookListCollection = value;
                OnPropertyChanged("BookList");
            }
        }

        private ObservableCollection<Book> _bookSelectResults;
        
        public ObservableCollection<Book> BookSelectResults
        {
            get
            {
                if (_bookSelectResults == null)
                {
                    _bookSelectResults = new ObservableCollection<Book>();
                }
                return _bookSelectResults;
            }
            set
            {
                _bookSelectResults = value;
                OnPropertyChanged("BookSelectResults");
            }

        }
                
        // Блокировка обьекта, для исключения конкуренции
        private object lockObject = new object();
        
        #endregion


        #region Commands

        //private ICommand _xmlProcessCommand;
        
        /// <summary>
        /// Relay Command для загрузки списка книг из XML
        /// </summary>
        //public ICommand XMLProcessCommand
        //{
        //    get
        //    {
        //        if (_xmlProcessCommand == null)
        //        {
        //            _xmlProcessCommand = new RelayCommand(ProcessXml);
        //        }
        //        return _xmlProcessCommand;
        //    }
        //}

        private ICommand _selectCommand;

        /// <summary>
        /// Relay Command для поиска выбранной книги по Title
        /// </summary>
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                {
                    _selectCommand = new RelayCommand(BookSelectByClick);
                }
                return _selectCommand;
            }
        }

        #endregion
        

        #region Actions

        /// <summary>
        /// Загрузка всех книг в коллекцию
        /// </summary>
        public void ProcessXml() //(object param)
        {
            string fileName = ConfigurationManager.AppSettings["FilePath"];
            //string fileName = param as string;

            // Очистка Error Message в случае если читать другой XML файл
            ErrorMessage = string.Empty;

            lock (lockObject)
            {
                XDocument xmlDoc = null;
                try
                {
                    // Чтение XML через Linq
                    xmlDoc = XmlParser.GetXmlDataFromFileName(fileName);
                }
                catch (FileNotFoundException ex)
                {
                    ErrorMessage = ex.Message;
                }

                if (xmlDoc != null)
                {
                    // Заполнение данных из XML используя Linq
                    var element = xmlDoc.Element("booklibrary");
                    if (element != null)
                    {
                        var books = (from material in element.Elements("book")
                            select new
                            {
                                Title = (string)material.Attribute("title"),
                                Author = (string)material.Attribute("author"),
                                Description = (string)material.Attribute("description"),
                                Year = (int)material.Attribute("year"),
                                Cover = (string)material.Attribute("cover"),
                                Url = (string)material.Attribute("url"),
                                Shelf = (string)material.Attribute("shelf")
                            });


                        //this.BookListCollection.IsUpdatePaused = true;

                        // Добавить все книги в коллекцию
                        foreach (var book in books)
                        {
                            BookListCollection.Add(
                                new Book
                                {
                                    Title = book.Title,
                                    Author = book.Author,
                                    Description = book.Description,
                                    Year = book.Year,
                                    Cover = book.Cover,
                                    Url = book.Url,
                                    Shelf = book.Shelf
                                }
                                );
                        }
                    }

                    //this.BookListCollection.IsUpdatePaused = false;
                }
            }
        }

        
        public void BookSelectByClick(object param)
        {
            var searchQuery = param as string;

            if (searchQuery == null)
            {
                ErrorMessage = "Ошибка: запрос невеверный";
                return;
            }

            BookSelectResults.Clear();

            BookSelectResults = new ObservableCollection<Book>(
                BookListCollection.Where(
                    x => x.Title.Contains(searchQuery)
                                                ));

        }        
        
        #endregion
    }
   
}
