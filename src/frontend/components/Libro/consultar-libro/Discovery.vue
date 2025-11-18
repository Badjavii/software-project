<script setup lang="ts">
import CardRectangleThree from "./discovery/CardRectangleThree.vue";
import CardRectangleTwo from "./discovery/CardRectangleTwo.vue";
import CardRectangleOne from "./discovery/CardRectangleOne.vue";
import CardRectangleFour from "./discovery/CardRectangleFour.vue";
</script>

<template>
  <article class="discovery-article">
    <section class="discovery-section">
      <CardRectangleOne class="discovery-card"></CardRectangleOne>
      <CardRectangleTwo class="discovery-card"></CardRectangleTwo>
    </section>
    <section class="discovery-section">
      <CardRectangleThree class="discovery-card"></CardRectangleThree>
      <CardRectangleFour class="discovery-card"></CardRectangleFour>
    </section>
  </article>
</template>

<style scoped>

 .discovery-article {
   display: flex;
   flex-direction: column;
   height: 57vh;
   margin: 5rem 0 5rem 0;
   gap: 2rem;
 }

 .discovery-section {
   display: flex;
   flex-direction: row;
   justify-content: center;
   gap: 2rem

 }

 .discovery-card:hover{
   animation: jump 1s ease;
 }

</style>