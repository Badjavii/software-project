<script setup lang="js">
  import NavBar from "../components/common/NavBar.vue";

  // CSS Imports
  import '../assets/styles/animations.css';

</script>

<template>
  <header>
    <NavBar></NavBar>
  </header>
  <main id="welcome-page">
    <router-view />
  </main>
</template>

<style scoped>
</style>
