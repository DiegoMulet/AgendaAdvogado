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
    public class ClienteController : ControllerBase
    {
        private readonly IAgendaAdvogadoRepository _repo;

        public ClienteController(IAgendaAdvogadoRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllClienteAsync();

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpGet("{ClienteId}")]
        public async Task<IActionResult> Get(int ClienteId)
        {
            try
            {
                var results = await _repo.GetAllClienteAsyncById(ClienteId);

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

        }

        [HttpGet("getQtdClientes")]
        public async Task<IActionResult> GetQtdClientes()
        {
            try
            {
                var results = await _repo.GetQtdClientesAsync();

                return Ok(results);
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repo.Add(cliente);

                    if (await _repo.SaveChangesAssync())
                    {
                        return Created($"/api/cliente/{cliente.Id}", cliente);
                    }
                }
            }
            catch (System.Exception erro)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, erro.InnerException);
            }

            return BadRequest();
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, Cliente model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = await _repo.GetAllClienteAsyncById(ClienteId);
                    if (cliente == null) return NotFound();

                    var processosExclusao = cliente.Processos.Where(
                        p => !model.Processos.Select(mp => mp.Id).Contains(p.Id)).ToList();

                    if (processosExclusao.Count > 0) _repo.DeleteRange(processosExclusao);

                    _repo.Update(model);

                    if (await _repo.SaveChangesAssync())
                    {
                        return Created($"/api/cliente/{cliente.Id}", model);
                    }
                }
            }
            catch (System.Exception error)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repo.GetAllClienteAsyncById(ClienteId);
                if (cliente == null) return NotFound();

                _repo.Delete(cliente);

                if (await _repo.SaveChangesAssync())
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }
    }
}