using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.ViewModel;

public class EditorProdutoViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 50 caracteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A Descricao é obrigatória")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "A imagemUrl é obrigatória")]
    public string? ImagemUrl { get; set; }

    [Required(ErrorMessage = "O categoriaId é obrigatório")]
    public int CategoriaId { get; set; }
}
