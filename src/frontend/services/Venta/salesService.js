import axios from 'axios';

const API_BASE = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE
});

const normalizeSale = (sale = {}) => ({
  CodigoDeCompra: sale.CodigoDeCompra ?? sale.codigoDeCompra ?? sale.codigo ?? 0,
  FechaDeVenta: sale.FechaDeVenta ?? sale.fechaDeVenta ?? '',
  MontoTotal: sale.MontoTotal ?? sale.montoTotal ?? 0,
  LibroId: sale.LibroId ?? sale.libroId ?? 0,
  CompradorId: sale.CompradorId ?? sale.compradorId ?? 0,
  VendedorId: sale.VendedorId ?? sale.vendedorId ?? 0,
  IsDeleted: sale.IsDeleted ?? sale.isDeleted ?? false
});

export async function registrarVenta(payload) {
  const { data } = await api.post('/ventas', payload);
  return normalizeSale(data);
}

export async function listarVentas() {
  const { data } = await api.get('/ventas');
  if (!Array.isArray(data)) return [];
  return data.map(normalizeSale);
}

export async function softDeleteVenta(codigoDeCompra) {
  await api.patch(`/ventas/${codigoDeCompra}/soft-delete`);
}

export async function eliminarVenta(codigoDeCompra) {
  await api.delete(`/ventas/${codigoDeCompra}/hard`);
}
