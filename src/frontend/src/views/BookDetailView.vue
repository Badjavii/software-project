<script setup lang="ts">

import Characteristics from "../../components/consultar-libros/book-detail/Characteristics.vue";
import Description from "../../components/consultar-libros/book-detail/Description.vue";
import CommentarySection from "../../components/consultar-libros/book-detail/CommentarySection.vue";
import BookCardDetail from "../../components/consultar-libros/book-detail/BookCardDetail.vue";
</script>

<template>
  <article class="article-book-detail">
    <section class="section-left">
      <Characteristics></Characteristics>
      <Description></Description>
      <CommentarySection></CommentarySection>
    </section>
    <section class="section-right">
      <BookCardDetail></BookCardDetail>
    </section>
  </article>
</template>

<style scoped>

.article-book-detail {
  display: flex;
  height: 120vh;
  width: 58vw;

  margin: 8rem 0 8rem 0;
  padding: 2rem;

  justify-self: center;
  justify-content: space-between;

  background-color: rgba(255,255,255);
  border-radius: 1rem;
}

.section-left {
  width: 38vw;
}

.section-right {
  width: 20vw;
}


</style>