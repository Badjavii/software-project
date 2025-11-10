<script setup lang="js">
import { ref, onMounted } from "vue";
import { getBookList } from '../../services/bookService.js';

const book = ref(null);
const loading = ref(true);
const error = ref(null);

onMounted(async () => {
  try {
    const data = await getBookList();
    book.value = data;
  } catch (err) {
    error.value = "No se pudo cargar el libro";
    console.error(err);
  } finally {
    loading.value = false;
  }
});
</script>

<template>
  <div>
    <h2>Prueba del servicio</h2>
    <pre>{{ book }}</pre>
  </div>
</template>