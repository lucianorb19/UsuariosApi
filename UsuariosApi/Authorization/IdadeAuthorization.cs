using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UsuariosApi.Authorization
{
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    //ESSA CLASSE É INDICADA COMO UMA CLASSE QUE MANUSEIA A LÓGICA DE FUNCIONAMENTO DA POLICY IdadeMinima
    {
        protected override Task HandleRequirementAsync(
                                    AuthorizationHandlerContext context,//ACESSO AO TOKEN
                                    IdadeMinima requirement)
        {
            //PEGA O CAMPO claim DE DATA NASCIMENTO - PRIMEIRO QUE ACHAR, DO TIPO DateOfBirth
            //ASSIM COMO FOI DEFINIDO NA ESTRUTURA DO TOKEN EM Services->TokenServices
            var dataNascimentoClaim = context.User.FindFirst(claim =>
                                                        claim.Type == ClaimTypes.DateOfBirth);

            //SE NEM EXISTIR NO TOKEN UM CAMPO DESSE TIPO - FINALIZO TAREFA
            //SEM DEFINIR QUE A POLÍTICA FOI ATENDIDA
            if(dataNascimentoClaim is null)
            {
                return Task.CompletedTask;
            }

            //SE EXISTIR

            //SALVO DATA NASCIMENTO
            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            //COMPARO SE É MAIOR DE IDADE
            var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;
            if(dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
            {
                idadeUsuario--;
            }

            //SE FOR - DEFINO QUE A POLÍTICA FOI ATENDIDA
            if(idadeUsuario >= requirement.Idade)
            {
                context.Succeed(requirement);
            }
            //FINALIZO TAREFA
            return Task.CompletedTask;

        }
    }
}
