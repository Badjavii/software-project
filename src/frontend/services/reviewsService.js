import axios from 'axios'
import reviewsSample from '../assets/data/reviews.sample.json'

const API_BASE = (import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api').replace(/\/$/, '')
const OFFLINE_KEY = 'booksy_offline_reviews'

const readOffline = () => {
  try {
    return JSON.parse(localStorage.getItem(OFFLINE_KEY) || '[]')
  } catch {
    return []
  }
}

const writeOffline = (list) => {
  localStorage.setItem(OFFLINE_KEY, JSON.stringify(list))
}

export function saveOfflineReview(review) {
  const list = readOffline()
  list.push(review)
  writeOffline(list)
}

const mergeReviews = (primary = [], libroId) => {
  const offline = readOffline().filter((r) => r.LibroId === libroId || r.libroId === libroId)
  return [...offline, ...primary]
}

export async function listarResenas(libroId) {
  try {
    const { data } = await axios.get(`${API_BASE}/reviews`, { params: { libroId } })
    if (Array.isArray(data)) return mergeReviews(data, libroId)
  } catch (error) {
    console.warn('No se pudieron cargar reseñas desde el API, usando datos locales.', error?.message)
  }
  const base = reviewsSample.filter((r) => r.LibroId === libroId || r.libroId === libroId)
  return mergeReviews(base, libroId)
}

export async function listarMisResenas(email) {
  const emailLower = email?.toLowerCase()
  try {
    const { data } = await axios.get(`${API_BASE}/reviews/mine`, { params: { email } })
    if (Array.isArray(data)) return data
  } catch (error) {
    console.warn('No se pudieron cargar reseñas del usuario, usando datos locales.', error?.message)
  }
  const offline = readOffline().filter((r) => r.UsuarioEmail?.toLowerCase() === emailLower)
  const base = reviewsSample.filter((r) => r.UsuarioEmail?.toLowerCase() === emailLower)
  return [...offline, ...base]
}

export async function registrarResena(payload) {
  const { data } = await axios.post(`${API_BASE}/reviews`, payload)
  return data
}
