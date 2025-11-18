<script setup lang="ts">
import CardEcosystemOne from "./ecosystem-card/CardEcosystemOne.vue";
import CardEcosystemTwo from "./ecosystem-card/CardEcosystemTwo.vue";
import CardEcosystemThree from "./ecosystem-card/CardEcosystemThree.vue";
</script>

<template>
  <article class="welcome-article">
    <cite class="text">"Para viajar lejos, no hay mejor nave que un libro."</cite>
    <ul class="filtrer-bar">
      <li class="card"><CardEcosystemOne></CardEcosystemOne></li>
      <li class="card"><CardEcosystemTwo></CardEcosystemTwo></li>
      <li class="card"><CardEcosystemThree></CardEcosystemThree></li>
    </ul>
  </article>
</template>

<style scoped>

@media (min-width: 1600px){


  .welcome-article {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100vh;

    background-image: linear-gradient(to bottom, rgba(0, 0, 0, 0.5), transparent),
    linear-gradient(to top, rgb(5, 33, 52), transparent),
    url("../../../assets/img/consultar-libros-img/background.png");
    background-position: center;
    background-size: cover;
    background-repeat: no-repeat;

    align-items: center;
    font-weight: bolder;

  }

  .text {
    text-align: center;
    align-content: center;
    height: 10%;
    width: 100%;

    margin: 0 0 10rem 0;
    font-size: 2.5rem;
    color: white;
  }

  .filtrer-bar {
    display: flex;
    list-style: none;
    gap: 3rem;
  }

  .card {
    flex-flow: wrap;
  }

  .card:hover {
    animation: jump 1s ease;
  }
}

@media (min-width: 1200px) and (max-width: 1599px) {


  .welcome-article {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 110vh;

    background-image: linear-gradient(to bottom, rgba(0, 0, 0, 0.5), transparent),
    linear-gradient(to top, rgb(5, 33, 52), transparent),
    url("../../../assets/img/consultar-libros-img/background.png");
    background-position: center;
    background-size: cover;
    background-repeat: no-repeat;

    align-items: center;
    font-weight: bolder;

  }

  .text {
    text-align: center;
    align-content: center;
    height: 10%;
    width: 100%;

    margin: 0 0 10rem 0;
    font-size: 2.5rem;
    color: white;
  }

  .filtrer-bar {
    display: flex;
    list-style: none;
    gap: 3rem;
  }

  .card {
    flex-flow: wrap;
  }

  .card:hover {
    animation: jump 1s ease;
  }
}

</style>