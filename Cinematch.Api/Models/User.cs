namespace Cinematch.Api.Models
{
    // Esta classe representa a tabela 'Users' no banco de dados SQLite.
    public class User
    {
        // Chave Primária (PK): Identificador único do usuário no banco.
        public int Id { get; set; }

        // O e-mail usado para login.
        public string Email { get; set; }

        // IMPORTANTE: Nunca salvamos a senha pura (ex: "123456").
        // Salvamos o 'Hash', que é a senha criptografada para segurança.
        public string PasswordHash { get; set; }
    }
}