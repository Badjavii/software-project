<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

import LoginFormSeller from "../../../../components/perfil/perfil-vendedor/LoginFormSeller.vue";
import MessagePopup from "../../../../components/Common/MessagePopup.vue";
import { loginUsuario } from "../../../../services/Perfil/userService.js";

const router = useRouter();

const showPopup = ref(false);
const popupMessage = ref("");
const popupType = ref("error");

async function handleLogin(data: { email: string; password: string }) {
  try {
    const response = await loginUsuario(data.email, data.password);

    if (response.status === 200) {
      const user = response.data;

      // Guardar objeto completo en localStorage
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("isBuyerLogged", "true"); 
      localStorage.setItem("isSellerLogged", "true");

      popupMessage.value = "Inicio de sesión exitoso como vendedor.";
      popupType.value = "success";
      showPopup.value = true;
    } else {
      popupMessage.value = "Credenciales incorrectas.";
      popupType.value = "error";
      showPopup.value = true;
    }
  } catch (error: any) {
    popupMessage.value =
      error.response?.data?.error || "Error al iniciar sesión.";
    popupType.value = "error";
    showPopup.value = true;
  }
}

function reloadAndGoHome() {
  location.href = "/"; // recarga y manda al inicio
}
</script>

<template>
  <article class="registro-article">
    <section class="register-section">
      <section class="section-left"></section>
      <section class="section-right">
        <LoginFormSeller @submit="handleLogin" />
      </section>
    </section>

    <!-- Popup de mensajes -->
    <MessagePopup
      v-if="showPopup"
      :message="popupMessage"
      :type="popupType"
      :buttonText="popupType === 'success' ? 'Ir al inicio' : 'Cerrar'"
      @action="popupType === 'success' ? reloadAndGoHome() : (showPopup = false)"
    />
  </article>
</template>

<style scoped>
article {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 86vh;
}

.register-section {
  display: flex;
  height: 70vh;
  width: 60vw;
  background-color: rgb(255, 255, 255);
  border-radius: 1rem;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 0 10px rgba(0, 44, 235, 1);
}

.section-left {
  width: 25vw;
  height: 70vh;
  background-image: url("../../../../assets/img/consultar-libros-img/medium-shot-student-holding-books-stack.jpg");
  border-radius: 1rem;
  background-position: center;
  background-size: cover;
}

.section-right {
  width: 35vw;
  height: 60vh;
  justify-items: center;
  align-content: center;
}
</style>
