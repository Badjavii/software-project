<script setup>
import { useRouter } from 'vue-router';
import FormProfile from '../../../components/perfil-vendedor/FormProfile.vue';
import { registrarVendedor } from '../../../services/sellerService.js';

const router = useRouter();

async function handleRegistro(data) {
  try {
    await registrarVendedor(data);
    localStorage.setItem("sellerEmail", data.email);
    localStorage.setItem("isSellerLogged", "true");
    localStorage.setItem("isBuyerLogged", "false");
    router.push('/vendedor/consultar');
  } catch (error) {
    alert("Error al registrar vendedor: " + error.message);
  }
}
</script>

<template>
  <article class="registro-article">
    <section class="section-left"></section>
    <section class="section-right">
      <h1 class="registro-title">REGISTRO VENDEDOR</h1>
      <FormProfile @submit="handleRegistro" />
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
