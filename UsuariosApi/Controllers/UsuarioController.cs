using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {

        //PROPRIEDADES
        private CadastroService _cadastroService;//SERVIÇO QUE CONTÉM A LÓGIDA DO CADASTRO

        //CONSTRUTOR
        public UsuarioController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        //DEMAIS MÉTODOS
        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
        {    //async Task - RETORNO DE MÉTODO await É TIPO async Task<>

            //LÓGICA DE CADASTRO COMO SERVIÇO EM Services->CadastroService->Cadastra()
            await _cadastroService.Cadastra(dto);
            return Ok("Usuário cadastrado!"); //200 CODE

        }
    }
}
