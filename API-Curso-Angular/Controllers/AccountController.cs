using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Account;
using API_Curso_Angular.Extensions;
using API_Curso_Angular.Repositories.Account;
using API_Curso_Angular.Services.Accounts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Controllers {


    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("registrar-cliente")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CreateAccountRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var createAccount = await _accountService.RegisterCustomer(model);

            if (createAccount.Errors.Any()) {
                return BadRequest(createAccount); //Erro detectado no repo
            }

            return Created("", createAccount);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var login = await _accountService.Login(model);

            if (login.Errors.Any()) {
                return BadRequest(login);
            }

            return Ok(login);
        }

        [HttpPost]
        [Route("recuperar-senha")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var forgotPassword = await _accountService.ForgotPassword(model);

            return Ok(forgotPassword);

        }

        [HttpPost]
        [Route("redefinir-senha")]
        public async Task<IActionResult> Login([FromBody] ResetPasswordRequestDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var changePassword = await _accountService.ResetPassword(model);

            if (changePassword.Errors.Any()) {
                return BadRequest(changePassword);
            }

            return Ok(changePassword);

        }

        [HttpGet]
        [Route("confirmar-email")]
        public async Task<IActionResult> ConfirmEmail(long userId, string code) {
            if (!ModelState.IsValid) {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var confirmEmail = await _accountService.ConfirmEmail(userId, code);

            if (confirmEmail.Errors.Any()) {
                return BadRequest(confirmEmail);
            }

            return Ok(confirmEmail);
        }
    }
}