using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Authentication.Application.Persistence;
using Test.Authentication.Domain.Entities;

namespace Test.Authentication.Application.Features.Login.Queries.ObtenerUsuario
{
    public class ObtenerUsuariosQueryHandler : IRequestHandler<ObtenerUsuariosQuery, IEnumerable<Usuario>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObtenerUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
        {
            // Obtener todos los usuarios usando el repositorio
            return await _usuarioRepository.ObtenerUsuarioGetAll();
        }
    }

}
