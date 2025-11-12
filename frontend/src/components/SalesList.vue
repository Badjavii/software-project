<template>
  <div class="sales-module">
    <div class="controls">
      <label class="field stretch">
        <span>ID del vendedor</span>
        <div class="input-with-button">
          <input
            v-model="filters.vendedor"
            type="text"
            placeholder="Ej. 1"
            @keyup.enter="fetchVentas"
          />
          <button
            type="button"
            class="card-button"
            @click="fetchVentas"
            :disabled="loading"
          >
            {{ loading ? 'Consultando…' : 'Consultar ventas' }}
          </button>
        </div>
      </label>

      <label class="field">
        <span>Filtrar por código</span>
        <input
          v-model="filters.codigo"
          type="text"
          placeholder="Ej. 5001"
        />
      </label>
    </div>

    <p v-if="lastUpdated" class="updated">
      Actualizado {{ formatRelativeTime(lastUpdated) }}
    </p>

    <p v-if="error" class="alert error">{{ error }}</p>

    <div v-if="filteredVentas.length" class="table-wrapper">
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
          <tr v-for="venta in filteredVentas" :key="venta.CodigoDeCompra">
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
    <div v-else class="empty-state">
      <p>No hay registros que coincidan con el filtro actual.</p>
      <small>Registra una venta o ajusta los criterios para verla aquí.</small>
    </div>
  </div>
</template>

<script>
import { listarVentas, softDeleteVenta as softDeleteVentaApi, eliminarVenta as eliminarVentaApi } from '../services/api'

const currencyFormatter = new Intl.NumberFormat('es-VE', {
  style: 'currency',
  currency: 'USD',
  minimumFractionDigits: 2
})

export default {
  name: 'SalesList',
  data() {
    return {
      ventas: [],
      error: null,
      loading: false,
      lastUpdated: null,
      filters: {
        codigo: '',
        vendedor: ''
      }
    }
  },
  computed: {
    filteredVentas() {
      const codigo = this.filters.codigo.trim().toLowerCase()
      const vendedor = this.filters.vendedor.trim().toLowerCase()
      if (!codigo && !vendedor) return this.ventas
      return this.ventas.filter(v => {
        const byCodigo = codigo
          ? v.CodigoDeCompra.toString().toLowerCase().includes(codigo)
          : true
        const byVendedor = vendedor
          ? v.VendedorId.toString().toLowerCase().includes(vendedor)
          : true
        return byCodigo && byVendedor
      })
    }
  },
  mounted() {
    this.fetchVentas()
  },
  methods: {
    async fetchVentas() {
      if (this.loading) return
      this.error = null
      this.loading = true
      try {
        this.ventas = await listarVentas()
        this.lastUpdated = new Date()
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'No pudimos obtener las ventas.'
      } finally {
        this.loading = false
      }
    },
    async marcarEliminada(codigo) {
      try {
        await softDeleteVentaApi(codigo)
        this.ventas = this.ventas.filter(v => v.CodigoDeCompra !== codigo)
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'No se pudo eliminar la venta.'
      }
    },
    async eliminarVentaDefinitiva(codigo) {
      try {
        await eliminarVentaApi(codigo)
        this.ventas = this.ventas.filter(v => v.CodigoDeCompra !== codigo)
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'No se pudo eliminar la venta.'
      }
    },
    formatDate(value) {
      try {
        return new Date(value).toLocaleString('es-VE', {
          dateStyle: 'medium',
          timeStyle: 'short'
        })
      } catch {
        return value
      }
    },
    formatMonto(value) {
      return currencyFormatter.format(value ?? 0)
    },
    formatRelativeTime(date) {
      const ts = new Date(date).getTime()
      if (Number.isNaN(ts)) return ''
      const diff = Date.now() - ts
      if (diff < 60000) return 'hace unos segundos'
      if (diff < 3600000) return `hace ${Math.round(diff / 60000)} min`
      return `hace ${Math.round(diff / 3600000)} h`
    }
  }
}
</script>

<style scoped>
.sales-module {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.controls {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
}

.field {
  flex: 1 1 200px;
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
  font-size: 0.85rem;
  color: #42516b;
  font-weight: 600;
}

.field input {
  border-radius: 16px;
  border: 1.5px solid #dfe7ff;
  background: #f7f9ff;
  padding: 11px 14px;
  font-size: 0.95rem;
  transition: border-color 0.2s ease, background 0.2s ease;
  outline: none;
}

.field input:focus {
  border-color: #2b89ff;
  background: #fff;
  box-shadow: 0 0 0 3px rgba(43, 137, 255, 0.1);
}

.stretch {
  flex: 2 1 260px;
}

.input-with-button {
  display: flex;
  gap: 0.6rem;
  align-items: center;
}

.input-with-button input {
  flex: 1;
}

.input-with-button .card-button {
  margin: 0;
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

@media (max-width: 640px) {
  .input-with-button {
    flex-direction: column;
    align-items: stretch;
  }

  .input-with-button button {
    width: 100%;
  }
}
</style>
