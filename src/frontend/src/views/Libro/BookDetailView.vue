<script setup lang="js">

import Characteristics from "../../../components/Libro/consultar-libro/book-detail/Characteristics.vue";
import Description from "../../../components/Libro/consultar-libro/book-detail/Description.vue";
import CommentarySection from "../../../components/Libro/consultar-libro/book-detail/CommentarySection.vue";
import BookCardDetail from "../../../components/Libro/consultar-libro/book-detail/BookCardDetail.vue";
import {useRoute} from "vue-router";

import { getBookList } from "../../../services/Libro/bookService.js"
import {onMounted, ref} from "vue";
import {Book} from "../../../assets/js/Book.js";

let books = ref([])
let book = ref({})

let route = useRoute()
let bookId = route.params.id


onMounted(async () => {
  try {
    books.value = await getBookList();
    book.value = books.value.find(b => String(b._id) === String(bookId))
  } catch (error) {
    console.error("Error cargando libros:", error);
  }
})
</script>

<template>
  <article class="article-book" v-if="book">
    <section class="section-left">
      <Characteristics :book = "book"></Characteristics>
      <Description :book="book"></Description>
      <CommentarySection></CommentarySection>
    </section>
    <section class="section-right">
      <BookCardDetail :book = "book"></BookCardDetail>
    </section>
  </article>
</template>

<style scoped>

.article-book {
  display: flex;
  height: 150vh;
  width: 65vw;

  margin: 8rem 0 8rem 0;
  padding: 2rem;

  justify-self: center;
  justify-content: space-between;

  background-color: rgba(255,255,255);
  border-radius: 1rem;
}

.section-left {
  margin: 3rem 0 0 0;
  display: flex;
  flex-direction: column;
  width: 37vw;
  height: 120vh;
}

.section-right {
  width: 18vw;
  padding: 2rem 0 0 0;
  background-color: rgb(5, 33, 52);
  border-radius: 1rem;
  justify-items: center;
}


</style>