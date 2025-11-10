using backend.models;

class Program
{
    static void Main(string[] args)
    {
        Book book = new Book("", "", "", new List<string>(), 0, 0, 0);
        List<Book> libros = book.LoadBooks("libros.json");

        Console.WriteLine("Ingrese término de búsqueda:");
        string entrada = Console.ReadLine();

        List<Book> resultados = book.SearchBooks(libros, entrada);

        book.ShowBooks(resultados);
    }
}
