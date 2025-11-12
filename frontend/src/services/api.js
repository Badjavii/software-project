import axios from 'axios'

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api'
})

export async function registrarVenta(payload) {
  const resp = await api.post('/ventas', payload)
  return resp.data
}

export async function getVenta(codigoDeCompra) {
  const resp = await api.get(`/ventas/${codigoDeCompra}`)
  return resp.data
}

export async function softDeleteVenta(codigoDeCompra) {
  await api.patch(`/ventas/${codigoDeCompra}/soft-delete`)
  return true
}

export async function eliminarVenta(codigoDeCompra) {
  await api.delete(`/ventas/${codigoDeCompra}/hard`)
  return true
}

export async function listarVentas() {
  // Call the backend endpoint that returns ventas no eliminadas
  try {
    const resp = await api.get('/ventas')
    return resp.data
  } catch (e) {
    // fallback empty
    return []
  }
}

export default api
