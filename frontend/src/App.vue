<template>
  <div class="app-shell">
    <header class="app-header">
      <div class="brand">
        <div class="logo-circle">B</div>
        <div class="brand-text">
          <span class="brand-title">Booksy</span>
          <small>UCAB</small>
        </div>
      </div>
      <div class="search-box">
        <input type="text" placeholder="Buscar libros en Booksy UCAB..." />
        <button aria-label="Buscar">
          <svg viewBox="0 0 24 24" role="img" aria-hidden="true">
            <path d="M15.5 14h-.8l-.3-.3a6 6 0 1 0-.7.7l.3.3v.8l4.5 4.4 1.3-1.3zm-6 0a4.5 4.5 0 1 1 0-9 4.5 4.5 0 0 1 0 9z" />
          </svg>
        </button>
      </div>
      <nav class="nav-links">
        <a>Inicio</a>
        <a>Catálogo</a>
        <a>Categorías</a>
        <a>Vender</a>
        <a>Crear cuenta</a>
        <a>Ingresa</a>
        <a class="active">Ventas</a>
      </nav>
    </header>

    <main class="app-main">
      <section class="card card-form">
        <div class="card-header">
          <h2>Registrar una nueva venta</h2>
          <p>Completa los datos de la transacción para agregarla al historial.</p>
        </div>
        <RegisterSale @registered="onRegistered" />
      </section>

      <section class="card card-list">
        <div class="card-header">
          <h2>Historial de ventas</h2>
          <p>Consulta y administra las ventas registradas por los vendedores.</p>
        </div>
        <SalesList ref="salesList" />
      </section>
    </main>
  </div>
</template>

<script>
import RegisterSale from './components/RegisterSale.vue'
import SalesList from './components/SalesList.vue'

export default {
  components: { RegisterSale, SalesList },
  methods: {
    onRegistered() {
      // Simple ref-based refresh after creating a sale
      this.$refs.salesList && this.$refs.salesList.fetchVentas()
    }
  }
}
</script>

<style>
:root {
  color-scheme: light;
  font-family: 'Inter', 'Segoe UI', system-ui, -apple-system, BlinkMacSystemFont, sans-serif;
}

body {
  margin: 0;
  background: #021b35;
}

.app-shell {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  color: #0a1c2f;
}

.app-header {
  padding: 18px clamp(1.5rem, 4vw, 4rem);
  background: #fff;
  display: grid;
  grid-template-columns: auto minmax(240px, 1fr) auto;
  gap: 1.5rem;
  align-items: center;
  box-shadow: 0 4px 18px rgba(2, 27, 53, 0.08);
  position: sticky;
  top: 0;
  z-index: 5;
}

.brand {
  display: flex;
  align-items: center;
  gap: 0.8rem;
}

.logo-circle {
  width: 44px;
  height: 44px;
  border-radius: 12px;
  background: linear-gradient(135deg, #2b89ff, #5ac2ff);
  color: #fff;
  font-weight: 700;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.2rem;
  box-shadow: 0 8px 16px rgba(33, 131, 255, 0.25);
}

.brand-title {
  font-weight: 700;
  text-transform: uppercase;
  letter-spacing: 0.05em;
}

.brand-text small {
  display: block;
  font-size: 0.7rem;
  color: #7d8ea9;
}

.search-box {
  display: flex;
  align-items: center;
  background: #f6f7fb;
  border-radius: 28px;
  padding-left: 18px;
  border: 1px solid transparent;
  transition: border-color 0.2s ease;
}

.search-box:focus-within {
  border-color: #2b89ff;
  background: #fff;
}

.search-box input {
  flex: 1;
  border: none;
  background: transparent;
  font-size: 0.95rem;
  padding: 10px 0;
  outline: none;
}

.search-box button {
  border: none;
  background: none;
  width: 46px;
  height: 46px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  color: #2b89ff;
}

.search-box svg {
  width: 20px;
  height: 20px;
  fill: currentColor;
}

.nav-links {
  display: flex;
  gap: 1rem;
  font-weight: 600;
  font-size: 0.95rem;
  color: #405272;
}

.nav-links a {
  text-decoration: none;
  color: inherit;
  padding: 4px 0;
  border-bottom: 2px solid transparent;
  cursor: pointer;
}

.nav-links a:hover,
.nav-links a.active {
  color: #1c3d68;
  border-bottom-color: #2b89ff;
}

.app-main {
  flex: 1;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: 1.5rem;
  padding: clamp(1.5rem, 4vw, 4rem);
}

.card {
  background: #fdfdff;
  border-radius: 22px;
  box-shadow: 0 18px 45px rgba(5, 22, 40, 0.25);
  padding: 1.5rem;
  display: flex;
  flex-direction: column;
}

.card-header {
  margin-bottom: 1rem;
}

.card-header h2 {
  margin: 0;
  font-size: 1.3rem;
}

.card-header p {
  margin: 0.35rem 0 0;
  color: #5e6a82;
  font-size: 0.95rem;
}

@media (max-width: 960px) {
  .app-header {
    grid-template-columns: 1fr;
  }

  .nav-links {
    flex-wrap: wrap;
  }
}
</style>
