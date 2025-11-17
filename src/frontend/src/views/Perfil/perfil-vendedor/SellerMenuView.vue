<script setup lang="js">
</script>

<template>
  <article class="seller-menu-article">
  <section class= "seller-menu-section">
    <router-link class="r-button" to="/seller/registerBook">Registrar libro</router-link>
    <router-link class="r-button" to="/seller/registerBook">Borrar Libro</router-link>
    <router-link class="r-button" to="/seller/registerBook">Ver perfil</router-link>
  </section>
  </article>
</template>

<style scoped>

.seller-menu-article {
  height: 79vh;
  justify-items: center;
  align-content: center;
}

.seller-menu-section {
  display: flex;
  flex-direction: column;
  height: 60vh;
  width: 30vw;

  margin: 4rem 0 0 0;
  background-color: rgb(255,255,255);
  border-radius: 1rem;
  justify-content: center;
  align-items: center;
  gap: 4rem;

}

.r-button {
  height: 10vh;
  width: 20vw;
  background-color: rgb(0,117,235);
  align-content: center;
  text-align: center;

  font-size: 2rem;
  color: rgb(255,255,255);
  text-decoration: none;
  border-radius: 1rem;

}

.r-button:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
}


</style>