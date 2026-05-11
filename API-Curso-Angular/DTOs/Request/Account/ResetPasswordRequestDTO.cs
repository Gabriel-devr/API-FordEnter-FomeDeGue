using System.ComponentModel.DataAnnotations;

namespace API_Curso_Angular.DTOs.Request.Account {
    public class ResetPasswordRequestDTO {

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Token é obrigatório")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório.")]
        [DataType(DataType.Password)]//DataType.Password é usado para indicar que o campo é do tipo senha, o que pode ajudar a ocultar os caracteres digitados em formulários.
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
