<script setup lang="ts">
  import WelcomeInterface from "../components/consultar-libros/WelcomeInterface.vue";
  import HorizontalList from "../components/consultar-libros/lists/HorizontalList.vue";
  import NavBar from "../components/common/NavBar.vue";
  import Discovery from "../components/consultar-libros/Discovery.vue";

  // CSS Imports
  import '../assets/styles/animations.css';

</script>

<template>
  <NavBar></NavBar>
  <main id="welcome-page">
    <WelcomeInterface></WelcomeInterface>
    <Discovery></Discovery>
    <HorizontalList></HorizontalList>
  </main>
</template>

<style scoped>

  #welcome-page{
     display: flex;
    flex-direction: column;
    border: none;
  }

</style>
