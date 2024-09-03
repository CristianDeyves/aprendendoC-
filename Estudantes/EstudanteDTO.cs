using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi.Estudantes
{
    public record EstudanteDTO (Guid Id, string Nome, string Email, DateTime Nascimento);
}