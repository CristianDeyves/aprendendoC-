using CrudApi.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Estudantes
{
    public static class EstudantesRotas
    {
        public static void AddRotasEstudantes(this WebApplication app)
        {
            var rotasEstudantes = app.MapGroup("/estudantes");

            rotasEstudantes.MapPost("",
            async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var jaExisteEstudante = await context.Estudantes
                    .AnyAsync(estudanteDb => estudanteDb.Nome == request.Nome, ct);

                if (jaExisteEstudante)
                {
                    return Results.Conflict("Estudante já existe");
                }

                var novoEstudante = new Estudante(request.Nome, request.Email);
                await context.Estudantes.AddAsync(novoEstudante, ct);
                await context.SaveChangesAsync(ct);

                var estudanteRetorno = new EstudanteDTO(novoEstudante.Id, novoEstudante.Nome, novoEstudante.Email, novoEstudante.Nascimento);

                return Results.Created(estudanteRetorno.Id.ToString(), estudanteRetorno);
            });

            rotasEstudantes.MapGet("",
            async (AppDbContext context, CancellationToken ct) =>
            {
                var estudantes = await context
                .Estudantes
                .Where(estudante => estudante.Ativo)
                .Select(estudante => new EstudanteDTO(estudante.Id, estudante.Nome, estudante.Email, estudante.Nascimento))
                .ToListAsync(ct);
                return Results.Ok(estudantes);
            });

            rotasEstudantes.MapPut("{id}",
            async (Guid id, UpDateEstudanteRequest request, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id, ct);

                if (estudante is null)
                {
                    return Results.NotFound();
                }

                estudante.AtualizarNome(request.Nome);
                await context.SaveChangesAsync(ct);

                return Results.Ok(new EstudanteDTO(estudante.Id, estudante.Nome, estudante.Email, estudante.Nascimento));
            });

            rotasEstudantes.MapDelete("{id}",
            async (Guid id, AppDbContext context, CancellationToken ct) =>
            {
                var estudante = await context.Estudantes.SingleOrDefaultAsync(estudante => estudante.Id == id, ct);

                if (estudante is null)
                {
                    return Results.NotFound();
                }

                estudante.Desativar();
                await context.SaveChangesAsync(ct);

                return Results.NoContent();
            });
        }
    }
}
