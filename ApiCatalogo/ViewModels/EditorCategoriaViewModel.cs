using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.ViewModel;

public class EditorCategoriaViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 50 ctacteres")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A imagemUrl é obrigatório")]
    public string? ImagemUrl { get; set; }    
}
