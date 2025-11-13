
// Aquí manejo las peticiones HTTP de epica Libro
// relative root: src/frontend/services/bookService.js

import axios from 'axios'
import { Book } from '../assets/js/Book.js'
import booksSample from '../assets/data/books.sample.json'

const API_BASE = (import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api').replace(/\/$/, '')

const mapToBooks = (items = []) =>
  items.map(
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
        b._seller
      )
  )

export async function getBookList() {
  try {
    const { data } = await axios.get(`${API_BASE}/books`)
    if (Array.isArray(data) && data.length) {
      return mapToBooks(data)
    }
  } catch (error) {
    console.warn('No se pudo consultar el API de libros, usando datos locales:', error.message)
  }
  return mapToBooks(booksSample)
}
