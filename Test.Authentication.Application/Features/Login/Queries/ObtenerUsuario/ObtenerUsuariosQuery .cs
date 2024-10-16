using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Authentication.Domain.Entities;

namespace Test.Authentication.Application.Features.Login.Queries.ObtenerUsuario
{
    public class ObtenerUsuariosQuery : IRequest<IEnumerable<Usuario>>
    {

    }
}
