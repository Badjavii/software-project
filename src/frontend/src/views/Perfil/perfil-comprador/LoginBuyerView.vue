<script setup>
import { useRouter } from 'vue-router';
import LoginForm from '../../../../components/perfil/perfil-comprador/LoginForm.vue';
import { loginComprador } from '../../../../services/Perfil/buyerService.js';
import LoginFormSeller from "../../../../components/perfil/perfil-vendedor/LoginFormSeller.vue";

const router = useRouter();

async function handleLogin(data) {
  try {
    const response = await loginComprador(data);
    if (response.data.success) {
      localStorage.setItem("buyerEmail", data.email);
      localStorage.setItem("isBuyerLogged", "true");
      localStorage.setItem("isSellerLogged", "false");
      router.push('/comprador/consultar');
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
        <LoginForm @submit="handleLogin"></LoginForm>
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

  align-content: end;
  text-align: center;
}

.section-right {
  width: 35vw;
  height: 60vh;
  justify-items: center;
  align-content: center;
}

</style>