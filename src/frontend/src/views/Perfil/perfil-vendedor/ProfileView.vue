<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";

import ProfileCard from "../../../../components/Perfil/perfil-vendedor/ProfileCard.vue";
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
  <section class="seller-profile-section">
    <h1 class="titulo">Perfil del Vendedor</h1>
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

<style scoped>
.seller-profile-section {
  display: flex;
  flex-direction: column;
  align-items: center;
  min-height: 80vh;
  background-color: rgb(255, 255, 255);
  border-radius: 1rem;
  box-shadow: 0 0 10px rgba(0, 44, 235, 0.5);
  margin: 2rem auto;
  padding: 2rem;
  width: 70vw;
}

.titulo {
  text-align: center;
  font-size: 2.5rem;
  margin-bottom: 2rem;
  color: rgb(0, 0, 0);
}

.logout-container {
  margin-top: 2rem;
}

.logout-button {
  background-color: rgb(5, 33, 52);
  color: white;
  border: none;
  border-radius: 0.6rem;
  padding: 0.8rem 1.5rem;
  font-size: 1.2rem;
  cursor: pointer;
  font-weight: bold;
}

.logout-button:hover {
  background-color: rgb(0, 44, 235);
  animation: jump 0.5s ease;
}
</style>
