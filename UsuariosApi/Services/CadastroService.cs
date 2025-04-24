using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using AutoMapper;

namespace UsuariosApi.Services
{
    public class CadastroService
    {
        //ATRIBUTOS
        private IMapper _mapper; //AutoMap
        private UserManager<Usuario> _userManager;//GERENCIA MÉTODOS DO IDENTITY PARA USUÁRIO


        //CONSTRUTOR
        public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        //DEMAIS MÉTODOS
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
    }



}
