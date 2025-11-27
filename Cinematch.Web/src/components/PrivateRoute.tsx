import React, { ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

// Define que este componente aceita "filhos" (outros componentes dentro dele)
interface PrivateRouteProps {
    children: ReactNode;
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
    // Acede ao Contexto Global para saber se o utilizador está logado
    const { isAuthenticated, loading } = useAuth();

    // 1. Estado de Carregamento:
    // Enquanto o Firebase/Backend ainda está a verificar o token, mostramos "Carregando..."
    // Isso evita que o site redirecione para o login indevidamente antes de terminar a verificação.
    if (loading) {
        return <div>Carregando...</div>;
    }

    // 2. Verificação de Segurança (Cliente-Side):
    // Se estiver autenticado, renderiza o conteúdo protegido ({children}).
    // Se NÃO estiver, redireciona o utilizador para a página de Login usando o <Navigate>.
    return isAuthenticated ? <>{children}</> : <Navigate to="/login" />;
};

export default PrivateRoute;