<script setup lang="js">
defineProps({
  book: { type: Object, required: true }
});
</script>

<template>
  <router-link :to="{ name: 'BookDetail', params: { id: book._id } }" :key="book._id" class="book-card">
    <section class="card-main">
      <h2 class="title-card">{{ book._nameBook }}</h2>
      <p class="author-card ">Autor: {{ book._author }}</p>
      <p class="year-card" v-if="book._publishYear">{{ book._publishYear }}</p>
    </section>
    <section class="card-meta">
      <p class="cost-card" v-if="book._cost">${{ book._cost }}</p>
    </section>
  </router-link>
</template>

<style scoped>
.book-card {
  height: 15vh;
  display: flex;
  justify-content: space-between;
  text-decoration: none;
  color: black;
  background: rgba(255, 255, 255);
  padding: 1rem;
  margin: 2rem;
  border-radius: 1rem;
  box-shadow: 0 1rem 2rem rgba(0,0,0,0.6);
}

.card-main {
  width: 90vw;
  align-content: center;
  padding: 0 0 0 3rem;
}

.title-card {
  font-size: 2.8rem;
  margin: 0 0 1rem 0;
}

.author-card {
  color: rgba(0, 0, 0, 0.5);
  font-size: 1.5rem;
}

.card-meta {
  justify-items: right;
  align-content: center;
}

.cost-card {
  margin: 0 4rem;
  font-weight: bolder;
  color: rgba(0,0,0);
  font-size: 5rem;
}

.year-card {
  margin-left: .5rem;
  color: #888;
  font-size: 1.5rem;
}

.book-card:hover {
  animation: jump 1s ease;
  color: rgb(0,44,235);
  background-color: rgba(250,250,250, 0.95);
}

</style>