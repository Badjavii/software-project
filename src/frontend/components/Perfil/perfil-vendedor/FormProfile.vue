<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

const email = ref("");
const firstName = ref("");
const lastName = ref("");
const age = ref(null);
const password = ref("");
const bankName = ref("");
const accountNumber = ref("");
const phoneNumber = ref("");

const emit = defineEmits(["submit"]);
const router = useRouter();

function handleSubmit() {
  emit("submit", {
    email: email.value,
    firstName: firstName.value,
    lastName: lastName.value,
    age: age.value,
    password: password.value,
    bankName: bankName.value,
    accountNumber: accountNumber.value,
    phoneNumber: phoneNumber.value,
  });
}

function goToLogin() {
  router.push("/beginSellerSection");
}
</script>

<template>
  <form class="form-profile" @submit.prevent="handleSubmit">
    <h1>REGISTRAR VENDEDOR</h1>
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

    <label for="bankName">
      Banco
      <input id="bankName" v-model="bankName" placeholder="Nombre del banco" required />
    </label>

    <label for="accountNumber">
      Número de cuenta
      <input id="accountNumber" v-model="accountNumber" placeholder="1234567890" required />
    </label>

    <label for="phoneNumber">
      Teléfono
      <input id="phoneNumber" v-model="phoneNumber" placeholder="04141234567" required />
    </label>

    <!-- Botones alineados -->
    <div class="form-buttons">
      <input type="submit" value="Crear cuenta" />
      <button type="button" class="login-button" @click="goToLogin">Iniciar sesión</button>
    </div>
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

#email, #age, #password, #bankName, #accountNumber, #phoneNumber {
  width: 35vw;
}

input, button {
  padding: 1rem;
  font-size: 1.5rem;
  border: 1px solid #ccc;
  border-radius: 0.5rem;
}

/* Contenedor de botones en fila */
.form-buttons {
  display: flex;
  justify-content: center;
  gap: 2rem;
  margin-top: 2rem;
}

input[type="submit"] {
  width: 16vw;
  height: 7vh;
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

.login-button {
  width: 16vw;
  height: 7vh;
  border-radius: 1rem;
  background-color: #5e6a82;
  border: none;
  color: white;
  cursor: pointer;
}

.login-button:hover {
  background-color: #333;
  animation: jump 1s ease;
}

h1 {
  font-size: 2.5rem;
  margin: 2rem 0 1rem 0;
  color: rgb(0,0,0);
}
</style>

