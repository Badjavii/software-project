<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';

const search = ref('');
const router = useRouter();
const route = useRoute();

onMounted(() => {
  const q = route.query.q;
  if (q) search.value = String(q);
});

function onSubmit(e: Event) {
  e.preventDefault();
  const q = search.value.trim();

  router.push({ path: '/catalog', query: q ? { q } : {} });
}
</script>

<template>
  <form id="form-search-bar" @submit="onSubmit">
    <input id="search-bar" v-model="search" type="text" placeholder="Buscar libros en Booksy UCAB..." />
    <button type="submit" class="button-search-bar">
      <img class="icon-search" alt="Icono de lupa" src="../../assets/img/common/search-icon.png" />
    </button>
  </form>
</template>

<style scoped>

  @media (min-width: 1600px) {
    #form-search-bar{
      height: 4.5vh;
      width: 53vw;
      margin: 0 7rem 0 0;
      padding: 0.1rem;
      justify-content: center;
      box-shadow: 0 4px 12px rgb(0,0,0,0.5);
      border-radius: 0.5rem;
      border: none;
    }

    #search-bar {
      background-color: rgb(255, 255, 255);
      height: 4vh;
      width: 48vw;
      padding: 0 0 0 1rem;

      font-size: 1.5rem;
      gap: 2rem;

      border-style: none;
    }

    .button-search-bar {
      height: 4.5vh;
      width: 2.3vw;
      margin: 0 0 0 0.1rem;
      border-style: none;
      background-color: rgb(255,255,255);
      border-radius:0.5rem;
    }

    .icon-search {
      height: 3vh;
      width: 1vw;
    }

    #search-bar:focus {
      animation: jump 1s ease;
    }

    .button-search-bar:hover {
      animation: jump 1s ease, zoom 1s ease;
    }
  }

  @media (min-width: 1200px) {
    #form-search-bar{
      height: 4.5vh;
      width: 52vw;
      margin: 0 7rem 0 0;
      padding: 0.1rem;
      justify-content: center;
      box-shadow: 0 4px 12px rgb(0,0,0,0.5);
      border-radius: 0.5rem;
      border: none;
    }

    #search-bar {
      background-color: rgb(255, 255, 255);
      height: 4vh;
      width: 48vw;
      padding: 0 0 0 1rem;

      font-size: 1.5rem;
      gap: 2rem;

      border-style: none;
    }

    .button-search-bar {
      height: 4.5vh;
      width: 3vw;
      margin: 0 0 0 0.1rem;
      border-style: none;
      background-color: rgb(255,255,255);
      border-radius:0.5rem;
    }

    .icon-search {
      height: 2vh;
      width: 1vw;
    }

    #search-bar:focus {
      animation: jump 1s ease;
    }

    .button-search-bar:hover {
      animation: jump 1s ease, zoom 1s ease;
    }
  }

</style>