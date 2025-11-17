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
  <form class="form-profile" @submit.prevent="handleSubmit">
    <h1>CREAR CUENTA</h1>
    <fieldset>
      <label for="nameInput">
        Nombre
        <input id="nameInput" v-model="firstName" placeholder="Nombre" required />
      </label>

      <label for="lastName">
        Apellido
        <input id="lastName" v-model="lastName" placeholder="Apellido" required />
      </label>
    </fieldset>

    <label for="email">
      Correo UCAB
      <input id="email" v-model="email" placeholder="example@est.ucab.edu.ve" required />
    </label>

    <label for="age">
      Edad
      <input id="age" v-model="age" type="number" placeholder="18" required />
    </label>

    <label for="password">
      Contraseña
      <input id="password" v-model="password" type="password" placeholder="Ingresar contraseña" required />
    </label>

    <fieldset class="fieldset-button">
      <input type="submit" value="Crear cuenta" />
      <router-link to="/beginSection" class="link">Iniciar sección</router-link>
    </fieldset>

  </form>
</template>

<style scoped>

form {
  display: flex;
  height: 75vh;
  flex-direction: column;
  align-items: center;
  padding: 2rem;
  gap: 2rem;
  width: 38vw;
  background-color: rgba(255,255,255);
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0,0,0,0.5);
}

fieldset {
  display: flex;
  justify-content: center;
  gap: 5rem;
}

label {
  display: flex;
  flex-direction: column;
  font-size: 1.5rem;
  gap: 1rem;
}

fieldset label input {
  width: 15vw;
}

form fieldset {
  border: none;
}

#email, #age, #password {
  width: 35vw;
}

input {
  padding: 1rem;
  font-size: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}

input[type="submit"] {
  width: 14vw;
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
  width: 14vw;

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
  margin: 1.5rem 0 0 0;
  gap: 3rem;
  border: none;
}

</style>
