<script setup lang="ts">

import Discovery from "../../../components/Libro/consultar-libro/Discovery.vue";
import WelcomeInterface from "../../../components/Libro/consultar-libro/WelcomeInterface.vue";
import HorizontalList from "../../../components/Libro/consultar-libro/lists/HorizontalList.vue";
</script>

<template>
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