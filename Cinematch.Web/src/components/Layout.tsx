import React from 'react';
import { Outlet, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const Header: React.FC = () => {
  const { isAuthenticated, logout } = useAuth();

  return (
    <header style={{ backgroundColor: '#c8e6c9', padding: '15px 0', color: '#4a7c59' }}>
      <div className="container" style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <Link to="/" style={{ textDecoration: 'none', color: '#4a7c59', fontSize: '1.5em' }}>
          Cinematch
        </Link>
        <nav>
          <Link to="/about" style={{ margin: '0 15px', textDecoration: 'none', color: '#4a7c59' }}>
            Sobre a gente
          </Link>
          {/* Link para Contato (pode ser adicionado depois) */}
          {isAuthenticated ? (
            <>
              <Link to="/profile" style={{ margin: '0 15px', textDecoration: 'none', color: '#4a7c59' }}>
                Meu Perfil
              </Link>
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

const Footer: React.FC = () => {
  return (
    <footer style={{ backgroundColor: '#c8e6c9', padding: '10px 0', color: '#4a7c59', textAlign: 'center', position: 'fixed', bottom: 0, width: '100%' }}>
      <p>© 2025 Cinematch. Todos os direitos reservados.</p>
    </footer>
  );
};

const Layout: React.FC = () => {
  return (
    <>
      <Header />
      <main className="container" style={{ paddingBottom: '60px' }}>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default Layout;
