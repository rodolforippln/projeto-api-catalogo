using ApiCatalogo.Data;
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
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

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GellAll()
    {
        try
        {
            var categorias = await _catalogoContex.Categorias
                .AsNoTracking().ToListAsync();

            return Ok(categorias);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
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

            if (categoria is null) return NotFound("Categoria não encontrado");

            return Ok(categoria);
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoriaDto categoriaDto)
    {
        try
        {
            var categoria = new Categoria
            {
                Nome = categoriaDto.Nome,
                ImagemUrl = categoriaDto.ImagemUrl
            };


            await _catalogoContex.Categorias.AddAsync(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Created($"v1/api/categorias/{categoria.CategoriaId}", categoria);
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível alterar a categoria");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(CategoriaDto categoriaDto, int id)
    {
        try
        {         
            var categoria = await _catalogoContex
                .Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null) return NotFound("Categoria não encontrada");

            categoria.Nome = categoriaDto.Nome;
            categoria.ImagemUrl = categoriaDto.ImagemUrl;


            _catalogoContex.Categorias.Update(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível alterar a categoria");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            var categoria = await _catalogoContex
                .Categorias.FirstOrDefaultAsync(x => x.CategoriaId == id);

            if (categoria is null) return NotFound("Categoria não encontrada");

            _catalogoContex.Categorias.Remove(categoria);
            await _catalogoContex.SaveChangesAsync();

            return Ok(categoria);
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, "Não foi possível remover a categoria");
        }
        catch (Exception)
        {
            return StatusCode(500, "Erro interno no servidor");
        }
    }
}
