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
        private UsuarioService _usuarioService;//SERVIÇO QUE CONTÉM A LÓGIDA DO CADASTRO

        //CONSTRUTOR
        public UsuarioController(UsuarioService cadastroService)
        {
            _usuarioService = cadastroService;
        }

        //DEMAIS MÉTODOS
        //MÉTODO QUE UTILIZA SERVIÇO UsuarioService->Cadastra PARA CADASTRAR USUÁRIO
        [HttpPost("cadastro")]//ROTA cadastro DIFERENCIAS DOS DEMAIS MÉTODOS POST
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
        {    //async Task - RETORNO DE MÉTODO await É TIPO async Task<>

            //LÓGICA DE CADASTRO COMO SERVIÇO EM Services->CadastroService->Cadastra()
            await _usuarioService.Cadastra(dto);
            return Ok("Usuário cadastrado!"); //200 CODE
        }


        //MÉTODO QUE UTILIZA SERVIÇO UsuarioService->Login PARA LOGAR USUÁRIO
        [HttpPost("login")]//ROTA login DIFERENCIA DOS DEMAIS MÉTODOS POST
        public async Task<IActionResult> Login(LoginUsuarioDto dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok(token);
        }
    }
}
