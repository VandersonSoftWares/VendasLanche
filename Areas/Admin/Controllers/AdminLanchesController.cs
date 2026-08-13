
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCLanche.Context;
using MVCLanche.Models;
using MVCLanche.Services;
using MVCLanche.ViewModels;

namespace MVCLanche.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUploadImagemService _uploadImagemService;

        public AdminLanchesController(
            AppDbContext context,
            IUploadImagemService uploadImagemService)
        {
            _context = context;
            _uploadImagemService = uploadImagemService;
        }

        // GET: LANCHES
        public async Task<IActionResult> Index()
        {
            var lanches = await _context.Lanches
                .Include(l => l.Categoria)
                .ToListAsync();

            return View(lanches);
        }

        // GET: LANCHES/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);

            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // GET: LANCHES/Create
        public IActionResult Create()
        {
            ViewBag.CategoriaId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Categorias,
                "CategoriaId",
                "CategoriaNome");

            return View(new LancheFormViewModel());//estava só retun view()
        }

        // POST: LANCHES/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LancheFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? caminhoImagem = null;

                if (model.ImagemArquivo != null)
                {
                    caminhoImagem =
                        await _uploadImagemService.UploadImagemAsync(model.ImagemArquivo);
                }

                var lanche = new Lanche
                {
                    Nome = model.Nome,
                    DescricaoCurta = model.DescricaoCurta,
                    DescricaoDetalhada = model.DescricaoDetalhada,
                    Preco = model.Preco,
                    CategoriaId = model.CategoriaId,
                    EmEstoque = model.EmEstoque,
                    IsLanchePreferido = model.IsLanchePreferido,
                    ImagemUrl = caminhoImagem ?? "",
                    ImagemThumbnailUrl = caminhoImagem ?? ""
                };

                _context.Lanches.Add(lanche);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Categorias,
                "CategoriaId",
                "CategoriaNome",
                model.CategoriaId);

            return View(model);
        }

        // GET: LANCHES/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches.FindAsync(id);

            if (lanche == null)
            {
                return NotFound();
            }

            var model = new LancheFormViewModel
            {
                LancheId = lanche.LancheId,
                Nome = lanche.Nome,
                DescricaoCurta = lanche.DescricaoCurta,
                DescricaoDetalhada = lanche.DescricaoDetalhada,
                Preco = lanche.Preco,
                CategoriaId = lanche.CategoriaId,
                EmEstoque = lanche.EmEstoque,
                IsLanchePreferido = lanche.IsLanchePreferido,
                ImagemUrl = lanche.ImagemUrl,
                ImagemThumbnailUrl = lanche.ImagemThumbnailUrl
            };

            ViewBag.CategoriaId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Categorias,
                "CategoriaId",
                "CategoriaNome",
                model.CategoriaId);

            return View(model);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: LANCHES/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LancheFormViewModel model)
        {
            if (id != model.LancheId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ViewBag.CategoriaId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                    _context.Categorias,
                    "CategoriaId",
                    "CategoriaNome",
                    model.CategoriaId);

                return View(model);
            }

            var lanche = await _context.Lanches.FindAsync(id);

            if (lanche == null)
            {
                return NotFound();
            }

            // Atualiza os dados do lanche
            lanche.Nome = model.Nome;
            lanche.DescricaoCurta = model.DescricaoCurta;
            lanche.DescricaoDetalhada = model.DescricaoDetalhada;
            lanche.Preco = model.Preco;
            lanche.CategoriaId = model.CategoriaId;
            lanche.EmEstoque = model.EmEstoque;
            lanche.IsLanchePreferido = model.IsLanchePreferido;

            // Se foi escolhida uma nova imagem...
            if (model.ImagemArquivo != null)
            {
                // Remove a imagem antiga
                if (!string.IsNullOrEmpty(lanche.ImagemUrl))
                {
                    _uploadImagemService.ExcluirImagem(lanche.ImagemUrl);
                }

                // Faz upload da nova imagem
                var novaImagem =
                    await _uploadImagemService.UploadImagemAsync(model.ImagemArquivo);

                lanche.ImagemUrl = novaImagem ?? "";
                lanche.ImagemThumbnailUrl = novaImagem ?? "";
            }

            _context.Update(lanche);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: LANCHES/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);

            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        // POST: LANCHES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var lanche = await _context.Lanches.FindAsync(id);
            if (lanche != null)
            {
                _context.Lanches.Remove(lanche);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
            return _context.Lanches.Any(e => e.LancheId == id);
        }
    }
}