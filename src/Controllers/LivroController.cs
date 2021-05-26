using System.Collections.Generic;
using AspNetCore.WebApi.Attributes;
using AspNetCore.WebApi.Controllers.Shared;
using AspNetCore.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Controllers
{
    [Route("api/")]
    public class LivroController : ApiController
    {
        [HttpGet("livros-nao-tipado")]
        public IActionResult ObterTodosNaoTipado()
        {
            var livros = Livro.ObterLivros();
            return Ok(livros);
        }

        [HttpGet("livros-tipado")]
        public ActionResult<IEnumerable<Livro>> ObterTodosTipado()
        {
            var livros = Livro.ObterLivros();
            return Ok(livros);
        }

        /// <summary>
        /// Obtém todos os livros.
        /// </summary>
        /// <returns>Retorna os livros encontrados</returns>
        /// <response code="200">Retorna os livros encontrados</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [HttpGet("livros")]
        public IActionResult ObterTodos()
        {
            var livros = Livro.ObterLivros();
            return ResponseOk(livros);
        }

        /// <summary>
        /// Obtém um livro pelo seu identificador.
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <returns>Retorna o livro encontrado</returns>
        /// <response code="200">Retorna o livro encontrado</response>
        /// <response code="400">Se o id passado for nulo</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [HttpGet("livros/{id}")]
        public IActionResult Obter(int id)
        {
            if (id <= 0)
                return ResponseBadRequest();
                
            var livro = Livro.ObterLivro(id);
            return ResponseOk(livro);
        }
        
        /// <summary>
        /// Cria um livro.
        /// </summary>
        /// <param name="livro">Dados do livro</param>
        /// <returns>Um novo livro criado</returns>
        /// <response code="201">Retorna com o ID criado</response>
        /// <response code="400">Se o livro passado for nulo</response>
        /// <response code="500">Se houver um erro ao criar um livro</response>
        [CustomResponse(StatusCodes.Status201Created)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("livros")]
        public IActionResult Criar(Livro livro)
        {
            if (livro == null)
                return ResponseBadRequest();

            // Criando livro (fake)
            var id = 99;

            return ResponseCreated(id);
        }

        /// <summary>
        /// Atualiza um livro.
        /// </summary>
        /// <param name="livro">Dados do livro</param>
        /// <response code="200">Retorna com o status da atualização</response>
        /// <response code="400">Se o livro passado for nulo</response>
        /// <response code="500">Se houver um erro ao atualizar um livro</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status500InternalServerError)]
        [HttpPut("livros")]
        public IActionResult Atualizar(Livro livro)
        {
            if (livro == null)
                return ResponseBadRequest();

            // Atualiza o livro

            return ResponseOk();
        }

        /// <summary>
        /// Atualiza parcialmente um livro.
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <param name="patchDoc">A JSON Patch document</param>
        /// <response code="200">Retorna com o livro atualizado</response>
        /// <response code="400">Se o livro passado for nulo</response>
        /// <response code="500">Se houver um erro ao atualizar um livro</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status500InternalServerError)]
        [HttpPatch("livros/{id}")]
        public IActionResult AtualizarParcialmente(int id, [FromBody] JsonPatchDocument<Livro> patchDoc)
        {
            if (id == 0)
                return ResponseBadRequest();

            var livro = Livro.ObterLivro(id);

            // Atualiza o livro
            patchDoc.ApplyTo(livro);

            return ResponseOk(livro);
        }

        /// <summary>
        /// Exclui um livro.
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <response code="200">Retorna com o status da exclusão</response>
        /// <response code="400">Se o livro passado for nulo</response>
        /// <response code="500">Se houver um erro ao excluir um livro</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status500InternalServerError)]
        [HttpDelete("livros/{id}")]
        public IActionResult Excluir(int id)
        {
            if (id == 0)
                return ResponseBadRequest();

            // Exclui o livro

            return ResponseOk();
        }

        /// <summary>
        /// Desativa um livro.
        /// </summary>
        /// <param name="id">id do livro</param>
        /// <response code="200">Retorna com o status da operação</response>
        /// <response code="400">Se o livro passado for nulo</response>
        /// <response code="500">Se houver um erro ao desativar um livro</response>
        [CustomResponse(StatusCodes.Status200OK)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status500InternalServerError)]
        [HttpPost("livros/{id}/desativar")]
        public IActionResult Desativar(int id)
        {
            if (id == 0)
                return ResponseBadRequest();

            // Criando livro
            var livro = Livro.ObterLivro(id);
            livro.Desativar();

            return ResponseOk();
        }
    }
}