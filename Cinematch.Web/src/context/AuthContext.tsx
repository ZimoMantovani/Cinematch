import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import api from '../api'; // Importamos a nossa configuração do Axios

// Define o "formato" do nosso contexto. Que funções e variáveis ele disponibiliza?
interface AuthContextType {
    isAuthenticated: boolean; // Verdadeiro se estiver logado
    loading: boolean;         // Verdadeiro enquanto verificamos o token ao carregar a página
    login: (email: string, password: string) => Promise<void>;
    register: (email: string, password: string) => Promise<void>;
    logout: () => void;
    getToken: () => string | null; // Função auxiliar para pegar o token quando precisarmos
}

// Criamos o Contexto (a "caixa mágica" que guarda os dados)
const AuthContext = createContext<AuthContextType | undefined>(undefined);

// O Provider é o componente que envolve todo o site (lá no App.tsx) e distribui os dados
export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    // Estado local para controlar se está logado ou não
    const [isAuthenticated, setIsAuthenticated] = useState(false);

    // Estado de carregamento: começa true para não mostrar a tela de login antes de verificar o storage
    const [loading, setLoading] = useState(true);

    // EFEITO DE INICIALIZAÇÃO:
    // Roda uma única vez quando o site é aberto (F5).
    useEffect(() => {
        // Tenta recuperar o token salvo no navegador (Persistência)
        const token = localStorage.getItem('jwtToken');

        // Se existir um token, assumimos que o utilizador está logado.
        // (Num sistema mais robusto, validaríamos se o token não expirou aqui)
        if (token) {
            setIsAuthenticated(true);
        }

        // Terminou a verificação, liberta o site para carregar
        setLoading(false);
    }, []);

    // Função de Login
    const login = async (email: string, password: string) => {
        // 1. Chama a API (Backend)
        const response = await api.post('/auth/login', { email, password });

        // 2. Pega o token que a API devolveu
        const token = response.data.token;

        // 3. Salva no navegador (LocalStorage) para não perder o login ao fechar a aba
        localStorage.setItem('jwtToken', token);

        // 4. Atualiza o estado global avisando que está logado
        setIsAuthenticated(true);
    };

    // Função de Registo (Funciona quase igual ao Login)
    const register = async (email: string, password: string) => {
        const response = await api.post('/auth/register', { email, password });
        const token = response.data.token;
        localStorage.setItem('jwtToken', token);
        setIsAuthenticated(true);
    };

    // Função de Logout
    const logout = () => {
        // Remove o "crachá" do navegador
        localStorage.removeItem('jwtToken');
        // Atualiza o estado, o que fará o PrivateRoute redirecionar para o Login
        setIsAuthenticated(false);
    };

    const getToken = (): string | null => {
        return localStorage.getItem('jwtToken');
    };

    // Retorna o Provider com todas as funções e estados acessíveis aos filhos
    return (
        <AuthContext.Provider value={{ isAuthenticated, loading, login, register, logout, getToken }}>
            {children}
        </AuthContext.Provider>
    );
};

// Hook Personalizado (Custom Hook)
// Facilita o uso do contexto nos outros arquivos. 
// Em vez de importar useContext e AuthContext toda vez, só chamamos useAuth().
export const useAuth = () => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};