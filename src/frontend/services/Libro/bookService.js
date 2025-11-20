// src/frontend/services/bookService.js
import axios from "axios";
import { Book } from "../../assets/js/Book.js";

const API_URL = "http://localhost:5000/api/books";

// Obtener todos los libros
export const getBookList = async () => {
    try {
        const answer = await axios.get(`${API_URL}/all`);
        const books = answer.data.map(
            (b) =>
                new Book(
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
                )
        );
        return books;
    } catch (error) {
        console.error("Error en getBookList:", error.message);
        throw error;
    }
};

// Registrar un libro
export const postBook = async (bookPayload) => {
    try {
        return await axios.post(`${API_URL}/add`, bookPayload, {
            headers: { "Content-Type": "application/json" },
        });
    } catch (error) {
        console.error(
            "Error en postBook:",
            error.response ? error.response.data : error.message
        );
        throw error;
    }
};

// Eliminar un libro por ID
export const removeBook = async (id) => {
    try {
        return await axios.delete(`${API_URL}/remove/${id}`);
    } catch (error) {
        console.error(
            "Error en removeBook:",
            error.response ? error.response.data : error.message
        );
        throw error;
    }
};

// Buscar un libro por ID
export const searchBookById = async (id) => {
    try {
        const response = await axios.get(`${API_URL}/search/${id}`);
        return response.data;
    } catch (error) {
        console.error(
            "Error en searchBookById:",
            error.response ? error.response.data : error.message
        );
        throw error;
    }
};

// Obtener catálogo de un vendedor
export const getCatalogBySeller = async (sellerEmail) => {
    try {
        const response = await axios.get(`${API_URL}/catalog`, {
            params: { sellerEmail },
        });
        return response.data;
    } catch (error) {
        console.error(
            "Error en getCatalogBySeller:",
            error.response ? error.response.data : error.message
        );
        throw error;
    }
};

// Obtener historial de compras de un comprador
export const getPurchaseHistory = async (buyerEmail) => {
    try {
        const response = await axios.get(`${API_URL}/purchase-history`, {
            params: { buyerEmail },
        });
        return response.data;
    } catch (error) {
        console.error(
            "Error en getPurchaseHistory:",
            error.response ? error.response.data : error.message
        );
        throw error;
    }
};
