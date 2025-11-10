
export class Book {
    constructor(_nameBook, _author, _typeBook, _categoryList, _numPages, _publishYear, _cost, _seller) {
        this._nameBook = _nameBook;
        this._author = _author;
        this._typeBook = _typeBook;
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
}