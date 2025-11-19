<script setup lang="ts">
import { ref } from 'vue';
import RegisterSale from '../../../components/Venta/RegisterSale.vue';
import SalesList from '../../../components/Venta/SalesList.vue';

const salesList = ref<InstanceType<typeof SalesList> | null>(null);

function handleRegistered() {
  salesList.value?.fetchVentas();
}
</script>

<template>

    <article class="ventas-header">
      <section>
        <h1>Panel de gestión de ventas</h1>
        <p>Registra tus transacciones y controla el historial de ventas en un solo lugar.</p>
      </section>
    </article>

    <article class="ventas-grid">
      <section class="card">
        <div class="card-header">
          <h2>Registrar una nueva venta</h2>
          <p>Completa los datos de la transacción para agregarla al historial.</p>
        </div>
        <RegisterSale @registered="handleRegistered" />
      </section>

      <section class="card">
        <div class="card-header">
          <h2>Historial de ventas</h2>
          <p>Consulta, filtra y elimina los registros creados por tus vendedores.</p>
        </div>
        <SalesList ref="salesList" />
      </section>
    </article>
</template>

<style scoped>

.ventas-header {
  display: flex;
  width: 90vw;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
  padding: 2rem clamp(1.5rem, 5vw, 4rem);
  color: white;
}

section h1 {
  margin: 0;
  font-size: clamp(1.5rem, 3vw, 2.5rem);
}

section p {
  margin: 0.4rem 0 0;
  color: rgba(255, 255, 255, 0.85);
  max-width: 520px;
}

.ventas-grid {
  display: flex;
  flex-direction: column;
  width: 90vw;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: 2rem;
  padding: 0 clamp(1.5rem, 5vw, 4rem);
}

.card {
  width: 90vw;
  background: #fdfdff;
  border-radius: 22px;
  padding: clamp(1.25rem, 3vw, 1.75rem);
  box-shadow: 0 18px 45px rgba(5, 22, 40, 0.25);
}

.card-header h2 {
  margin: 0;
  font-size: 1.3rem;
}

.card-header p {
  margin: 0.35rem 0 1rem;
  color: #5e6a82;
}

:global(.card-button) {
  margin: 1.2rem 0 0;
  height: auto;
  min-height: 44px;
  width: auto;
  min-width: 160px;
  background-color: rgb(0, 117, 235);
  border-radius: 1rem;
  border: none;
  color: #fff;
  font-size: 1.05rem;
  font-weight: 600;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: transform 0.15s ease, box-shadow 0.15s ease, opacity 0.2s ease;
  box-shadow: 0 10px 18px rgba(0, 117, 235, 0.35);
  text-decoration: none;
  padding: 0 1.5rem;
}

:global(.card-button:disabled) {
  opacity: 0.6;
  cursor: not-allowed;
  box-shadow: none;
}

:global(.card-button:not(:disabled):hover) {
  transform: translateY(-1px);
  box-shadow: 0 14px 24px rgba(0, 117, 235, 0.45);
}

@media (max-width: 720px) {
  .ventas-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 1rem;
  }
}
</style>