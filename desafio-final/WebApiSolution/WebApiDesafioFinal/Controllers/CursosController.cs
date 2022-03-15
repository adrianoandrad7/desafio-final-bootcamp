using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiDesafioFinal.Domain;
using WebApiDesafioFinal.Services;

namespace WebApiDesafioFinal.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ICursoService _cursoService;

        public CursosController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso model)
        {
            try
            {
                var curso = await _cursoService.AddCurso(model);
                if (curso == null) return BadRequest("Error ao tentar adicionar curso.");

                return Ok(curso);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar adicionar curso. Erro{ex.Message}");
            }

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCursos()
        {
            try
            {
                var cursos = await _cursoService.GetAllCursosAsync();
                if (cursos == null) return NotFound("Nenhum curso encontrado.");

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cursos. Erro{ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpGet("{status}/status")]
        public async Task<ActionResult<Curso[]>> GetByStatus(int status)
        {
            try
            {
                var cursos = await _cursoService.GetAllCursosByStatusAsync(status);

                if (cursos == null || status <= 0 || status > 3) return NotFound("Status do curso não encontrado");

                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar por status do curso. Erro{ex.Message}");
            }
        }

        [Authorize(Roles = "Gerente,Secretaria")]
        [HttpPut]
        public async Task<IActionResult> PutCurso(int cursoId, Curso model)
        {
            try
            {
                var curso = await _cursoService.UpdateCurso(cursoId, model);
                if (curso == null) return BadRequest("Error ao tentar atualizar curso.");

                return Ok(curso);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar cursos. Erro{ex.Message}");
            }
        }


        [Authorize(Roles = "Gerente")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCurso(int cursoId)
        {
            try
            {
                return await _cursoService.DeleteCurso(cursoId) ?
                Ok("Curso deletado com sucesso.") :
                BadRequest("Error ao tentar deletar curso.");

            } catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar deletar o curso. Erro{ex.Message}");
            }
        }

    }
}
