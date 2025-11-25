import React, { useState, useEffect } from 'react';
import api from '../api';

interface WatchedMovie {
  movieId: number;
  movieTitle: string;
  moviePosterUrl: string;
}

interface UserRating {
  movieId: number;
  movieTitle: string;
  moviePosterUrl: string;
  rating: number;
  reviewText: string;
}

interface UserProfile {
  watchedMovies: WatchedMovie[];
  userRatings: UserRating[];
}

const ProfilePage: React.FC = () => {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    const fetchProfile = async () => {
      try {
        const response = await api.get<UserProfile>('/movie/myprofile');
        setProfile(response.data);
        setLoading(false);
      } catch (err) {
        setError('Erro ao carregar o perfil. Certifique-se de estar logado e com o Firebase configurado.');
        setLoading(false);
      }
    };
    fetchProfile();
  }, []);

  if (loading) {
    return <div className="card">Carregando Perfil...</div>;
  }

  if (error) {
    return <div className="card" style={{ color: 'red' }}>{error}</div>;
  }

  return (
    <div className="card" style={{ maxWidth: '1000px', margin: '50px auto' }}>
      <h2>Meu Perfil</h2>

      <h3 style={{ marginTop: '30px' }}>Filmes que você já assistiu ({profile?.watchedMovies.length || 0})</h3>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px', justifyContent: 'center' }}>
        {profile?.watchedMovies.map((movie) => (
          <div key={movie.movieId} style={{ width: '150px', textAlign: 'center' }}>
            <img src={movie.moviePosterUrl} alt={movie.movieTitle} style={{ width: '100%', borderRadius: '8px' }} />
            <p style={{ fontSize: '0.9em' }}>{movie.movieTitle}</p>
          </div>
        ))}
      </div>

      <h3 style={{ marginTop: '30px' }}>Suas Avaliações ({profile?.userRatings.length || 0})</h3>
      <div style={{ display: 'flex', flexWrap: 'wrap', gap: '20px', justifyContent: 'center' }}>
        {profile?.userRatings.map((rating) => (
          <div key={rating.movieId} style={{ width: '200px', textAlign: 'center', border: '1px solid #c8e6c9', padding: '10px', borderRadius: '8px' }}>
            <img src={rating.moviePosterUrl} alt={rating.movieTitle} style={{ width: '100%', borderRadius: '8px' }} />
            <p style={{ fontSize: '1.1em', fontWeight: 'bold' }}>{rating.movieTitle}</p>
            <p>Nota: {rating.rating} / 5</p>
            {rating.reviewText && <p style={{ fontStyle: 'italic', fontSize: '0.9em' }}>"{rating.reviewText}"</p>}
          </div>
        ))}
      </div>
    </div>
  );
};

export default ProfilePage;
