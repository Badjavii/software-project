<script setup>
import { ref } from "vue";
import { postBook } from "../../services/bookService.js"; // nueva función a crear

const error = ref(false);

async function handleSubmit(event) {
  event.preventDefault();

  try {
    const bookName = document.getElementById("nameBook").value;
    const subtitle = document.getElementById("subtitleBook").value;
    const seriesBook = document.getElementById("seriesBook").value;
    const authorBook = document.getElementById("authorBook").value;
    const languageBook = document.getElementById("languageBook").value;
    const publisherBook = document.getElementById("publisherBook").value;
    const coverBook = document.getElementById("coverBook").value;
    const typeBook = document.getElementById("typeBook").value;
    const volumeBook = document.getElementById("volumeBook").value;
    const heightBook = document.getElementById("heightBook").value;
    const widthBook = document.getElementById("widthBook").value;
    const categoriesBook = document.getElementById("categoriesBook").value;
    const numPagesBook = parseInt(document.getElementById("numPagesBook").value);
    const publishYearBook = parseInt(document.getElementById("publishYearBook").value);
    const costBook = parseFloat(document.getElementById("costBook").value);

    const payload = {
      _id: (Math.floor(Math.random() * (100000 - 1 + 1)) + 1),
      _nameBook: bookName,
      _subtitle: subtitle,
      _series: seriesBook,
      _author: authorBook,
      _language: languageBook,
      _publisher: publisherBook,
      _bookCover: coverBook,
      _typeBook: typeBook,
      _bookVolume: volumeBook,
      _bookHeight: parseFloat(heightBook) || 0,
      _bookWidth: parseFloat(widthBook) || 0,
      _categoryList: categoriesBook ? categoriesBook.split(',').map(s => s.trim()) : [],
      _numPages: numPagesBook || 0,
      _publishYear: publishYearBook || 0,
      _cost: costBook || 0.0,
      _seller: {
        _firstName: "VendedorEjemplo",
        _email: "vendedor@ejemplo.com",
        _qualification: 0
      }
    };

    const response = await postBook(payload);

    console.log("Respuesta del backend:", response.data);

  } catch (err) {
    console.error("Error al registrar libro:", err);
    error.value = true;
  }
}
</script>

<template>
<article class="register-book-article">
  <form id="register-book-form" action="" method="get" class="register-book-form" @submit="handleSubmit" >

    <h1 class="register-book-title">REGISTRAR LIBRO</h1>

    <label class="register-book-label" for="nameBook">Titulo</label>
    <input id="nameBook" class="register-book-input" placeholder="Ingresar titulo del libro" type="text" />

    <label class="register-book-label" for="subtitleBook">Sub-titulo</label>
    <input id="subtitleBook" class="register-book-input" placeholder="Ingresar sub-titulo del libro" type="text" />

    <label class="register-book-label" for="seriesBook">Serie</label>
    <input id="seriesBook" class="register-book-input" placeholder="Ingresar serie del libro" type="text" />

    <label class="register-book-label" for="authorBook">Autor</label>
    <input id="authorBook" class="register-book-input" placeholder="Ingresar autor del libro" type="text" />

    <label class="register-book-label" for="languageBook">Lenguaje</label>
    <input id="languageBook" class="register-book-input" placeholder="Ingresar lenguaje del libro" type="text" />

    <label class="register-book-label" for="publisherBook">Editorial</label>
    <input id="publisherBook" class="register-book-input" placeholder="Ingresar editorial del libro" type="text" />

    <label class="register-book-label" for="coverBook">Tipo de tapa</label>
    <input id="coverBook" class="register-book-input" placeholder="Ingresar tipo de tapa del libro (dura/blanda)" type="text" />

    <label class="register-book-label" for="typeBook">Formato</label>
    <input id="typeBook" class="register-book-input" placeholder="Ingresar formato del libro (físico/digital)" type="text" />

    <label class="register-book-label" for="volumeBook">Volumen</label>
    <input id="volumeBook" class="register-book-input" placeholder="Ingresar volumen del libro" type="text" />

    <label class="register-book-label" for="heightBook">Altura</label>
    <input id="heightBook" class="register-book-input" placeholder="Ingresar altura del libro (cm)" type="text" />

    <label class="register-book-label" for="widthBook">Ancho</label>
    <input id="widthBook" class="register-book-input" placeholder="Ingresar ancho del libro (cm)" type="text" />

    <label class="register-book-label" for="categoriesBook">Categorías</label>
    <input id="categoriesBook" class="register-book-input" placeholder="Ingresar categorías del libro (separarlo por comas)" type="text" />

    <label class="register-book-label" for="numPagesBook">Número de páginas</label>
    <input id="numPagesBook" class="register-book-input" placeholder="Ingresar número de páginas del libro" type="text" />

    <label class="register-book-label" for="publishYearBook">Año de publicación</label>
    <input id="publishYearBook" class="register-book-input" placeholder="Ingresar año de publicación del libro" type="text" />

    <label class="register-book-label" for="costBook">Coste</label>
    <input id="costBook" class="register-book-input" placeholder="Ingresar coste del libro" type="text" />

    <input class="register-book-button" type="submit" value="Registrar libro" />
  </form>
</article>
</template>

<style scoped>

.register-book-article {
  justify-items: center;
  align-content: center;

  height: 350vh;
}

.register-book-title {
  margin: 5rem 0 8rem 0;
  font-size: 5rem;
  height: 5vh;
}

.register-book-label {
  margin: 0 0 2rem 0;
  font-size: 3rem;
  height: 5vh;
}

.register-book-form {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  height: 315vh;
  width: 70vw;

  align-content: center;

  background-color: rgb(255,255,255);
  border-radius: 1rem;
}

.register-book-input {
  height: 5vh;
  width: 60vw;
  font-size: 2rem;
  padding: 0 0 0 2rem;

  margin: 0 0 2.5rem 0;
  border: none rgb(200, 200, 200);
  border-bottom-style: solid;
}

.register-book-button {
  height: 10vh;
  width: 40vw;
  margin: 5rem 0 0 0;
  background-color: rgb(0,117,235);
  border-radius: 1rem;
  border: none;
  color: rgb(255,255,255);
  font-size: 2.5rem;
}

.register-book-button:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
}

</style>