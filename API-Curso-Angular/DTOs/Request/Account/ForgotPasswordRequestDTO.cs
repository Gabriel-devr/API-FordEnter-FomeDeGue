using System.ComponentModel.DataAnnotations;

namespace API_Curso_Angular.DTOs.Request.Account {
    public class ForgotPasswordRequestDTO {

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de e-mail válido.")]
        public string Email { get; set; } = string.Empty;
    }
}
