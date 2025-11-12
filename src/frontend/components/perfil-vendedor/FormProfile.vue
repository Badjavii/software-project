<script setup>
import { ref } from 'vue';

const email = ref('');
const firstName = ref('');
const lastName = ref('');
const age = ref(null);
const password = ref('');

const emit = defineEmits(['submit']);

function handleSubmit() {
  emit('submit', {
    email: email.value,
    firstName: firstName.value,
    lastName: lastName.value,
    age: age.value,
    password: password.value
  });
}
</script>

<template>
  <form class="formulario-perfil" @submit.prevent="handleSubmit">
    <input v-model="email" placeholder="Correo UCAB" required />
    <input v-model="firstName" placeholder="Nombre" required />
    <input v-model="lastName" placeholder="Apellido" required />
    <input v-model="age" type="number" placeholder="Edad" required />
    <input v-model="password" type="password" placeholder="Contraseña" required />
    <button type="submit">Registrarse</button>
  </form>
</template>

<style scoped>
.formulario-perfil {
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
