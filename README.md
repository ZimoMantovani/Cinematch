# 🎬 Cinematch

O **Cinematch** é uma aplicação Full-Stack desenvolvida para amantes de cinema. O sistema permite que usuários criem uma conta, busquem filmes, marquem os que já assistiram e avaliem suas obras favoritas.

Este projeto foi construído para demonstrar a integração entre um Frontend moderno e reativo com uma API Backend robusta e segura.

---

## 🚀 Tecnologias Utilizadas

### Frontend (Web)
* **React** (com TypeScript)
* **Vite** (Build tool de alta performance)
* **Axios** (Requisições HTTP)
* **React Router Dom** (Navegação SPA)
* **CSS Modules** (Estilização modular)

### Backend (API)
* **C# .NET 8** (ASP.NET Core Web API)
* **Entity Framework Core** (ORM)
* **SQLite** (Banco de dados relacional leve)
* **JWT Bearer** (Autenticação e Segurança)
* **Swagger** (Documentação automática da API)

### Integrações
* **TMDb API** (The Movie Database) - Fonte dos dados de filmes e posters.

---

## ✨ Funcionalidades

* ✅ **Autenticação:** Cadastro e Login de usuários com tokens JWT seguros.
* ✅ **Catálogo:** Busca de filmes em tempo real integrando com a API do TMDb.
* ✅ **Diário:** Marcar filmes como "Assistidos" (Watchlist).
* ✅ **Avaliação:** Sistema de notas (1 a 5 estrelas) com comentários.
* ✅ **Perfil:** Página dedicada para visualizar o histórico de filmes do usuário.

---

## 🔧 Pré-requisitos

Antes de começar, certifique-se de ter instalado em sua máquina:
* [Node.js](https://nodejs.org/) (Versão 18 ou superior)
* [.NET SDK 8.0](https://dotnet.microsoft.com/download)
* [Git](https://git-scm.com/)

---

## 📦 Como rodar o projeto

Siga o passo a passo abaixo para configurar o ambiente de desenvolvimento.

### 1. Clonar o repositório

```bash
git clone [https://github.com/SEU_USUARIO/Cinematch.git](https://github.com/SEU_USUARIO/Cinematch.git)
cd Cinematch
2. Configurando o Backend (API)
Entre na pasta do servidor:

Bash

cd Cinematch.Api
⚠️ Passo Importante: Como arquivos de configuração com senhas não são enviados ao GitHub, você precisa criar um arquivo chamado appsettings.json dentro da pasta Cinematch.Api.

Crie o arquivo e cole o seguinte conteúdo (lembre-se de colocar sua chave do TMDb):

JSON

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "TMDbSettings": {
    "ApiKey": "COLE_SUA_CHAVE_DO_TMDB_AQUI",
    "BaseUrl": "[https://api.themoviedb.org/3/](https://api.themoviedb.org/3/)"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Cinematch.db"
  },
  "Jwt": {
    "Key": "minha-chave-secreta-super-poderosa-para-testes-123",
    "Issuer": "CinematchIssuer",
    "Audience": "CinematchAudience"
  }
}
Agora, crie o banco de dados SQLite e rode o servidor:

Bash

# Instala a ferramenta do EF Core (caso não tenha)
dotnet tool install --global dotnet-ef

# Cria o arquivo do banco de dados (Cinematch.db)
dotnet ef database update

# Roda a API
dotnet run
Anotação: Observe no terminal em qual porta a API está rodando (ex: https://localhost:61582).

3. Configurando o Frontend (Web)
Abra um novo terminal, volte para a raiz do projeto e entre na pasta Web:

Bash

cd Cinematch.Web
Instale as dependências e inicie o projeto:

Bash

npm install
npm run dev
Configuração de API: Verifique se o arquivo src/api.ts está apontando para a porta correta do seu Backend.

TypeScript

// Exemplo em src/api.ts
baseURL: 'https://localhost:61582/api' // Ajuste a porta conforme necessário
📂 Estrutura do Projeto
Cinematch/
├── Cinematch.Api/        # Backend em .NET 8
│   ├── Controllers/      # Rotas da API (Auth, Movies)
│   ├── Data/             # Contexto do Banco de Dados (SQLite)
│   ├── Models/           # Classes das Tabelas (User, Movie)
│   └── appsettings.json  # Configurações (Não versionado)
│
├── Cinematch.Web/        # Frontend em React
│   ├── src/
│   │   ├── components/   # Componentes reutilizáveis
│   │   ├── context/      # Gerenciamento de Estado (Auth)
│   │   ├── pages/        # Telas do sistema
│   │   └── api.ts        # Configuração do Axios
│   └── vite.config.ts    # Configuração do Vite
│
└── README.md             # Documentação
🤝 Contribuindo
Contribuições são bem-vindas! Se você tiver sugestões de melhorias ou encontrar bugs, sinta-se à vontade para abrir uma issue ou enviar um pull request.

📝 Licença
Este projeto foi desenvolvido para fins educacionais.