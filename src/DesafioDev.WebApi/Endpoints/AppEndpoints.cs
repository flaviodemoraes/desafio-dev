using DesafioDev.Operacoes.Applications.Services;
using System.Text;

namespace DesafioDev.WebApi.Endpoints
{
    public static class AppEndpoints
    {
        const string Group = "CNAB";

        public static void MapAppEndpoints(this WebApplication app)
        {
            app.MapGroup("api")
                .MapGet("/", () => $"Desafio.MinimalAPI :: DateTime: {DateTime.Now} UTC: {DateTime.UtcNow}").WithTags(Group);

            app.MapGroup("api")
                .MapPost("/fileUpload", async (IFormFile file, ITranslateFileDataAppService _service) =>
                {
                    if (file.FileName == null || file.Length <= 0)
                        return Results.BadRequest("Nenhum arquivo foi enviado.");

                    using (var reader = new StreamReader(file.OpenReadStream(), Encoding.UTF8))
                        await _service.TranslateFile(reader);

                    return Results.Ok();
                }).WithTags(Group);

            app.MapGroup("api")
                .MapGet("/obterListaLojas", async (int page, int limit, IOperacaoAppService _service) =>
                {
                    return Results.Ok(await _service.GetListLojasAsync(page, limit));

                }).WithTags(Group);

            app.MapGroup("api")
                .MapGet("/obterOperacoesPorLojas", async (Guid lojaId, int page, int limit, IOperacaoAppService _service) =>
                {
                    return Results.Ok(await _service.GetListOperacoesByLojaAsync(lojaId, page, limit));

                }).WithTags(Group);
        }
    }
}
