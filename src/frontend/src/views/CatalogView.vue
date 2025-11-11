<script setup lang="js">
import {onMounted, ref} from 'vue';
import {getBookList} from '../../services/bookService.js';
import BookCard from '../../components/consultar-libros/cards/catalog-card/BookCard.vue';

const books = ref([]);
const error = ref(true);

onMounted(async () => {
    try {
      books.value = await getBookList();
    } catch (error){
      console.error(error.message)
    } finally {
      error.value = false;
    }
});
</script>

<template>
  <article class="catalog-article">
    <section class="section-left">
    </section>
    <section id="catalog" class="section-right">
      <h1 class="catalog-title">CATÁLOGO</h1>
      <p v-if="error">No hay resultados...</p>
      <section v-else>
        <BookCard v-for="b in books" :key="b._id" :book="b" />
        <p v-if="books.length === 0">No hay libros</p>
      </section>
    </section>
  </article>
</template>

<style scoped>

.catalog-article {
  display: flex;
  justify-content: space-between;
  height: 88vh;
}

.section-left {
  width: 20vw;
  background-color: rgba(245,245,245);
}

.section-right {
  padding: 2rem;
  overflow-x: auto;
}

.catalog-title {
  margin: 1rem 0 4rem 1rem;
  font-size: 5rem;
  color: rgba(250,250,250)
}

.catalog-title:hover {
  animation: jump 1s ease;
}

</style>