<template>
  <div>
    <h2>Ventas registradas</h2>
    <button @click="fetchVentas">Refrescar</button>
    <div v-if="error" style="color:red">{{ error }}</div>
    <table v-if="ventas.length" border="1" cellpadding="6" style="margin-top:8px; border-collapse:collapse">
      <thead>
        <tr><th>Codigo</th><th>Fecha</th><th>Monto</th><th>LibroId</th><th>CompradorId</th><th>VendedorId</th><th>Acciones</th></tr>
      </thead>
      <tbody>
        <tr v-for="v in ventas" :key="v.CodigoDeCompra">
          <td>{{ v.CodigoDeCompra }}</td>
          <td>{{ formatDate(v.FechaDeVenta) }}</td>
          <td>{{ v.MontoTotal }}</td>
          <td>{{ v.LibroId }}</td>
          <td>{{ v.CompradorId }}</td>
          <td>{{ v.VendedorId }}</td>
          <td>
            <button @click="eliminar(v.CodigoDeCompra, v.VendedorId)">Eliminar</button>
          </td>
        </tr>
      </tbody>
    </table>
    <div v-else style="margin-top:8px">No hay ventas registradas (o ventas.json no es accesible desde el front).</div>
  </div>
</template>

<script>
import { listarVentas, eliminarVenta } from '../services/api'

export default {
  name: 'SalesList',
  data() {
    return { ventas: [], error: null }
  },
  mounted() {
    this.fetchVentas()
  },
  methods: {
    async fetchVentas() {
      this.error = null
      try {
        this.ventas = await listarVentas()
      } catch (err) {
        this.error = err.message || 'Error al obtener ventas'
      }
    },
    async eliminar(codigo, vendedorId) {
      try {
        const ok = await eliminarVenta(codigo, vendedorId)
        if (ok) {
          this.ventas = this.ventas.filter(v => v.CodigoDeCompra !== codigo)
        }
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'Error al eliminar'
      }
    },
    formatDate(d) {
      try { return new Date(d).toLocaleString() } catch { return d }
    }
  }
}
</script>

<style scoped>
table { width:100% }
</style>
