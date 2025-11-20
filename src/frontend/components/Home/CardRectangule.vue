<script setup lang="ts">
import { defineProps } from "vue";

const props = defineProps({
  text: { type: String, required: true },
  title: { type: String, required: true },
  buttonText: { type: String, required: true },
  imageSrc: { type: String, required: true },
  altText: { type: String, default: "Imagen de card" },
  href: { type: String, default: "#" }
});
</script>

<template>
  <a class="card-link" :href="href">
    <section class="card-section">
      <p class="card-text">{{ text }}</p>
      <h3 class="card-title">{{ title }}</h3>
      <button class="card-button">{{ buttonText }}</button>
    </section>
    <img class="card-img" :alt="altText" :src="imageSrc" />
  </a>
</template>

<!-- Aquí importas el CSS externo -->
<style scoped src="../../assets/styles/components-style/card-rectangle.css"></style>
