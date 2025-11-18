<script setup lang="ts">
import { reactive, ref } from 'vue';
import { registrarVenta } from '../../services/Venta/salesService.js';

const emit = defineEmits<{
  (e: 'registered'): void;
}>();

const form = reactive({
  libroId: null as number | null,
  vendedorId: null as number | null,
  compradorId: null as number | null
});

const loading = ref(false);
const success = ref(false);
const error = ref<string | null>(null);

async function submit() {
  if (loading.value) return;

  error.value = null;
  success.value = false;
  loading.value = true;

  try {
    await registrarVenta({
      libroId: form.libroId,
      vendedorId: form.vendedorId,
      compradorId: form.compradorId
    });

    success.value = true;
    emit('registered');

    form.libroId = null;
    form.vendedorId = null;
    form.compradorId = null;
  } catch (err: any) {
    error.value =
      err?.response?.data?.message ||
      err?.response?.data?.error ||
      err?.message ||
      'No se pudo registrar la Venta.';
  } finally {
    loading.value = false;
  }
}
</script>

<template>
  <form class="sale-form" @submit.prevent="submit">
    <div class="form-grid">
      <label class="field">
        <span>ID del libro</span>
        <input
          v-model.number="form.libroId"
          type="number"
          min="1"
          step="1"
          required
          placeholder="Ej. 1"
        />
      </label>

      <label class="field">
        <span>ID del vendedor</span>
        <input
          v-model.number="form.vendedorId"
          type="number"
          min="1"
          step="1"
          required
          placeholder="Ej. 2"
        />
      </label>

      <label class="field">
        <span>ID del comprador</span>
        <input
          v-model.number="form.compradorId"
          type="number"
          min="1"
          step="1"
          required
          placeholder="Ej. 3"
        />
      </label>
    </div>

    <p class="helper">
      El código de compra se genera automáticamente. Los montos se calculan desde el catálogo configurado en el backend.
    </p>

    <div class="actions">
      <button class="card-button" type="submit" :disabled="loading">
        {{ loading ? 'Registrando…' : 'Registrar Venta' }}
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
  justify-content: flex-start;
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
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
