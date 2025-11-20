<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

import FormProfile from "../../../../components/perfil/perfil-comprador/FormProfile.vue";
import { registrarComprador } from "../../../../services/Perfil/userService.js";
import MessagePopup from "../../../../components/Common/MessagePopup.vue";

const router = useRouter();

const showPopup = ref(false);
const popupMessage = ref("");
const popupType = ref("error");

async function handleRegistro(data: any) {
  try {
    await registrarComprador(data);

    // Mostrar popup de éxito
    popupMessage.value = "Usuario registrado exitosamente.";
    popupType.value = "success";
    showPopup.value = true;
  } catch (error: any) {
    // Mostrar popup de error con el mensaje del backend
    popupMessage.value = error.response?.data?.error || "Error al registrar.";
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
        <FormProfile @submit="handleRegistro" />
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

<style src="../../../../assets/styles/views/Perfil/register-buyer.css"></style>
