// src/services/buyerService.js
import axios from 'axios';

const API = 'http://localhost:5000/api/buyer';

export const registrarComprador = (data) => axios.post(`${API}/register`, data);
