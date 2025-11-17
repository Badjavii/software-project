using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
namespace backend.models;


public class Book {
    
    // Attributes

    private int _id;
    private string _nameBook;
    private string _subtitle;
    private string _series;
    private string _author;
    private string _language;
    private string _publisher;
    private string _bookCover;
    private string _typeBook;
    private string _bookVolume;
    private float _bookHeight;
    private float _bookWidth;
    private List<string> _categoryList;
    private int _numPages;
    private DateTime _publishYear;
    private float _cost;
    private string _description;
    private Seller _seller; 

    // Methods

    // Constructor
    public Book(int id, string nameBook, string subtitle, string series, string author, string language, string publisher, string bookCover, string typeBook, string bookVolume, float bookHeight, float bookWidth, List<string> categoryList, int numPages, DateTime publishYear, float cost, string description, Seller seller) {
        _id = id;
        _nameBook = nameBook;
        _subtitle = subtitle;
        _series = series;
        _author = author;
        _language = language;
        _publisher = publisher;
        _bookCover = bookCover;
        _typeBook = typeBook;
        _bookVolume = bookVolume;
        _bookHeight = bookHeight;
        _bookWidth = bookWidth;
        _categoryList = categoryList;
        _numPages = numPages;
        _publishYear = publishYear;
        _cost = cost;
        _description = description;
        _seller = seller;
    }

    public Book() {
        _id = 0;
        _nameBook = "";
        _subtitle = "";
        _series = "";
        _author = "";
        _language = "";
        _publisher = "";
        _bookCover = "";
        _typeBook = "";
        _bookVolume = "";
        _bookHeight = 0;
        _bookWidth = 0;
        _categoryList = new List<string>();
        _numPages = 0;
        _publishYear = DateTime.Today;
        _cost = 0;
        _description = "";
        _seller = new Seller();
    }

    // Getter and Setter
    
    [JsonPropertyName("_id")]
    public int Id {
        get => _id;
        set => _id = value;
    }

    [JsonPropertyName("_nameBook")]
    public string NameBook {
        get => _nameBook;
        set => _nameBook = value;
    }

    [JsonPropertyName("_subtitle")]
    public string Subtitle {
        get => _subtitle;
        set => _subtitle = value;
    }

    [JsonPropertyName("_series")]
    public string Series {
        get => _series;
        set => _series = value;
    }

    [JsonPropertyName("_author")]
    public string Author {
        get => _author;
        set => _author = value;
    }

    [JsonPropertyName("_language")]
    public string Language {
        get => _language;
        set => _language = value;
    }

    [JsonPropertyName("_publisher")]
    public string Publisher {
        get => _publisher;
        set => _publisher = value;
    }

    [JsonPropertyName("_bookCover")]
    public string BookCover {
        get => _bookCover;
        set => _bookCover = value;
    }

    [JsonPropertyName("_typeBook")]
    public string TypeBook {
        get => _typeBook;
        set => _typeBook = value;
    }

    [JsonPropertyName("_bookVolume")]
    public string BookVolume {
        get => _bookVolume;
        set => _bookVolume = value;
    }

    [JsonPropertyName("_bookHeight")]
    public float BookHeight {
        get => _bookHeight;
        set => _bookHeight = value;
    }

    [JsonPropertyName("_bookWidth")]
    public float BookWidth {
        get => _bookWidth;
        set => _bookWidth = value;
    }

    [JsonPropertyName("_categoryList")]
    public List<string> CategoryList {
        get => _categoryList;
        set => _categoryList = value;
    }

    [JsonPropertyName("_numPages")]
    public int NumPages {
        get => _numPages;
        set => _numPages = value;
    }

    [JsonPropertyName("_publishYear")]
    public DateTime PublishYear {
        get => _publishYear;
        set => _publishYear = value;
    }

    [JsonPropertyName("_cost")]
    public float Cost {
        get => _cost;
        set => _cost = value;
    }

    [JsonPropertyName("_description")]
    public string Description {
        get => _description;
        set => _description = value;
    }

    [JsonPropertyName("_seller")] 
    public Seller Seller {
        get => _seller;
        set => _seller = value;
    }

    // Verification methods
    
    bool VerificateNameBook(string nameBook) {
        string pattern = @"^[A-Za-zÁÉÍÓÚáéíóúÑñ0-9\s\:\-\,\.\'\(\)]+$";
        return Regex.IsMatch(nameBook, pattern);
    }
    
    // Other Methods

    public List<Book> LoadBooks(string rootFile) {
        try {
            string json = File.ReadAllText(rootFile);
            List<Book>? list = JsonSerializer.Deserialize<List<Book>>(json);
            return  list != null ? list :  new List<Book>();
        }  
        catch (JsonException e) {
            Console.WriteLine($"JsonException: {e.Message}");
            List<Book> list = new List<Book>();
            return list;
        }

    }

    public List<Book> SearchBooks(List<Book> books, string dataEnter) {
        string bookSame = dataEnter.ToLower();
        var sameBook = books.Where(book => book._nameBook.ToLower() == bookSame).ToList();
        var similarBook = books.Where(book => book._nameBook.ToLower().Contains(bookSame) && book._nameBook.ToLower() != bookSame);
        return sameBook.Concat(similarBook).ToList();
    }

    public void ShowBooks(List<Book> bookList) {
        for (int i = 0; i < bookList.Count(); ++i) {
            Console.WriteLine($"Nombre del libro: {bookList[i]._nameBook}");
        }
    }
}