using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace MVCLanche.Services
{
    public class UploadImagemService : IUploadImagemService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadImagemService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string?> UploadImagemAsync(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return null;

            // Gera um nome único para evitar arquivos com o mesmo nome
            var nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);

            // Caminho da pasta wwwroot/images
            var pastaImagens = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            // Se a pasta não existir, cria automaticamente
            if (!Directory.Exists(pastaImagens))
            {
                Directory.CreateDirectory(pastaImagens);
            }

            var caminhoCompleto = Path.Combine(pastaImagens, nomeArquivo);

            using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return "/images/" + nomeArquivo;
        }

        public void ExcluirImagem(string nomeArquivo)
        {
            if (string.IsNullOrEmpty(nomeArquivo))
                return;

            // Remove a "/" inicial caso exista
            nomeArquivo = nomeArquivo.TrimStart('/');

            // Caminho físico do arquivo
            var caminhoArquivo = Path.Combine(_webHostEnvironment.WebRootPath, nomeArquivo);

            if (File.Exists(caminhoArquivo))
            {
                File.Delete(caminhoArquivo);
            }
        }
    }
}