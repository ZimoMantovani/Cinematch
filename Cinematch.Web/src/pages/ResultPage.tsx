import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import api from '../api';

interface Movie {
  movieId: number;
  title: string;
  overview: string;
  posterUrl: string;
}

const ResultPage: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const movie: Movie | undefined = location.state?.movie;
  const error: string | undefined = location.state?.error;

  const [rating, setRating] = useState(0);
  const [reviewText, setReviewText] = useState('');
  const [message, setMessage] = useState('');

  const handleWatch = async () => {
    if (!movie) return;
    try {
      await api.post('/movie/watch', {
        movieId: movie.movieId,
        movieTitle: movie.title,
        moviePosterUrl: movie.posterUrl,
      });
      setMessage('Filme marcado como visto!');
    } catch (err) {
      setMessage('Erro ao marcar como visto.');
    }
  };

  const handleRate = async () => {
    if (!movie || rating === 0) {
      setMessage('Selecione uma nota de 1 a 5.');
      return;
    }
    try {
      await api.post('/movie/rate', {
        movieId: movie.movieId,
        rating: rating,
        reviewText: reviewText,
        movieTitle: movie.title,
        moviePosterUrl: movie.posterUrl,
      });
      setMessage('Avaliação salva com sucesso!');
    } catch (err) {
      setMessage('Erro ao salvar avaliação.');
    }
  };

  if (error) {
    return (
      <div className="card" style={{ maxWidth: '600px', margin: '50px auto' }}>
        <h2>Resultado do Quiz</h2>
        <p style={{ color: 'red', fontSize: '1.2em' }}>{error}</p>
        <button className="button" onClick={() => navigate('/quiz')} style={{ marginTop: '20px' }}>
          Reiniciar Quiz
        </button>
      </div>
    );
  }

  if (!movie) {
    return <div className="card">Nenhuma recomendação encontrada.</div>;
  }

  return (
    <div className="card" style={{ maxWidth: '800px', margin: '50px auto' }}>
      <h2>O seu filme escolhido é:</h2>
      <div style={{ display: 'flex', gap: '20px', textAlign: 'left', alignItems: 'flex-start' }}>
        <img src={movie.posterUrl} alt={movie.title} style={{ width: '200px', borderRadius: '8px' }} />
        <div>
          <h3>{movie.title}</h3>
          <p>{movie.overview}</p>
          <div style={{ marginTop: '20px' }}>
            <button className="button" onClick={handleWatch}>
              Marcar como Visto
            </button>
            <button className="button" onClick={() => navigate('/quiz')}>
              Reiniciar Quiz
            </button>
          </div>
        </div>
      </div>

      <div style={{ marginTop: '30px', borderTop: '1px solid #c8e6c9', paddingTop: '20px' }}>
        <h3>Avalie o Filme</h3>
        <div style={{ marginBottom: '10px' }}>
          {[1, 2, 3, 4, 5].map((star) => (
            <span
              key={star}
              style={{ cursor: 'pointer', fontSize: '2em', color: star <= rating ? '#FFD700' : '#f5f5dc' }}
              onClick={() => setRating(star)}
            >
              ★
            </span>
          ))}
        </div>
        <textarea
          placeholder="Escreva sua avaliação (opcional)"
          value={reviewText}
          onChange={(e) => setReviewText(e.target.value)}
          style={{ width: '100%', minHeight: '100px', padding: '10px', boxSizing: 'border-box', backgroundColor: '#f5f5dc', color: '#4a7c59', border: 'none', borderRadius: '5px', fontFamily: 'VT323, monospace' }}
        />
        <button className="button" onClick={handleRate} style={{ marginTop: '10px' }}>
          Salvar Avaliação
        </button>
      </div>
      {message && <p style={{ marginTop: '10px', color: '#c8e6c9' }}>{message}</p>}
    </div>
  );
};

export default ResultPage;
