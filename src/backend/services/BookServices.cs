using backend.models;
using backend.repositories;

namespace backend.services
{
    /**
     * @class BookService
     * @brief Servicio que encapsula la lógica de negocio relacionada con libros.
     *
     * Permite consultar, agregar, eliminar y buscar libros.
     */
    public class BookService
    {
        private readonly BookRepository _repository = BookRepository.Instance;

        /**
         * @brief Devuelve todos los libros.
         */
        public List<Book> GetBooks()
        {
            return _repository.ReturnBooks();
        }

        /**
         * @brief Agrega un nuevo libro al repositorio.
         */
        public Book AddBook(Book book)
        {
            _repository.AddBook(book);
            return book;
        }

        /**
         * @brief Elimina un libro por ID.
         */
        public bool RemoveBook(int id)
        {
            return _repository.RemoveBook(id);
        }

        /**
         * @brief Busca un libro por ID.
         */
        public Book? SearchBook(int id)
        {
            return _repository.SearchBook(id);
        }

        /**
         * @brief Devuelve el catálogo de un vendedor específico.
         */
        public List<Book> GetCatalogBySeller(string sellerEmail)
        {
            return _repository.GetCatalogBySellerEmail(sellerEmail);
        }

        /**
         * @brief Devuelve el historial de compras de un comprador específico.
         */
        public List<Book> GetPurchaseHistory(string buyerEmail)
        {
            return _repository.GetPurchaseHistoryByBuyerEmail(buyerEmail);
        }
    }
}
