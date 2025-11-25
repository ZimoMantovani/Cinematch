# Guia de Configuração e Execução do Projeto Cinematch no Visual Studio 2022

Este guia detalhado irá ajudá-lo a configurar e executar o projeto **Cinematch**, uma aplicação full-stack que utiliza **C# .NET 8 Web API** no backend e **React com TypeScript** no frontend.

## 1. Pré-requisitos

Para rodar o projeto, você precisará dos seguintes softwares instalados:

1. **Visual Studio 2022**: Com as seguintes cargas de trabalho instaladas:
  - Desenvolvimento ASP.NET e Web
  - Desenvolvimento Node.js

1. **SDK do .NET 8**: (Geralmente instalado com o VS 2022)

1. **Node.js e npm**: (Recomendado usar a versão LTS)

1. **Conta no Firebase**: Para autenticação e banco de dados (Firestore).

1. **Chave de API do TMDb (The Movie Database)**: Para buscar filmes.

## 2. Configuração de Serviços Externos

### 2.1. Configuração do Firebase

O projeto utiliza o Firebase para autenticação e banco de dados.

1. **Crie um Projeto Firebase**: Acesse o [Console do Firebase](https://console.firebase.google.com/) e crie um novo projeto.

1. **Habilite a Autenticação**:
  - No menu lateral, vá em **Build** > **Authentication**.
  - Clique em **Get started** e habilite os provedores **Email/Password** e **Google**.

1. **Habilite o Firestore**:
  - No menu lateral, vá em **Build** > **Firestore Database**.
  - Clique em **Create database** e escolha o modo de produção ou teste (o modo de teste é mais fácil para começar).

1. **Obtenha as Configurações do Aplicativo Web**:
  - Na visão geral do projeto, clique no ícone `</>` (Web) para registrar um novo aplicativo.
  - Copie o objeto de configuração que será gerado. Você precisará de: `apiKey`, `authDomain`, `projectId`, `storageBucket`, `messagingSenderId`, `appId`.

### 2.2. Configuração do TMDb

1. **Obtenha uma Chave de API**: Crie uma conta no [The Movie Database (TMDb)](https://www.themoviedb.org/) e gere uma chave de API (v3).

## 3. Configuração do Backend (Cinematch.Api)

O backend é uma API Web em C# .NET 8.

1. **Abra a Solução no Visual Studio 2022**:
  - Abra o arquivo `Cinematch.sln` no Visual Studio 2022.
  - A solução deve carregar dois projetos: `Cinematch.Api` e `Cinematch.Web`.

1. **Instale o SDK do Google Cloud (Opcional, mas Recomendado)**:
  - Para que o backend se comunique com o Firestore, você precisa autenticar o aplicativo. A maneira mais fácil é instalar o [Google Cloud CLI](https://cloud.google.com/sdk/docs/install) e executar:

1. **Configure o ****`appsettings.json`**:
  - Abra o arquivo `Cinematch/Cinematch.Api/appsettings.json`.
  - Substitua os placeholders com suas chaves:

## 4. Configuração do Frontend (Cinematch.Web )

O frontend é uma aplicação React com TypeScript.

1. **Instale as Dependências do Node.js**:
  - No Visual Studio, clique com o botão direito no projeto `Cinematch.Web` e selecione **Abrir Terminal**.
  - Execute o comando para instalar as dependências:

1. **Configure o ****`AuthContext.tsx`**:
  - Abra o arquivo `Cinematch/Cinematch.Web/src/context/AuthContext.tsx`.
  - Substitua o objeto `firebaseConfig` com as configurações que você obteve no passo 2.1:

## 5. Execução do Projeto

O Visual Studio 2022 pode executar ambos os projetos simultaneamente.

1. **Definir Projetos de Inicialização**:
  - Clique com o botão direito na **Solução 'Cinematch'** no Gerenciador de Soluções.
  - Selecione **Configurar Projetos de Inicialização...**.
  - Escolha a opção **Vários projetos de inicialização**.
  - Defina a **Ação** para ambos os projetos (`Cinematch.Api` e `Cinematch.Web`) como **Iniciar**.

1. **Iniciar a Solução**:
  - Pressione **F5** ou clique no botão **Iniciar** (Geralmente "Cinematch.Api, Cinematch.Web").
    - O Visual Studio iniciará:
      - O backend `Cinematch.Api` (geralmente na porta 5000/5001).
      - O frontend `Cinematch.Web` (geralmente na porta 3000).

1. **Acessar a Aplicação**:
  - O navegador deve abrir automaticamente a aplicação em `http://localhost:3000`.
  - **Se não abrir**: Navegue manualmente para `http://localhost:3000`.

## 6. Estrutura de Arquivos

O projeto está organizado da seguinte forma:

| Projeto | Descrição | Tecnologias |
| --- | --- | --- |
| `Cinematch.Api` | Backend (API RESTful ) | C# .NET 8, Google Firestore, TMDb API |
| `Cinematch.Web` | Frontend (Single-Page Application) | React, TypeScript, Firebase Auth, Axios |

**Arquivos Chave do Backend (****`Cinematch.Api`****)**:

| Arquivo | Função |
| --- | --- |
| `appsettings.json` | Configurações de API Key e Project ID. |
| `Program.cs` | Configuração de serviços, CORS, autenticação JWT. |
| `Controllers/QuizController.cs` | Endpoint para obter perguntas e recomendar filmes. |
| `Controllers/MovieController.cs` | Endpoints protegidos para marcar como visto e avaliar filmes. |
| `Services/TMDbService.cs` | Lógica de comunicação com a API TMDb. |
| `Services/FirebaseService.cs` | Lógica de comunicação com o Firestore. |
| `Services/QuizLogicService.cs` | Algoritmo de pontuação e perguntas do quiz. |

**Arquivos Chave do Frontend (****`Cinematch.Web`****)**:

| Arquivo | Função |
| --- | --- |
| `src/index.css` | Estilos globais (paleta de cores e fonte monoespaçada). |
| `src/context/AuthContext.tsx` | Contexto React para gerenciar o estado de autenticação do Firebase. |
| `src/api.ts` | Configuração do Axios com interceptor para adicionar o token JWT. |
| `src/components/Layout.tsx` | Componente de layout (Header e Footer). |
| `src/pages/HomePage.tsx` | Página inicial com link para o quiz. |
| `src/pages/LoginPage.tsx` | Formulário de login. |
| `src/pages/QuizPage.tsx` | Lógica de exibição e envio das respostas do quiz. |
| `src/pages/ResultPage.tsx` | Exibição da recomendação e funcionalidades de "Visto" e "Avaliar". |
| `src/pages/ProfilePage.tsx` | Exibição de filmes vistos e avaliações do usuário. |

---

**Observação Importante sobre Segurança**: A configuração de autenticação JWT no `Program.cs` é uma implementação básica para fins acadêmicos. Em um ambiente de produção, a validação do token Firebase deve ser feita de forma mais robusta, idealmente usando o **Firebase Admin SDK** para C#.

Espero que este guia ajude você a rodar o projeto com sucesso!

