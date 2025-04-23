using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models
{
    public class Usuario : IdentityUser //USUÁRIO HERDA DE IdentityUser (QUE JÁ TEM DIVERSOS CAMPOS)
    {                                   //CRIAR NOSSA CLASSE PERMITE CRIAR AINDA MAIS CAMPOS
        //PROPRIEDADES
        public DateTime DataNascimento { get; set; }

        //CONSTRUTOR QUE USAR O CONSTRUTOR BASE (DE IdentityUser)
        public Usuario():base() {}
    }
}
