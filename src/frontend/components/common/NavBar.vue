<script setup lang="ts">

import SearchBar from "./SearchBar.vue";
</script>

<template>
  <nav id="nav-bar">
    <section class="nav-bar-one">
      <img class="navbar-img" alt="Logo de Booksy UCAB" src="../../assets/img/common/booksy-logo-horizontal.png" />
      <SearchBar></SearchBar>
    </section>
    <section class="nav-bar-two">
      <menu class="navbar-list">
        <li class="navbar-list-item"><a class="link">Inicio</a></li>
        <li class="navbar-list-item"><a class="link">Categorías</a></li>
        <li class="navbar-list-item"><a class="link">Vender</a></li>
        <li class="navbar-list-item"><a class="link">Crear cuenta</a></li>
        <li class="navbar-list-item"><a class="link">Ingresa</a></li>
        <li class="navbar-list-item"><a class="link">Mis compras</a></li>
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

  .link {
    height: 3vh;
    width: 5vw;
    font-size: 1.3em;
    font-weight: bold;
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

@media (min-width: 1200px) {
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

  .link {
    height: 3vh;
    width: 6vw;
    font-size: 1.1em;
    font-weight: bold;
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