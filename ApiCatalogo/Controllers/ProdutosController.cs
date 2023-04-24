using ApiCatalogo.Data;
using ApiCatalogo.Extentions;
using ApiCatalogo.Models;
using ApiCatalogo.ViewModel;
using ApiCatalogo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class ProdutosController : Controller
    {
        private readonly CatalogoContex _catalogoContex;

        public ProdutosController(CatalogoContex catalogoContex)
        {
            _catalogoContex = catalogoContex;
        }

        [HttpGet]
        public async Task<IActionResult> GetlAllAsync()
        {
            try
            {
                var produtos = await _catalogoContex.Produtos
                    .AsNoTracking().ToListAsync();

                return Ok(new ResultViewModel<List<Produto>>(produtos));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<List<Produto>>("Falha interna no servidor"));
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var produto = await _catalogoContex.Produtos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) 
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro interno no servidor"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(EditorProdutoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));

            try
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Preco = model.Preco,
                    ImagemUrl = model.ImagemUrl,
                    CategoriaId = model.CategoriaId
                };

                await _catalogoContex.Produtos.AddAsync(produto);
                await _catalogoContex.SaveChangesAsync();
                return Created($"v1/api/produtos/{produto.ProdutoId}", new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Não foi possível criar o produto"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro interno do servidor"));
            }            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(EditorProdutoViewModel model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Produto>(ModelState.GetErrors()));

            try
            {
                var produto = await _catalogoContex
                    .Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) 
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Preco = model.Preco;
                produto.ImagemUrl = model.ImagemUrl;
                produto.CategoriaId = model.CategoriaId;

                _catalogoContex.Produtos.Update(produto);
                await _catalogoContex.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Não foi possível alterar o produto"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro interno no servidor"));
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var produto = await _catalogoContex
                    .Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) 
                    return NotFound(new ResultViewModel<Produto>("Produto não encontrado"));

                _catalogoContex.Produtos.Remove(produto);
                await _catalogoContex.SaveChangesAsync();

                return Ok(new ResultViewModel<Produto>(produto));
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Não foi possível remover o produtoa"));
            }
            catch (Exception)
            {
                return StatusCode(500, new ResultViewModel<Produto>("Erro interno do servidor"));
            }
        }
    }
}
