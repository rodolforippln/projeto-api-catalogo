using ApiCatalogo.Data;
using ApiCatalogo.Extentions;
using ApiCatalogo.Models;
using ApiCatalogo.ViewModel;
using ApiCatalogo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class CategoriasController : Controller
{
    private readonly CatalogoContex _catalogoContex;

    public CategoriasController(CatalogoContex catalogoContex)
    {
        _catalogoContex = catalogoContex;        
}

    [HttpGet("produtos")]
    public async Task<IActionResult> GetAllCategoriasProdutosAsync()
    {
        try
        {
            var categorias = await _catalogoContex.Categorias
                .AsNoTracking()
                .Include(x => x.Produtos)
                .ToListAsync();

            return Ok(new ResultViewModel<List<Categoria>>(categorias));
        }
        catch
        {
            return StatusCode(500, new ResultViewModel<List<Categoria>>("Falha interna no servidor"));
        }
    }

    [HttpGet]
    public async Task<IActionResult> GellAll()
    {
        try
        {
            var categorias = await _catalogoContex.Categorias
                .AsNoTracking().ToListAsync();

            return Ok(new ResultViewModel<List<Categoria>>(categorias));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<List<Categoria>>("Falha interna no servidor"));
        }
    }    

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            var categoria = await _catalogoContex.Categorias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null) return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

            return Ok(new ResultViewModel<Categoria>(categoria));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Erro interno no servidor"));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(EditorCategoriaViewModel model)
    {
        if (!ModelState.IsValid) 
            return BadRequest(new ResultViewModel<Categoria>(ModelState.GetErrors()));

        try
        {
            var categoria = new Categoria
            {
                Nome = model.Nome,
                ImagemUrl = model.ImagemUrl
            };

            await _catalogoContex.Categorias.AddAsync(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Created($"v1/api/categorias/{categoria.CategoriaId}", new ResultViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Não foi possível criar a categoria"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Erro interno do servidor"));
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(EditorCategoriaViewModel model, int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<Categoria>(ModelState.GetErrors()));

        try
        {         
            var categoria = await _catalogoContex
                .Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null) 
                return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

            categoria.Nome = model.Nome;
            categoria.ImagemUrl = model.ImagemUrl;


            _catalogoContex.Categorias.Update(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Ok(new ResultViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Não foi possível alterar a categoria"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Erro interno no servidor"));
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var categoria = await _catalogoContex
                .Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null) 
                return NotFound(new ResultViewModel<Categoria>("Categoria não encontrada"));

            _catalogoContex.Categorias.Remove(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Ok(new ResultViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Não foi possível remover a categoria"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<Categoria>("Erro interno do servidor"));
        }
    }
}
