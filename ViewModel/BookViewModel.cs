using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Test1.Helper;
using Test1.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Linq;

using System.Windows.Data;
using System.ComponentModel;


namespace Test1.ViewModel
{
    class BookViewModel : BaseViewModel
    {
        #region Properties

        private string xmlPath;

        /// <summary>
        /// Путь к XML файлу
        /// </summary>
        public string XMLPath
        {
            get
            {
                return xmlPath;
            }
            set
            {
                this.xmlPath = value;
                this.OnPropertyChanged("XMLPath");
            }
        }

        private string errorMessage;

        /// <summary>
        /// Сообщение об ошибке на WPF
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                this.errorMessage = value;
                this.OnPropertyChanged("ErrorMessage");
            }
        }

        private string searchQuery;

        public string SearchQuery
        {
            get
            {
                return this.searchQuery;
            }
            set
            {
                this.searchQuery = value;
                this.OnPropertyChanged("SearchQuery");
            }
        }
        
        private MyObservableCollection<Book> bookListCollection;

        public MyObservableCollection<Book> BookListCollection
        {
            get
            {
                if (bookListCollection == null)
                {                    
                    this.bookListCollection = new MyObservableCollection<Book>();                    
                }
                return bookListCollection;
            }
            set
            {
                bookListCollection = value;
                this.OnPropertyChanged("BookList");
            }
        }

        private ObservableCollection<Book> bookSelectResults;

       
        public ObservableCollection<Book> BookSelectResults
        {
            get
            {
                if (bookSelectResults == null)
                {
                    this.bookSelectResults = new ObservableCollection<Book>();
                }
                return bookSelectResults;
            }
            set
            {
                bookSelectResults = value;
                this.OnPropertyChanged("BookSelectResults");
            }

        }
                
        // Блокировка обьекта, для исключения конкуренции
        private object lockObject = new object();            

        
        #endregion

        #region Commands

        private ICommand xmlProcessCommand;
        

        /// <summary>
        /// Relay Command для загрузки списка книг из XML
        /// </summary>
        public ICommand XMLProcessCommand
        {
            get
            {
                if (xmlProcessCommand == null)
                {
                    xmlProcessCommand = new RelayCommand(ProcessXML);
                }
                return xmlProcessCommand;
            }
        }

        private ICommand selectCommand;

        /// <summary>
        /// Relay Command для поиска выбранной книги по Title
        /// </summary>
        public ICommand SelectCommand
        {
            get
            {
                if (selectCommand == null)
                {
                    selectCommand = new RelayCommand(BookSelectByClick);
                }
                return selectCommand;
            }
        }
        

        #endregion

        #region Constructor

        public BookViewModel()
        {
        }

        #endregion

        #region Actions

        /// <summary>
        /// Загрузка всех книг в коллекцию
        /// </summary>
        /// <param name="param">XML File source path</param>
        public void ProcessXML(object param)
        {
            string fileName = param as string;

            // Очистка Error Message в случае если читать другой XML файл
            this.ErrorMessage = string.Empty;

            lock (lockObject)
            {
                XDocument xmlDoc = null;
                try
                {
                    // Чтение XML через Linq
                    xmlDoc = XMLParser.GetXMLDataFromFileName(fileName);
                }
                catch (FileNotFoundException ex)
                {
                    this.ErrorMessage = ex.Message;
                }

                if (xmlDoc != null)
                {
                    // Заполнение данных из XML используя Linq
                    var books = (from material in xmlDoc.Element("booklibrary").Elements("book")
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


                    this.BookListCollection.IsUpdatePaused = true;

                    // Добавить все книги в коллекцию
                    foreach (var book in books)
                    {
                        this.BookListCollection.Add(
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

                    this.BookListCollection.IsUpdatePaused = false;
                }
            }
        }

        public void BookSelectByClick(object param)
        {
            string searchQuery = param as string;

            if (searchQuery == null)
            {
                this.ErrorMessage = "Ошибка: запрос невеверный";
                return;
            }

            this.BookSelectResults.Clear();

            this.BookSelectResults = new ObservableCollection<Book>(
                this.BookListCollection.Where(
                    x => x.Title.Contains(searchQuery)
                                                ));

        }        
        
        #endregion
    }
   
}
