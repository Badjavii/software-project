<script setup lang="js">
  import NavBar from "../components/common/NavBar.vue";

  // CSS Imports
  import '../assets/styles/animations.css';

  // HTTP request of books
  import { ref, onMounted } from 'vue';
  import { getBookList } from '../services/bookService.js';

  let books = ref([])

  onMounted(async () => {
    try {
      let answer = await getBookList();
      books.value = answer.data;
      console.log(books.value);
    } catch (error) {
      console.error('Error mensaje:', error);
    }
  });

</script>

<template>
  <NavBar></NavBar>
  <main id="welcome-page">
    <router-view />
  </main>
</template>

<style scoped>
</style>
