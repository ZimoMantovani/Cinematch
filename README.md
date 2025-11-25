# 🎬 Cinematch

O **Cinematch** é uma aplicação Full-Stack desenvolvida para amantes de
cinema. O sistema permite que usuários criem uma conta, busquem filmes,
marquem os que já assistiram e avaliem suas obras favoritas.

Este projeto foi construído para demonstrar a integração entre um
Frontend moderno e reativo com uma API Backend robusta e segura.

------------------------------------------------------------------------

## 🚀 Tecnologias Utilizadas

### **Frontend (Web)**

-   React (com TypeScript)
-   Vite
-   Axios
-   React Router Dom
-   CSS Modules

### **Backend (API)**

-   C# .NET 8
-   Entity Framework Core
-   SQLite
-   JWT Bearer
-   Swagger

### **Integrações**

-   TMDb API (The Movie Database)

------------------------------------------------------------------------

## ✨ Funcionalidades

-   Autenticação com JWT\
-   Catálogo integrado ao TMDb\
-   Marcação de filmes assistidos\
-   Avaliações e comentários\
-   Página de perfil com histórico

------------------------------------------------------------------------

## 🔧 Pré-requisitos

-   Node.js 18+
-   .NET SDK 8
-   Git

------------------------------------------------------------------------

## 📦 Como rodar o projeto

### 1. Clonar o repositório

``` bash
git clone https://github.com/SEU_USUARIO/Cinematch.git
cd Cinematch
```

------------------------------------------------------------------------

## 2. Configurando o Backend (API)

``` bash
cd Cinematch.Api
```

Crie o arquivo `appsettings.json`:

``` json
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
    "BaseUrl": "https://api.themoviedb.org/3/"
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
```

Criar banco e rodar API:

``` bash
dotnet tool install --global dotnet-ef
dotnet ef database update
dotnet run
```

------------------------------------------------------------------------

## 3. Configurando o Frontend

``` bash
cd Cinematch.Web
npm install
npm run dev
```

Ajuste `src/api.ts` conforme a porta da API:

``` ts
baseURL: "https://localhost:61582/api";
```

------------------------------------------------------------------------

## 📂 Estrutura do Projeto

    Cinematch/
    ├── Cinematch.Api/
    │   ├── Controllers/
    │   ├── Data/
    │   ├── Models/
    │   └── appsettings.json
    │
    ├── Cinematch.Web/
    │   └── src/
    │       ├── components/
    │       ├── context/
    │       ├── pages/
    │       └── api.ts
    │
    └── README.md

------------------------------------------------------------------------

## 🤝 Contribuindo

Contribuições são bem-vindas!

------------------------------------------------------------------------

## 📝 Licença

Projeto criado para fins educacionais.
