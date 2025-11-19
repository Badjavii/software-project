<script setup>
import { useRouter } from 'vue-router';
import LoginFormSeller from '../../../../components/perfil/perfil-vendedor/LoginFormSeller.vue';
import { loginVendedor } from '../../../../services/Perfil/sellerService.js';
import FormProfile from "../../../../components/perfil/perfil-vendedor/FormProfile.vue";

const router = useRouter();

async function handleLogin(data) {
  try {
    const response = await loginVendedor(data);
    if (response.data.success) {
      localStorage.setItem("sellerEmail", data.email);
      localStorage.setItem("isSellerLogged", "true");
      localStorage.setItem("isBuyerLogged", "false");
      router.push('/vendedor/consultar');
    } else {
      alert("Credenciales incorrectas");
    }
  } catch (error) {
    alert("Error al iniciar sesión: " + error.message);
  }
}
</script>

<template>
  <article class="registro-article">
    <section class="register-section">
      <section class="section-left">
      </section>
      <section class="section-right">
        <LoginFormSeller @submit="handleLogin"></LoginFormSeller>
      </section>
    </section>
  </article>
</template>

<style scoped>

article {
  display: flex;
  justify-content:center;
  align-items: center;
  height: 86vh;
}

.register-section {
  display: flex;
  height: 70vh;
  width: 60vw;
  background-color: rgb(255,255,255);
  border-radius: 1rem;
  justify-content: space-between;
  align-items: center;
  box-shadow: 0 0 10px rgba(0,44,235,1);
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
