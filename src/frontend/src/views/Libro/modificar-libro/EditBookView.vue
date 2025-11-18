<script setup>
import { ref } from "vue";
import { postBook } from "../../../../services/Libro/bookService.js";
import {Book} from "../../../../assets/js/Book.js";

import { Seller } from '../../../../assets/js/Seller.js';

const sellerEjemplo = new Seller(
  "vendedor@ejemplo.com",   // _email
  "Juan",                   // _firstName
  "Pérez",                  // _lastName
  35,                       // _age
  "password123",            // _password
  [],                       // _listBook (puedes dejarlo vacío o luego añadir libros)
  5                         // _qualification
);

// Ahora creamos un libro de ejemplo
const book = new Book(
  1,                        // _id
  "Cien años de soledad",   // _nameBook
  "Edición especial",       // _subtitle
  "Serie Realismo Mágico",  // _series
  "Gabriel García Márquez", // _author
  "Español",                // _language
  "Editorial Sudamericana", // _publisher
  "Dura",                   // _bookCover
  "Físico",                 // _typeBook
  "Volumen I",              // _bookVolume
  23.5,                     // _bookHeight
  15.0,                     // _bookWidth
  ["Realismo mágico", "Novela"], // _categoryList
  471,                      // _numPages
  1967,                     // _publishYear
  25.99,                    // _cost
  "Una de las obras más importantes de la literatura latinoamericana.", // _description
  sellerEjemplo             // _seller
);

function okMessage(event){
  alert('Libro editado exitosamente')
}

const error = ref(false);

let bookName = ref(book._nameBook);
let subtitle = ref(book._subtitle);
let seriesBook = ref(book._series);
let authorBook = ref(book._author);
let publisherBook = ref(book._publisher);
let volumeBook = ref(book._bookVolume);
let heightBook = ref(book._bookHeight);
let widthBook = ref(book._bookWidth);
let numPagesBook = ref(book._numPages);
let publishYearBook = ref(book._publishYear);
let costBook = ref(book._cost);
let description = ref(book._description);
let seller = ref(book._seller);

let categoriesBookItems = ["Otro", "Novela", "Cuento", "Poesía", "Teatro", "Ensayo", "Crónica", "Biografía",
  "Autobiografía", "Memorias", "Diarios", "Epistolar", "Fábula", "Leyenda", "Mito", "Relato corto", "Narrativa",
  "Prosa poética", "Literatura infantil", "Literatura juvenil", "Literatura fantástica", "Ciencia ficción", "Distopía",
  "Utopía", "Realismo mágico", "Romántica", "Histórica", "Policíaca", "Thriller", "Suspenso", "Terror", "Humor",
  "Satírica", "Didáctica", "Académica", "Periodística", "Divulgación científica", "Filosófica", "Política",
  "Religiosa", "Espiritual", "Viajes", "Aventura", "Erótica", "Gótica", "Costumbrista", "Experimental", "Minimalista",
  "Posmoderna"]
let categoriesBook = ref(book._categoryList);

let coverBookItem = ['Dura', 'Blanda'];
let coverBook = ref(book._bookCover);

let typeBookItem = ['Físico', 'Digital'];
let typeBook = ref(book._typeBook);

let languageBookItem = ['Español', 'Ingles', 'Portuguese', 'Alemán', 'Italiano', 'Frances']
let languageBook = ref(book._language);

async function handleSubmit(event) {
  event.preventDefault();

  try {
    const payload = {
      _id: (Math.floor(Math.random() * (100000 - 1 + 1)) + 1),
      _nameBook: bookName.value,
      _subtitle: subtitle.value,
      _series: seriesBook.value,
      _author: authorBook.value,
      _language: languageBook.value,
      _publisher: publisherBook.value,
      _bookCover: coverBook.value,
      _typeBook: typeBook.value,
      _bookVolume: volumeBook.value,
      _bookHeight: parseFloat(heightBook.value),
      _bookWidth: parseFloat(widthBook.value),
      _categoryList: categoriesBook.value,
      _numPages: numPagesBook.value,
      _publishYear: publishYearBook.value,
      _cost: parseFloat(costBook.value),
      _description: description.value,
      _seller: {
        _firstName: "VendedorEjemplo",
        _email: "vendedor@ejemplo.com",
        _qualification: 0
      }
    };

    const response = await postBook(payload);

    console.log("Respuesta del backend:", response.data);
    okMessage();

  } catch (err) {
    console.error("Error al editar libro:", err);
    error.value = true;
  }
}
</script>

