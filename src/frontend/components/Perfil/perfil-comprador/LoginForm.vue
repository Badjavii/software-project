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
  <form @submit.prevent="handleSubmit">
    <h1>Log-in</h1>
    <label>
      Correo UCAB
      <input v-model="email" type="email" placeholder="example@est.ucab.edu.ve" required />
    </label>
    <label>
      Contraseña
      <input v-model="password" type="password" placeholder="Ingresar contraseña" required />
    </label>
    <fieldset class="fieldset-button">
      <input type="submit" value="Iniciar sección" />
      <router-link to="/registerBuyerSection" class="link">Crear cuenta</router-link>
    </fieldset>
  </form>
</template>

<style scoped>

form {
  display: flex;
  height: 55vh;
  flex-direction: column;
  justify-items: center;
  align-items: center;
  gap: 2rem;
  width: 25vw;
  padding: 1rem;
  background-color: rgba(255,255,255);
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

label {
  display: flex;
  flex-direction: column;
  font-size: 1.5rem;
  gap: 1rem;
}

input {
  padding: 1rem;
  width: 20vw;
  font-size: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}

input[type="submit"] {
  width: 10vw;
  height: 7.5vh;
  border-radius: 1rem;
  background-color: #0077cc;
  border: none;
  color: rgb(255,255,255);
}

input[type="submit"]:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
  cursor: pointer;
}

h1 {
  font-size: 2.5rem;
  margin: 2rem 0 1rem 0;
  color: rgb(0,0,0);
}

:deep(.link) {
  height: 7.5vh;
  width: 10vw;

  display: inline-block;
  box-sizing: border-box;

  text-align: center;
  align-content: center;

  text-decoration: none;
  color: white;
  font-size: 1.5rem;
  border-radius: 1rem;
  background-color: #0077cc;
}

:deep(.link):hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
  cursor: pointer;
}

.fieldset-button {
  display: flex;
  flex-direction: row;
  justify-content: center;
  width: 25vw;
  margin: 2rem 0 0 0;
  gap: 3rem;
  border: none;
}

</style>