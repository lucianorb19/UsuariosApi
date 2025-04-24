using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UsuarioController : ControllerBase
    {

        //PROPRIEDADES
        private IMapper _mapper; //AutoMap
        private UserManager<Usuario> _userManager;//GERENCIA MÉTODOS DO IDENTITY PARA USUÁRIO

        //CONSTRUTOR
        public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        //DEMAIS MÉTODOS
        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
        {    //async Task - RETORNO DE MÉTODO await É TIPO async Task<>
            
            Usuario usuario = _mapper.Map<Usuario>(dto);
            
            //CRIAÇÃO USUÁRIO IDENTITY (USUÁRIO E SENHA)
            //SENHA VEM DO OBJETO dto NESSE CASO, JÁ QUE OBJETO usuario NÃO TEM CAMPO SENHA
            //await - INDICA QUE O PROGRAMA AGUARDA O USUÁRIO SER CRIADO PARA SALVAR EM resultado
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            //EM CASO DE SUCESSO
            if(resultado.Succeeded) return Ok("Usuário cadastrado!"); //201 CODE

            //EM CASO DE FALHA
            throw new ApplicationException("Falha ao cadastrar usuário");
        }
    }
}
