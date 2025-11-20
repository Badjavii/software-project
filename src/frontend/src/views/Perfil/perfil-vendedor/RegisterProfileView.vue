<script setup lang="ts">
import { ref } from "vue";
import { useRouter } from "vue-router";

import FormProfile from "../../../../components/perfil/perfil-vendedor/FormProfile.vue";
import MessagePopup from "../../../../components/Common/MessagePopup.vue";
import { registrarVendedor } from "../../../../services/Perfil/userService.js";

const router = useRouter();

const showPopup = ref(false);
const popupMessage = ref("");
const popupType = ref("error");

async function handleRegistro(data: any) {
  try {
    await registrarVendedor(
      {
        email: data.email,
        firstName: data.firstName,
        lastName: data.lastName,
        age: data.age,
        password: data.password,
      },
      data.bankName,
      data.accountNumber,
      data.phoneNumber
    );

    localStorage.setItem("user", JSON.stringify(data));

    popupMessage.value = "¡Felicidades! Ya eres un vendedor en Booksy UCAB.";
    popupType.value = "success";
    showPopup.value = true;
  } catch (error: any) {
    popupMessage.value = error.response?.data?.error || "Error al registrar vendedor.";
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

    <MessagePopup
      v-if="showPopup"
      :message="popupMessage"
      :type="popupType"
      :buttonText="popupType === 'success' ? 'Ir al inicio' : 'Cerrar'"
      @action="popupType === 'success' ? reloadAndGoHome() : (showPopup = false)"
    />
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
