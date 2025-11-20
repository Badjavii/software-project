<script setup lang="ts">
import { defineProps } from "vue";

const props = defineProps({
  title: { type: String, required: true },
  description: { type: String, required: true },
  buttonText: { type: String, required: true },
  imageSrc: { type: String, required: true },
  altText: { type: String, default: "Imagen de card" },
  to: { type: String, default: "" },   // para router-link
  href: { type: String, default: "" }  // para enlaces externos
});
</script>

<template>
  <!-- Si es ruta interna -->
  <router-link v-if="to" class="link-card" :to="to">
    <h2 class="title-card">{{ title }}</h2>
    <img class="image-card" :alt="altText" :src="imageSrc" />
    <p class="description-card">{{ description }}</p>
    <button class="button-card">{{ buttonText }}</button>
  </router-link>

  <!-- Si es enlace externo -->
  <a v-else-if="href" class="link-card" :href="href" target="_blank">
    <h2 class="title-card">{{ title }}</h2>
    <img class="image-card" :alt="altText" :src="imageSrc" />
    <p class="description-card">{{ description }}</p>
    <button class="button-card">{{ buttonText }}</button>
  </a>
</template>

<!-- Aquí importas el CSS externo -->
<style scoped src="../../assets/styles/components-style/card-ecosystem.css"></style>