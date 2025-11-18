<script setup lang="js">

const options = [
  {
    title: 'Registrar perfil de vendedor',
    description: 'Crea tu perfil oficial y comienza a publicar tus libros en Booksy.',
    to: '/registerSellerSection',
    cta: 'Registrar'
  },
  {
    title: 'Iniciar sesión como vendedor',
    description: 'Accede a tu panel para administrar tus publicaciones y pedidos.',
    to: '/beginSellerSection',
    cta: 'Ingresar'
  },
  {
    title: 'Gestionar Ventas',
    description: 'Registra transacciones, revisa el historial y elimina ventas cuando sea necesario.',
    to: '/gestionarVenta/sale',
    cta: 'Gestionar'
  },
  {
    title: 'Registrar libro',
    description: 'Registra todos los libros que deseas vender.',
    to: '/seller/registerBook',
    cta: 'Registrar'
  },
  {
    title: 'Eliminar libro',
    description: 'Registra todos los libros que deseas sacar del stock.',
    to: '/ventas/gestionar',
    cta: 'Eliminar'
  },
  {
    title: 'Editar libro',
    description: 'Editar tus libros',
    to: '/seller/editBook',
    cta: 'Editar'
  }
];
</script>

<template>
  <article class="seller-hub">
    <section class="hero">
      <h1 class="tag">Vender en Booksy</h1>
      <p>Controla tu negocio desde un solo panel</p>
      <p>Selecciona la acción que necesitas para continuar creciendo dentro del marketplace.</p>
    </section>

      <section v-for="option in options" :key="option.title" class="option-card">
        <h2>{{ option.title }}</h2>
        <p>{{ option.description }}</p>
        <router-link class="card-button" :to="option.to">{{ option.cta }}</router-link>
      </section>
  </article>
</template>

<style scoped>
.seller-hub {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  height: 135vh;
  margin: 0;

  justify-content: center;
  align-content: center;

  background: linear-gradient(120deg, rgba(0, 30, 60, 0.9), rgba(2, 23, 53, 0.95));
  color: white;
}

.hero {
  max-width: 720px;
  margin-bottom: 2rem;
}

.hero h1 {
  font-size: clamp(2rem, 4vw, 3rem);
  margin: 0.5rem 0;
}

.hero p {
  margin: 0.25rem 0;
  color: rgba(255, 255, 255, 0.85);
}

.tag {
  text-transform: uppercase;
  letter-spacing: 0.2em;
  font-size: 0.85rem;
  color: #7fc1ff;
}

.options-grid {
  height: 100vh;
  gap: 1.5rem;
}

.option-card {
  height: 15vh;
  width: 30vw;
  margin: 2rem;
  background: #fdfdff;
  color: #0a1c2f;
  border-radius: 20px;
  padding: 1.8rem;
  box-shadow: 0 18px 45px rgba(5, 22, 40, 0.25);
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.option-card h2 {
  margin: 0;
}

.option-card p {
  margin: 0;
  color: #5e6a82;
  flex: 1;
}

.option-card .card-button {
  width: auto;
  align-self: flex-start;
  margin-top: 0.5rem;
}
</style>
