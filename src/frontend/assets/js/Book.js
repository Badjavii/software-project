
export class Book {
    constructor(_id, _nameBook, _subtitle, _series, _author, _language, _publisher, _bookCover, _typeBook, _bookVolume, _bookHeight, _bookWidth, _categoryList, _numPages, _publishYear, _cost, _seller) {
        this._id = _id;
        this._nameBook = _nameBook;
        this._subtitle = _subtitle;
        this._series = _series;
        this._author = _author;
        this._language = _language;
        this._publisher = _publisher;
        this._bookCover = _bookCover;
        this._typeBook = _typeBook;
        this._bookVolume = _bookVolume;
        this._bookHeight = _bookHeight;
        this._bookWidth = _bookWidth;
        this._categoryList = _categoryList;
        this._numPages = _numPages;
        this._publishYear = _publishYear;
        this._cost = _cost;
        this._seller = _seller;
    }

     searchBook(listBook, dataEnter){

        let title = dataEnter.toLowerCase();

        let sameBooks = listBook.filter(book => {
                return (book._nameBook.toLowerCase() === title || book._author.toLowerCase() === title);
            });

        let similarBooks = listBook.filter(book => {
                return (book._nameBook.toLowerCase().includes(title) || book._author.toLowerCase().includes(title));
            });

        return [...new Set([...sameBooks, ...similarBooks])];
    }

    static catalog(container, list) {
        
    }
}