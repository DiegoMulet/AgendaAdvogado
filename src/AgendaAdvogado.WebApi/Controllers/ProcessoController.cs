using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaAdvogado.Domain;
using AgendaAdvogado.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgendaAdvogado.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessoController : ControllerBase
    {
        private readonly IAgendaAdvogadoRepository _repo;

        public ProcessoController(IAgendaAdvogadoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllProcessoAsync();

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpGet("{ProcessoId}")]
        public async Task<IActionResult> Get(int ProcessoId)
        {
            try
            {
                var results = await _repo.GetAllProcessoAsyncById(ProcessoId);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

        }

        [HttpGet("getQtdProcessos")]
        public async Task<IActionResult> GetQtpProcessos()
        {
            try
            {
                var results = await _repo.GetQtdProcessosAsync();

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

        }
        [HttpGet("getQtdProcessos/{Ativo}")]
        public async Task<IActionResult> GetQtpProcessos(bool ativo)
        {
            try
            {
                var results = await _repo.GetQtdProcessosAsync(ativo);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

        }
        [HttpGet("getValorProcessos/{Ativo}")]
        public async Task<IActionResult> GetValorProcessos(bool ativo)
        {
            try
            {
                var results = await _repo.GetValorProcessosAsync(ativo);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

        }

        [HttpGet("getMediaValorProcessosByCliente/{ClienteId}/{Estado}")]
        public async Task<IActionResult> GetMediaValorProcessosByCliente(int ClienteId, string Estado)
        {
            try
            {
                var results = await _repo.GetMediaValorProcessosAsyncByCliente(ClienteId, Estado);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpPost("getProcessosByFiltros")]
        public async Task<IActionResult> PostProcessosByFiltros(ProcessoFiltros Filtros)
        {
            try
            {
                var results = await _repo.GetAllProcessosAsyncByFiltro(Filtros);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpGet("getProcessosByEstadoCliente/{ClienteId}")]
        public async Task<IActionResult> GetProcessosByEstadoCliente(int ClienteId)
        {
            try
            {
                var results = await _repo.GetAllProcessosAsyncByEstadoCliente(ClienteId);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Processo processo)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    _repo.Add(processo);

                    if (await _repo.SaveChangesAssync())
                    {
                        return Created($"/api/processo/{processo.Id}", processo);
                    }
                }
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

            return BadRequest();
        }

        [HttpPut("{ProcessoId}")]
        public async Task<IActionResult> Put(int ProcessoId, Processo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var processo = await _repo.GetAllProcessoAsyncById(ProcessoId);
                    if (processo == null) return NotFound();

                    _repo.Update(model);

                    if (await _repo.SaveChangesAssync())
                    {
                        return Created($"/api/processo/{processo.Id}", model);
                    }
                }
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

            return BadRequest();
        }

        [HttpDelete("{ProcessoId}")]
        public async Task<IActionResult> Delete(int ProcessoId)
        {
            try
            {
                var processo = await _repo.GetAllProcessoAsyncById(ProcessoId);
                if (processo == null) return NotFound();

                _repo.Delete(processo);

                if (await _repo.SaveChangesAssync())
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }
    }
}
