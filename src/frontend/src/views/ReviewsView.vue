<script setup lang="js">
import { onMounted, ref, reactive, computed, watch } from 'vue'
import { getBookList } from '../../services/bookService.js'
import { listarResenas, registrarResena, saveOfflineReview } from '../../services/reviewsService.js'

const books = ref([])
const selectedBookId = ref(null)
const reviews = ref([])
const loadingBooks = ref(true)
const loadingReviews = ref(false)
const submitting = ref(false)
const form = reactive({
  rating: 0,
  comentario: '',
  nombre: '',
  email: ''
})

const maxChars = 100

const comentarioRestante = computed(() => maxChars - form.comentario.length)
const puedeEnviar = computed(
  () =>
    selectedBookId.value &&
    form.rating > 0 &&
    form.comentario.trim().length > 0 &&
    form.comentario.length <= maxChars &&
    form.nombre.trim().length > 0 &&
    form.email.trim().length > 0
)

const loadBooks = async () => {
  loadingBooks.value = true
  try {
    books.value = await getBookList()
    if (books.value.length && !selectedBookId.value) {
      selectedBookId.value = books.value[0]._id
    }
  } catch (err) {
    console.error('No se pudo cargar libros', err)
  } finally {
    loadingBooks.value = false
  }
}

const loadReviews = async () => {
  if (!selectedBookId.value) return
  loadingReviews.value = true
  try {
    reviews.value = await listarResenas(selectedBookId.value)
  } catch (err) {
    console.error('No se pudieron cargar reseñas', err)
    reviews.value = []
  } finally {
    loadingReviews.value = false
  }
}

const setRating = (value) => {
  form.rating = value
}

const submitReview = async () => {
  if (!puedeEnviar.value) return

  const payload = {
    libroId: selectedBookId.value,
    rating: form.rating,
    comentario: form.comentario.trim(),
    usuarioEmail: form.email.trim(),
    usuarioNombre: form.nombre.trim()
  }

  submitting.value = true
  try {
    await registrarResena(payload)
    await loadReviews()
  } catch (err) {
    console.warn('Fallo al guardar reseña en API, se almacenará localmente.', err?.message)
    saveOfflineReview({
      ...payload,
      LibroId: payload.libroId,
      Rating: payload.rating,
      Comentario: payload.comentario,
      UsuarioEmail: payload.usuarioEmail,
      UsuarioNombre: payload.usuarioNombre,
      Id: Date.now()
    })
    await loadReviews()
  } finally {
    submitting.value = false
    form.rating = 0
    form.comentario = ''
    form.nombre = ''
    form.email = ''
  }
}

watch(selectedBookId, () => {
  loadReviews()
})

onMounted(async () => {
  await loadBooks()
  await loadReviews()
})
</script>

<template>
  <section class="reviews-page">
    <header class="reviews-hero">
      <div>
        <p class="tag">Reseñas</p>
        <h1>Comparte tu experiencia con otros lectores de Booksy</h1>
        <p>
          Inicia sesión como comprador o vendedor para dejar una reseña con una calificación de 1 a 5 estrellas y un
          comentario corto de máximo 100 caracteres. Todas las reseñas quedan asociadas a tu usuario.
        </p>
      </div>
    </header>

    <main class="reviews-grid">
      <section class="card">
        <div class="card-header">
          <h2>Ingresar reseña</h2>
          <p>Selecciona un libro, califícalo y escribe una breve reseña.</p>
        </div>

        <div class="form-group">
          <label>Libro</label>
          <select v-model.number="selectedBookId" :disabled="loadingBooks || !books.length">
            <option value="" disabled>Selecciona un libro</option>
            <option v-for="book in books" :key="book._id" :value="book._id">
              {{ book._nameBook }}
            </option>
          </select>
          <small v-if="!books.length && !loadingBooks">No hay libros disponibles.</small>
        </div>

        <div class="form-grid">
          <label class="form-group">
            <span>Tu nombre</span>
            <input v-model="form.nombre" type="text" placeholder="Ej. Ana Pérez" maxlength="60" />
          </label>
          <label class="form-group">
            <span>Tu correo</span>
            <input v-model="form.email" type="email" placeholder="usuario@ucab.edu.ve" />
          </label>
        </div>

        <div class="form-group">
          <label>Calificación</label>
          <div class="rating">
            <button
              v-for="star in 5"
              :key="star"
              type="button"
              :class="['star', { active: star <= form.rating }]"
              @click="setRating(star)"
            >
              ★
            </button>
          </div>
        </div>

        <div class="form-group">
          <label>Comentario (máx. 100 caracteres)</label>
          <textarea
            v-model="form.comentario"
            :maxlength="maxChars"
            rows="3"
            placeholder="Describe tu experiencia de lectura"
          ></textarea>
          <small :class="{ warning: comentarioRestante < 0 }">{{ comentarioRestante }} caracteres restantes</small>
        </div>

        <button class="card-button" type="button" :disabled="!puedeEnviar || submitting" @click="submitReview">
          {{ submitting ? 'Guardando…' : 'Guardar reseña' }}
        </button>
      </section>

      <section class="card">
        <div class="card-header">
          <h2>Reseñas del libro</h2>
          <p v-if="selectedBookId">Mostrando reseñas para el libro #{{ selectedBookId }}</p>
        </div>

        <p v-if="loadingReviews" class="state-copy">Cargando reseñas…</p>
        <p v-else-if="reviews.length === 0" class="state-copy">Todavía no hay reseñas para este libro.</p>

        <ul v-else class="reviews-list">
          <li v-for="review in reviews" :key="review.id" class="review-card">
            <header>
              <strong>{{ review.usuarioNombre || review.UsuarioNombre }}</strong>
              <span class="stars">{{ '★'.repeat(review.rating ?? review.Rating) }}{{ '☆'.repeat(5 - (review.rating ?? review.Rating)) }}</span>
            </header>
            <p>{{ review.comentario || review.Comentario }}</p>
          </li>
        </ul>
      </section>
    </main>
  </section>
