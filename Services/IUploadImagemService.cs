using Microsoft.AspNetCore.Http;

namespace MVCLanche.Services
{
    public interface IUploadImagemService
    {
        Task<string?> UploadImagemAsync(IFormFile arquivo);
        void ExcluirImagem(string nomeArquivo);
    }
}