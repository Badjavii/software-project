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
    <h2>{{ perfil.firstName || perfil._firstName }} {{ perfil.lastName || perfil._lastName }}</h2>
    <p><strong>Correo:</strong> {{ perfil.email || perfil._email }}</p>
    <p><strong>Edad:</strong> {{ perfil.age || perfil._age }}</p>
  </div>
</template>

<style scoped>
.profile-card {
  width: 40vw;
  margin: 4rem auto;
  padding: 2.5rem;
  background-color: #fff;
  border-radius: 1rem;
  box-shadow: 0 0 15px rgba(0,0,0,0.15);
  font-family: sans-serif;
}

h2 {
  font-size: 5rem; /* antes 2rem */
  margin-bottom: 1.5rem;
}

p {
  font-size: 3rem; /* antes 1.2rem */
  margin: 0.8rem 0;
}

strong {
  font-size: 3rem; /* antes 1.2rem */
  margin: 0.8rem 0;
}

</style>