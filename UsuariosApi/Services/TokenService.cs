using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TokenService
    {
        //PROPRIEDADES



        //CONSTRUTOR



        //DEMAIS MÉTODOS
        //MÉTODO QUE GERA O JSON WEB TOKEN-JWT, DADO UM OBJETO USUARIO 
        public void GenerateToken(Usuario usuario)
        {
            //Claim[] - VETOR DE REIVINDICAÇÕES PARA A GERAÇÃO DO JWT
            Claim[] claims = new Claim[]
            {
                new Claim("username", usuario.UserName),
                new Claim("id", usuario.Id),//GERADO PELO BD
                new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString())
                //HÁ MUITOS ClaimTypes PARA CADA TIPO DE CAMPO
            };

            //CHAVE DE GERAÇÃO DAS CREDENCIAIS
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("luciano"));
            //GERAÇÃO DA CHAVE USA UMA SEQUENCIA QUALQUER DE CARACTERES - luciano OU QUALQUER OUTRA COISA

            //CREDENCIAIS
            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
            //SecurityAlgorithms.HmacSha256 - Algorito de criptografia sha256

            //GERAÇÃO JWT COM CREDENCIAIS
            var token = new JwtSecurityToken
                (
                expires: DateTime.Now.AddMinutes(10),//EXPIRA EM 10MIN
                claims: claims,//REINVINDICAÇÕES PASSADAS ACIMA
                signingCredentials: signingCredentials
                );





        }
    }
}