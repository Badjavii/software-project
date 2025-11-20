<script setup>
defineProps({
  perfil: {
    type: Object,
    required: true
  }
});
</script>

<template>
  <div class="profile-card">
    <h2>{{ perfil.firstName || perfil._firstName }} {{ perfil.lastName || perfil._lastName }}</h2>
    <p><strong>Correo:</strong> {{ perfil.email || perfil._email }}</p>
    <p><strong>Edad:</strong> {{ perfil.age || perfil._age }}</p>

    <!-- Datos extra de vendedor -->
    <p v-if="perfil.sellerInfo || perfil._sellerInfo">
      <strong>Banco:</strong> {{ perfil.sellerInfo?.bankName || perfil._sellerInfo?._bankName }}
    </p>
    <p v-if="perfil.sellerInfo || perfil._sellerInfo">
      <strong>Número de cuenta:</strong> {{ perfil.sellerInfo?.accountNumber || perfil._sellerInfo?._accountNumber }}
    </p>
    <p v-if="perfil.sellerInfo || perfil._sellerInfo">
      <strong>Teléfono:</strong> {{ perfil.sellerInfo?.phoneNumber || perfil._sellerInfo?._phoneNumber }}
    </p>
  </div>
</template>

<style scoped>
.profile-card {
  width: 40vw;
  margin: 4rem auto;
  padding: 2.5rem;
  background-color: #fff;
  border-radius: 1rem;
  box-shadow: 0 0 15px rgba(0,0,0,0.15);
  font-family: sans-serif;
}

h2 {
  font-size: 5rem; /* más grande */
  margin-bottom: 1.5rem;
}

p {
  font-size: 3rem; /* más grande */
  margin: 0.8rem 0;
}

strong {
  font-size: 3rem;
}
</style>
