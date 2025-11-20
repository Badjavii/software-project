<script setup lang="ts">

const props = defineProps({
  message: { type: String, required: true },
  buttonText: { type: String, default: "Cerrar" },
  type: { type: String, default: "error" } // "error" | "success"
});

// El padre decide qué hacer con la acción
const emit = defineEmits(["action"]);
function handleAction() {
  emit("action");
}
</script>

<template>
  <div class="popup-overlay">
    <div class="popup-container" :class="type">
      <p class="popup-message">{{ message }}</p>
      <button class="popup-button" @click="handleAction">
        {{ buttonText }}
      </button>
    </div>
  </div>
</template>

<style scoped src="../../assets/styles/components-style/message-popup.css"></style>