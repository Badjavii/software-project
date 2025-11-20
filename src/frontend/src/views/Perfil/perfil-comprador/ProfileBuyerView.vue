<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";

import ProfileCard from "../../../../components/Perfil/perfil-comprador/ProfileCard.vue";
import MessagePopup from "../../../../components/Common/MessagePopup.vue";

const perfil = ref<any>(null);
const showPopup = ref(false);
const popupMessage = ref("");
const popupType = ref("success");

const router = useRouter();

onMounted(() => {
  const userJson = localStorage.getItem("user");
  if (!userJson) return;

  try {
    perfil.value = JSON.parse(userJson);
  } catch (error: any) {
    popupMessage.value = "Error al cargar perfil desde sesión.";
    popupType.value = "error";
    showPopup.value = true;
  }
});

function cerrarSesion() {
  // Limpiar localStorage
  localStorage.removeItem("user");
  localStorage.removeItem("isBuyerLogged"); 
  localStorage.removeItem("isSellerLogged");

  // Mostrar popup
  popupMessage.value = "Has cerrado sesión exitosamente.";
  popupType.value = "success";
  showPopup.value = true;
}

function reloadAndGoHome() {
  location.href = "/"; 
}

</script>

<template>
  <section class="buyer-profile-section">
    <h1 class="titulo">Perfil del Comprador</h1>
    <ProfileCard v-if="perfil" :perfil="perfil" />

    <div class="logout-container">
      <button class="logout-button" @click="cerrarSesion">Cerrar sesión</button>
    </div>

    <!-- Mensaje emergente -->
    <MessagePopup
      v-if="showPopup"
      :message="popupMessage"
      :type="popupType"
      :buttonText="popupType === 'success' ? 'Ir al inicio' : 'Cerrar'"
      @action="popupType === 'success' ? reloadAndGoHome() : (showPopup = false)"
    />
  </section>
</template>

<style src="../../../../assets/styles/views/Perfil/profile-buyer.css"></style>
