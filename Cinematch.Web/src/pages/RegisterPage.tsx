import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const RegisterPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const { register } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      await register(email, password);
      navigate('/');
    } catch (err: any) {
      setError('Falha no cadastro. Tente novamente.');
      console.error(err);
    }
  };

  return (
    <div className="card" style={{ maxWidth: '400px', margin: '50px auto' }}>
      <h2>Cadastro</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="E-mail"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="input-field"
          required
        />
        <input
          type="password"
          placeholder="Senha (mínimo 6 caracteres)"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="input-field"
          required
        />
        {error && <p style={{ color: 'red' }}>{error}</p>}
        <button type="submit" className="button" style={{ width: '100%', marginBottom: '10px' }}>
          Cadastrar
        </button>
      </form>
      <p style={{ marginTop: '20px' }}>
        Já tem conta? <Link to="/login" style={{ color: '#c8e6c9' }}>Faça Login</Link>
      </p>
    </div>
  );
};

export default RegisterPage;
