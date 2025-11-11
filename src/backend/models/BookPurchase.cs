using System;

namespace backend.models
{
    /**
     * @class BookPurchase
     * @brief Representa una compra de libro realizada por un comprador en el sistema Booksy.
     *
     * Contiene el libro adquirido y la fecha en que se realizó la compra.
     * Este objeto se utiliza para construir el historial de compras de un Buyer.
     */
    public class BookPurchase
    {
        private Book _book;                 //**@brief Libro que fue adquirido en la compra.
        private DateTime _purchaseDate;     //**@brief Fecha en que se realizó la compra.

        /**
         * @brief Constructor de la compra de libro.
         *
         * Inicializa los atributos con el libro comprado y la fecha de compra.
         *
         * @param book Objeto Book que representa el libro adquirido.
         * @param purchaseDate Fecha en que se realizó la compra.
         */
        public BookPurchase(Book book, DateTime purchaseDate)
        {
            _book = book ?? throw new ArgumentNullException(nameof(book));
            _purchaseDate = purchaseDate;
        }

        //** @brief Obtiene el libro adquirido.
        public Book Book
        {
            get => _book;
        }

        //**@brief Obtiene la fecha de la compra.
        public DateTime PurchaseDate
        {
            get => _purchaseDate;
        }
    }
}
