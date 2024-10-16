using Test.Authentication.Application.Features.Login.Queries.IniciarSesion;
using Test.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Application.Persistence
{
    public interface IUsuarioRepository
    {
        Task<bool> GuardarUsuario(Usuario usuario);
        Task<IEnumerable<Usuario>> ObtenerUsuarioGetAll();

        Task<IniciarSesionResponse?> ObtenerUsuario(string correo);
        Task<bool> EliminarUsuario(string correo);
    }
}
