using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
namespace backend.models;


public class Book {
    
    // Attributes

    private string _nameBook;
    private string _author;
    private string _typeBook; 
    private List<string> _categoryList;
    private int _numPages;
    private int _publishYear;
    private float _cost;
    private Seller _seller; 

    // Methods

    // Constructor
    public Book(string nameBook, string author, string typeBook, List<string> categoryList, int numPages, int publishYear, float cost) {
        this._nameBook = nameBook;
        this._author = author;
        this._typeBook = typeBook;
        this._categoryList = categoryList;
        this._numPages = numPages;
        this._publishYear = publishYear;
        this._cost = cost;
    }

    // Getter and Setter

    [JsonPropertyName("_nameBook")]
    public string NameBook {
        get => _nameBook;
        set => _nameBook = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_author")]
    public string Author {
        get => _author;
        set => _author = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_typeBook")]
    public string TypeBook {
        get => _typeBook;
        set => _typeBook = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_categoryList")]
    public List<string> CategoryList {
        get => _categoryList;
        set => _categoryList = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_numPages")]
    public int NumPages {
        get => _numPages;
        set => _numPages = value;
    }

    [JsonPropertyName("_publishYear")]
    public int PublishYear {
        get => _publishYear;
        set => _publishYear = value;
    }

    [JsonPropertyName("_cost")]
    public float Cost {
        get => _cost;
        set => _cost = value;
    }

    [JsonPropertyName("_seller")]
    public Seller Seller {
        get => _seller;
        set => _seller = value ?? throw new ArgumentNullException(nameof(value));
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
            List<Book> list = JsonSerializer.Deserialize<List<Book>>(json);
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