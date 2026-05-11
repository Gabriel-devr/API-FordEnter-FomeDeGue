using API_Curso_Angular.DTOs;
using API_Curso_Angular.DTOs.Request.Products;
using API_Curso_Angular.Extensions;
using API_Curso_Angular.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace API_Curso_Angular.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository) {
            _repository = repository;
        }

        [HttpPost]
        [Route("criar-produto")]
        public async Task<IActionResult> CriarProduto(CreateProductRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var criarProduto = await _repository.CriarProduto(model);
            if (criarProduto.Errors.Any())
            {
                return BadRequest(criarProduto);
            }

            return CreatedAtAction(nameof(ProdutoPorId), new { id = criarProduto.Data.Id }, criarProduto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ProdutoPorId(long id)
        {
            var produtoPorId = await _repository.ProdutoPorId(id);

            if (produtoPorId.Errors.Any())
            {
                return NotFound(produtoPorId);
            }

            return Ok(produtoPorId);
        }

        [HttpPost]
        [Route("deletar-produto/{id}")]
        public async Task<IActionResult> DeletarProduto(long id)
        {
            var deletarProduto = await _repository.DeletarProduto(id);
            if (deletarProduto.Errors.Any())
            {
                return BadRequest(deletarProduto);
            }
            return Ok(deletarProduto);
        }

        [HttpGet]
        [Route("listar-produtos")]
        public async Task<IActionResult> ListarProdutos()
        {
            var listarProdutos = await _repository.ListarProdutos();

            if (listarProdutos.Errors.Any())
            {
                return BadRequest(listarProdutos);
            }

            return Ok(listarProdutos);
        }

        [HttpPut]
        [Route("atualizar-produto/{id}")]
        public async Task<IActionResult> AtualizarProduto (long id, [FromBody] UpdateProductRequestDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultDTO<string>(ModelState.GetErrors()));
            }

            var atualizarProduto = await _repository.AtualizarProduto(id, model);

            if (atualizarProduto.Errors.Any())
            {
                return BadRequest(atualizarProduto);
            }

            return Ok(atualizarProduto);
        }
    }
}
