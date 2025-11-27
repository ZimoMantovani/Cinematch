import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';

// Ponto de Entrada (Entry Point):
// Seleciona a <div id="root"> que está lá no ficheiro index.html.
// Todo o site React será "desenhado" (renderizado) dentro dessa única div.
const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

root.render(
    // React.StrictMode: Uma ferramenta de desenvolvimento que ajuda a encontrar
    // problemas potenciais no código (ex: efeitos colaterais inseguros).
    // Ele executa alguns componentes duas vezes apenas em modo DEV para testar a estabilidade.
    <React.StrictMode>
        <App />
    </React.StrictMode>
);