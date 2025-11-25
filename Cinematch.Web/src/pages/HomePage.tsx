import React from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const HomePage: React.FC = () => {
  const { isAuthenticated } = useAuth();

  return (
    <div className="card">
      <h1>Cinematch</h1>
      {isAuthenticated && <h2>Olá!</h2>}
      <p>
        Quer assistir algum filme mas não sabe qual? Responda a esse pequeno quiz e deixe o Cinematch
        encontrar a combinação perfeita para o seu humor e suas preferências.
      </p>
      <Link to="/quiz">
        <button className="button" style={{ marginTop: '20px' }}>
          Vamos começar
        </button>
      </Link>
    </div>
  );
};

export default HomePage;
