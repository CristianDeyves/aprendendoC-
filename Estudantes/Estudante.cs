using System.ComponentModel;

namespace CrudApi.Estudantes
{
    public class Estudante
    {
        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime Nascimento { get; private set; }
        public bool Ativo { get; private set; }

        public Estudante(string nome, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Nascimento = new DateTime().ToUniversalTime();
            Ativo = true;
        }

        public void AtualizarNome(string nome)
        {
            Nome = nome;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void AtualizarEmail(string email)
        {
            Email = email;
        }

        public void AtualizarNascimento(string nascimento)
        {
            Nascimento = new DateTime().ToUniversalTime();
        }
    }
}