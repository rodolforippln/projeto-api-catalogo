using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalogo.Models;

public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(50)]
    public string? Nome { get; set; }

    [StringLength(250)]
    public string? Descricao { get; set; }

    [Required]
    [Column(TypeName = "Decimal(10, 2)")]
    public decimal Preco { get; set; }

    [Required]
    [StringLength(50)]
    public string? ImagemUrl { get; set; }

    public int Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }

    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
