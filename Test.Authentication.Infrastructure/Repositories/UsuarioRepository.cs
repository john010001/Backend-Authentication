using Dapper;
using Test.Authentication.Application.Features.Login.Queries.IniciarSesion;
using Test.Authentication.Application.Persistence;
using Test.Authentication.Application.Security;
using Test.Authentication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Authentication.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IUnitOfWork unitOfWork;

        public UsuarioRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

      

        public async Task<bool> GuardarUsuario(Usuario usuario)
        {
            string str = Encryptor.RandomString(4);
            string password = Encryptor.Sha1(usuario.Password);
            usuario.FechaRegistro = DateTime.Now;

            DynamicParameters parameters = new();

            parameters.Add("@CORREO", usuario.Correo);
            parameters.Add("@PASSWORD", password);
            parameters.Add("@FECHAREGISTRO", usuario.FechaRegistro);

            await unitOfWork.Connection.ExecuteAsync("sp_GuardarUsuario", param: parameters, commandType: CommandType.StoredProcedure);

            return true;
        }

      

        public async Task<IniciarSesionResponse?> ObtenerUsuario(string correo)
        {
            DynamicParameters parameters = new();
            parameters.Add("@Correo", correo);

            return await unitOfWork.Connection.QueryFirstOrDefaultAsync<IniciarSesionResponse>("sp_ObtenerUsuarioCorreo",
                param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> EliminarUsuario(string correo)
        {
            try
            {
                DynamicParameters parameters = new();
                parameters.Add("@Correo", correo);

                var filasAfectadas = await unitOfWork.Connection.ExecuteAsync("sp_EliminarUsuario",
                    param: parameters, commandType: CommandType.StoredProcedure);

                return filasAfectadas > 0;
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarioGetAll()
        {
            DynamicParameters parameters = new();

            // Cambiar a QueryAsync para obtener una colección de resultados
            return await unitOfWork.Connection.QueryAsync<Usuario>(
                "sp_ObtenerUsuariosActivos",
                param: parameters,
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
