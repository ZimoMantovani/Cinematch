import React, { createContext, useContext, useState, useEffect, ReactNode } from 'react';
import api from '../api';

interface AuthContextType {
  isAuthenticated: boolean;
  loading: boolean;
  login: (email: string, password: string) => Promise<void>;
  register: (email: string, password: string) => Promise<void>;
  logout: () => void;
  getToken: () => string | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      setIsAuthenticated(true);
    }
    setLoading(false);
  }, []);

  const login = async (email: string, password: string) => {
    const response = await api.post('/auth/login', { email, password });
    const token = response.data.token;
    localStorage.setItem('jwtToken', token);
    setIsAuthenticated(true);
  };

  const register = async (email: string, password: string) => {
    const response = await api.post('/auth/register', { email, password });
    const token = response.data.token;
    localStorage.setItem('jwtToken', token);
    setIsAuthenticated(true);
  };

  const logout = () => {
    localStorage.removeItem('jwtToken');
    setIsAuthenticated(false);
  };

  const getToken = (): string | null => {
    return localStorage.getItem('jwtToken');
  };

  return (
    <AuthContext.Provider value={{ isAuthenticated, loading, login, register, logout, getToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
