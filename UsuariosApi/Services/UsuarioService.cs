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
        private TokenService _tokenService;//GERA JSON WEB TOKEN - AUTENTICAÇÃO APÓS LOGAR

        //CONSTRUTOR
        public UsuarioService(IMapper mapper, 
                              UserManager<Usuario> userManager,
                              SignInManager<Usuario> signInManager,
                              TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
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
        public async Task<string> Login(LoginUsuarioDto dto)
        {    //async Taks<> - POR CAUSA DO await
             //string - POR QUE O TOKEN RETORNADO É STRING

            var resultado = await _signInManager.PasswordSignInAsync(dto.Username,
                                                                     dto.Password, 
                                                                     false, false);
            //LOGIN FALHOU
            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado");
            }

            //LOGIN OK
            //CONSULTAR QUAL O USUÁRIO ATUAL - PELO Username (MAIÚSCULO)
            //Username é único? (Identity GARANTE)
            var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            //OBTER O TOKEN DE ACESSO PARA ESSE USUÁRIO
            var token = _tokenService.GenerateToken(usuario);

            //RETORNÁ-LO
            return token;
        }




    }



}
