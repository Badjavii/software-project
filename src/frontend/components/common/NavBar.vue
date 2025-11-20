<script setup lang="ts">
import { ref, onMounted } from "vue";
import SearchBar from "./SearchBar.vue";

const isLoggedIn = ref(false);
const isBuyer = ref(false);
const isSeller = ref(false);

onMounted(() => {
  isBuyer.value = localStorage.getItem("isBuyerLogged") === "true";
  isSeller.value = localStorage.getItem("isSellerLogged") === "true";
  isLoggedIn.value = isBuyer.value || isSeller.value;
});
</script>

<template>
  <nav id="nav-bar">
    <section class="nav-bar-one">
      <img
        class="navbar-img"
        alt="Logo de Booksy UCAB"
        src="../../assets/img/common/booksy-logo-horizontal.png"
      />
      <SearchBar />
    </section>
    <section class="nav-bar-two">
      <menu class="navbar-list">
        <li class="navbar-list-item">
          <router-link to="/" class="link">Inicio</router-link>
        </li>
        <li class="navbar-list-item">
          <router-link to="/catalog" class="link">Catálogo</router-link>
        </li>
        <li class="navbar-list-item">
          <router-link to="/books" class="link">Categorías</router-link>
        </li>
        <li class="navbar-list-item">
          <router-link to="/seller/sale" class="link">Vender</router-link>
        </li>
        <li class="navbar-list-item">
          <router-link to="/registerBuyerSection" class="link">Crear cuenta</router-link>
        </li>

        <!-- Botón dinámico -->
        <li class="navbar-list-item">
          <router-link v-if="!isLoggedIn" to="/beginSection" class="link">Ingresa</router-link>
          <router-link v-else-if="isSeller" to="/seller/profile" class="link">Mi Cuenta</router-link>
          <router-link v-else-if="isBuyer" to="/buyer/profile" class="link">Mi Cuenta</router-link>
        </li>
      </menu>
    </section>
  </nav>
</template>

<style scoped>

@media (min-width: 1600px) {
  #nav-bar {
    display: flex;
    flex-direction: column;
    height: 12vh;
    margin: 0;
    padding: 0;
    background-color: rgb(255,255,255);

    align-items: center;
  }

  .nav-bar-one {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: white;
    height: 8vh;
  }

  .nav-bar-two {
    display: flex;
    justify-content: right;
    align-items: center;
    background-color: rgb(255,255,255);
    height: 4vh;
  }
  .navbar-img {
    height: 7vh;
    margin: 0 0 0 8rem;
  }

  .navbar-list {
    display: flex;
    margin: 0 8rem 0 0;
    list-style: none;
    gap: 3.5rem;
  }

  .navbar-list-item {
    height: 3vh;
    width: 4.8vw;
    justify-content: center;
    align-content: center;
    text-align: center;
    border-radius: 1rem;
    transition: transform 0.3s ease;
  }

  :deep(.link) {
    height: 3vh;
    width: 4.8vw;

    display: inline-block;
    box-sizing: border-box;

    justify-items: center;
    align-content: center;

    text-decoration: none;
    color: black;
    font-size: 1.3em;
    font-weight: bold;
    border-radius: 1rem;
  }

  .navbar-list-item:hover{
    height: 3vh;
    width: 5vw;
    font-weight: bolder;
    background-color: rgb(146,230,255);
    animation: jump 0.3s ease, zoom 0.3s ease;
  }

  .navbar-img:hover {
    animation: jump 1s ease, zoom 1s ease;
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {
  #nav-bar {
    display: flex;
    flex-direction: column;
    height: 14vh;
    margin: 0;
    padding: 0;
    background-color: rgb(255,255,255);

    align-items: center;
  }

  .nav-bar-one {
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: white;
    height: 10vh;
  }

  .nav-bar-two {
    display: flex;
    justify-content: right;
    align-items: center;
    background-color: rgb(255,255,255);
    height: 4vh;
  }
  .navbar-img {
    height: 7vh;
    margin: 0 0 0 8rem;
  }

  .navbar-list {
    display: flex;
    margin: 0 8rem 0 0;
    list-style: none;
    gap: 3.5rem;
  }

  .navbar-list-item {
    height: 3vh;
    width: 6vw;
    justify-content: center;
    align-content: center;
    text-align: center;
    border-radius: 1rem;
    transition: transform 0.3s ease;
  }

  :deep(.link) {
    height: 3vh;
    width: 6vw;

    display: inline-block;
    box-sizing: border-box;

    justify-items: center;
    align-content: center;

    text-decoration: none;
    color: black;
    font-size: 1.3em;
    font-weight: bold;
    border-radius: 1rem;
  }

  .navbar-list-item:hover{
    height: 3vh;
    width: 6vw;
    font-weight: bolder;
    background-color: rgb(146,230,255);
    animation: jump 0.3s ease, zoom 0.3s ease;
  }

  .navbar-img:hover {
    animation: jump 1s ease, zoom 1s ease;
  }
}

</style>