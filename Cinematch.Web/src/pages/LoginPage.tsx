import React, { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    try {
      await login(email, password);
      navigate('/');
    } catch (err: any) {
      setError('Falha no login. Verifique seu e-mail e senha.');
      console.error(err);
    }
  };



  return (
    <div className="card" style={{ maxWidth: '400px', margin: '50px auto' }}>
      <h2>Login</h2>
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
          placeholder="Senha"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="input-field"
          required
        />
        {error && <p style={{ color: 'red' }}>{error}</p>}
        <button type="submit" className="button" style={{ width: '100%', marginBottom: '10px' }}>
          Entrar
        </button>
      </form>
      <p style={{ marginTop: '20px' }}>
        Não tem conta? <Link to="/register" style={{ color: '#c8e6c9' }}>Cadastre-se</Link>
      </p>
    </div>
  );
};

export default LoginPage;
