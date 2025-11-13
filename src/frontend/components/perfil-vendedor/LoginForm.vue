<script setup>
import { ref } from 'vue';

const email = ref('');
const password = ref('');

const emit = defineEmits(['submit']);

function handleSubmit() {
  emit('submit', {
    email: email.value,
    password: password.value
  });
}
</script>

<template>
  <form class="formulario-login" @submit.prevent="handleSubmit">
    <input v-model="email" type="email" placeholder="Correo UCAB" required />
    <input v-model="password" type="password" placeholder="Contraseña" required />
    <button type="submit">Iniciar Sesión</button>
  </form>
</template>

<style scoped>
.formulario-login {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  width: 40vw;
  margin: 4rem auto;
  padding: 2rem;
  background-color: rgba(255,255,255);
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

input {
  padding: 0.8rem;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}

button {
  padding: 1rem;
  background-color: #0077cc;
  color: white;
  border: none;
  border-radius: 0.5rem;
  font-weight: bold;
  cursor: pointer;
}

button:hover {
  background-color: #005fa3;
}
</style>