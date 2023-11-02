using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using APICategoria.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace APICategoria.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContexto _context;

    public CategoriasController(AppDbContexto context)
    {
        _context = context;
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriaProduto()
    {
        var categoriaEProdutos = _context.Categorias?
            .AsNoTracking()
            .Include(incluirProduto => incluirProduto.Produtos)
            .Where(c => c.CategoriaId <= 5)
            .ToList();
        return Ok(categoriaEProdutos);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        try
        {
            var categoria = _context.Categorias?.AsNoTracking().ToList();

            if (categoria == null)
            {
                return BadRequest(categoria);
            }

            return categoria;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "Ocorreu um problema, entre em contato com o suporte!");
        }

    }

    [HttpGet("{id:int}")]
    public ActionResult<Produto> Get(int id)
    {
        var categoria = _context.Categorias?.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrado...");
        }

        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
            return BadRequest();

        _context.Categorias?.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias?.FirstOrDefault(p => p.CategoriaId == id);
        //var produto = _context.Produtos.Find(id);

        if (categoria is null)
        {
            return NotFound("Categoria não localizado...");
        }
        _context.Categorias?.Remove(categoria);
        _context.SaveChanges();

        return Ok(categoria);
    }
}
