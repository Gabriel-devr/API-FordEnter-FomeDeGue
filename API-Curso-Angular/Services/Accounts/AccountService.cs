using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Account;
using API_Curso_Angular.DTOs.Response;
using API_Curso_Angular.Models;
using API_Curso_Angular.Models.Auth;
using API_Curso_Angular.Repositories.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace API_Curso_Angular.Services.Accounts {
    public class AccountService : IAccountService{
        private readonly IAccountRepository _repository;
        private readonly TokenService _tokenService;

        public AccountService(IAccountRepository repository, TokenService tokenService) {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<ResultDTO<string>> ForgotPassword(ForgotPasswordRequestDTO model) {
            var user = await _repository.GetUserByEmail(model.Email);

            if (user == null) {
                return new ResultDTO<string>("Se o e-mail estiver cadastrado, um link de recuperação será enviado.");
            }

            var token = await _repository.GeneratePasswordResetToken(user);
            //var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            //var frontendUrl = _configuration["Frontend:ResetPasswordUrl"] ?? "http://localhost:4200/reset-password";
            //var callbackUrl = $"{frontendUrl}?token={code}&email={Uri.EscapeDataString(user.Email)}";

            //var html = $"<p>Redefina sua senha clicando <a href=\"{callbackUrl}\">aqui</a>.</p>";

            return new ResultDTO<string>("Link de recuperação gerado com sucesso.", new List<string>());
        }

        public async Task<ResultDTO<LoginResponseDTO>> Login(LoginRequestDTO model) {

            var user = await _repository.GetUserByEmail(model.Email);
            //Verificamos se o email está no nosso banco de dados

            if (user == null) {
                return new ResultDTO<LoginResponseDTO>("Usuário ou senha inválidos");
            }

            var checkPassword = await _repository.CheckPassword(user, model.Password);
            //Verifica se o email checado corresponde a sua senha

            if (!checkPassword) {
                return new ResultDTO<LoginResponseDTO>("Usuário ou senha inválidos");
            }

            var roles = await _repository.GetRoles(user);
            var token = _tokenService.GenerateToken(user, roles);

            var loginResponse = new LoginResponseDTO() {
                Token = token,
                User = new UserResponseDTO() {
                    Email = user.Email!,
                    FullName = user.NomeCompleto
                }
            };
            return new ResultDTO<LoginResponseDTO>(loginResponse);
        }

        public async Task<ResultDTO<UserResponseDTO>> RegisterCustomer(CreateAccountRequestDTO model) {

            var user = new User() {
                Email = model.Email,
                NomeCompleto = model.Nome,
                UserName = model.Nome
            };

            var createUser = await _repository.CreateUser(user, model.Password);

            if (!createUser.Succeeded) {
                return new ResultDTO<UserResponseDTO>(createUser.Errors.Select(x => x.Description).ToList());
            }

            var addRoleResult = await _repository.AddToRole(user, "USUARIO");
            if (!addRoleResult.Succeeded) {
                return new ResultDTO<UserResponseDTO>(addRoleResult.Errors.Select(x => x.Description).ToList());
            }

            var customer = new Cliente() {
                IdentityUserId = user.Id,
                User = user,
            };

            await _repository.CriarCliente(customer);


            return new ResultDTO<UserResponseDTO>(new UserResponseDTO { Email = model.Email, FullName = model.Nome});

        }

        public async Task<ResultDTO<UserResponseDTO>> RegisterAdmin(CreateAccountRequestDTO model) {

            var user = new User() {
                Email = model.Email,
                NomeCompleto = model.Nome,
                UserName = model.Nome
            };

            var createUser = await _repository.CreateUser(user, model.Password);

            if (!createUser.Succeeded) {
                return new ResultDTO<UserResponseDTO>(createUser.Errors.Select(x => x.Description).ToList());
            }

            var addRoleResult = await _repository.AddToRole(user, "ADMIN");
            if (!addRoleResult.Succeeded) {
                return new ResultDTO<UserResponseDTO>(addRoleResult.Errors.Select(x => x.Description).ToList());
            }

            var customer = new Cliente() {
                IdentityUserId = user.Id,
                User = user,
            };

            await _repository.CriarCliente(customer);


            return new ResultDTO<UserResponseDTO>(new UserResponseDTO { Email = model.Email, FullName = model.Nome });
        }

        public async Task<ResultDTO<string>> ResetPassword(ResetPasswordRequestDTO model) {
            var user = await _repository.GetUserByEmail(model.Email);

            if (user == null) {
                return new ResultDTO<string>("Usuário ou senha inválidos.");
            }

            var resetPassword = await _repository.ResetPassword(user, model.Token, model.Password);

            if (!resetPassword.Succeeded) {
                return new ResultDTO<string>(resetPassword.Errors.Select(x => x.Description).ToList());
            }

            return new ResultDTO<string>("Senha redefinida com sucesso", new List<string>());
        }

        public async Task<ResultDTO<string>> ConfirmEmail(long userId, string code) {
            var user = await _repository.GetUserById(userId);
            if (user == null) {
                return new ResultDTO<string>("Usuário não encontrado");
            }

            //var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _repository.ConfirmEmail(user, code);

            if (!result.Succeeded) {
                return new ResultDTO<string>(result.Errors.Select(x => x.Description).ToList());
            }

            return new ResultDTO<string>("E-mail confirmado com sucesso", new List<string>());
        }

    }
}
