import axios from 'axios';

const API = 'http://localhost:5000/api/seller';

export const registrarVendedor = (data) => axios.post(`${API}/register`, data);