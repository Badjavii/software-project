// src/services/sellerService.js
import axios from 'axios';

const API = 'http://localhost:5000/api/seller';

export const registrarVendedor = (data) =>
  axios.post(`${API}/register`, data);

export const loginVendedor = (data) =>
  axios.post(`${API}/login`, data);

export const consultarVendedor = (firstName, lastName) =>
  axios.get(`${API}/get`, {
    params: { firstName, lastName }
  });
