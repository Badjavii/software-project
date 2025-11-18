<script setup lang="js">

defineProps({
  book: {type: Object, required: true}
})
</script>

<template>
  <section class="book-detail-section">
    <h1 class="book-detail-title">{{ book._nameBook }}</h1>
    <p class="book-detail-cost">${{ book._cost }}</p>
    <p class="book-detail-delivery">Entrega: Acordar con el vendedor</p>
  </section>

  <section class="book-detail-seller-section" v-if="book && book._seller">
    <h2 class="seller-information">Información sobre el vendedor</h2>
    <p class="seller-name">Nombre del vendedor: {{ book._seller._firstName }}</p>
    <p class="seller-phone">Calificación del vendedor: {{ book._seller._qualification }}</p>
    <p class="seller-email">Correo UCAB: {{ book._seller._email }}</p>
  </section>

  <router-link class="book-detail-link" to="/books">Comprar ahora</router-link>
</template>

<style scoped>

.book-detail-section {
  height: 30vh;

  width: 15vw;

  background-color: rgb(255,255,255);
  justify-items: center;
  align-content: center;
  border-radius: 1rem;
}

.book-detail-title {
  font-size: 3rem;
  text-align: center;
}

.book-detail-cost {
  font-size: 2.8rem;
  font-weight: lighter;
}

.book-detail-seller-section {
  margin: 2rem;
  height: 20vh;
  width: 15vw;

  background-color: rgb(255,255,255);
  justify-items: center;
  align-content: center;
  border-radius: 1rem;
}

.seller-information {
  font-size: 1.6rem;
  font-weight: bold;
}

.seller-name, .seller-phone, .seller-email {
  font-size: 1.4rem;
  font-weight: lighter;
}

.book-detail-link {
  height: 5vh;
  width: 15vw;
  margin: 0 0 0 8%;

  display: inline-block;
  box-sizing: border-box;

  justify-items: center;
  align-content: center;
  text-align: center;

  text-decoration: none;
  background-color: rgb(0,117,235);
  color: rgb(255,255,255);
  font-size: 1.3em;
  font-weight: bold;
  border-radius: 1rem;
}

.book-detail-link:hover {
  background-color: rgb(0,44,235);
  animation: jump 1s ease;
}

</style>