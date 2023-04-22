using ApiCatalogo.Data;
using ApiCatalogo.Models;
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
        public async Task<IActionResult> GellAll()
        {
            try
            {
                var produtos = await _catalogoContex.Produtos
                    .AsNoTracking().ToListAsync();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var produto = await _catalogoContex.Produtos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) return NotFound("Produto não encontrado");
            
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                await _catalogoContex.Produtos.AddAsync(produto);
                await _catalogoContex.SaveChangesAsync();
                return Created("", produto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível alterar o produto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor");
            }            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(Produto model, int id)
        {
            try
            {
                var produto = await _catalogoContex
                    .Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) return NotFound("Produto não encontrado");

                produto.Nome = model.Nome;
                produto.Descricao = model.Descricao;
                produto.Preco = model.Preco;
                produto.ImagemUrl = model.ImagemUrl;
                produto.Estoque = model.Estoque;
                produto.CategoriaId = model.CategoriaId;

                _catalogoContex.Produtos.Update(produto);
                await _catalogoContex.SaveChangesAsync();

                return Ok(produto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível alterar o produto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var produto = await _catalogoContex
                    .Produtos.FirstOrDefaultAsync(x => x.ProdutoId == id);

                if (produto is null) return NotFound("Produto não encontrado");

                _catalogoContex.Produtos.Remove(produto);
                await _catalogoContex.SaveChangesAsync();

                return Ok(produto);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, "Não foi possível remover o produto");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno no servidor");
            }
        }
    }
}
