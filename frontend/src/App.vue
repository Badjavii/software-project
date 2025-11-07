<template>
  <div style="max-width:800px;margin:24px auto;font-family:Arial,Helvetica,sans-serif;">
    <h1>Registro de Ventas</h1>
    <RegisterSale @registered="onRegistered" />
    <hr />
    <SalesList ref="salesList" />
  </div>
</template>

<script>
import RegisterSale from './components/RegisterSale.vue'
import SalesList from './components/SalesList.vue'

export default {
  components: { RegisterSale, SalesList },
  methods: {
    onRegistered() {
      // notify sales list to refresh (simple approach via ref)
      this.$refs.salesList && this.$refs.salesList.fetchVentas()
    }
  }
}
</script>

<style>
/* minimal styles */
</style>
