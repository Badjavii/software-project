using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class BookService
     * @brief Servicio que encapsula la lógica de negocio relacionada con libros.
     */
    public class BookService
    {
        private readonly BookRepository _repository;

        public BookService()
        {
            _repository = BookRepository.Instance;
        }

        public void RegisterBook(Book book)
        {
            if (_repository.ExistsByName(book.NameBook))
                throw new ArgumentException("Ya existe un libro con ese nombre.");

            _repository.AddBook(book);
            _repository.Save();
        }

        public Book? GetBookByName(string name)
        {
            return _repository.FindByName(name);
        }

        public List<Book> GetBooksBySeller(string sellerEmail)
        {
            return _repository.FindBySeller(sellerEmail);
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            return _repository.FindByAuthor(author);
        }

        public List<Book> GetBooksByCategory(string category)
        {
            return _repository.FindByCategory(category);
        }

        public bool BookExists(string name)
        {
            return _repository.ExistsByName(name);
        }

        public Book? GetBookInstance(string name)
        {
            return _repository.GetBookInstance(name);
        }

        public List<Book> GetAllBooks()
        {
            return _repository.GetAllBooks();
        }
    }
}
