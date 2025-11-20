<script setup lang="ts">
import CardEcosystem from "../../components/Home/CardEcosystem.vue";
import CardRectangle from "../../components/Home/CardRectangule.vue";
import CategoryCard from "../../components/Home/CategoryCard.vue";

const categories = [
  {
    title: "Matemáticas y Estadística",
    imageSrc: "/assets/img/consultar-libros-img/math-category.jpg",
    altText: "Libro de matemáticas"
  },
  {
    title: "Ficción",
    imageSrc: "/assets/img/consultar-libros-img/fiction-category.jpg",
    altText: "Libro de ficción"
  },
  {
    title: "No Ficción",
    imageSrc: "/assets/img/consultar-libros-img/no-fiction-category.jpg",
    altText: "Libro de no ficción"
  },
  {
    title: "Arte y diseño",
    imageSrc: "/assets/img/consultar-libros-img/art-design-category.png",
    altText: "Libro de arte y diseño"
  },
  {
    title: "Literatura clásica",
    imageSrc: "/assets/img/consultar-libros-img/classic-literature-category.jpg",
    altText: "Libro de literatura clásica"
  },
  {
    title: "Poesía",
    imageSrc: "/assets/img/consultar-libros-img/poesia-category.jpg",
    altText: "Libro de poesía"
  }
];
</script>

<template>
  <main id="home-page">
    <!-- Sección Ecosystem -->
    <article class="welcome-article">
      <cite class="text">"Para viajar lejos, no hay mejor nave que un libro."</cite>
      <ul class="filtrer-bar">
        <li class="card">
          <CardEcosystem
            title="Ingresa a tu cuenta"
            description="Disfruta sin límites con tu cuenta de Booksy UCAB"
            buttonText="Ingresa a tu cuenta"
            imageSrc="/assets/img/consultar-libros-img/user.png"
            to="/beginSection"
          />
        </li>
        <li class="card">
          <CardEcosystem
            title="Nuestras categorías"
            description="Encuentra libros educativos, de entretenimiento y más"
            buttonText="Ir a categorías"
            imageSrc="/assets/img/consultar-libros-img/application.png"
            href="https://github.com/freddybit"
          />
        </li>
        <li class="card">
          <CardEcosystem
            title="Historial de compras"
            description="Revisa tus últimas compras"
            buttonText="Ver últimas compras"
            imageSrc="/assets/img/consultar-libros-img/receipt.png"
            href="https://github.com/freddybit"
          />
        </li>
      </ul>
    </article>

    <!-- Sección Discovery -->
    <article class="discovery-article">
      <section class="discovery-section">
        <CardRectangle
          text="¡Aquí en la UCAB!"
          title="TODO PARA ESTAR AL DÍA EN TUS ESTUDIOS"
          buttonText="Ver más"
          imageSrc="/assets/img/consultar-libros-img/libros-academicos.jpg"
        />
        <CardRectangle
          text="¡Aquí en la UCAB!"
          title="REFUERZA TU MENTE DESDE DONDE ESTÉS"
          buttonText="Ver más"
          imageSrc="/assets/img/consultar-libros-img/habitos-atomicos.jpg"
        />
      </section>
      <section class="discovery-section">
        <CardRectangle
          text="¡Aquí en la UCAB!"
          title="LECTURAS QUE TE ACERCAN A LA META"
          buttonText="Ver más"
          imageSrc="/assets/img/consultar-libros-img/libros-deportivos.jpg"
        />
        <CardRectangle
          text="¡Aquí en la UCAB!"
          title="CONVIERTE TUS IDEAS EN CAPITAL"
          buttonText="Ver más"
          imageSrc="/assets/img/consultar-libros-img/libros-financieros.jpeg"
        />
      </section>
    </article>

    <!-- Sección Categorías -->
    <article class="book-list-article">
      <section class="horizontal-section">
        <h2 class="article-title">Categorías</h2>
        <div class="div-category-card">
          <CategoryCard
            v-for="(cat, index) in categories"
            :key="index"
            :title="cat.title"
            :imageSrc="cat.imageSrc"
            :altText="cat.altText"
          />
        </div>
      </section>
    </article>
  </main>
</template>

<style src="../../assets/styles/views/HomeView.css"></style>