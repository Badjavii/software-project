<script setup lang="ts">

import TableOne from "./TableOne.vue";
import TableTwo from "./TableTwo.vue";
import TableThree from "./TableThree.vue";

defineProps({
  book: { type: Object, required: true }
})
</script>

<template>
  <article class="characteristics-article">
      <h2 class="characteristics-title">Características del libro</h2>
      <section class="main-section">
        <div>
          <TableOne :book = "book"></TableOne>
        </div>
        <div>
          <TableThree :book = "book"></TableThree>
          <TableTwo :book = "book"></TableTwo>
        </div>
      </section>
  </article>
</template>

<style scoped>

.characteristics-article {
  display: flex;
  flex-direction: column;
  width: 60vw;
  height: 35vw;
}

.characteristics-title{
  height: 5vh;
  width: 30vw;
  font-size: 3rem;
  font-weight: lighter;
}

.main-section {
  margin: 5rem 5rem 5rem 0;
  display: flex;
  gap: 7rem;
}

</style>