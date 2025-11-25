import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import Layout from './components/Layout';
import HomePage from './pages/HomePage';
import QuizPage from './pages/QuizPage';
import ResultPage from './pages/ResultPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import ProfilePage from './pages/ProfilePage';
import AboutPage from './pages/AboutPage';
import PrivateRoute from './components/PrivateRoute';

const App: React.FC = () => {
  return (
    <Router>
      <AuthProvider>
        <Routes>
          <Route path="/" element={<Layout />}>
            <Route index element={<HomePage />} />
            <Route path="login" element={<LoginPage />} />
            <Route path="register" element={<RegisterPage />} />
            <Route path="about" element={<AboutPage />} />
            <Route path="quiz" element={<PrivateRoute><QuizPage /></PrivateRoute>} />
            <Route path="result" element={<PrivateRoute><ResultPage /></PrivateRoute>} />
            <Route path="profile" element={<PrivateRoute><ProfilePage /></PrivateRoute>} />
            {/* Adicione outras rotas aqui */}
          </Route>
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
