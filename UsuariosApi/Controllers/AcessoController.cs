using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : ControllerBase
    {
        //MÉTODO QUE
        [HttpGet]
        [Authorize(Policy = "IdadeMinima")]//SÓ É ACESSÍVEL DADA DETERMINADA CONDIÇÃO DE AUTORIZAÇÃO
        public IActionResult Get()
        {
            return Ok("Acesso permitido");
        }
    }
}
