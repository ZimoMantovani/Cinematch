import React from 'react';

const AboutPage: React.FC = () => {
  return (
    <div className="card" style={{ maxWidth: '600px', margin: '50px auto' }}>
      <h2>Sobre o Cinematch</h2>
      <p style={{ textAlign: 'left' }}>
        O projeto Cinematch é totalmente acadêmico, desenvolvido por estudantes de engenharia da computação do IFSP, Campus Piracicaba.
      </p>
      <p style={{ textAlign: 'left' }}>
        Desenvolvedores:
        <ul>
          <li>Symon Mantovani</li>
          <li>Felipe Nakano</li>
          <li>Natália Mazzilli</li>
        </ul>
      </p>
      <p style={{ textAlign: 'left' }}>
        O objetivo é demonstrar a integração de tecnologias modernas como C# .NET 8 Web API, React com TypeScript, JWT e a API externa TMDb, em uma aplicação completa de página única (SPA).
      </p>
    </div>
  );
};

export default AboutPage;
