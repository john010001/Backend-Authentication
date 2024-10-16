using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Authentication.Application.Wrappers;

namespace Test.Authentication.Application.Features.Login.Commands.Eliminar
{
    public class EliminarUsuarioCommand : IRequest<ResultResponse<bool>>
    {
        public string Correo { get; set; }

        public EliminarUsuarioCommand(string correo)
        {
            Correo = correo;
        }
    }

}
