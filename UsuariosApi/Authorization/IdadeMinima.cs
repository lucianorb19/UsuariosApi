using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization
{
    public class IdadeMinima : IAuthorizationRequirement //INDICA CLASSE USADA COMO POLICY REQUIREMENT
    {                                                    //EM PROGRAM
        //PROPRIEDADES
        public int Idade { get; set; }

        //CONSTRUTOR
        public IdadeMinima(int idade)
        {
            Idade = idade;
        }


    }
}
