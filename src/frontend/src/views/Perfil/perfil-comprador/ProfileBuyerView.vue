<script setup>
import { ref, onMounted } from 'vue';
import ProfileCard from '../../../../components/perfil-comprador/ProfileCard.vue';
import { consultarComprador } from '../../../../services/Perfil/buyerService.js';

const perfil = ref(null);

onMounted(async () => {
  const email = localStorage.getItem("buyerEmail");
  if (!email) return;

  try {
    const response = await consultarComprador("Nombre", "Apellido"); // puedes usar email si el backend lo permite
    perfil.value = response.data;
  } catch (error) {
    alert("Error al consultar perfil: " + error.message);
  }
});
</script>

<template>
  <section>
    <h1 class="titulo">Perfil del Comprador</h1>
    <ProfileCard v-if="perfil" :perfil="perfil" />
  </section>
</template>

<style scoped>
.titulo {
  text-align: center;
  font-size: 2.5rem;
  margin-top: 2rem;
}
</style>