</template>

<style scoped>
.reviews-page {
  min-height: 88vh;
  background: linear-gradient(180deg, #001731 0%, #032248 60%, #052049 100%);
  color: white;
  padding-bottom: 3rem;
}

.reviews-hero {
  padding: clamp(1.5rem, 5vw, 4rem);
}

.reviews-hero h1 {
  margin: 0.6rem 0;
  font-size: clamp(2rem, 4vw, 3.2rem);
}

.reviews-hero p {
  margin: 0.3rem 0;
  max-width: 720px;
  color: rgba(255, 255, 255, 0.85);
}

.tag {
  text-transform: uppercase;
  letter-spacing: 0.3em;
  font-size: 0.85rem;
  color: #7fc1ff;
}

.reviews-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(360px, 1fr));
  gap: 2rem;
  padding: 0 clamp(1.5rem, 4vw, 4rem);
  align-items: start;
}

.card {
  background: #fdfdff;
  color: #0a1c2f;
  border-radius: 22px;
  padding: clamp(1.5rem, 3vw, 2rem);
  box-shadow: 0 18px 35px rgba(5, 22, 40, 0.25);
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.card-header h2 {
  margin: 0;
}

.card-header p {
  margin: 0.2rem 0 0;
  color: #5e6a82;
}

.form-group {
  display: flex;
  flex-direction: column;
  gap: 0.4rem;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 1rem;
}

.form-group label {
  font-weight: 600;
}

select,
textarea,
input {
  border-radius: 14px;
  border: 1.5px solid #dfe7ff;
  padding: 0.75rem 1rem;
  font-size: 1rem;
  outline: none;
  background: #f7f9ff;
}

textarea:focus,
select:focus,
input:focus {
  border-color: #2b89ff;
  background: white;
  box-shadow: 0 0 0 3px rgba(43, 137, 255, 0.15);
}

.search-input {
  margin-bottom: 0.5rem;
}

.rating {
  display: flex;
  gap: 0.4rem;
}

.star {
  width: 44px;
  height: 44px;
  border-radius: 50%;
  border: none;
  font-size: 1.6rem;
  cursor: pointer;
  background: #e6ecff;
  color: #a0a7c4;
}

.star.active {
  background: #2b89ff;
  color: #fff;
}

.card-button {
  align-self: flex-start;
  background-color: rgb(0, 117, 235);
  border-radius: 1rem;
  border: none;
  color: #fff;
  font-weight: 600;
  padding: 0.85rem 1.8rem;
  cursor: pointer;
  box-shadow: 0 10px 18px rgba(0, 117, 235, 0.35);
  transition: transform 0.15s ease, box-shadow 0.15s ease, opacity 0.2s ease;
}

.card-button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
  box-shadow: none;
}

.reviews-list {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.review-card {
  border: 1px solid #e7eaf5;
  border-radius: 16px;
  padding: 1rem;
}

.review-card header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.4rem;
}

.stars {
  color: #f5b301;
}

.state-copy {
  color: #566175;
}

.state-copy.warning {
  color: #b42318;
}

@media (max-width: 640px) {
  .review-card header {
    flex-direction: column;
    gap: 0.2rem;
  }
}
</style>
