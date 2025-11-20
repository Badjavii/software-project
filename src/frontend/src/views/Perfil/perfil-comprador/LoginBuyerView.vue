<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

import LoginForm from "../../../../components/perfil/perfil-comprador/LoginForm.vue";
import { loginUsuario } from "../../../../services/Perfil/userService.js";
import MessagePopup from "../../../../components/Common/MessagePopup.vue";

const router = useRouter();

const showPopup = ref(false);
const popupMessage = ref("");
const popupType = ref("error");

async function handleLogin(data: { email: string; password: string }) {
  try {
    const response = await loginUsuario(data.email, data.password);

    if (response.status === 200) {
      // Guardar el objeto user completo en localStorage
      const user = response.data;
      localStorage.setItem("user", JSON.stringify(user));
      localStorage.setItem("isBuyerLogged", "true"); 
      localStorage.setItem("isSellerLogged", "false");

      popupMessage.value = "Inicio de sesión exitoso.";
      popupType.value = "success";
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
  location.href = "/"; 
}

</script>

<template>
  <article class="registro-article">
    <section class="register-section">
      <section class="section-left"></section>
      <section class="section-right">
        <LoginForm @submit="handleLogin" />
      </section>
    </section>

    <!-- Mensaje emergente -->
    <MessagePopup
      v-if="showPopup"
      :message="popupMessage"
      :type="popupType"
      :buttonText="popupType === 'success' ? 'Ir al inicio' : 'Cerrar'"
      @action="popupType === 'success' ? reloadAndGoHome() : (showPopup = false)"
    />
  </article>
</template>

<style src="../../../../assets/styles/views/Perfil/login-buyer.css"></style>
