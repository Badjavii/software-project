<script setup lang="js">
import { onMounted, ref, computed } from 'vue';
import { useRoute } from 'vue-router';
import { getBookList } from '../../../services/Libro/bookService.js';
import BookCard from '../../../components/Libro/consultar-libro/catalog-card/BookCard.vue';

const books = ref([]);
const error = ref(true);
const route = useRoute();

onMounted(async () => {
  try {
    books.value = await getBookList();
  } catch (err){
    console.error(err?.message ?? err);
  } finally {
    error.value = false;
  }
});

const query = computed(() => (route.query.q || '').toString().toLowerCase());

const filteredBooks = computed(() => {
  if (!query.value) return books.value || [];
  return (books.value || []).filter(b => {
    const title = (b._nameBook || '').toString().toLowerCase();
    const author = (b._author || '').toString().toLowerCase();
    return title.includes(query.value) || author.includes(query.value);
  });
});
</script>

<template>
  <article class="catalog-article">
    <section class="section-left">
    </section>
    <section id="catalog" class="section-right">
      <h1 class="catalog-title">CATÁLOGO</h1>

      <p v-if="error">Cargando resultados...</p>

      <section v-else>
        <p v-if="query && filteredBooks.length === 0">No hay resultados para "{{ route.query.q }}"</p>
        <BookCard v-for="b in filteredBooks" :key="b._id" :book="b" />
        <p v-if="!query && books.length === 0">No hay libros</p>
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