import axios from 'axios';
// Configuração da URL base da API C#
const API_BASE_URL = 'https://localhost:61582/api';

const api = axios.create({
  baseURL: API_BASE_URL,
});

// Interceptor para adicionar o token JWT do localStorage em todas as requisições
api.interceptors.request.use((config) => {
  const token = localStorage.getItem('jwtToken');

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}, (error) => {
  return Promise.reject(error);
});

export default api;
