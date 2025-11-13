<script setup>
import { useRouter } from 'vue-router';
import LoginForm from '../../../components/perfil-vendedor/LoginForm.vue';
import { loginVendedor } from '../../../services/sellerService.js';

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
  <article class="login-article">
    <section class="section-left"></section>
    <section class="section-right">
      <h1 class="login-title">INICIAR SESIÓN VENDEDOR</h1>
      <LoginForm @submit="handleLogin" />
    </section>
  </article>
</template>

<style scoped>
.login-article {
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

.login-title {
  font-size: 3rem;
  margin-bottom: 2rem;
  color: #333;
}
</style>
