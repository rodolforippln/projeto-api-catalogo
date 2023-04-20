using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models;

public class Categoria
{
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(50)]    
    public string? Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string? ImagemUrl { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}