<template>
<article>
  <form action="" @submit.prevent="handleSubmit">

    <h1>EDITAR LIBRO</h1>

    <fieldset id="partOne">
      <legend>Características principales</legend>

      <fieldset class="fieldsetPart">

        <label for="nameBook">
          Titulo
          <input v-model="bookName" id="nameBook" value="{{ bookName }}"  type="text" required />
        </label>

        <label for="subtitleBook">
          Sub-titulo
          <input v-model="subtitle" id="subtitleBook" value="{{ subtitle }}" type="text" required />
        </label>

      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="seriesBook">
          Serie
          <input v-model="seriesBook" value="{{ seriesBook }}" id="seriesBook" type="text" required />
        </label>

        <label for="authorBook">
          Autor
          <input v-model="authorBook" id="authorBook" value="{{ authorBook }}" type="text" required />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="publishYearBook">
          Año de publicación
          <input v-model="publishYearBook" id="publishYearBook" value="{{ publishYearBook }}" type="date" />
        </label>

        <label for="publisherBook">
          Editorial
          <input v-model="publisherBook" id="publisherBook" value="{{ publisherBook }}" type="text" required />
        </label>
      </fieldset>
    </fieldset>

    <fieldset id="partTwo">
      <legend>Detalles y dimensiones</legend>

      <fieldset class="fieldsetPart">
        <label for="volumeBook">
          Volumen
          <input v-model="volumeBook" id="volumeBook" placeholder="Ingresar volumen del libro" type="text" />
        </label>

        <label for="heightBook">
          Altura (cm)
          <input v-model="heightBook" id="heightBook" placeholder="Ingresar altura del libro (cm)" type="number" step="0.01" />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="widthBook">
          Ancho (cm)
          <input v-model="widthBook" id="widthBook" placeholder="Ingresar ancho del libro (cm)" type="number" step="0.01" />
        </label>

        <label for="numPagesBook">
          Número de páginas
          <input v-model="numPagesBook" id="numPagesBook" placeholder="Ingresar número de páginas del libro" type="number" />
        </label>
      </fieldset>

      <fieldset class="fieldsetPart">
        <label for="costBook">
          Coste
          <input v-model="costBook" id="costBook" placeholder="Ingresar coste del libro" type="number" step="0.01" />
        </label>

        <label for="description">
          Descripción
          <input v-model="description" id="description" placeholder="Ingresar descripción del libro" type="text" />
        </label>
      </fieldset>

    </fieldset>

    <fieldset id="partThree">

      <legend>Lenguaje, formato, tipo de tapa y categorías del libro</legend>

      <v-combobox  v-model="languageBook" :items="languageBookItem" label="Lenguaje del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox  v-model="typeBook" :items="typeBookItem" label="Formato del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox  v-model="coverBook" :items="coverBookItem" label="Tapa del libro" chips clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

      <v-combobox v-model="categoriesBook" :items="categoriesBookItems" label="Categorías del libro" chips multiple clearable>
        <template v-slot:chip="{ props, item }">
          <v-chip v-bind="props" color="primary" text-color="white">
            {{ typeof item === 'string' ? item : (item.raw || item) }}
          </v-chip>
        </template>
      </v-combobox>

    </fieldset>

    <input class="register-book-button" type="submit" value="Editar libro" />
  </form>
</article>
</template>

<style scoped>

article {
  justify-items: center;
  align-content: center;

  height: 275vh;
}

form {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  height: 250vh;
  width: 70vw;

  align-items: center;

  box-shadow: 0 0 10px rgba(0,0,0,1);
  background-color: rgb(255,255,255);
  border-radius: 1rem;
  border: none;
}

h1 {
  width: 70vw;
  height: 20vh;
  background-position: center;
  background-repeat: no-repeat;
  font-size: 3rem;
  text-align: center;
  background-image: url("../../../../assets/img/consultar-libros-img/books-banner.jpg");
  background-size: cover;
  align-content: center;
  margin: 0 0 5rem 0;
  border-radius: 1rem 1rem 0 0;
}

#partOne, #partTwo{
  display: flex;
  flex-direction: column;
  height: 50vh;
  width: 50vw;
  border: none;
  justify-items: center;
}

#partThree {
  display: flex;
  flex-direction: column;
  align-items: center;
  height: 100vh;
  width: 50vw;
  border: none;
}

.fieldsetPart {
  display: flex;
  flex-direction: row;
  justify-content: center;
  gap: 5rem;
  border: none;
}

legend {
  font-size: 2rem;
  font-weight: bold;
  width: 40vw;
  margin: 0 0 2rem 0
}

label {
  display: flex;
  flex-direction: column;
  font-size: 1.5rem;
  height: 13vh;
  width: 20vw;
  font-weight: normal;
}

input {
  height: 6vh;
  width: 20vw;
  font-size: 1.5rem;
  padding: 0 0 0 2rem;

  margin: 0 0 2.5rem 0;
  border:none;
  border-radius: 1rem;
  background-color: rgb(163, 189, 224);
}

.register-book-button {
  height: 10vh;
  width: 40vw;
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

/* Componentes de Vuetify */

:deep(.v-field) {
  height: 15vh;
}

:deep(.v-field-label) {
  font-size: 1.5rem;
  font-weight: normal;
}

:deep(.v-input){
  width:  50vw;
  border-radius: 1rem;
}

</style>