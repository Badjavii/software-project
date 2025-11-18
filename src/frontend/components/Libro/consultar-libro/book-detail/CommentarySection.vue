<script setup lang="ts">

</script>

<template>
  <form class="commentary-form">
    <label for="commentary" class="commentary-label">Deja un comentario</label>
    <input id="commentary" class="commentary-input" placeholder="Escribe tu comentario..." type="text" />
    <button class="commentary-button">Enviar comentario</button>
  </form>
</template>

<style scoped>

.commentary-form {
  display: flex;
  flex-direction: column;
  height: 15vh;
}

.commentary-label {
  margin: 0 0 2rem 0;
  font-size: 2.5rem;
}

.commentary-input {
  height: 3vh;
  margin: 0 0 2rem 0;
  padding: 0 0 0 1rem;

  border-style: none;
  border-bottom-style: solid;
  border-color: rgb(200,200,200);

  font-size: 1.8rem;
}

.commentary-input:focus {
  border: none;
}

.commentary-button {
  height: 4vh;
  width: 10vw;

  border-radius: 1rem;
  border: none;

  background-color: rgb(0,117,235);
  color: rgb(255,255,255);

  font-size: 1.3rem;
}

.commentary-button:hover {
  background-color: rgb(0,44,235);
  animation: jump 0.3s ease;
  color: rgb(255,255,255);
}


</style>