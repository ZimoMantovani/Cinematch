import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

// Componente Header: A barra de navegação superior
const Header: React.FC = () => {
    // Acede ao AuthContext para saber se deve mostrar "Login" ou "Sair"
    const { isAuthenticated, logout } = useAuth();

    return (
        <header style={{ backgroundColor: '#c8e6c9', padding: '15px 0', color: '#4a7c59' }}>
            <div className="container" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                {/* O <Link> é vital em SPAs: ele troca de página sem recarregar o navegador inteiro */}
                <Link to="/" style={{ textDecoration: 'none', color: '#4a7c59', fontSize: '1.5em' }}>
                    Cinematch
                </Link>
                <nav>
                    <Link to="/about" style={{ margin: '0 15px', textDecoration: 'none', color: '#4a7c59' }}>
                        Sobre a gente
                    </Link>

                    {/* Renderização Condicional: Muda o menu baseado no estado de login */}
                    {isAuthenticated ? (
                        <>
                            <Link to="/profile" style={{ margin: '0 15px', textDecoration: 'none', color: '#4a7c59' }}>
                                Meu Perfil
                            </Link>
                            {/* Botão de Logout chama a função do Contexto para limpar o token */}
                            <button onClick={logout} className="button" style={{ margin: '0 15px', padding: '5px 10px', fontSize: '1em' }}>
                                Sair
                            </button>
                        </>
                    ) : (
                        <Link to="/login" style={{ margin: '0 15px', textDecoration: 'none', color: '#4a7c59' }}>
                            Login
                        </Link>
                    )}
                </nav>
            </div>
        </header>
    );
};

// Componente Footer: O rodapé fixo ou estático
const Footer: React.FC = () => {
    return (
        <footer style={{ backgroundColor: '#c8e6c9', padding: '10px 0', color: '#4a7c59', textAlign: 'center', position: 'fixed', bottom: 0, width: '100%' }}>
            <p>© 2025 Cinematch. Todos os direitos reservados.</p>
        </footer>
    );
};

// O Layout principal que junta tudo
const Layout: React.FC = () => {
    return (
        <>
            <Header />

            {/* <main>: Área de conteúdo principal */}
            <main className="container" style={{ paddingBottom: '60px' }}>
                {/* IMPORTANTE: O <Outlet /> é um placeholder do React Router Dom. 
            É aqui que as páginas filhas (Home, Login, Perfil) serão renderizadas 
            dependendo da URL, enquanto o Header e Footer se mantêm fixos.
        */}
                <Outlet />
            </main>

            <Footer />
        </>
    );
};

export default Layout;