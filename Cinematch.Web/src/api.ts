import axios from 'axios';

// Define o endereço do nosso Backend C# (.NET 8)
// Em produção, isto viria de uma variável de ambiente (.env), mas hardcoded serve para o TCC.
const API_BASE_URL = 'https://localhost:61582/api';

// Cria uma instância do Axios configurada.
// Em vez de usar 'fetch' ou 'axios' solto em cada arquivo, usamos sempre esta instância 'api'.
const api = axios.create({
    baseURL: API_BASE_URL,
});

// INTERCEPTADOR (O Segredo da Autenticação):
// Este código roda AUTOMATICAMENTE antes de TODA requisição sair do site.
api.interceptors.request.use((config) => {
    // 1. Vai ao navegador ver se temos um crachá (Token JWT) guardado.
    const token = localStorage.getItem('jwtToken');

    // 2. Se tiver token, cola ele no Cabeçalho (Header) da carta.
    // Formato padrão: "Authorization: Bearer eyJhbGciOiJIUz..."
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, (error) => {
    return Promise.reject(error);
});

export default api;