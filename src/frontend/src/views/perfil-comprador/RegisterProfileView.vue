<script setup>
import { useRouter } from 'vue-router';

import FormularioPerfil
  from '../../components/perfil-comprador/FormProfile.vue';
import { registrarComprador } from '../../services/compradorService.js';

const router = useRouter();

async function handleRegistro(data) {
  try {
    await registrarComprador(data);
    localStorage.setItem("buyerEmail", data.email);
    localStorage.setItem("isBuyerLogged", "true");
    localStorage.setItem("isSellerLogged", "false");
    router.push('/comprador/consultar');
  } catch (error) {
    alert("Error al registrar: " + error.message);
  }
}
</script>

<template>
  <article class="registro-article">
    <section class="section-left"></section>
    <section class="section-right">
      <h1 class="registro-title">REGISTRO COMPRADOR</h1>
      <FormularioPerfil @submit="handleRegistro" />
    </section>
  </article>
</template>

<style scoped>
.registro-article {
  display: flex;
  justify-content: space-between;
  height: 88vh;
}

.section-left {
  width: 20vw;
  background-color: rgba(245,245,245);
}

.section-right {
  padding: 2rem;
  width: 80vw;
}

.registro-title {
  font-size: 3rem;
  margin-bottom: 2rem;
  color: #333;
}
</style>
