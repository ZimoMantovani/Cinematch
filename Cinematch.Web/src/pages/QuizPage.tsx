import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api';

interface QuizQuestion {
  id: number;
  text: string;
  options: { [key: string]: string };
}

const QuizPage: React.FC = () => {
  const [questions, setQuestions] = useState<QuizQuestion[]>([]);
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
  const [answers, setAnswers] = useState<string[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchQuestions = async () => {
      try {
        const response = await api.get<QuizQuestion[]>('/quiz/questions');
        setQuestions(response.data);
        setLoading(false);
      } catch (err) {
        setError('Erro ao carregar as perguntas do quiz.');
        setLoading(false);
      }
    };
    fetchQuestions();
  }, []);

  const handleAnswer = (optionKey: string) => {
    const questionId = questions[currentQuestionIndex].id;
    const newAnswer = `Q${questionId}${optionKey}`;
    const newAnswers = [...answers, newAnswer];
    setAnswers(newAnswers);

    if (currentQuestionIndex < questions.length - 1) {
      setCurrentQuestionIndex(currentQuestionIndex + 1);
    } else {
      // Última pergunta, enviar respostas
      submitQuiz(newAnswers);
    }
  };

  const submitQuiz = async (finalAnswers: string[]) => {
    setLoading(true);
    setError('');
    try {
      const response = await api.post('/quiz/recommend', finalAnswers);
      navigate('/result', { state: { movie: response.data } });
    } catch (err: any) {
      // Tratamento de Erro (RF 1.3)
      const errorMessage = err.response?.data?.message || "Ops, não encontramos um filme. Tente o quiz novamente.";
      navigate('/result', { state: { error: errorMessage } });
    } finally {
      setLoading(false);
    }
  };

  if (loading && questions.length === 0) {
    return <div className="card">Carregando Quiz...</div>;
  }

  if (error) {
    return <div className="card" style={{ color: 'red' }}>{error}</div>;
  }

  const currentQuestion = questions[currentQuestionIndex];
  const isLastQuestion = currentQuestionIndex === questions.length - 1;

  return (
    <div className="card" style={{ maxWidth: '600px', margin: '50px auto' }}>
      <h2>Quiz Cinematch</h2>
      {currentQuestion && (
        <>
          <p>Pergunta {currentQuestionIndex + 1}/{questions.length}</p>
          <h3>{currentQuestion.text}</h3>
          <div style={{ display: 'flex', flexDirection: 'column', gap: '10px', marginTop: '20px' }}>
            {Object.entries(currentQuestion.options).map(([key, value]) => (
              <button
                key={key}
                className="button"
                onClick={() => handleAnswer(key)}
              >
                {key}) {value}
              </button>
            ))}
          </div>
          {isLastQuestion && (
            <p style={{ marginTop: '20px', color: '#c8e6c9' }}>
              Ao responder esta pergunta, você descobrirá sua recomendação!
            </p>
          )}
        </>
      )}
    </div>
  );
};

export default QuizPage;
