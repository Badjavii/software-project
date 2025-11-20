<script setup lang="ts">

const props = defineProps({
  title: { type: String, required: true },
  imageSrc: { type: String, required: true },
  altText: { type: String, default: "Imagen de categoría" }
});
</script>

<template>
  <a class="card-link">
    <img class="card-img" :alt="altText" :src="imageSrc" />
    <section class="card-section">
      <h3 class="card-title">{{ title }}</h3>
    </section>
  </a>
</template>

<!-- Aquí importas el CSS externo -->
<style scoped src="../../assets/styles/components-style/category-card.css"></style>
