
// Aquí manejo las peticiones HTTP de epica Libro
// relative root: src/frontend/services/bookService.js

import axios from "axios";
import {Book} from '../../assets/js/Book.js';

const API_URL = "http://localhost:5000/api/";

export let getBookList = async () => {
    try {
        let answer = await axios.get(API_URL + 'books');
        let books = answer.data.map(b => new Book(
            b._id,
            b._nameBook,
            b._subtitle,
            b._series,
            b._author,
            b._language,
            b._publisher,
            b._bookCover,
            b._typeBook,
            b._bookVolume,
            b._bookHeight,
            b._bookWidth,
            b._categoryList,
            b._numPages,
            b._publishYear,
            b._cost,
            b._description,
            b._seller
        ));
        console.log(books);
        return books;
    } catch (error){
        console.log('Mensaje de error: ', error.message);
        throw error;
    }
}

export const postBook = async (bookPayload) => {
    try {
        return await axios.post(API_URL + 'books', bookPayload, {
            headers: { "Content-Type": "application/json" }
        });
    } catch (error) {
        console.error("Error en postBook:", error.response ? error.response.data : error.message);
        throw error;
    }
}

export const editBook = async (bookPayload) => {
    try {
        return await axios.post(API_URL + 'books', bookPayload, {
            headers: { "Content-Type": "application/json" }
        });
    } catch (error) {
        console.error("Error en postBook:", error.response ? error.response.data : error.message);
        throw error;
    }
}