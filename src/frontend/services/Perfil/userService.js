// src/services/userService.js
import axios from 'axios';

const API = 'http://localhost:5000/api/user';

// Registrar comprador
export const registrarComprador = (data) =>
    axios.post(`${API}/register-buyer`, data);

// Registrar vendedor
export const registrarVendedor = (data, bankName, accountNumber, phoneNumber) =>
    axios.post(`${API}/register-seller`, data, {
        params: { bankName, accountNumber, phoneNumber }
    });

// Login
export const loginUsuario = (email, password) =>
    axios.post(`${API}/login`, null, {
        params: { email, password }
    });

// Consultar usuario por correo
export const consultarUsuario = (email) =>
    axios.get(`${API}/get`, { params: { email } });

// Historial de compras
export const historialCompras = (email) =>
    axios.get(`${API}/purchase-history`, { params: { email } });
