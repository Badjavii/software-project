<script setup>
import { useRouter } from 'vue-router';

import FormularioPerfil
  from '../../../../components/perfil/perfil-comprador/FormProfile.vue';
import { registrarComprador } from '../../../../services/Perfil/buyerService.js';
import FormProfile from '../../../../components/perfil/perfil-comprador/FormProfile.vue';

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
    <section class="register-section">
      <section class="section-left">
      </section>
      <section class="section-right">
        <FormProfile @submit="handleRegistro"></FormProfile>
      </section>
    </section>
  </article>
</template>

<style scoped>

.registro-article {
  display: flex;
  justify-content:center;
  align-items: center;
  height: 100vh;

}

.register-section {
  display: flex;
  height: 85vh;
  width: 76vw;
  background-color: rgb(255,255,255);
  border-radius: 1rem;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 0 10px rgba(0,44,235,1);
}

.section-left {
  width: 35vw;
  height: 85vh;
  background-image: url("../../../../assets/img/consultar-libros-img/from-opened-books.jpg");
  border-radius: 1rem;
  background-position: center;
  background-size: cover;
}

.section-right {
  width: 45vw;
  height: 85vh;
  justify-items: center;
  align-content: center;
}

</style>