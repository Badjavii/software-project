<script setup>
defineProps({
  perfil: {
    type: Object,
    required: true
  }
});
</script>

<template>
  <div class="profile-card">
    <h2>{{ perfil.firstName }} {{ perfil.lastName }}</h2>
    <p><strong>Correo:</strong> {{ perfil.email }}</p>
    <p><strong>Edad:</strong> {{ perfil.age }}</p>
  </div>
</template>

<style scoped>
.profile-card {
  width: 40vw;
  margin: 4rem auto;
  padding: 2rem;
  background-color: #fff;
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
  font-family: sans-serif;
}

h2 {
  font-size: 2rem;
  margin-bottom: 1rem;
}

p {
  font-size: 1.2rem;
  margin: 0.5rem 0;
}
</style>