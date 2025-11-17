<script setup lang="ts">

import Discovery from "../../../components/consultar-libros/Discovery.vue";
import WelcomeInterface from "../../../components/consultar-libros/WelcomeInterface.vue";
import HorizontalList from "../../../components/consultar-libros/lists/HorizontalList.vue";
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