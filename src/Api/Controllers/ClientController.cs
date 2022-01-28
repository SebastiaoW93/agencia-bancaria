using Agencia.Bancaria.Plataforma.Domain.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Agencia.Bancaria.Plataforma.Api.Controllers
{
    /// <summary>Controller que provê endpoints relacionados a entidade de cliente.</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        /// <summary>Serviço que provê acesso aos dados e operações relaciondas aos clientes.</summary>
        private readonly ClientService _clientService;

        /// <summary>Construtor com parâmetros para inicialização.</summary>
        /// <param name="clienteService">Injeção de dependência do serviço que provê acesso aos dados e operações relacionadas aos clientes.</param>
        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> BuscarPorId(string id)
        {
            var client = await _clientService.BuscarClientePorIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        /// <summary></summary>
        [HttpPost]
        public async Task<IActionResult> CadastrarCliente(string id, string nome, string email, ClientType tipoDeCliente)
        {
            await _clientService.CadastrarClienteAsync(id, nome, email, tipoDeCliente);         
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditarCliente(string id, string nome, string email, ClientType tipoDeCliente)
        {
            var queriedClient = await _clientService.BuscarClientePorIdAsync(id);
            if(queriedClient == null)
            {
                return NotFound();
            }
            await _clientService.EditarClienteAsync(id, nome, email, tipoDeCliente);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var client = await _clientService.BuscarClientePorIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            await _clientService.DeletarClienteAsync(id);
            return NoContent();
        }
    }
}