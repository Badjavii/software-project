<template>
  <form class="sale-form" @submit.prevent="submit">
    <div class="form-grid">
      <label class="field">
        <span>ID del libro</span>
        <input
          type="number"
          min="1"
          step="1"
          v-model.number="form.libroId"
          placeholder="Ej. 12"
          required
        />
      </label>

      <label class="field">
        <span>Código de compra</span>
        <input
          type="number"
          min="1"
          step="1"
          v-model.number="form.codigoDeCompra"
          placeholder="Ej. 4501"
          required
        />
      </label>

      <label class="field">
        <span>ID del vendedor</span>
        <input
          type="number"
          min="1"
          step="1"
          v-model.number="form.vendedorId"
          placeholder="Ej. 4"
          required
        />
      </label>

      <label class="field">
        <span>ID del comprador</span>
        <input
          type="number"
          min="1"
          step="1"
          v-model.number="form.compradorId"
          placeholder="Ej. 2"
          required
        />
      </label>
    </div>

    <p class="helper">
      Los correos y montos son calculados por el backend usando los catálogos existentes.
    </p>

    <div class="actions">
      <button class="primary-btn" type="submit" :disabled="loading">
        {{ loading ? 'Registrando…' : 'Registrar venta' }}
      </button>
    </div>

    <transition name="fade">
      <p v-if="success" class="status success">Venta registrada exitosamente.</p>
    </transition>
    <transition name="fade">
      <p v-if="error" class="status error">{{ error }}</p>
    </transition>
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
      loading: false,
      error: null,
      success: false
    }
  },
  methods: {
    async submit() {
      if (this.loading) return
      this.error = null
      this.success = false
      this.loading = true
      try {
        await registrarVenta(this.form)
        this.success = true
        this.$emit('registered')
        this.form = {
          codigoDeCompra: null,
          libroId: null,
          compradorId: null,
          vendedorId: null
        }
      } catch (err) {
        this.error = err.response?.data?.message || err.message || 'No se pudo registrar la venta.'
      } finally {
        this.loading = false
      }
    }
  }
}
</script>

<style scoped>
.sale-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(210px, 1fr));
  gap: 1rem;
}

.field {
  font-size: 0.9rem;
  color: #415068;
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
  font-weight: 600;
}

.field input {
  border: 1.5px solid #dfe7ff;
  border-radius: 18px;
  padding: 12px 16px;
  font-size: 0.95rem;
  background: #f7f9ff;
  transition: border-color 0.2s ease, background 0.2s ease;
  outline: none;
}

.field input:focus {
  border-color: #2b89ff;
  background: #fff;
  box-shadow: 0 0 0 3px rgba(43, 137, 255, 0.12);
}

.helper {
  margin: 0;
  color: #7a89a8;
  font-size: 0.9rem;
}

.actions {
  display: flex;
  justify-content: flex-end;
}

.primary-btn {
  border: none;
  border-radius: 18px;
  padding: 12px 26px;
  font-weight: 600;
  color: #fff;
  cursor: pointer;
  background: linear-gradient(120deg, #1c68f3, #3597ff);
  box-shadow: 0 14px 24px rgba(45, 122, 255, 0.35);
  transition: transform 0.15s ease, box-shadow 0.15s ease;
}

.primary-btn:disabled {
  opacity: 0.65;
  cursor: not-allowed;
  box-shadow: none;
}

.primary-btn:not(:disabled):hover {
  transform: translateY(-1px);
  box-shadow: 0 18px 28px rgba(45, 122, 255, 0.45);
}

.status {
  font-size: 0.9rem;
  margin: 0;
  padding: 0.6rem 0.9rem;
  border-radius: 12px;
}

.status.success {
  color: #0f5132;
  background: #d1f7e2;
}

.status.error {
  color: #842029;
  background: #fde2e0;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.15s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
