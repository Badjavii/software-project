// src/services/buyerService.js
import axios from 'axios';

const API = 'http://localhost:5000/api/buyer';

export const registrarComprador = (data) =>
  axios.post(`${API}/register`, data);

export const loginComprador = (data) =>
  axios.post(`${API}/login`, data);

export const consultarComprador = (firstName, lastName) =>
  axios.get(`${API}/get`, {
    params: { firstName, lastName }
  });
