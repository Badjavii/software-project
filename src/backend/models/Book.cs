using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
namespace backend.models;


public class Book
{

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
    private int _publishYear;
    private float _cost;
    private string _sellerEmail;

    // Methods

    // Constructor
    public Book(int id, string nameBook, string subtitle, string series, string author, string language, string publisher, string bookCover, string typeBook, string bookVolume, float bookHeight, float bookWidth, List<string> categoryList, int numPages, int publishYear, float cost, string sellerEmail)
    {
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
        _sellerEmail = sellerEmail;
    }

    // Getter and Setter

    [JsonPropertyName("_id")]
    public int Id
    {
        get => _id;
        set => _id = value;
    }

    [JsonPropertyName("_nameBook")]
    public string NameBook
    {
        get => _nameBook;
        set => _nameBook = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_subtitle")]
    public string Subtitle
    {
        get => _subtitle;
        set => _subtitle = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_series")]
    public string Series
    {
        get => _series;
        set => _series = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_author")]
    public string Author
    {
        get => _author;
        set => _author = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_language")]
    public string Language
    {
        get => _language;
        set => _language = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_publisher")]
    public string Publisher
    {
        get => _publisher;
        set => _publisher = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("bookCover")]
    public string BookCover
    {
        get => _bookCover;
        set => _bookCover = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_typeBook")]
    public string TypeBook
    {
        get => _typeBook;
        set => _typeBook = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_bookVolume")]
    public string BookVolume
    {
        get => _bookVolume;
        set => _bookVolume = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_bookHeight")]
    public float BookHeight
    {
        get => _bookHeight;
        set => _bookHeight = value;
    }

    [JsonPropertyName("_bookWidth")]
    public float BookWidth
    {
        get => _bookWidth;
        set => _bookWidth = value;
    }

    [JsonPropertyName("_categoryList")]
    public List<string> CategoryList
    {
        get => _categoryList;
        set => _categoryList = value ?? throw new ArgumentNullException(nameof(value));
    }

    [JsonPropertyName("_numPages")]
    public int NumPages
    {
        get => _numPages;
        set => _numPages = value;
    }

    [JsonPropertyName("_publishYear")]
    public int PublishYear
    {
        get => _publishYear;
        set => _publishYear = value;
    }

    [JsonPropertyName("_cost")]
    public float Cost
    {
        get => _cost;
        set => _cost = value;
    }

    [JsonPropertyName("_sellerEmail")]
    public string SellerEmail
    {
        get => _sellerEmail;
        set => _sellerEmail = value ?? throw new ArgumentNullException(nameof(value));
    }
}