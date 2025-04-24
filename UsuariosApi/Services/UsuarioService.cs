using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using AutoMapper;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        //ATRIBUTOS
        private IMapper _mapper; //AutoMap
        private UserManager<Usuario> _userManager;//GERENCIA ALGUNS MÉTODOS DO IDENTITY PARA USUÁRIO
        private SignInManager<Usuario> _signInManager;//GERENCIA LOGIN DO IDENTITY PARA USUÁRIO

        //CONSTRUTOR
        public UsuarioService(IMapper mapper, 
                              UserManager<Usuario> userManager, 
                              SignInManager<Usuario> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //DEMAIS MÉTODOS
        //MÉTODO QUE CADASTRA UM USUÁRIO AO BD
        public async Task Cadastra(CreateUsuarioDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            //CRIAÇÃO USUÁRIO IDENTITY (USUÁRIO E SENHA)
            //SENHA VEM DO OBJETO dto NESSE CASO, JÁ QUE OBJETO usuario NÃO TEM CAMPO SENHA
            //await - INDICA QUE O PROGRAMA AGUARDA O USUÁRIO SER CRIADO PARA SALVAR EM resultado
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            //EM CASO DE FALHA
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            }
        }

        //MÉTODO QUE REALIZA O LOGIN DO USUÁRIO
        public async Task Login(LoginUsuarioDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username,
                                                                     dto.Password, 
                                                                     false, false);
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado");
            }
        }




    }



}
