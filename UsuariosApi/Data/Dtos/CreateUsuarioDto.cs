using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dtos
{
    public class CreateUsuarioDto
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public DateTime DataNascimento { get; set; }
        
        [Required]
        [DataType(DataType.Password)]//INDICA QUE O CAMPO Password É SENHA
        public string Password { get; set; }

        [Required]
        [Compare("Password")]//INDICA QUE O CAMPO ABAIXO SERÁ COMPARADO COM Password
        public string RePassword { get; set; }//CAMPO DE CONFIRMAÇÃO DA SENHA
    }
}
