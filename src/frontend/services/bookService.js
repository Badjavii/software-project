
// Aquí manejo las peticiones HTTP de epica Libro
// relative root: src/frontend/services/bookService.js

import axios from "axios";
import { Book } from '../assets/js/Book.js';

const API_URL = "http://localhost:5000/api/";

export let getBookList = async ()=>{
    try {
        let answer = await axios.get(API_URL + 'books');
        let books = answer.data.map(b => new Book(b._nameBook, b._author, b._typeBook, b._categoryList, b._numPages, b._publishYear, b._cost, b._seller));
        console.log(books);
        return books;
    } catch (error){
        console.log('Mensaje de error: ', error.message);
        throw error;
    }
}