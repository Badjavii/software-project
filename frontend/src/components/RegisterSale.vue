<template>
  <form @submit.prevent="submit">
    <div>
      <label>Codigo de Compra</label>
      <input v-model.number="form.codigoDeCompra" required />
    </div>
    <div>
      <label>Libro Id</label>
      <input v-model.number="form.libroId" required />
    </div>
    <div>
      <label>Comprador Id</label>
      <input v-model.number="form.compradorId" required />
    </div>
    <div>
      <label>Vendedor Id</label>
      <input v-model.number="form.vendedorId" required />
    </div>
    <div style="margin-top:8px">
      <button type="submit">Registrar Venta</button>
    </div>
    <div v-if="error" style="color:red;margin-top:8px">{{ error }}</div>
    <div v-if="success" style="color:green;margin-top:8px">Venta registrada</div>
  </form>
</template>

<script>
import { registrarVenta } from '../services/api'

export default {
  name: 'RegisterSale',
  data() {
    return {
      form: {
        codigoDeCompra: null,
        libroId: null,
        compradorId: null,
        vendedorId: null
      },
      error: null,
      success: false
    }
  },
  methods: {
    async submit() {
      this.error = null
      this.success = false
      try {
        await registrarVenta(this.form)
        this.success = true
        this.$emit('registered')
        // reset form
        this.form.codigoDeCompra = null
        this.form.libroId = null
        this.form.compradorId = null
        this.form.vendedorId = null
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'Error'
      }
    }
  }
}
</script>

<style scoped>
label { display:block; font-weight:600; margin-top:8px }
input { padding:6px; width:100%; max-width:320px }
button { padding:8px 12px }
</style>
