<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { eliminarVenta, listarVentas, softDeleteVenta } from '../../services/Venta/salesService.js';

type Venta = {
  CodigoDeCompra: number;
  FechaDeVenta: string;
  MontoTotal: number;
  LibroId: number;
  CompradorId: number;
  VendedorId: number;
  IsDeleted: boolean;
};

const ventas = ref<Venta[]>([]);
const loading = ref(false);
const error = ref<string | null>(null);
const lastUpdated = ref<Date | null>(null);

async function fetchVentas() {
  if (loading.value) return;

  error.value = null;
  loading.value = true;

  try {
    ventas.value = await listarVentas();
    lastUpdated.value = new Date();
  } catch (err: any) {
    error.value = err?.message || 'No pudimos obtener las ventas.';
  } finally {
    loading.value = false;
  }
}

async function marcarEliminada(codigo: number) {
  try {
    await softDeleteVenta(codigo);
    ventas.value = ventas.value.filter((v) => v.CodigoDeCompra !== codigo);
  } catch (err: any) {
    error.value = err?.response?.data?.message || err?.message || 'No se pudo eliminar la Venta.';
  }
}

async function eliminarVentaDefinitiva(codigo: number) {
  try {
    await eliminarVenta(codigo);
    ventas.value = ventas.value.filter((v) => v.CodigoDeCompra !== codigo);
  } catch (err: any) {
    error.value = err?.response?.data?.message || err?.message || 'No se pudo eliminar la Venta.';
  }
}

function formatDate(value: string) {
  try {
    return new Date(value).toLocaleString('es-VE', {
      dateStyle: 'medium',
      timeStyle: 'short'
    });
  } catch {
    return value;
  }
}

function formatMonto(value: number) {
  return new Intl.NumberFormat('es-VE', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 2
  }).format(value ?? 0);
}

function formatRelativeTime(date: Date | null) {
  if (!date) return '';
  const diff = Date.now() - date.getTime();

  if (diff < 60000) return 'hace unos segundos';
  if (diff < 3600000) return `hace ${Math.round(diff / 60000)} min`;
  return `hace ${Math.round(diff / 3600000)} h`;
}

defineExpose({ fetchVentas });

onMounted(fetchVentas);
</script>

<template>
  <div class="sales-module">
    <p v-if="lastUpdated" class="updated">
      Actualizado {{ formatRelativeTime(lastUpdated) }}
      <span v-if="loading">· Cargando…</span>
    </p>
    <p v-else class="updated">Cargando ventas…</p>

    <p v-if="error" class="alert error">{{ error }}</p>

    <div v-if="ventas.length" class="table-wrapper">
      <table>
        <thead>
          <tr>
            <th>Código</th>
            <th>Fecha</th>
            <th>Monto</th>
            <th>Libro</th>
            <th>Comprador</th>
            <th>Vendedor</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="venta in ventas" :key="venta.CodigoDeCompra">
            <td>#{{ venta.CodigoDeCompra }}</td>
            <td>{{ formatDate(venta.FechaDeVenta) }}</td>
            <td>{{ formatMonto(venta.MontoTotal) }}</td>
            <td>{{ venta.LibroId }}</td>
            <td>{{ venta.CompradorId }}</td>
            <td>{{ venta.VendedorId }}</td>
            <td class="actions-cell">
              <button class="ghost" @click="marcarEliminada(venta.CodigoDeCompra)">
                Marcar eliminada
              </button>
              <button class="ghost danger" @click="eliminarVentaDefinitiva(venta.CodigoDeCompra)">
                Eliminar
              </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div v-else-if="!loading" class="empty-state">
      <p>No hay ventas registradas todavía.</p>
      <small>Registra una venta para verla en esta lista.</small>
    </div>
  </div>
</template>

<style scoped>
.sales-module {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

.updated {
  margin: 0;
  font-size: 0.85rem;
  color: #7e8ba4;
}

.alert {
  margin: 0;
  padding: 0.75rem 1rem;
  border-radius: 14px;
  font-size: 0.9rem;
}

.alert.error {
  background: #fde3e5;
  color: #842029;
}

.table-wrapper {
  border-radius: 18px;
  border: 1px solid #eef1fb;
  overflow-x: auto;
}

table {
  width: 100%;
  min-width: 760px;
  border-collapse: collapse;
  font-size: 0.92rem;
}

thead {
  background: #eff4ff;
  color: #40506c;
}

th,
td {
  padding: 0.85rem 1rem;
  text-align: left;
}

tbody tr:nth-child(odd) {
  background: #fcfdff;
}

tbody tr + tr {
  border-top: 1px solid #f1f3fb;
}

.actions-cell {
  text-align: right;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap: 0.4rem;
  flex-wrap: wrap;
}

.ghost {
  border: 1px solid #d2d9ed;
  background: transparent;
  color: #324463;
  padding: 6px 12px;
  border-radius: 12px;
  font-weight: 600;
  cursor: pointer;
  white-space: nowrap;
}

.ghost:hover {
  border-color: #2b89ff;
  color: #2b89ff;
}

.ghost.danger {
  border-color: #ffb8b5;
  color: #c4372c;
}

.ghost.danger:hover {
  border-color: #ff6b5f;
  color: #ff3b2f;
}

.empty-state {
  text-align: center;
  color: #6a7a96;
  background: #f8faff;
  border-radius: 16px;
  padding: 1.5rem;
  border: 1px dashed #cfd8f5;
}

.empty-state small {
  display: block;
  margin-top: 0.3rem;
}
</style>
