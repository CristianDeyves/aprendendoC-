using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi.Estudantes
{
    public class Estudante
    {
        public Guid Id { get; init; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; }

        public Estudante(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Ativo = true;
        }
    }
}